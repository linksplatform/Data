namespace Platform::Data::Exceptions
{
    template <typename ...> class ArgumentLinkHasDependenciesException;
    template <typename TLinkAddress> class ArgumentLinkHasDependenciesException<TLinkAddress> : public std::invalid_argument
    {
        public: ArgumentLinkHasDependenciesException(TLinkAddress link, std::string paramName) : std::invalid_argument(FormatMessage(link, paramName), paramName) { }

        public: ArgumentLinkHasDependenciesException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        public: ArgumentLinkHasDependenciesException(std::string message, const std::exception& innerException) : std::invalid_argument(message, innerException) { }

        public: ArgumentLinkHasDependenciesException(std::string message) : std::invalid_argument(message) { }

        public: ArgumentLinkHasDependenciesException() { }

        private: static std::string FormatMessage(TLinkAddress link, std::string paramName) { return std::string("У связи [").append(Platform::Converters::To<std::string>(link)).append("] переданной в аргумент [").append(paramName).append("] присутствуют зависимости, которые препятствуют изменению её внутренней структуры."); }

        private: static std::string FormatMessage(TLinkAddress link) { return std::string("У связи [").append(Platform::Converters::To<std::string>(link)).append("] переданной в качестве аргумента присутствуют зависимости, которые препятствуют изменению её внутренней структуры."); }
    };
}