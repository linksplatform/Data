using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Lock-free implementation of request queue using ConcurrentQueue.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class LockFreeRequestQueue<TLinkAddress> : IRequestQueue<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        private readonly ConcurrentQueue<StorageRequest<TLinkAddress>> _queue;
        private readonly SemaphoreSlim _semaphore;
        private volatile bool _isShutdown;

        /// <summary>
        /// <para>Gets the number of pending requests in the queue.</para>
        /// <para></para>
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// <para>Gets whether the queue is empty.</para>
        /// <para></para>
        /// </summary>
        public bool IsEmpty => _queue.IsEmpty;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="LockFreeRequestQueue{TLinkAddress}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        public LockFreeRequestQueue()
        {
            _queue = new ConcurrentQueue<StorageRequest<TLinkAddress>>();
            _semaphore = new SemaphoreSlim(0);
            _isShutdown = false;
        }

        /// <summary>
        /// <para>Enqueues a request for processing.</para>
        /// <para></para>
        /// </summary>
        /// <param name="request">
        /// <para>The request to enqueue.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if the request was successfully enqueued, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool TryEnqueue(StorageRequest<TLinkAddress> request)
        {
            if (_isShutdown)
                return false;

            _queue.Enqueue(request);
            _semaphore.Release();
            return true;
        }

        /// <summary>
        /// <para>Dequeues a request for processing.</para>
        /// <para></para>
        /// </summary>
        /// <param name="request">
        /// <para>The dequeued request, if any.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if a request was successfully dequeued, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool TryDequeue(out StorageRequest<TLinkAddress>? request)
        {
            return _queue.TryDequeue(out request);
        }

        /// <summary>
        /// <para>Waits for a request to become available and dequeues it.</para>
        /// <para></para>
        /// </summary>
        /// <param name="cancellationToken">
        /// <para>The cancellation token.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A task that completes with the dequeued request, or null if cancelled.</para>
        /// <para></para>
        /// </returns>
        public async Task<StorageRequest<TLinkAddress>?> WaitForRequestAsync(CancellationToken cancellationToken = default)
        {
            if (_isShutdown)
                return null;

            try
            {
                await _semaphore.WaitAsync(cancellationToken);
                if (_isShutdown || cancellationToken.IsCancellationRequested)
                    return null;

                if (TryDequeue(out var request))
                    return request;

                return null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        /// <summary>
        /// <para>Signals that new requests are available for processing.</para>
        /// <para></para>
        /// </summary>
        public void SignalNewRequest()
        {
            if (!_isShutdown)
                _semaphore.Release();
        }

        /// <summary>
        /// <para>Shuts down the queue and prevents new requests from being enqueued.</para>
        /// <para></para>
        /// </summary>
        public void Shutdown()
        {
            _isShutdown = true;
            _semaphore.Release(100); // Release enough to wake up all waiting threads
        }

        /// <summary>
        /// <para>Disposes the queue resources.</para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            Shutdown();
            _semaphore.Dispose();
        }
    }

    /// <summary>
    /// <para>
    /// Lock-free implementation of result queue using ConcurrentQueue.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class LockFreeResultQueue<TLinkAddress> : IResultQueue<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        private readonly ConcurrentQueue<StorageResult<TLinkAddress>> _queue;
        private readonly SemaphoreSlim _semaphore;
        private volatile bool _isShutdown;

        /// <summary>
        /// <para>Gets the number of pending results in the queue.</para>
        /// <para></para>
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// <para>Gets whether the queue is empty.</para>
        /// <para></para>
        /// </summary>
        public bool IsEmpty => _queue.IsEmpty;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="LockFreeResultQueue{TLinkAddress}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        public LockFreeResultQueue()
        {
            _queue = new ConcurrentQueue<StorageResult<TLinkAddress>>();
            _semaphore = new SemaphoreSlim(0);
            _isShutdown = false;
        }

        /// <summary>
        /// <para>Enqueues a result from processing.</para>
        /// <para></para>
        /// </summary>
        /// <param name="result">
        /// <para>The result to enqueue.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if the result was successfully enqueued, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool TryEnqueue(StorageResult<TLinkAddress> result)
        {
            if (_isShutdown)
                return false;

            _queue.Enqueue(result);
            _semaphore.Release();
            return true;
        }

        /// <summary>
        /// <para>Dequeues a result from processing.</para>
        /// <para></para>
        /// </summary>
        /// <param name="result">
        /// <para>The dequeued result, if any.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if a result was successfully dequeued, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool TryDequeue(out StorageResult<TLinkAddress>? result)
        {
            return _queue.TryDequeue(out result);
        }

        /// <summary>
        /// <para>Waits for a result to become available and dequeues it.</para>
        /// <para></para>
        /// </summary>
        /// <param name="cancellationToken">
        /// <para>The cancellation token.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A task that completes with the dequeued result, or null if cancelled.</para>
        /// <para></para>
        /// </returns>
        public async Task<StorageResult<TLinkAddress>?> WaitForResultAsync(CancellationToken cancellationToken = default)
        {
            if (_isShutdown)
                return null;

            try
            {
                await _semaphore.WaitAsync(cancellationToken);
                if (_isShutdown || cancellationToken.IsCancellationRequested)
                    return null;

                if (TryDequeue(out var result))
                    return result;

                return null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        /// <summary>
        /// <para>Signals that new results are available for processing.</para>
        /// <para></para>
        /// </summary>
        public void SignalNewResult()
        {
            if (!_isShutdown)
                _semaphore.Release();
        }

        /// <summary>
        /// <para>Shuts down the queue and prevents new results from being enqueued.</para>
        /// <para></para>
        /// </summary>
        public void Shutdown()
        {
            _isShutdown = true;
            _semaphore.Release(100); // Release enough to wake up all waiting threads
        }

        /// <summary>
        /// <para>Disposes the queue resources.</para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            Shutdown();
            _semaphore.Dispose();
        }
    }
}