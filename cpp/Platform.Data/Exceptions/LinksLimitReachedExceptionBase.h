namespace Platform::Data::Exceptions
{
    class LinksLimitReachedExceptionBase : public std::exception
    {
        public: inline static std::string DefaultMessage = "Достигнут лимит количества связей в хранилище.";

        protected: LinksLimitReachedExceptionBase(std::string message, const std::exception& innerException) : base(message, innerException) { }

        protected: LinksLimitReachedExceptionBase(std::string message) : base(message) { }
    };
}
