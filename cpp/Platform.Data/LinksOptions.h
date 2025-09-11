namespace Platform::Data
{
    /// @brief Configuration template for customizing link storage and operations.
    /// @tparam TLinkAddress The integral type used for link addressing (default: uint64_t).
    /// @tparam VConstants Configuration constants for the links system.
    /// @tparam TLink The container type used to represent links (default: std::vector<TLinkAddress>).
    /// @tparam TReadHandler Function type for handling read operations (default: std::function<TLinkAddress(TLink)>).
    /// @tparam TWriteHandler Function type for handling write operations (default: std::function<TLinkAddress(TLink, TLink)>).
    /// 
    /// This template struct provides type aliases and configuration options used throughout
    /// the links system. It allows customization of address types, link representations,
    /// and handler function signatures.
    template<std::integral TLinkAddress = std::uint64_t, LinksConstants<TLinkAddress> VConstants = LinksConstants<TLinkAddress>{true}, typename TLink = std::vector<TLinkAddress>, typename TReadHandler = std::function<TLinkAddress(TLink)>, typename TWriteHandler = std::function<TLinkAddress(TLink, TLink)>>
    struct LinksOptions
    {
        /// @brief Type alias for link address values.
        using LinkAddressType = TLinkAddress;
        
        /// @brief Type alias for link data representation.
        using LinkType = TLink;
        
        /// @brief Type alias for read operation handler functions.
        using ReadHandlerType = TReadHandler;
        
        /// @brief Type alias for write operation handler functions.
        using WriteHandlerType = TWriteHandler;
        
        /// @brief Configuration constants for the links system.
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
