using System;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// Read/Write aliases for IUniLinks.
    /// </remarks>
    public interface IUniLinksRW<TLinkAddress>
    {
        TLinkAddress Read(ulong partType, TLinkAddress link);
        bool Read(Func<TLinkAddress, bool> handler, params TLinkAddress[] pattern);
        TLinkAddress Write(TLinkAddress[] before, TLinkAddress[] after);
    }
}