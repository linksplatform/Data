/// @file LinkWithSameValueAlreadyExistsException.h
/// @brief Exception for duplicate link creation attempts.

namespace Platform::Data::Exceptions
{
    /// @brief Exception thrown when attempting to create a link that already exists.
    /// 
    /// This exception is raised when the system prevents creation of duplicate links
    /// with the same value, typically when uniqueness constraints are enforced.
    class LinkWithSameValueAlreadyExistsException : public std::exception
    {
        /// @brief Default error message in Russian.
        public: inline static std::string DefaultMessage = "Связь с таким же значением уже существует.";

        /// @brief Constructs exception with custom message and inner exception.
        /// @param message Custom error message.
        /// @param innerException The underlying exception.
        public: LinkWithSameValueAlreadyExistsException(std::string message, const std::exception& innerException) : base(message, innerException) { }

        /// @brief Constructs exception with custom message.
        /// @param message Custom error message.
        public: LinkWithSameValueAlreadyExistsException(std::string message) : base(message) { }

        /// @brief Default constructor using default message.
        public: LinkWithSameValueAlreadyExistsException() : base(DefaultMessage) { }
    };
}
