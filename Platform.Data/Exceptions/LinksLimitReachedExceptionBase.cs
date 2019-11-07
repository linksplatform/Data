using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Exceptions
{
    public abstract class LinksLimitReachedExceptionBase : Exception
    {
        public static readonly string DefaultMessage = "Достигнут лимит количества связей в хранилище.";

        protected LinksLimitReachedExceptionBase(string message, Exception innerException) : base(message, innerException) { }

        protected LinksLimitReachedExceptionBase(string message) : base(message) { }
    }
}
