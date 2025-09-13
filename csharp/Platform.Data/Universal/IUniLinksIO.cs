using System;
using System.Collections.Generic;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// In/Out aliases for IUniLinks.
    /// TLinkAddress can be any number type of any size.
    /// </remarks>
    public interface IUniLinksIO<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Outputs links that match the specified pattern using the provided handler.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="handler">
        /// <para>The handler function to process each matching link.</para>
        /// <para></para>
        /// </param>
        /// <param name="pattern">
        /// <para>The pattern to match against links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if all links were processed successfully, false if stopped by handler.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// default(TLinkAddress) means any link.
        /// Single element pattern means just element (link).
        /// Handler gets array of link contents.
        /// * link[0] is index or identifier.
        /// * link[1] is source or first.
        /// * link[2] is target or second.
        /// * link[3] is linker or third.
        /// * link[n] is nth part/parent/element/value 
        /// of link (if variable length links used).
        /// 
        /// Stops and returns false if handler return false.
        /// 
        /// Acts as Each, Foreach, Select, Search, Match &amp; ...
        /// 
        /// Handles all links in store if pattern/restrictions is not defined.
        /// </remarks>
        bool Out(Func<IList<TLinkAddress>?, bool> handler, IList<TLinkAddress>? pattern);

        /// <summary>
        /// <para>
        /// Performs an input operation to create, update, or delete links based on before/after states.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="before">
        /// <para>The state of the link before the operation.</para>
        /// <para></para>
        /// </param>
        /// <param name="after">
        /// <para>The state of the link after the operation.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the input operation.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// default(TLinkAddress) means itself.
        /// Equivalent to:
        /// * creation if before == null
        /// * deletion if after == null
        /// * update if before != null &amp;&amp; after != null
        /// * default(TLinkAddress) if before == null &amp;&amp; after == null
        /// 
        /// Possible interpretation
        /// * In(null, new[] { }) creates point (link that points to itself using minimum number of parts).
        /// * In(new[] { 4 }, null) deletes 4th link.
        /// * In(new[] { 4 }, new [] { 5 }) delete 5th link if it exists and moves 4th link to 5th index.
        /// * In(new[] { 4 }, new [] { 0, 2, 3 }) replaces 4th link with new doublet link (with 2 as source and 3 as target), 0 means it can be placed in any address.
        /// ...
        /// </remarks>
        TLinkAddress In(IList<TLinkAddress>? before, IList<TLinkAddress>? after);
    }
}