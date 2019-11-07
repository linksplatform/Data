using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Exceptions
{
    public class LinkWithSameValueAlreadyExistsException : Exception
    {
        public const string DefaultMessage = "Связь с таким же значением уже существует.";

        public LinkWithSameValueAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

        public LinkWithSameValueAlreadyExistsException(string message) : base(message) { }

        public LinkWithSameValueAlreadyExistsException() : base(DefaultMessage) { }
    }
}
