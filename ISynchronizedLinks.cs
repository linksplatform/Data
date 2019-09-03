using Platform.Threading.Synchronization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public interface ISynchronizedLinks<TLink, TLinks, TConstants> : ISynchronized<TLinks>, ILinks<TLink, TConstants>
        where TConstants : LinksConstants<TLink>
        where TLinks : ILinks<TLink, TConstants>
    {
    }
}
