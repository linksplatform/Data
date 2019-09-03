using System;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// Get/Set aliases for IUniLinks.
    /// </remarks>
    public interface IUniLinksGS<TLinkAddress>
    {
        TLinkAddress Get(ulong partType, TLinkAddress link);
        bool Get(Func<TLinkAddress, bool> handler, params TLinkAddress[] pattern);
        TLinkAddress Set(TLinkAddress[] before, TLinkAddress[] after);
    }
}