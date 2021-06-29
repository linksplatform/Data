namespace Platform::Data
{
    template <typename ...> class ISynchronizedLinks;
    template <typename TLinkAddress, typename TLinks, typename TConstants> class ISynchronizedLinks<TLinkAddress, TLinks, TConstants> : public ISynchronized<TLinks>, ILinks<TLinkAddress, TConstants>
        where TLinks : ILinks<TLinkAddress, TConstants>
        where TConstants : LinksConstants<TLinkAddress>
    {
    public:
    }
}
