#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Runtime.CompilerServices;

namespace Platform.Data
{
    public static class LinksConstantsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.IsInnerReference(address) || linksConstants.IsExternalReference(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInnerReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.PossibleInnerReferencesRange.ContainsValue(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExternalReference<TLinkAddress>(this LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) => linksConstants.PossibleExternalReferencesRange?.ContainsValue(address) ?? false;
    }
}
