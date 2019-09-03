using System;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// CRUD aliases for IUniLinks.
    /// </remarks>
    public interface IUniLinksCRUD<TLinkAddress>
    {
        TLinkAddress Read(ulong partType, TLinkAddress link);
        bool Read(Func<TLinkAddress, bool> handler, params TLinkAddress[] pattern);
        TLinkAddress Create(TLinkAddress[] parts);
        TLinkAddress Update(TLinkAddress[] before, TLinkAddress[] after);
        void Delete(TLinkAddress[] parts);
    }
}