namespace Platform::Data
{
    using namespace Platform::Interfaces;
    
    /// @brief Core interface for managing links in a data structure.
    /// @tparam TLinksOptions Template parameter specifying the configuration options for links,
    ///         including address type, link type, and handler types.
    /// 
    /// This interface defines the fundamental CRUD (Create, Read, Update, Delete) operations
    /// for working with links. A link represents a connection or relationship between data elements.
    template<typename TLinksOptions>
    struct ILinks
    {
    public:
        /// @brief Type alias for the links options configuration.
        using LinksOptionsType = TLinksOptions;
        
        /// @brief Type alias for link addresses used to identify links.
        using LinkAddressType = typename LinksOptionsType::LinkAddressType;
        
        /// @brief Constants used throughout the links system.
        static constexpr LinksConstants<LinkAddressType> Constants = LinksOptionsType::Constants;
        
        /// @brief Type alias for link data structures.
        using LinkType = typename LinksOptionsType::LinkType;
        
        /// @brief Type alias for read operation handler functions.
        using ReadHandlerType = typename LinksOptionsType::ReadHandlerType;
        
        /// @brief Type alias for write operation handler functions.
        using WriteHandlerType = typename LinksOptionsType::WriteHandlerType;

        /// @brief Counts the number of links matching the specified restriction.
        /// @param restriction Vector of link addresses defining search criteria.
        /// @return Number of links matching the restriction.
        virtual LinkAddressType Count(const std::vector<LinkAddressType>& restriction) const = 0;

        /// @brief Iterates through links matching the restriction and applies a handler to each.
        /// @param restriction Vector of link addresses defining search criteria.
        /// @param handler Function to be called for each matching link.
        /// @return Result code from the iteration operation.
        virtual LinkAddressType Each(const std::vector<LinkAddressType>& restriction, const ReadHandlerType& handler) const = 0;

        /// @brief Creates a new link with the specified substitution values.
        /// @param substitution Vector of link addresses specifying the new link's properties.
        /// @param handler Function to handle the creation operation.
        /// @return Address of the created link or result code.
        virtual LinkAddressType Create(const std::vector<LinkAddressType>& substitution, const WriteHandlerType& handler) = 0;

        /// @brief Updates existing links matching the restriction with new substitution values.
        /// @param restriction Vector of link addresses defining which links to update.
        /// @param substitution Vector of link addresses specifying the new values.
        /// @param handler Function to handle the update operation.
        /// @return Result code from the update operation.
        virtual LinkAddressType Update(const std::vector<LinkAddressType>& restriction, const std::vector<LinkAddressType>& substitution, const WriteHandlerType& handler) = 0;

        /// @brief Deletes links matching the specified restriction.
        /// @param restriction Vector of link addresses defining which links to delete.
        /// @param handler Function to handle the deletion operation.
        /// @return Result code from the delete operation.
        virtual LinkAddressType Delete(const std::vector<LinkAddressType>& restriction, const WriteHandlerType& handler) = 0;
    };
}
