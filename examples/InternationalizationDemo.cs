using System;
using System.Globalization;
using Platform.Data.Exceptions;

/// <summary>
/// Demonstrates the internationalization functionality of exception messages.
/// This shows how exception messages are now properly localized based on the current culture.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Platform.Data Exception Internationalization Demo ===\n");

        // Test with English (default) culture
        Console.WriteLine("--- English (en-US) Culture ---");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        DemonstrateExceptions();

        Console.WriteLine("\n--- Russian (ru-RU) Culture ---");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
        DemonstrateExceptions();

        Console.WriteLine("\n--- Switching back to English ---");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        DemonstrateExceptions();
    }

    static void DemonstrateExceptions()
    {
        try
        {
            // ArgumentLinkDoesNotExistsException
            throw new ArgumentLinkDoesNotExistsException<int>(123, "testArgument");
        }
        catch (ArgumentLinkDoesNotExistsException<int> ex)
        {
            Console.WriteLine($"ArgumentLinkDoesNotExistsException: {ex.Message}");
        }

        try
        {
            // ArgumentLinkHasDependenciesException
            throw new ArgumentLinkHasDependenciesException<uint>(456, "linkParam");
        }
        catch (ArgumentLinkHasDependenciesException<uint> ex)
        {
            Console.WriteLine($"ArgumentLinkHasDependenciesException: {ex.Message}");
        }

        try
        {
            // LinksLimitReachedException
            throw new LinksLimitReachedException<ulong>(1000000);
        }
        catch (LinksLimitReachedException<ulong> ex)
        {
            Console.WriteLine($"LinksLimitReachedException: {ex.Message}");
        }

        try
        {
            // LinkWithSameValueAlreadyExistsException
            throw new LinkWithSameValueAlreadyExistsException();
        }
        catch (LinkWithSameValueAlreadyExistsException ex)
        {
            Console.WriteLine($"LinkWithSameValueAlreadyExistsException: {ex.Message}");
        }
    }
}