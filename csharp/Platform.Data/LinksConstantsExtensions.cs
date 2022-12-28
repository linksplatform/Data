#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Platform.Data
{
    /// <summary>
    /// <para>
    /// Represents the links constants extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class LinksConstantsExtensions
    {
        /// <summary>
        /// <para>
        /// Determines whether is reference.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TLinkAddress">
        /// <para>The link address.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="linksConstants">
        /// <para>The links constants.</para>
        /// <para></para>
        /// </param>
        /// <param name="address">
        /// <para>The address.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) where TLinkAddress : IUnsignedNumber<TLinkAddress> => linksConstants.IsInternalReference(address) || linksConstants.IsExternalReference(address);

        /// <summary>
        /// <para>
        /// Determines whether is internal reference.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TLinkAddress">
        /// <para>The link address.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="linksConstants">
        /// <para>The links constants.</para>
        /// <para></para>
        /// </param>
        /// <param name="address">
        /// <para>The address.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInternalReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) where TLinkAddress : IUnsignedNumber<TLinkAddress> => linksConstants.InternalReferencesRange.Contains(address);

        /// <summary>
        /// <para>
        /// Determines whether is external reference.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TLinkAddress">
        /// <para>The link address.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="linksConstants">
        /// <para>The links constants.</para>
        /// <para></para>
        /// </param>
        /// <param name="address">
        /// <para>The address.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExternalReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) where TLinkAddress : IUnsignedNumber<TLinkAddress> => linksConstants.ExternalReferencesRange?.Contains(address) ?? false;
    }
}
