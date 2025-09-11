/// @file ArgumentLinkHasDependenciesException.h
/// @brief Exception for links that cannot be modified due to dependencies.

namespace Platform::Data::Exceptions
{
    /// @brief Generic template declaration for ArgumentLinkHasDependenciesException.
    template <typename ...> class ArgumentLinkHasDependenciesException;
    
    /// @brief Exception thrown when attempting to modify a link that has dependencies.
    /// @tparam TLinkAddress The type used for link addressing.
    /// 
    /// This exception indicates that a link cannot be modified or deleted because
    /// other links depend on it, which would break referential integrity.
    template <typename TLinkAddress> class ArgumentLinkHasDependenciesException<TLinkAddress> : public std::invalid_argument
    {
        /// @brief Constructs exception with link address and parameter name.
        /// @param link The link that has dependencies.
        /// @param paramName The parameter name that contained the link.
        public: ArgumentLinkHasDependenciesException(TLinkAddress link, std::string paramName) : std::invalid_argument(FormatMessage(link, paramName), paramName) { }

        /// @brief Constructs exception with link address only.
        /// @param link The link that has dependencies.
        public: ArgumentLinkHasDependenciesException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        /// @brief Constructs exception with custom message and inner exception.
        /// @param message Custom error message.
        /// @param innerException The underlying exception.
        public: ArgumentLinkHasDependenciesException(std::string message, const std::exception& innerException) : std::invalid_argument(message, innerException) { }

        /// @brief Constructs exception with custom message.
        /// @param message Custom error message.
        public: ArgumentLinkHasDependenciesException(std::string message) : std::invalid_argument(message) { }

        /// @brief Default constructor.
        public: ArgumentLinkHasDependenciesException() { }

        /// @brief Formats error message with link and parameter name (in Russian).
        /// @param link The link that has dependencies.
        /// @param paramName The parameter name.
        /// @return Formatted error message.
        private: static std::string FormatMessage(TLinkAddress link, std::string paramName) { return std::string("У связи [").append(Platform::Converters::To<std::string>(link)).append("] переданной в аргумент [").append(paramName).append("] присутствуют зависимости, которые препятствуют изменению её внутренней структуры."); }

        /// @brief Formats error message with link only (in Russian).
        /// @param link The link that has dependencies.
        /// @return Formatted error message.
        private: static std::string FormatMessage(TLinkAddress link) { return std::string("У связи [").append(Platform::Converters::To<std::string>(link)).append("] переданной в качестве аргумента присутствуют зависимости, которые препятствуют изменению её внутренней структуры."); }
    };
}