#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Runtime.CompilerServices;

namespace Platform.Data
{
    public static class LinksConstantsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsReference<TAddress>(this LinksConstants<TAddress> linksConstants, TAddress address) => linksConstants.IsInnerReference(address) || linksConstants.IsExternalReference(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInnerReference<TAddress>(this LinksConstants<TAddress> linksConstants, TAddress address) => linksConstants.PossibleInnerReferencesRange.ContainsValue(address);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExternalReference<TAddress>(this LinksConstants<TAddress> linksConstants, TAddress address) => linksConstants.PossibleExternalReferencesRange?.ContainsValue(address) ?? false;
    }
}
