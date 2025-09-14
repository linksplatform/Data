using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Resources
{
    /// <summary>
    /// <para>
    /// Provides access to localized exception messages.
    /// </para>
    /// <para>
    /// Обеспечивает доступ к локализованным сообщениям об ошибках.
    /// </para>
    /// </summary>
    public static class ExceptionMessages
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("Platform.Data.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);

        /// <summary>
        /// <para>
        /// Gets the formatted message for an argument link that does not exist.
        /// </para>
        /// <para>
        /// Получает отформатированное сообщение для аргумента связи, которая не существует.
        /// </para>
        /// </summary>
        /// <param name="link">
        /// <para>The link identifier.</para>
        /// <para>Идентификатор связи.</para>
        /// </param>
        /// <param name="argumentName">
        /// <para>The argument name.</para>
        /// <para>Имя аргумента.</para>
        /// </param>
        /// <returns>
        /// <para>The formatted localized message.</para>
        /// <para>Отформатированное локализованное сообщение.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentLinkDoesNotExist<TLinkAddress>(TLinkAddress link, string argumentName)
        {
            return string.Format(GetString("ArgumentLinkDoesNotExist"), link, argumentName);
        }

        /// <summary>
        /// <para>
        /// Gets the formatted message for an argument link that does not exist (simple version).
        /// </para>
        /// <para>
        /// Получает отформатированное сообщение для аргумента связи, которая не существует (упрощенная версия).
        /// </para>
        /// </summary>
        /// <param name="link">
        /// <para>The link identifier.</para>
        /// <para>Идентификатор связи.</para>
        /// </param>
        /// <returns>
        /// <para>The formatted localized message.</para>
        /// <para>Отформатированное локализованное сообщение.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentLinkDoesNotExistSimple<TLinkAddress>(TLinkAddress link)
        {
            return string.Format(GetString("ArgumentLinkDoesNotExistSimple"), link);
        }

        /// <summary>
        /// <para>
        /// Gets the formatted message for an argument link that has dependencies.
        /// </para>
        /// <para>
        /// Получает отформатированное сообщение для аргумента связи, которая имеет зависимости.
        /// </para>
        /// </summary>
        /// <param name="link">
        /// <para>The link identifier.</para>
        /// <para>Идентификатор связи.</para>
        /// </param>
        /// <param name="paramName">
        /// <para>The parameter name.</para>
        /// <para>Имя параметра.</para>
        /// </param>
        /// <returns>
        /// <para>The formatted localized message.</para>
        /// <para>Отформатированное локализованное сообщение.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentLinkHasDependencies<TLinkAddress>(TLinkAddress link, string paramName)
        {
            return string.Format(GetString("ArgumentLinkHasDependencies"), link, paramName);
        }

        /// <summary>
        /// <para>
        /// Gets the formatted message for an argument link that has dependencies (simple version).
        /// </para>
        /// <para>
        /// Получает отформатированное сообщение для аргумента связи, которая имеет зависимости (упрощенная версия).
        /// </para>
        /// </summary>
        /// <param name="link">
        /// <para>The link identifier.</para>
        /// <para>Идентификатор связи.</para>
        /// </param>
        /// <returns>
        /// <para>The formatted localized message.</para>
        /// <para>Отформатированное локализованное сообщение.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentLinkHasDependenciesSimple<TLinkAddress>(TLinkAddress link)
        {
            return string.Format(GetString("ArgumentLinkHasDependenciesSimple"), link);
        }

        /// <summary>
        /// <para>
        /// Gets the formatted message for when links limit is reached.
        /// </para>
        /// <para>
        /// Получает отформатированное сообщение о достижении лимита связей.
        /// </para>
        /// </summary>
        /// <param name="limit">
        /// <para>The limit value.</para>
        /// <para>Значение лимита.</para>
        /// </param>
        /// <returns>
        /// <para>The formatted localized message.</para>
        /// <para>Отформатированное локализованное сообщение.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string LinksLimitReached<TLinkAddress>(TLinkAddress limit)
        {
            return string.Format(GetString("LinksLimitReached"), limit);
        }

        /// <summary>
        /// <para>
        /// Gets the default message for when links limit is reached.
        /// </para>
        /// <para>
        /// Получает сообщение по умолчанию о достижении лимита связей.
        /// </para>
        /// </summary>
        public static string LinksLimitReachedDefault => GetString("LinksLimitReachedDefault");

        /// <summary>
        /// <para>
        /// Gets the message for when a link with same value already exists.
        /// </para>
        /// <para>
        /// Получает сообщение о том, что связь с таким же значением уже существует.
        /// </para>
        /// </summary>
        public static string LinkWithSameValueAlreadyExists => GetString("LinkWithSameValueAlreadyExists");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetString(string name)
        {
            return _resourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;
        }
    }
}