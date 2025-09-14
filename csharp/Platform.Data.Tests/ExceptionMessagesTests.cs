using System;
using System.Globalization;
using System.Threading;
using Xunit;
using Platform.Data.Exceptions;
using Platform.Data.Resources;

namespace Platform.Data.Tests
{
    /// <summary>
    /// <para>
    /// Tests for internationalization of exception messages.
    /// </para>
    /// <para>
    /// Тесты для интернационализации сообщений об ошибках.
    /// </para>
    /// </summary>
    public static class ExceptionMessagesTests
    {
        /// <summary>
        /// <para>
        /// Tests that exception messages are localized correctly for English culture.
        /// </para>
        /// <para>
        /// Тестирует, что сообщения об ошибках правильно локализуются для английской культуры.
        /// </para>
        /// </summary>
        [Fact]
        public static void ExceptionMessagesInEnglish()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentUICulture;
            try
            {
                // Set culture to English
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

                // Act & Assert - Test ArgumentLinkDoesNotExist messages
                var linkDoesNotExistMessage = ExceptionMessages.ArgumentLinkDoesNotExist(123, "testArgument");
                Assert.Equal("Link [123] passed in argument [testArgument] does not exist.", linkDoesNotExistMessage);

                var linkDoesNotExistSimpleMessage = ExceptionMessages.ArgumentLinkDoesNotExistSimple(456);
                Assert.Equal("Link [456] passed as argument does not exist.", linkDoesNotExistSimpleMessage);

                // Test ArgumentLinkHasDependencies messages
                var linkHasDependenciesMessage = ExceptionMessages.ArgumentLinkHasDependencies(789, "testParam");
                Assert.Equal("Link [789] passed in argument [testParam] has dependencies that prevent modification of its internal structure.", linkHasDependenciesMessage);

                var linkHasDependenciesSimpleMessage = ExceptionMessages.ArgumentLinkHasDependenciesSimple(101);
                Assert.Equal("Link [101] passed as argument has dependencies that prevent modification of its internal structure.", linkHasDependenciesSimpleMessage);

                // Test LinksLimitReached messages
                var linksLimitReachedMessage = ExceptionMessages.LinksLimitReached(1000);
                Assert.Equal("Links storage limit reached (1000).", linksLimitReachedMessage);

                var linksLimitReachedDefaultMessage = ExceptionMessages.LinksLimitReachedDefault;
                Assert.Equal("Links storage limit reached.", linksLimitReachedDefaultMessage);

                // Test LinkWithSameValueAlreadyExists message
                var linkWithSameValueMessage = ExceptionMessages.LinkWithSameValueAlreadyExists;
                Assert.Equal("Link with same value already exists.", linkWithSameValueMessage);
            }
            finally
            {
                // Restore original culture
                CultureInfo.CurrentUICulture = originalCulture;
            }
        }

        /// <summary>
        /// <para>
        /// Tests that exception messages are localized correctly for Russian culture.
        /// </para>
        /// <para>
        /// Тестирует, что сообщения об ошибках правильно локализуются для русской культуры.
        /// </para>
        /// </summary>
        [Fact]
        public static void ExceptionMessagesInRussian()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentUICulture;
            try
            {
                // Set culture to Russian
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

                // Act & Assert - Test ArgumentLinkDoesNotExist messages
                var linkDoesNotExistMessage = ExceptionMessages.ArgumentLinkDoesNotExist(123, "testArgument");
                Assert.Equal("Связь [123] переданная в аргумент [testArgument] не существует.", linkDoesNotExistMessage);

                var linkDoesNotExistSimpleMessage = ExceptionMessages.ArgumentLinkDoesNotExistSimple(456);
                Assert.Equal("Связь [456] переданная в качестве аргумента не существует.", linkDoesNotExistSimpleMessage);

                // Test ArgumentLinkHasDependencies messages
                var linkHasDependenciesMessage = ExceptionMessages.ArgumentLinkHasDependencies(789, "testParam");
                Assert.Equal("У связи [789] переданной в аргумент [testParam] присутствуют зависимости, которые препятствуют изменению её внутренней структуры.", linkHasDependenciesMessage);

                var linkHasDependenciesSimpleMessage = ExceptionMessages.ArgumentLinkHasDependenciesSimple(101);
                Assert.Equal("У связи [101] переданной в качестве аргумента присутствуют зависимости, которые препятствуют изменению её внутренней структуры.", linkHasDependenciesSimpleMessage);

                // Test LinksLimitReached messages
                var linksLimitReachedMessage = ExceptionMessages.LinksLimitReached(1000);
                Assert.Equal("Достигнут лимит количества связей в хранилище (1000).", linksLimitReachedMessage);

                var linksLimitReachedDefaultMessage = ExceptionMessages.LinksLimitReachedDefault;
                Assert.Equal("Достигнут лимит количества связей в хранилище.", linksLimitReachedDefaultMessage);

                // Test LinkWithSameValueAlreadyExists message
                var linkWithSameValueMessage = ExceptionMessages.LinkWithSameValueAlreadyExists;
                Assert.Equal("Связь с таким же значением уже существует.", linkWithSameValueMessage);
            }
            finally
            {
                // Restore original culture
                CultureInfo.CurrentUICulture = originalCulture;
            }
        }

        /// <summary>
        /// <para>
        /// Tests that exception classes use localized messages correctly.
        /// </para>
        /// <para>
        /// Тестирует, что классы исключений правильно используют локализованные сообщения.
        /// </para>
        /// </summary>
        [Fact]
        public static void ExceptionClassesUseLocalizedMessages()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentUICulture;
            try
            {
                // Test with English culture
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

                // Test ArgumentLinkDoesNotExistsException
                var linkDoesNotExistException = new ArgumentLinkDoesNotExistsException<int>(123, "testArg");
                Assert.Contains("Link [123] passed in argument [testArg] does not exist", linkDoesNotExistException.Message);

                var linkDoesNotExistSimpleException = new ArgumentLinkDoesNotExistsException<int>(456);
                Assert.Contains("Link [456] passed as argument does not exist", linkDoesNotExistSimpleException.Message);

                // Test ArgumentLinkHasDependenciesException
                var linkHasDependenciesException = new ArgumentLinkHasDependenciesException<int>(789, "testParam");
                Assert.Contains("Link [789] passed in argument [testParam] has dependencies", linkHasDependenciesException.Message);

                var linkHasDependenciesSimpleException = new ArgumentLinkHasDependenciesException<int>(101);
                Assert.Contains("Link [101] passed as argument has dependencies", linkHasDependenciesSimpleException.Message);

                // Test LinksLimitReachedException
                var linksLimitException = new LinksLimitReachedException<int>(1000);
                Assert.Contains("Links storage limit reached (1000)", linksLimitException.Message);

                var linksLimitDefaultException = new LinksLimitReachedException<int>();
                Assert.Contains("Links storage limit reached", linksLimitDefaultException.Message);

                // Test LinkWithSameValueAlreadyExistsException
                var linkWithSameValueException = new LinkWithSameValueAlreadyExistsException();
                Assert.Contains("Link with same value already exists", linkWithSameValueException.Message);

                // Test with Russian culture
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

                var linkDoesNotExistExceptionRu = new ArgumentLinkDoesNotExistsException<int>(123, "testArg");
                Assert.Contains("Связь [123] переданная в аргумент [testArg] не существует", linkDoesNotExistExceptionRu.Message);

                var linksLimitExceptionRu = new LinksLimitReachedException<int>(1000);
                Assert.Contains("Достигнут лимит количества связей в хранилище (1000)", linksLimitExceptionRu.Message);
            }
            finally
            {
                // Restore original culture
                CultureInfo.CurrentUICulture = originalCulture;
            }
        }

        /// <summary>
        /// <para>
        /// Tests that default message properties are localized correctly.
        /// </para>
        /// <para>
        /// Тестирует, что свойства сообщений по умолчанию правильно локализованы.
        /// </para>
        /// </summary>
        [Fact]
        public static void DefaultMessagePropertiesAreLocalized()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentUICulture;
            try
            {
                // Test with English culture
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                Assert.Equal("Links storage limit reached.", LinksLimitReachedExceptionBase.DefaultMessage);
                Assert.Equal("Link with same value already exists.", LinkWithSameValueAlreadyExistsException.DefaultMessage);

                // Test with Russian culture
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                Assert.Equal("Достигнут лимит количества связей в хранилище.", LinksLimitReachedExceptionBase.DefaultMessage);
                Assert.Equal("Связь с таким же значением уже существует.", LinkWithSameValueAlreadyExistsException.DefaultMessage);
            }
            finally
            {
                // Restore original culture
                CultureInfo.CurrentUICulture = originalCulture;
            }
        }

        /// <summary>
        /// <para>
        /// Tests that localization works with different numeric types.
        /// </para>
        /// <para>
        /// Тестирует, что локализация работает с различными числовыми типами.
        /// </para>
        /// </summary>
        [Fact]
        public static void LocalizationWorksWithDifferentNumericTypes()
        {
            // Arrange
            var originalCulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

                // Test with byte
                var byteMessage = ExceptionMessages.ArgumentLinkDoesNotExistSimple<byte>(255);
                Assert.Equal("Link [255] passed as argument does not exist.", byteMessage);

                // Test with uint
                var uintMessage = ExceptionMessages.ArgumentLinkDoesNotExistSimple<uint>(4294967295);
                Assert.Equal("Link [4294967295] passed as argument does not exist.", uintMessage);

                // Test with ulong
                var ulongMessage = ExceptionMessages.LinksLimitReached<ulong>(18446744073709551615);
                Assert.Equal("Links storage limit reached (18446744073709551615).", ulongMessage);
            }
            finally
            {
                // Restore original culture
                CultureInfo.CurrentUICulture = originalCulture;
            }
        }
    }
}