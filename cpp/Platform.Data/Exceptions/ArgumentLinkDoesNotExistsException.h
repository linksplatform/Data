#pragma once
#include <stdexcept>
#include <string>
#include "Platform.Converters.Fallback.h"

namespace Platform::Data::Exceptions
{
    template <typename ...> class ArgumentLinkDoesNotExistsException;
    template <typename TLinkAddress> class ArgumentLinkDoesNotExistsException<TLinkAddress> : public std::invalid_argument
    {
        public: ArgumentLinkDoesNotExistsException(TLinkAddress link, std::string argumentName) : std::invalid_argument(FormatMessage(link, argumentName)) { }

        public: ArgumentLinkDoesNotExistsException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        public: ArgumentLinkDoesNotExistsException(std::string message, const std::exception& innerException) : std::invalid_argument(message) { }

        public: ArgumentLinkDoesNotExistsException(std::string message) : std::invalid_argument(message) { }

        public: ArgumentLinkDoesNotExistsException() : std::invalid_argument("") { }

        private: static std::string FormatMessage(TLinkAddress link, std::string argumentName) { return std::string("Связь [").append(Platform::Converters::To<std::string>(link)).append("] переданная в аргумент [").append(argumentName).append("] не существует."); }

        private: static std::string FormatMessage(TLinkAddress link) { return std::string("Связь [").append(Platform::Converters::To<std::string>(link)).append("] переданная в качестве аргумента не существует."); }
    };
}