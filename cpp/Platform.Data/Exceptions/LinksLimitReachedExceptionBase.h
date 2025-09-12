#pragma once
#include <exception>
#include <string>

namespace Platform::Data::Exceptions
{
    class LinksLimitReachedExceptionBase : public std::exception
    {
        private: std::string _message;
        
        public: inline static std::string DefaultMessage = "Достигнут лимит количества связей в хранилище.";

        protected: LinksLimitReachedExceptionBase(std::string message, const std::exception& innerException) : _message(message) { }

        protected: LinksLimitReachedExceptionBase(std::string message) : _message(message) { }
        
        public: const char* what() const noexcept override { return _message.c_str(); }
    };
}
