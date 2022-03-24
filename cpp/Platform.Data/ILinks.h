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

        virtual LinkAddressType Count(const std::vector<typename TStorage::LinkAddressType>& restriction) const = 0;

        virtual LinkAddressType Each(const std::vector<typename TStorage::LinkAddressType>& restriction, const ReadHandlerType& handler) const = 0;

        virtual LinkAddressType Create(const std::vector<typename TStorage::LinkAddressType>& restriction, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Update(const std::vector<typename TStorage::LinkAddressType>& restriction, const std::vector<typename TStorage::LinkAddressType>& substitution, const WriteHandlerType& handler) = 0;

        virtual LinkAddressType Delete(const std::vector<typename TStorage::LinkAddressType>& restriction, const WriteHandlerType& handler) = 0;
    };
}
