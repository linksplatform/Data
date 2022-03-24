namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TLinksOptions>
    struct ILinks
    {
    public:
        using LinksOptionsType = TLinksOptions;
        using WriteHandlerType = typename LinksOptionsType::WriteHandlerType;
        using ReadHandlerType = typename LinksOptionsType::ReadHandlerType;
        static constexpr LinksConstants<LinkAddressType> Constants = LinksOptionsType::Constants;

        virtual LinkAddressType Count(const LinkType& restriction) const = 0;

        virtual LinkAddressType Each(const LinkType& restriction, const ReadHandlerType& handler) const = 0;

        virtual LinkAddressType Create(const LinkType& restriction, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(const LinkType& restriction, const LinkType& substitution, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Delete(const LinkType& restriction, const WriteHandlerType& handler) = 0;
    };
}
