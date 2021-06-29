namespace Platform::Data::Exceptions
{
    class LinkWithSameValueAlreadyExistsException : public std::exception
    {
        public: inline static std::string DefaultMessage = "Связь с таким же значением уже существует.";

        public: LinkWithSameValueAlreadyExistsException(std::string message, const std::exception& innerException) : base(message, innerException) { }

        public: LinkWithSameValueAlreadyExistsException(std::string message) : base(message) { }

        public: LinkWithSameValueAlreadyExistsException() : base(DefaultMessage) { }
    };
}
