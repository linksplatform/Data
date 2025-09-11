/// @file ISynchronizedLinks.h
/// @brief Thread-safe interface for link operations.

namespace Platform::Data
{
    /// @brief Generic template declaration for ISynchronizedLinks.
    // TODO: rework Platform.Threading
    template <typename ...> class ISynchronizedLinks;
    
    /// @brief Interface combining thread synchronization with link operations.
    /// @tparam TLinkAddress The type used for link addressing.
    /// @tparam TLinks The underlying links implementation type.
    /// @tparam TConstants The constants type for link operations.
    /// 
    /// This interface provides thread-safe access to link operations by combining
    /// synchronization primitives with the core ILinks interface.
    template <typename TLinkAddress, typename TLinks, typename TConstants> class ISynchronizedLinks<TLinkAddress, TLinks, TConstants> : public ISynchronized<TLinks>, ILinks<TLinkAddress, TConstants>
        where TLinks : ILinks<TLinkAddress, TConstants>
        where TConstants : LinksConstants<TLinkAddress>
    {
    public:
        /// @brief Empty interface body - functionality provided by base classes.
    }
}
