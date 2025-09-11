/// @file LinksLimitReachedException.h
/// @brief Typed exception for link storage limit violations.

namespace Platform::Data::Exceptions
{
    /// @brief Generic template declaration for LinksLimitReachedException.
    template <typename ...> class LinksLimitReachedException;
    
    /// @brief Typed exception thrown when link storage limits are reached.
    /// @tparam TLinkAddress The type used for link addressing.
    template <typename TLinkAddress> class LinksLimitReachedException<TLinkAddress> : public LinksLimitReachedExceptionBase
    {
        /// @brief Constructs exception with specific limit value.
        /// @param limit The limit value that was reached.
        public: LinksLimitReachedException(TLinkAddress limit) : this(FormatMessage(limit)) { }

        /// @brief Constructs exception with custom message and inner exception.
        /// @param message Custom error message.
        /// @param innerException The underlying exception.
        public: LinksLimitReachedException(std::string message, const std::exception& innerException) : LinksLimitReachedExceptionBase(message, innerException) { }

        /// @brief Constructs exception with custom message.
        /// @param message Custom error message.
        public: LinksLimitReachedException(std::string message) : LinksLimitReachedExceptionBase(message) { }

        /// @brief Default constructor using base class default message.
        public: LinksLimitReachedException() : LinksLimitReachedExceptionBase(DefaultMessage) { }

        /// @brief Formats error message with limit value (in Russian).
        /// @param limit The limit value that was reached.
        /// @return Formatted error message.
        private: static std::string FormatMessage(TLinkAddress limit) { return std::string("Достигнут лимит количества связей в хранилище (").append(Platform::Converters::To<std::string>(limit)).append(")."); }
    };
}