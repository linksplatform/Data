#include <gtest/gtest.h>
#include <typeinfo>
#include <stdexcept>
#include <exception>

// Include all exception headers
#include "../Platform.Data/Exceptions/ArgumentLinkDoesNotExistsException.h"
#include "../Platform.Data/Exceptions/ArgumentLinkHasDependenciesException.h"
#include "../Platform.Data/Exceptions/LinksLimitReachedExceptionBase.h"
#include "../Platform.Data/Exceptions/LinksLimitReachedException.h"
#include "../Platform.Data/Exceptions/LinkWithSameValueAlreadyExistsException.h"

using namespace Platform::Data::Exceptions;

// Test ArgumentLinkDoesNotExistsException
TEST(ExceptionsTests, ArgumentLinkDoesNotExistsExceptionInheritance)
{
    // Test that it properly inherits from std::invalid_argument
    ArgumentLinkDoesNotExistsException<int> ex;
    EXPECT_TRUE(dynamic_cast<std::invalid_argument*>(&ex) != nullptr);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&ex) != nullptr);
}

TEST(ExceptionsTests, ArgumentLinkDoesNotExistsExceptionConstructors)
{
    // Test default constructor
    ArgumentLinkDoesNotExistsException<int> ex1;
    EXPECT_NE(ex1.what(), nullptr);
    
    // Test string constructor
    ArgumentLinkDoesNotExistsException<int> ex2("Test message");
    EXPECT_STREQ(ex2.what(), "Test message");
    
    // Test link constructor
    ArgumentLinkDoesNotExistsException<int> ex3(42);
    std::string whatStr(ex3.what());
    EXPECT_TRUE(whatStr.find("42") != std::string::npos);
    
    // Test link and argument name constructor
    ArgumentLinkDoesNotExistsException<int> ex4(42, "testArg");
    std::string whatStr2(ex4.what());
    EXPECT_TRUE(whatStr2.find("42") != std::string::npos);
    EXPECT_TRUE(whatStr2.find("testArg") != std::string::npos);
    
    // Test inner exception constructor (note: inner exception info is not stored in std::invalid_argument)
    std::runtime_error inner("inner");
    ArgumentLinkDoesNotExistsException<int> ex5("outer", inner);
    EXPECT_STREQ(ex5.what(), "outer");
}

TEST(ExceptionsTests, ArgumentLinkDoesNotExistsExceptionThrowCatch)
{
    // Test that it can be thrown and caught as std::invalid_argument
    try {
        throw ArgumentLinkDoesNotExistsException<int>(123, "testParam");
    } catch (const std::invalid_argument& e) {
        std::string whatStr(e.what());
        EXPECT_TRUE(whatStr.find("123") != std::string::npos);
        EXPECT_TRUE(whatStr.find("testParam") != std::string::npos);
    }
    
    // Test that it can be caught as std::exception
    try {
        throw ArgumentLinkDoesNotExistsException<int>("test message");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test message");
    }
}

// Test ArgumentLinkHasDependenciesException
TEST(ExceptionsTests, ArgumentLinkHasDependenciesExceptionInheritance)
{
    ArgumentLinkHasDependenciesException<int> ex;
    EXPECT_TRUE(dynamic_cast<std::invalid_argument*>(&ex) != nullptr);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&ex) != nullptr);
}

TEST(ExceptionsTests, ArgumentLinkHasDependenciesExceptionConstructors)
{
    // Test default constructor
    ArgumentLinkHasDependenciesException<int> ex1;
    EXPECT_NE(ex1.what(), nullptr);
    
    // Test string constructor
    ArgumentLinkHasDependenciesException<int> ex2("Test message");
    EXPECT_STREQ(ex2.what(), "Test message");
    
    // Test link constructor
    ArgumentLinkHasDependenciesException<int> ex3(42);
    std::string whatStr(ex3.what());
    EXPECT_TRUE(whatStr.find("42") != std::string::npos);
    
    // Test link and parameter name constructor
    ArgumentLinkHasDependenciesException<int> ex4(42, "testParam");
    std::string whatStr2(ex4.what());
    EXPECT_TRUE(whatStr2.find("42") != std::string::npos);
    EXPECT_TRUE(whatStr2.find("testParam") != std::string::npos);
}

TEST(ExceptionsTests, ArgumentLinkHasDependenciesExceptionThrowCatch)
{
    try {
        throw ArgumentLinkHasDependenciesException<int>(123, "testParam");
    } catch (const std::invalid_argument& e) {
        std::string whatStr(e.what());
        EXPECT_TRUE(whatStr.find("123") != std::string::npos);
        EXPECT_TRUE(whatStr.find("testParam") != std::string::npos);
    }
}

// Test LinksLimitReachedExceptionBase
TEST(ExceptionsTests, LinksLimitReachedExceptionBaseInheritance)
{
    class TestLinksLimitReachedExceptionBase : public LinksLimitReachedExceptionBase {
    public:
        TestLinksLimitReachedExceptionBase(std::string message) : LinksLimitReachedExceptionBase(message) {}
    };
    
    TestLinksLimitReachedExceptionBase ex("test");
    EXPECT_TRUE(dynamic_cast<std::exception*>(&ex) != nullptr);
}

TEST(ExceptionsTests, LinksLimitReachedExceptionBaseMessage)
{
    class TestLinksLimitReachedExceptionBase : public LinksLimitReachedExceptionBase {
    public:
        TestLinksLimitReachedExceptionBase(std::string message) : LinksLimitReachedExceptionBase(message) {}
    };
    
    TestLinksLimitReachedExceptionBase ex("test message");
    EXPECT_STREQ(ex.what(), "test message");
}

TEST(ExceptionsTests, LinksLimitReachedExceptionBaseDefaultMessage)
{
    EXPECT_FALSE(LinksLimitReachedExceptionBase::DefaultMessage.empty());
    EXPECT_TRUE(LinksLimitReachedExceptionBase::DefaultMessage.find("лимит") != std::string::npos);
}

// Test LinksLimitReachedException
TEST(ExceptionsTests, LinksLimitReachedExceptionInheritance)
{
    LinksLimitReachedException<int> ex;
    EXPECT_TRUE(dynamic_cast<LinksLimitReachedExceptionBase*>(&ex) != nullptr);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&ex) != nullptr);
}

TEST(ExceptionsTests, LinksLimitReachedExceptionConstructors)
{
    // Test default constructor
    LinksLimitReachedException<int> ex1;
    std::string whatStr1(ex1.what());
    EXPECT_TRUE(whatStr1.find("лимит") != std::string::npos);
    
    // Test string constructor
    LinksLimitReachedException<int> ex2("Custom message");
    EXPECT_STREQ(ex2.what(), "Custom message");
    
    // Test limit constructor
    LinksLimitReachedException<int> ex3(1000);
    std::string whatStr3(ex3.what());
    EXPECT_TRUE(whatStr3.find("1000") != std::string::npos);
    EXPECT_TRUE(whatStr3.find("лимит") != std::string::npos);
}

TEST(ExceptionsTests, LinksLimitReachedExceptionThrowCatch)
{
    try {
        throw LinksLimitReachedException<int>(500);
    } catch (const LinksLimitReachedExceptionBase& e) {
        std::string whatStr(e.what());
        EXPECT_TRUE(whatStr.find("500") != std::string::npos);
    }
    
    try {
        throw LinksLimitReachedException<int>("test");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test");
    }
}

// Test LinkWithSameValueAlreadyExistsException
TEST(ExceptionsTests, LinkWithSameValueAlreadyExistsExceptionInheritance)
{
    LinkWithSameValueAlreadyExistsException ex;
    EXPECT_TRUE(dynamic_cast<std::exception*>(&ex) != nullptr);
}

TEST(ExceptionsTests, LinkWithSameValueAlreadyExistsExceptionConstructors)
{
    // Test default constructor
    LinkWithSameValueAlreadyExistsException ex1;
    std::string whatStr1(ex1.what());
    EXPECT_TRUE(whatStr1.find("значением") != std::string::npos);
    
    // Test string constructor
    LinkWithSameValueAlreadyExistsException ex2("Custom message");
    EXPECT_STREQ(ex2.what(), "Custom message");
}

TEST(ExceptionsTests, LinkWithSameValueAlreadyExistsExceptionDefaultMessage)
{
    EXPECT_FALSE(LinkWithSameValueAlreadyExistsException::DefaultMessage.empty());
    EXPECT_TRUE(LinkWithSameValueAlreadyExistsException::DefaultMessage.find("значением") != std::string::npos);
}

TEST(ExceptionsTests, LinkWithSameValueAlreadyExistsExceptionThrowCatch)
{
    try {
        throw LinkWithSameValueAlreadyExistsException("test message");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test message");
    }
    
    try {
        throw LinkWithSameValueAlreadyExistsException();
    } catch (const std::exception& e) {
        std::string whatStr(e.what());
        EXPECT_TRUE(whatStr.find("значением") != std::string::npos);
    }
}

// Test that all exceptions can be caught as std::exception
TEST(ExceptionsTests, AllExceptionsCatchableAsStdException)
{
    // Test ArgumentLinkDoesNotExistsException
    try {
        throw ArgumentLinkDoesNotExistsException<int>("test");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test");
    }
    
    // Test ArgumentLinkHasDependenciesException
    try {
        throw ArgumentLinkHasDependenciesException<int>("test");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test");
    }
    
    // Test LinksLimitReachedException
    try {
        throw LinksLimitReachedException<int>("test");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test");
    }
    
    // Test LinkWithSameValueAlreadyExistsException
    try {
        throw LinkWithSameValueAlreadyExistsException("test");
    } catch (const std::exception& e) {
        EXPECT_STREQ(e.what(), "test");
    }
}

// Test template instantiation with different types
TEST(ExceptionsTests, TemplateInstantiationWithDifferentTypes)
{
    // Test with int
    ArgumentLinkDoesNotExistsException<int> intEx(42);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&intEx) != nullptr);
    
    // Test with long
    ArgumentLinkDoesNotExistsException<long> longEx(123456L);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&longEx) != nullptr);
    
    // Test with unsigned int
    ArgumentLinkHasDependenciesException<unsigned int> uintEx(42u);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&uintEx) != nullptr);
    
    // Test LinksLimitReachedException with different types
    LinksLimitReachedException<size_t> sizeEx(1000);
    EXPECT_TRUE(dynamic_cast<std::exception*>(&sizeEx) != nullptr);
}