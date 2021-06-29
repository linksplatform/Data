namespace Platform::Data::Exceptions
{
    template <typename ...> class LinksLimitReachedException;
    template <typename TLinkAddress> class LinksLimitReachedException<TLinkAddress> : public LinksLimitReachedExceptionBase
    {
        public: LinksLimitReachedException(TLinkAddress limit) : this(FormatMessage(limit)) { }

        public: LinksLimitReachedException(std::string message, const std::exception& innerException) : LinksLimitReachedExceptionBase(message, innerException) { }

        public: LinksLimitReachedException(std::string message) : LinksLimitReachedExceptionBase(message) { }

        public: LinksLimitReachedException() : LinksLimitReachedExceptionBase(DefaultMessage) { }

        private: static std::string FormatMessage(TLinkAddress limit) { return std::string("Достигнут лимит количества связей в хранилище (").append(Platform::Converters::To<std::string>(limit)).append(")."); }
    };
}