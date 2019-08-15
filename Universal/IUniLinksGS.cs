using System;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>
    /// Get/Set aliases for IUniLinks.
    /// </remarks>
    public interface IUniLinksGS<TLink>
    {
        TLink Get(ulong partType, TLink link);
        bool Get(Func<TLink, bool> handler, params TLink[] pattern);
        TLink Set(TLink[] before, TLink[] after);
    }
}