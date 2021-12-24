using System;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Exceptions
{
    /// <summary>
    /// <para>
    /// Represents the argument link does not exists exception.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="ArgumentException"/>
    public class ArgumentLinkDoesNotExistsException<TLinkAddress> : ArgumentException
    {
        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/> instance.
        /// </para>
        /// <para>
        /// Инициализирует новый экземпляр класса <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>A link.</para>
        /// <para>Связь.</para>
        /// </param>
        /// <param name="argumentName">
        /// <para>A argument name.</para>
        /// <para>Имя аргумента.</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArgumentLinkDoesNotExistsException(TLinkAddress link, string argumentName) : base(FormatMessage(link, argumentName), argumentName) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/> instance.
        /// </para>
        /// <para>
        /// Инициализирует новый экземпляр класса <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>A link.</para>
        /// <para>Связь.</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArgumentLinkDoesNotExistsException(TLinkAddress link) : base(FormatMessage(link)) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/> instance.
        /// </para>
        /// <para>
        /// Инициализирует новый экземпляр класса <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/>.
        /// </para>
        /// </summary>
        /// <param name="message">
        /// <para>A message.</para>
        /// <para>Сообщение.</para>
        /// </param>
        /// <param name="innerException">
        /// <para>A inner exception.</para>
        /// <para>Внутренняя ошибка.</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArgumentLinkDoesNotExistsException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/> instance.
        /// </para>
        /// <para>
        /// Инициализирует новый экземпляр класса <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/>.
        /// </para>
        /// </summary>
        /// <param name="message">
        /// <para>A message.</para>
        /// <para>Сообщение.</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArgumentLinkDoesNotExistsException(string message) : base(message) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/> instance.
        /// </para>
        /// <para>
        /// Инициализирует новый экземпляр класса <see cref="ArgumentLinkDoesNotExistsException{TLinkAddress}"/>.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArgumentLinkDoesNotExistsException() { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormatMessage(TLinkAddress link, string argumentName) => $"Связь [{link}] переданная в аргумент [{argumentName}] не существует.";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormatMessage(TLinkAddress link) => $"Связь [{link}] переданная в качестве аргумента не существует.";
    }
}
