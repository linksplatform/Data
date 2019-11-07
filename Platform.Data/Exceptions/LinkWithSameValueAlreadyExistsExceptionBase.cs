using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Exceptions
{
    public abstract class LinkWithSameValueAlreadyExistsExceptionBase : Exception
    {
        public const string DefaultMessage = "Связь с таким же значением уже существует.";

        protected LinkWithSameValueAlreadyExistsExceptionBase(string message, Exception innerException) : base(message, innerException) { }

        protected LinkWithSameValueAlreadyExistsExceptionBase(string message) : base(message) { }

        protected LinkWithSameValueAlreadyExistsExceptionBase() { }
    }
}
