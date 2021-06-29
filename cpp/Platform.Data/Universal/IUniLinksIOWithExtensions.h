// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    template <typename ...> class IUniLinksIOWithExtensions;
    template <typename TLinkAddress> class IUniLinksIOWithExtensions<TLinkAddress> : public IUniLinksIO<TLinkAddress>
    {
    public:
        virtual TLinkAddress OutOne(std::int32_t partType, IList<TLinkAddress> &pattern) = 0;

        IList<IList<TLinkAddress>> OutAll(IList<TLinkAddress> &pattern);

        virtual std::uint64_t OutCount(IList<TLinkAddress> &pattern) = 0;
    };
}