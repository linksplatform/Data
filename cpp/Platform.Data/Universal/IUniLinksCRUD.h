

// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    template <typename ...> class IUniLinksCRUD;
    template <typename TLinkAddress> class IUniLinksCRUD<TLinkAddress>
    {
    public:
        virtual TLinkAddress Read(std::int32_t partType, TLinkAddress link) = 0;
        virtual TLinkAddress Read(Func<TLinkAddress, bool> handler, IList<TLinkAddress> &pattern) = 0;
        virtual TLinkAddress Create(IList<TLinkAddress> &parts) = 0;
        virtual TLinkAddress Update(IList<TLinkAddress> &before, IList<TLinkAddress> &after) = 0;
        virtual void Delete(IList<TLinkAddress> &parts) = 0;
    };
}