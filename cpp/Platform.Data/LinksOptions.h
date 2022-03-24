namespace Platform::Data
{
    template<typename TLinkAddress = std::uint64_t, typename TWriteHandler = std::function<TLinkAddress(std::vector<TLinkAddress>, std::vector<TLinkAddress>)>, typename TReadHandler = std::function<TLinkAddress(std::vector<TLinkAddress>)>, LinksConstants<TLinkAddress> VConstants = LinksConstants<TLinkAddress>{true}>
    struct LinksOptions
    {
        using LinkType = std::vector<TLinkAddress>;
        using LinkAddressType = TLinkAddress;
        using WriteHandlerType = TWriteHandler;
        using ReadHandlerType = TReadHandler;
        static constexpr LinksConstants<LinkAddressType> Constants = VConstants;
    };
}
