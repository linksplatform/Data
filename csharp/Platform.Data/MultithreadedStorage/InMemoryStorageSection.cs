using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Delegates;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// In-memory implementation of a storage section that manages a specific range of link addresses.
    /// Each section operates independently and can be managed by a dedicated thread.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class InMemoryStorageSection<TLinkAddress> : IStorageSection<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        private readonly LinksConstants<TLinkAddress> _constants;
        private readonly ConcurrentDictionary<TLinkAddress, TLinkAddress[]> _links;
        private readonly (TLinkAddress Min, TLinkAddress Max) _addressRange;
        private readonly int _threadId;
        private readonly TLinkAddress _capacity;
        private TLinkAddress _nextAvailableAddress;
        private volatile bool _disposed;

        /// <summary>
        /// <para>Gets the range of link addresses managed by this section.</para>
        /// <para></para>
        /// </summary>
        public (TLinkAddress Min, TLinkAddress Max) AddressRange => _addressRange;

        /// <summary>
        /// <para>Gets the thread ID that manages this section.</para>
        /// <para></para>
        /// </summary>
        public int ThreadId => _threadId;

        /// <summary>
        /// <para>Gets the maximum capacity of this section.</para>
        /// <para></para>
        /// </summary>
        public TLinkAddress Capacity => _capacity;

        /// <summary>
        /// <para>Gets the current number of links in this section.</para>
        /// <para></para>
        /// </summary>
        public TLinkAddress Count => TLinkAddress.CreateTruncating((ulong)_links.Count);

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="InMemoryStorageSection{TLinkAddress}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="threadId">
        /// <para>The thread ID that will manage this section.</para>
        /// <para></para>
        /// </param>
        /// <param name="addressRange">
        /// <para>The range of addresses this section manages.</para>
        /// <para></para>
        /// </param>
        /// <param name="constants">
        /// <para>The constants for link operations.</para>
        /// <para></para>
        /// </param>
        public InMemoryStorageSection(int threadId, (TLinkAddress Min, TLinkAddress Max) addressRange, LinksConstants<TLinkAddress> constants)
        {
            _threadId = threadId;
            _addressRange = addressRange;
            _constants = constants ?? throw new ArgumentNullException(nameof(constants));
            _capacity = addressRange.Max - addressRange.Min + TLinkAddress.One;
            _nextAvailableAddress = addressRange.Min;
            _links = new ConcurrentDictionary<TLinkAddress, TLinkAddress[]>();
            _disposed = false;
        }

        /// <summary>
        /// <para>Determines if a link address belongs to this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="address">
        /// <para>The link address to check.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if the address belongs to this section, false otherwise.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsAddress(TLinkAddress address)
        {
            return address.CompareTo(_addressRange.Min) >= 0 && address.CompareTo(_addressRange.Max) <= 0;
        }

        /// <summary>
        /// <para>Counts links that match the specified restriction within this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>The restriction to apply.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The number of matching links in this section.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress CountLinks(IList<TLinkAddress>? restriction)
        {
            if (_disposed)
                return TLinkAddress.Zero;

            if (restriction == null || restriction.Count == 0)
                return Count;

            var count = TLinkAddress.Zero;
            foreach (var link in _links.Values)
            {
                if (MatchesRestriction(link, restriction))
                    count++;
            }
            return count;
        }

        /// <summary>
        /// <para>Iterates through links that match the specified restriction within this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>The restriction to apply.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>The handler to call for each matching link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the iteration.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress EachLink(IList<TLinkAddress>? restriction, ReadHandler<TLinkAddress>? handler)
        {
            if (_disposed || handler == null)
                return _constants.Continue;

            foreach (var kvp in _links)
            {
                var link = kvp.Value;
                if (restriction == null || restriction.Count == 0 || MatchesRestriction(link, restriction))
                {
                    var result = handler(link);
                    if (EqualityComparer<TLinkAddress>.Default.Equals(result, _constants.Break))
                        return _constants.Break;
                }
            }
            return _constants.Continue;
        }

        /// <summary>
        /// <para>Creates a new link in this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="substitution">
        /// <para>The content of the new link.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>The handler to call for write operations.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the create operation.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress CreateLink(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            if (_disposed)
                return _constants.Null;

            if (_nextAvailableAddress.CompareTo(_addressRange.Max) > 0)
                return _constants.Null; // Section is full

            var newAddress = _nextAvailableAddress++;
            var linkContent = CreateLinkContent(newAddress, substitution);
            
            if (_links.TryAdd(newAddress, linkContent))
            {
                handler?.Invoke(Array.Empty<TLinkAddress>(), linkContent);
                return newAddress;
            }

            return _constants.Null;
        }

        /// <summary>
        /// <para>Updates a link in this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>The restriction to identify the link to update.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The new content for the link.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>The handler to call for write operations.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the update operation.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress UpdateLink(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            if (_disposed || restriction == null || restriction.Count == 0)
                return _constants.Continue;

            var linkAddress = restriction[_constants.IndexPart];
            if (!ContainsAddress(linkAddress))
                return _constants.Continue;

            if (_links.TryGetValue(linkAddress, out var oldLink))
            {
                var newLinkContent = CreateLinkContent(linkAddress, substitution);
                if (_links.TryUpdate(linkAddress, newLinkContent, oldLink))
                {
                    handler?.Invoke(oldLink, newLinkContent);
                    return _constants.Continue;
                }
            }

            return _constants.Continue;
        }

        /// <summary>
        /// <para>Deletes links in this section.</para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>The restriction to identify links to delete.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>The handler to call for write operations.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the delete operation.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress DeleteLink(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler)
        {
            if (_disposed || restriction == null || restriction.Count == 0)
                return _constants.Continue;

            var linkAddress = restriction[_constants.IndexPart];
            if (!ContainsAddress(linkAddress))
                return _constants.Continue;

            if (_links.TryRemove(linkAddress, out var deletedLink))
            {
                var emptyLink = new TLinkAddress[deletedLink.Length];
                handler?.Invoke(deletedLink, emptyLink);
                return _constants.Continue;
            }

            return _constants.Continue;
        }

        private TLinkAddress[] CreateLinkContent(TLinkAddress address, IList<TLinkAddress>? substitution)
        {
            if (substitution == null || substitution.Count == 0)
            {
                // Create a basic triplet: [index, null, null]
                return new TLinkAddress[] { address, _constants.Null, _constants.Null };
            }

            var content = new TLinkAddress[Math.Max(3, substitution.Count + 1)];
            content[_constants.IndexPart] = address;
            
            for (int i = 0; i < substitution.Count; i++)
            {
                content[i + 1] = substitution[i];
            }

            return content;
        }

        private bool MatchesRestriction(TLinkAddress[] link, IList<TLinkAddress> restriction)
        {
            for (int i = 0; i < Math.Min(link.Length, restriction.Count); i++)
            {
                var restrictionValue = restriction[i];
                if (!EqualityComparer<TLinkAddress>.Default.Equals(restrictionValue, _constants.Any) &&
                    !EqualityComparer<TLinkAddress>.Default.Equals(restrictionValue, link[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// <para>Disposes the section and releases its resources.</para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _links.Clear();
        }
    }
}