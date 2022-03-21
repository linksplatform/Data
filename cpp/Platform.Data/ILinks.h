namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename LinkTypesOptions>
    struct ILinks
    {
    public:
        using Options = LinkTypesOptions;
        using LinkType = typename Options::LinkType;
        using LinkAddressType = typename LinkType::value_type;
        using WriteHandlerType = typename Options::TWriteHandler;
        using ReadHandlerType = typename Options::TReadHandler;
        static constexpr LinksConstants<LinkAddressType> Constants = Options::Constants;

        virtual LinkAddressType Count(const LinkType& restriction) = 0;

        virtual LinkAddressType Each(const LinkType& restrictions, const ReadHandlerType& handler) = 0;

        virtual LinkAddressType Each(const LinkType&& restrictions, const ReadHandlerType& handler) = 0;

        virtual LinkAddressType Create(const LinkType& restrictions, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Create(LinkType&& restrictions, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(const LinkType& restrictions, const LinkType& substitution, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(LinkType&& restrictions, LinkType&& substitution, const WriteHandlerType& handler) = 0;

        virtual void Delete(const LinkType& restrictions, const WriteHandlerType& handler) = 0;

        virtual void Delete(const LinkType& restrictions, const WriteHandlerType&& handler) = 0;

        virtual void Delete(LinkType&& restrictions, const WriteHandlerType&& handler) = 0;
    };
}
