using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Exceptions
{
    public class LinksLimitReachedException<TLinkAddress> : LinksLimitReachedExceptionBase
    {
        public LinksLimitReachedException(TLinkAddress limit) : this(FormatMessage(limit)) { }

        public LinksLimitReachedException(string message, Exception innerException) : base(message, innerException) { }

        public LinksLimitReachedException(string message) : base(message) { }

        public LinksLimitReachedException() : base(DefaultMessage) { }

        private static string FormatMessage(TLinkAddress limit) => $"Достигнут лимит количества связей в хранилище ({limit}).";
    }
}