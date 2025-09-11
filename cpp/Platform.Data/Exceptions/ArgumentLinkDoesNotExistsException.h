/// @file ArgumentLinkDoesNotExistsException.h
/// @brief Exception thrown when a referenced link does not exist.

namespace Platform::Data::Exceptions
{
    /// @brief Generic template declaration for ArgumentLinkDoesNotExistsException.
    template <typename ...> class ArgumentLinkDoesNotExistsException;
    
    /// @brief Exception thrown when attempting to use a link address that doesn't exist.
    /// @tparam TLinkAddress The type used for link addressing.
    /// 
    /// This exception is thrown when operations reference a link that is not present
    /// in the storage system, indicating an invalid link address was used.
    template <typename TLinkAddress> class ArgumentLinkDoesNotExistsException<TLinkAddress> : public std::invalid_argument
    {
        /// @brief Constructs exception with link address and argument name.
        /// @param link The non-existent link address.
        /// @param argumentName The name of the argument that contained the invalid link.
        public: ArgumentLinkDoesNotExistsException(TLinkAddress link, std::string argumentName) : std::invalid_argument(FormatMessage(link, argumentName), argumentName) { }

        /// @brief Constructs exception with link address only.
        /// @param link The non-existent link address.
        public: ArgumentLinkDoesNotExistsException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        /// @brief Constructs exception with custom message and inner exception.
        /// @param message Custom error message.
        /// @param innerException The underlying exception that caused this error.
        public: ArgumentLinkDoesNotExistsException(std::string message, const std::exception& innerException) : std::invalid_argument(message, innerException) { }

        /// @brief Constructs exception with custom message.
        /// @param message Custom error message.
        public: ArgumentLinkDoesNotExistsException(std::string message) : std::invalid_argument(message) { }

        /// @brief Default constructor.
        public: ArgumentLinkDoesNotExistsException() { }

        /// @brief Formats error message with link and argument name (in Russian).
        /// @param link The non-existent link address.
        /// @param argumentName The argument name.
        /// @return Formatted error message.
        private: static std::string FormatMessage(TLinkAddress link, std::string argumentName) { return std::string("Связь [").append(Platform::Converters::To<std::string>(link)).append("] переданная в аргумент [").append(argumentName).append("] не существует."); }

        /// @brief Formats error message with link only (in Russian).
        /// @param link The non-existent link address.
        /// @return Formatted error message.
        private: static std::string FormatMessage(TLinkAddress link) { return std::string("Связь [").append(Platform::Converters::To<std::string>(link)).append("] переданная в качестве аргумента не существует."); }
    };
}