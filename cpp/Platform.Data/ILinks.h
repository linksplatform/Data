namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TLinksOptions>
    struct ILinks
    {
    public:
        using LinksOptionsType = TLinksOptions;
        using LinkAddressType = typename LinksOptionsType::LinkAddressType;
        using WriteHandlerType = typename LinksOptionsType::WriteHandlerType;
        using ReadHandlerType = typename LinksOptionsType::ReadHandlerType;
        static constexpr LinksConstants<LinkAddressType> Constants = LinksOptionsType::Constants;

        virtual LinkAddressType Count(const std::vector<LinkAddressType>& restriction) const = 0;

        virtual LinkAddressType Each(const std::vector<LinkAddressType>& restriction, const ReadHandlerType& handler) const = 0;

        virtual LinkAddressType Create(const std::vector<LinkAddressType>& restriction, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(const std::vector<LinkAddressType>& restriction, const std::vector<LinkAddressType>& substitution, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Delete(const std::vector<LinkAddressType>& restriction, const WriteHandlerType& handler) = 0;
    };
}
