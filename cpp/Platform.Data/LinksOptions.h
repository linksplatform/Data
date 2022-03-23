namespace Platform::Data
{
    template<typename TLink, typename TWriteHandler = std::function<typename TLink::value_type(TLink, TLink)>, typename TReadHandler = std::function<typename TLink::value_type(TLink)>, LinksConstants<typename TLink::value_type> VConstants = LinksConstants<typename TLink::value_type>{true}>
    struct LinksOptions
    {
        using LinkType = TLink;
        using LinkAddressType = typename TLink::value_type;
        using WriteHandlerType = TWriteHandler;
        using ReadHandlerType = TReadHandler;
        static constexpr LinksConstants<LinkAddressType> Constants = VConstants;
    };
}
