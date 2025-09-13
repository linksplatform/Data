// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;

namespace Platform.Data.Universal
{
    /// <remarks>Contains some optimizations of Out.</remarks>
    public interface IUniLinksIOWithExtensions<TLinkAddress> : IUniLinksIO<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Outputs a specific part of a link matching the pattern.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="partType">
        /// <para>The type of part to retrieve (0=index, 1=source, 2=target, etc.).</para>
        /// <para></para>
        /// </param>
        /// <param name="pattern">
        /// <para>The pattern to match against links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The specified part of the matching link, or default value if not found.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// default(TLinkAddress) means nothing or null.
        /// Single element pattern means just element (link).
        /// OutPart(n, null) returns default(TLinkAddress).
        /// OutPart(0, pattern) ~ Exists(link) or Search(pattern)
        /// OutPart(1, pattern) ~ GetSource(link) or GetSource(Search(pattern))
        /// OutPart(2, pattern) ~ GetTarget(link) or GetTarget(Search(pattern))
        /// OutPart(3, pattern) ~ GeTLinkAddresser(link) or GeTLinkAddresser(Search(pattern))
        /// OutPart(n, pattern) => For any variable length links, returns link or default(TLinkAddress).
        /// 
        /// Outs(returns) inner contents of link, its part/parent/element/value.
        /// </remarks>
        TLinkAddress OutOne(int partType, IList<TLinkAddress>? pattern);

        /// <summary>
        /// <para>
        /// Outputs all links matching the specified pattern as an array.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="pattern">
        /// <para>The pattern to match against links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>An array of all links matching the pattern.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>OutCount() returns total links in store as array.</remarks>
        IList<IList<TLinkAddress>?> OutAll(IList<TLinkAddress>? pattern);

        /// <summary>
        /// <para>
        /// Counts the total number of links matching the specified pattern.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="pattern">
        /// <para>The pattern to match against links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The total count of links matching the pattern.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>OutCount() returns total amount of links in store.</remarks>
        ulong OutCount(IList<TLinkAddress>? pattern);
    }
}