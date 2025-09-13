using System;
using System.Collections.Generic;
using System.Numerics;
using Platform.Delegates;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Represents a single section of links storage that can be managed by a dedicated thread.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public interface IStorageSection<TLinkAddress> : IDisposable
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets the range of link addresses managed by this section.</para>
        /// <para></para>
        /// </summary>
        (TLinkAddress Min, TLinkAddress Max) AddressRange { get; }

        /// <summary>
        /// <para>Gets the thread ID that manages this section.</para>
        /// <para></para>
        /// </summary>
        int ThreadId { get; }

        /// <summary>
        /// <para>Gets the maximum capacity of this section.</para>
        /// <para></para>
        /// </summary>
        TLinkAddress Capacity { get; }

        /// <summary>
        /// <para>Gets the current number of links in this section.</para>
        /// <para></para>
        /// </summary>
        TLinkAddress Count { get; }

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
        bool ContainsAddress(TLinkAddress address);

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
        TLinkAddress CountLinks(IList<TLinkAddress>? restriction);

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
        TLinkAddress EachLink(IList<TLinkAddress>? restriction, ReadHandler<TLinkAddress>? handler);

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
        TLinkAddress CreateLink(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

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
        TLinkAddress UpdateLink(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

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
        TLinkAddress DeleteLink(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler);
    }
}