﻿using Platform.Threading.Synchronization;
using Platform.Data.Constants;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public interface ISynchronizedLinks<TLink, TLinks, TConstants> : ISynchronized<TLinks>, ILinks<TLink, TConstants>
        where TConstants : ILinksCombinedConstants<TLink, TLink, int, TConstants>
        where TLinks : ILinks<TLink, TConstants>
    {
    }
}
