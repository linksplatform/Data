/// @file LinksLimitReachedExceptionBase.h
/// @brief Base class for exceptions thrown when link storage limits are exceeded.

namespace Platform::Data::Exceptions
{
    /// @brief Base exception class for link storage limit violations.
    /// 
    /// This exception is thrown when operations would exceed the maximum
    /// number of links that can be stored in the system.
    class LinksLimitReachedExceptionBase : public std::exception
    {
        /// @brief Default error message in Russian.
        public: inline static std::string DefaultMessage = "Достигнут лимит количества связей в хранилище.";

        /// @brief Protected constructor with custom message and inner exception.
        /// @param message Custom error message.
        /// @param innerException The underlying exception.
        protected: LinksLimitReachedExceptionBase(std::string message, const std::exception& innerException) : base(message, innerException) { }

        /// @brief Protected constructor with custom message.
        /// @param message Custom error message.
        protected: LinksLimitReachedExceptionBase(std::string message) : base(message) { }
    };
}
