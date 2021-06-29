namespace Platform::Data
{
    template <typename ...> class ILinks;
    template <typename TLinkAddress, typename TConstants> class ILinks<TLinkAddress, TConstants>
        where TConstants : LinksConstants<TLinkAddress>
    {
    public:
        const TConstants Constants;

        virtual TLinkAddress Count(IList<TLinkAddress> &restriction) = 0;

        virtual TLinkAddress Each(Func<IList<TLinkAddress>, TLinkAddress> handler, IList<TLinkAddress> &restrictions) = 0;

        virtual TLinkAddress Create(IList<TLinkAddress> &restrictions) = 0;

        virtual TLinkAddress Update(IList<TLinkAddress> &restrictions, IList<TLinkAddress> &substitution) = 0;

        virtual void Delete(IList<TLinkAddress> &restrictions) = 0;
    }
}
