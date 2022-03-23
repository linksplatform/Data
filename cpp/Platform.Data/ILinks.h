namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TLinksOptions>
    struct ILinks
    {
    public:
        using LinksOptionsType = TLinksOptions;
        using LinkType = typename LinksOptionsType::LinkType;
        using LinkAddressType = typename LinkType::value_type;
        using WriteHandlerType = typename LinksOptionsType::TWriteHandler;
        using ReadHandlerType = typename LinksOptionsType::TReadHandler;
        static constexpr LinksConstants<LinkAddressType> Constants = LinksOptionsType::Constants;

        virtual LinkAddressType Count(const LinkType& restriction) = 0;

        virtual LinkAddressType Each(const LinkType& restrictions, const ReadHandlerType& handler) = 0;

        virtual LinkAddressType Create(const LinkType& restrictions, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(const LinkType& restrictions, const LinkType& substitution, const WriteHandlerType& handler) = 0;

        virtual void Delete(const LinkType& restrictions, const WriteHandlerType& handler) = 0;
    };
}
