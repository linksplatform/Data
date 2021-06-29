

// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    template <typename ...> class IUniLinksRW;
    template <typename TLinkAddress> class IUniLinksRW<TLinkAddress>
    {
    public:
        virtual TLinkAddress Read(std::int32_t partType, TLinkAddress link) = 0;
        virtual bool Read(Func<TLinkAddress, bool> handler, IList<TLinkAddress> &pattern) = 0;
        virtual TLinkAddress Write(IList<TLinkAddress> &before, IList<TLinkAddress> &after) = 0;
    };
}