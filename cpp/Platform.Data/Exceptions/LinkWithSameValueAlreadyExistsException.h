#pragma once
#include <exception>
#include <string>

namespace Platform::Data::Exceptions
{
    class LinkWithSameValueAlreadyExistsException : public std::exception
    {
        private: std::string _message;
        
        public: inline static std::string DefaultMessage = "Связь с таким же значением уже существует.";

        public: LinkWithSameValueAlreadyExistsException(std::string message, const std::exception& innerException) : _message(message) { }

        public: LinkWithSameValueAlreadyExistsException(std::string message) : _message(message) { }

        public: LinkWithSameValueAlreadyExistsException() : _message(DefaultMessage) { }
        
        public: const char* what() const noexcept override { return _message.c_str(); }
    };
}
