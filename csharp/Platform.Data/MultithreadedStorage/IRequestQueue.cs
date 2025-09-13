using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Represents a lock-free queue for handling storage requests between threads.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public interface IRequestQueue<TLinkAddress> : IDisposable
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets the number of pending requests in the queue.</para>
        /// <para></para>
        /// </summary>
        int Count { get; }

        /// <summary>
        /// <para>Gets whether the queue is empty.</para>
        /// <para></para>
        /// </summary>
        bool IsEmpty { get; }

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
        bool TryEnqueue(StorageRequest<TLinkAddress> request);

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
        bool TryDequeue(out StorageRequest<TLinkAddress>? request);

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
        Task<StorageRequest<TLinkAddress>?> WaitForRequestAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Signals that new requests are available for processing.</para>
        /// <para></para>
        /// </summary>
        void SignalNewRequest();

        /// <summary>
        /// <para>Shuts down the queue and prevents new requests from being enqueued.</para>
        /// <para></para>
        /// </summary>
        void Shutdown();
    }

    /// <summary>
    /// <para>
    /// Represents a lock-free queue for handling storage results between threads.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public interface IResultQueue<TLinkAddress> : IDisposable
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets the number of pending results in the queue.</para>
        /// <para></para>
        /// </summary>
        int Count { get; }

        /// <summary>
        /// <para>Gets whether the queue is empty.</para>
        /// <para></para>
        /// </summary>
        bool IsEmpty { get; }

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
        bool TryEnqueue(StorageResult<TLinkAddress> result);

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
        bool TryDequeue(out StorageResult<TLinkAddress>? result);

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
        Task<StorageResult<TLinkAddress>?> WaitForResultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Signals that new results are available for processing.</para>
        /// <para></para>
        /// </summary>
        void SignalNewResult();

        /// <summary>
        /// <para>Shuts down the queue and prevents new results from being enqueued.</para>
        /// <para></para>
        /// </summary>
        void Shutdown();
    }
}