using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Platform.Delegates;
using Platform.Ranges;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// MapReduce multithread combined storage implementation that distributes links across multiple sections,
    /// each managed by a dedicated thread. Implements lock-free queues for request/response streaming.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    /// <typeparam name="TConstants">
    /// <para>The type of constants.</para>
    /// <para></para>
    /// </typeparam>
    public class MapReduceCombinedLinksStorage<TLinkAddress, TConstants> : ILinks<TLinkAddress, TConstants>, IDisposable
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
        where TConstants : LinksConstants<TLinkAddress>
    {
        private readonly TConstants _constants;
        private readonly List<IStorageSection<TLinkAddress>> _sections;
        private readonly List<IRequestQueue<TLinkAddress>> _requestQueues;
        private readonly IResultQueue<TLinkAddress> _resultQueue;
        private readonly List<Task> _sectionTasks;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly int _maxSectionCapacity;
        private readonly int _numberOfSections;
        private readonly object _sectionLock;
        private volatile bool _disposed;

        /// <summary>
        /// <para>Gets the constants for this storage instance.</para>
        /// <para></para>
        /// </summary>
        public TConstants Constants
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _constants;
        }

        /// <summary>
        /// <para>Gets the number of active sections.</para>
        /// <para></para>
        /// </summary>
        public int SectionCount => _sections.Count;

        /// <summary>
        /// <para>Gets the maximum capacity per section.</para>
        /// <para></para>
        /// </summary>
        public int MaxSectionCapacity => _maxSectionCapacity;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="MapReduceCombinedLinksStorage{TLinkAddress, TConstants}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="constants">
        /// <para>The constants for this storage instance.</para>
        /// <para></para>
        /// </param>
        /// <param name="configuration">
        /// <para>The configuration for this storage instance.</para>
        /// <para></para>
        /// </param>
        public MapReduceCombinedLinksStorage(TConstants constants, StorageConfiguration<TLinkAddress>? configuration = null)
        {
            _constants = constants ?? throw new ArgumentNullException(nameof(constants));
            var config = configuration ?? StorageConfiguration<TLinkAddress>.CreateDefault();
            config.Validate();
            _maxSectionCapacity = config.MaxSectionCapacity;
            _numberOfSections = config.EffectiveNumberOfSections;
            _sections = new List<IStorageSection<TLinkAddress>>();
            _requestQueues = new List<IRequestQueue<TLinkAddress>>();
            _resultQueue = new LockFreeResultQueue<TLinkAddress>();
            _sectionTasks = new List<Task>();
            _cancellationTokenSource = new CancellationTokenSource();
            _sectionLock = new object();
            _disposed = false;

            InitializeSections();
        }

        private void InitializeSections()
        {
            for (int i = 0; i < _numberOfSections; i++)
            {
                CreateNewSection();
            }
        }

        private void CreateNewSection()
        {
            var sectionId = _sections.Count;
            var addressRangeStart = TLinkAddress.CreateTruncating((ulong)sectionId * (ulong)_maxSectionCapacity + 1);
            var addressRangeEnd = TLinkAddress.CreateTruncating((ulong)(sectionId + 1) * (ulong)_maxSectionCapacity);

            // Create a basic in-memory section implementation
            var section = new InMemoryStorageSection<TLinkAddress>(sectionId, (addressRangeStart, addressRangeEnd), _constants);
            var requestQueue = new LockFreeRequestQueue<TLinkAddress>();

            _sections.Add(section);
            _requestQueues.Add(requestQueue);

            // Start section processing task
            var task = Task.Run(() => ProcessSectionRequests(section, requestQueue, _cancellationTokenSource.Token));
            _sectionTasks.Add(task);
        }

        private async Task ProcessSectionRequests(IStorageSection<TLinkAddress> section, IRequestQueue<TLinkAddress> requestQueue, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var request = await requestQueue.WaitForRequestAsync(cancellationToken);
                    if (request == null)
                        continue;

                    var result = ProcessRequest(section, request);
                    _resultQueue.TryEnqueue(result);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // Log error and continue processing
                    Console.WriteLine($"Error processing request in section {section.ThreadId}: {ex.Message}");
                }
            }
        }

        private StorageResult<TLinkAddress> ProcessRequest(IStorageSection<TLinkAddress> section, StorageRequest<TLinkAddress> request)
        {
            try
            {
                TLinkAddress result = request.OperationType switch
                {
                    StorageOperationType.Count => section.CountLinks(request.Restriction),
                    StorageOperationType.Each => section.EachLink(request.Restriction, request.ReadHandler),
                    StorageOperationType.Create => section.CreateLink(request.Substitution, request.WriteHandler),
                    StorageOperationType.Update => section.UpdateLink(request.Restriction, request.Substitution, request.WriteHandler),
                    StorageOperationType.Delete => section.DeleteLink(request.Restriction, request.WriteHandler),
                    _ => throw new ArgumentException($"Unknown operation type: {request.OperationType}")
                };

                return new StorageResult<TLinkAddress>(request.Id, section.ThreadId, result);
            }
            catch (Exception ex)
            {
                return new StorageResult<TLinkAddress>(request.Id, section.ThreadId, default, ex);
            }
        }

        /// <summary>
        /// <para>Maps a request to all relevant sections and reduces the results.</para>
        /// <para></para>
        /// </summary>
        /// <param name="request">
        /// <para>The request to process.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The reduced result from all sections.</para>
        /// <para></para>
        /// </returns>
        private async Task<TLinkAddress> MapReduceRequest(StorageRequest<TLinkAddress> request)
        {
            var relevantSections = GetRelevantSections(request);
            var tasks = new List<Task<TLinkAddress>>();

            // Map: send request to all relevant sections
            foreach (var (section, queue) in relevantSections)
            {
                var sectionRequest = new StorageRequest<TLinkAddress>(
                    request.OperationType,
                    request.Restriction,
                    request.Substitution,
                    request.ReadHandler,
                    request.WriteHandler);

                queue.TryEnqueue(sectionRequest);
                tasks.Add(sectionRequest.CompletionSource.Task);
            }

            // Wait for results from result queue
            var results = new List<StorageResult<TLinkAddress>>();
            var expectedResults = tasks.Count;
            var resultTasks = tasks.Select(async task =>
            {
                while (true)
                {
                    var result = await _resultQueue.WaitForResultAsync(_cancellationTokenSource.Token);
                    if (result != null)
                    {
                        results.Add(result);
                        
                        // Find and complete the corresponding task
                        var matchingTask = tasks.FirstOrDefault(t => !t.IsCompleted);
                        if (matchingTask != null && result.IsSuccess)
                        {
                            try
                            {
                                ((TaskCompletionSource<TLinkAddress>)matchingTask.AsyncState!)?.SetResult(result.Value);
                            }
                            catch { }
                        }
                        
                        if (results.Count >= expectedResults)
                            break;
                    }
                }
            });

            await Task.WhenAll(resultTasks);

            // Reduce: combine results based on operation type
            return ReduceResults(request.OperationType, results);
        }

        private List<(IStorageSection<TLinkAddress> section, IRequestQueue<TLinkAddress> queue)> GetRelevantSections(StorageRequest<TLinkAddress> request)
        {
            var relevantSections = new List<(IStorageSection<TLinkAddress>, IRequestQueue<TLinkAddress>)>();

            // For now, send to all sections. In a more sophisticated implementation,
            // we would analyze the restriction to determine which sections are relevant
            for (int i = 0; i < _sections.Count; i++)
            {
                relevantSections.Add((_sections[i], _requestQueues[i]));
            }

            return relevantSections;
        }

        private TLinkAddress ReduceResults(StorageOperationType operationType, List<StorageResult<TLinkAddress>> results)
        {
            var successfulResults = results.Where(r => r.IsSuccess).ToList();
            
            return operationType switch
            {
                StorageOperationType.Count => successfulResults.Aggregate(TLinkAddress.Zero, (sum, result) => sum + result.Value),
                StorageOperationType.Each => successfulResults.LastOrDefault()?.Value ?? _constants.Continue,
                StorageOperationType.Create => successfulResults.FirstOrDefault()?.Value ?? _constants.Null,
                StorageOperationType.Update => successfulResults.FirstOrDefault()?.Value ?? _constants.Continue,
                StorageOperationType.Delete => successfulResults.FirstOrDefault()?.Value ?? _constants.Continue,
                _ => throw new ArgumentException($"Unknown operation type: {operationType}")
            };
        }

        #region ILinks Implementation

        /// <summary>
        /// <para>Counts and returns the total number of links in the storage that meet the specified restriction.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The total number of links in the storage that meet the specified restriction.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Count(IList<TLinkAddress>? restriction)
        {
            var request = new StorageRequest<TLinkAddress>(StorageOperationType.Count, restriction);
            return MapReduceRequest(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>A handler for each matching link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>Constants.Continue, if the pass through the links was not interrupted, and Constants.Break otherwise.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Each(IList<TLinkAddress>? restriction, ReadHandler<TLinkAddress>? handler)
        {
            var request = new StorageRequest<TLinkAddress>(StorageOperationType.Each, restriction, readHandler: handler);
            return MapReduceRequest(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// <para>Creates a link.</para>
        /// <para></para>
        /// </summary>
        /// <param name="substitution">
        /// <para>The content of a new link.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>Constants.Continue if all executed changes are handled.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Create(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            var request = new StorageRequest<TLinkAddress>(StorageOperationType.Create, substitution: substitution, writeHandler: handler);
            return MapReduceRequest(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// <para>Updates a link.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of links.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The new content for the link.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>Constants.Continue if all executed changes are handled.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Update(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            var request = new StorageRequest<TLinkAddress>(StorageOperationType.Update, restriction, substitution, writeHandler: handler);
            return MapReduceRequest(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of a link.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>Constants.Continue if all executed changes are handled.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Delete(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler)
        {
            var request = new StorageRequest<TLinkAddress>(StorageOperationType.Delete, restriction, writeHandler: handler);
            return MapReduceRequest(request).GetAwaiter().GetResult();
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// <para>Disposes the storage and all its resources.</para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _cancellationTokenSource.Cancel();

            // Shutdown all queues
            foreach (var queue in _requestQueues)
            {
                queue.Shutdown();
            }
            _resultQueue.Shutdown();

            // Wait for all tasks to complete
            Task.WaitAll(_sectionTasks.ToArray(), TimeSpan.FromSeconds(5));

            // Dispose all resources
            foreach (var section in _sections)
            {
                section.Dispose();
            }
            foreach (var queue in _requestQueues)
            {
                queue.Dispose();
            }
            _resultQueue.Dispose();
            _cancellationTokenSource.Dispose();
        }

        #endregion
    }
}