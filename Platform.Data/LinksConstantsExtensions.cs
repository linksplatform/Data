#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Runtime.CompilerServices;

namespace Platform.Data
{
    public static class LinksConstantsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.IsInternalReference(address) || linksConstants.IsExternalReference(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInternalReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.InternalReferencesRange.Contains(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExternalReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.ExternalReferencesRange?.Contains(address) ?? false;
    }
}
