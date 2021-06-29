

// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    template <typename ...> class IUniLinksGS;
    template <typename TLinkAddress> class IUniLinksGS<TLinkAddress>
    {
    public:
        virtual TLinkAddress Get(std::int32_t partType, TLinkAddress link) = 0;
        virtual TLinkAddress Get(Func<TLinkAddress, bool> handler, IList<TLinkAddress> &pattern) = 0;
        virtual TLinkAddress Set(IList<TLinkAddress> &before, IList<TLinkAddress> &after) = 0;
    };
}