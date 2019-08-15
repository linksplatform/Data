using System;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// CRUD aliases for IUniLinks.
    /// </remarks>
    public interface IUniLinksCRUD<TLink>
    {
        TLink Read(ulong partType, TLink link);
        bool Read(Func<TLink, bool> handler, params TLink[] pattern);
        TLink Create(TLink[] parts);
        TLink Update(TLink[] before, TLink[] after);
        void Delete(TLink[] parts);
    }
}