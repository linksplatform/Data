namespace Platform::Data
{

    template<std::integral TLinkAddress = std::uint64_t, LinksConstants<TLinkAddress> VConstants = LinksConstants<TLinkAddress>{true}, typename TLink = std::vector<TLinkAddress>, typename TReadHandler = std::function<TLinkAddress(TLink)>, typename TWriteHandler = std::function<TLinkAddress(TLink, TLink)>>
    struct LinksOptions
    {
        using LinkAddressType = TLinkAddress;
        using LinkType = TLink;
        using ReadHandlerType = TReadHandler;
        using WriteHandlerType = TWriteHandler;
        static constexpr LinksConstants<LinkAddressType> Constants = VConstants;
    };

//    template<typename ...>
//    struct LinksOptions;
//
//    template<typename TLinkAddress, LinksConstants<TLinkAddress> VConstants, typename TLink, typename TWriteHandler, typename TReadHandler>
//    struct LinksOptions<TLinkAddress, VConstants, TLink, TWriteHandler, TReadHandler>
//    {
//        using LinkAddressType = TLinkAddress;
//        using LinkType = std::vector<LinkAddressType>;
//        using WriteHandlerType = TWriteHandler;
//        using ReadHandlerType = TReadHandler;
//        using ConstantsType = VConstants;
//    };
//
//
//    template<typename TLinkAddress, LinksConstants<TLinkAddress> VConstants, typename TLink, typename TWriteHandler>
//    struct LinksOptions<TLinkAddress, VConstants, TLink, TWriteHandler> : LinksOptions<TLinkAddress, VConstants, TLink, TWriteHandler, std::function<TLinkAddress(TLink)>>
//    {
//
//    };
//
//
//    template<typename TLinkAddress, LinksConstants<TLinkAddress> VConstants, typename TLink>
//    struct LinksOptions<TLinkAddress, VConstants, TLink> : LinksOptions<TLinkAddress, VConstants, TLink, std::function<TLinkAddress(TLink, TLink)>>
//    {
//
//    };
//
//    template<typename TLinkAddress, LinksConstants<TLinkAddress> VConstants>
//    struct LinksOptions<TLinkAddress, VConstants> : LinksOptions<TLinkAddress, VConstants, std::vector<TLinkAddress>>
//    {
//
//    };
//
//    template<typename TLinkAddress>
//    struct LinksOptions<TLinkAddress> : LinksOptions<TLinkAddress, LinksConstants<TLinkAddress>{true}>
//    {
//
//    };
//
//    struct LinksOptions<> : LinksOptions<std::uint64_t>
//    {
//
//    };


}
