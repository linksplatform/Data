#include <iostream>
#include <stdexcept>
#include <exception>
#include <string>
#include <typeinfo>
#include <sstream>
#include <vector>
#include <memory>

// Simple converter mock for testing
template<typename T>
std::string ToStr(const T& value) {
    std::stringstream ss;
    ss << value;
    return ss.str();
}

// Standalone exception definitions for testing (without external dependencies)
namespace Platform::Data::Exceptions
{
    // ArgumentLinkDoesNotExistsException
    template <typename ...> class ArgumentLinkDoesNotExistsException;
    template <typename TLinkAddress> 
    class ArgumentLinkDoesNotExistsException<TLinkAddress> : public std::invalid_argument
    {
        public: ArgumentLinkDoesNotExistsException(TLinkAddress link, std::string argumentName) : std::invalid_argument(FormatMessage(link, argumentName)) { }

        public: ArgumentLinkDoesNotExistsException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        public: ArgumentLinkDoesNotExistsException(std::string message, const std::exception& innerException) : std::invalid_argument(message) { }

        public: ArgumentLinkDoesNotExistsException(std::string message) : std::invalid_argument(message) { }

        public: ArgumentLinkDoesNotExistsException() : std::invalid_argument("") { }

        private: static std::string FormatMessage(TLinkAddress link, std::string argumentName) { 
            return std::string("Связь [").append(ToStr(link)).append("] переданная в аргумент [").append(argumentName).append("] не существует.");
        }

        private: static std::string FormatMessage(TLinkAddress link) { 
            return std::string("Связь [").append(ToStr(link)).append("] переданная в качестве аргумента не существует.");
        }
    };

    // ArgumentLinkHasDependenciesException
    template <typename ...> class ArgumentLinkHasDependenciesException;
    template <typename TLinkAddress> 
    class ArgumentLinkHasDependenciesException<TLinkAddress> : public std::invalid_argument
    {
        public: ArgumentLinkHasDependenciesException(TLinkAddress link, std::string paramName) : std::invalid_argument(FormatMessage(link, paramName)) { }

        public: ArgumentLinkHasDependenciesException(TLinkAddress link) : std::invalid_argument(FormatMessage(link)) { }

        public: ArgumentLinkHasDependenciesException(std::string message, const std::exception& innerException) : std::invalid_argument(message) { }

        public: ArgumentLinkHasDependenciesException(std::string message) : std::invalid_argument(message) { }

        public: ArgumentLinkHasDependenciesException() : std::invalid_argument("") { }

        private: static std::string FormatMessage(TLinkAddress link, std::string paramName) { 
            return std::string("У связи [").append(ToStr(link)).append("] переданной в аргумент [").append(paramName).append("] присутствуют зависимости, которые препятствуют изменению её внутренней структуры.");
        }

        private: static std::string FormatMessage(TLinkAddress link) { 
            return std::string("У связи [").append(ToStr(link)).append("] переданной в качестве аргумента присутствуют зависимости, которые препятствуют изменению её внутренней структуры.");
        }
    };

    // LinksLimitReachedExceptionBase
    class LinksLimitReachedExceptionBase : public std::exception
    {
        private: std::string _message;
        
        public: inline static std::string DefaultMessage = "Достигнут лимит количества связей в хранилище.";

        protected: LinksLimitReachedExceptionBase(std::string message, const std::exception& innerException) : _message(message) { }

        protected: LinksLimitReachedExceptionBase(std::string message) : _message(message) { }
        
        public: const char* what() const noexcept override { return _message.c_str(); }
    };

    // LinksLimitReachedException
    template <typename ...> class LinksLimitReachedException;
    template <typename TLinkAddress> 
    class LinksLimitReachedException<TLinkAddress> : public LinksLimitReachedExceptionBase
    {
        public: LinksLimitReachedException(TLinkAddress limit) : LinksLimitReachedExceptionBase(FormatMessage(limit)) { }

        public: LinksLimitReachedException(std::string message, const std::exception& innerException) : LinksLimitReachedExceptionBase(message, innerException) { }

        public: LinksLimitReachedException(std::string message) : LinksLimitReachedExceptionBase(message) { }

        public: LinksLimitReachedException() : LinksLimitReachedExceptionBase(DefaultMessage) { }

        private: static std::string FormatMessage(TLinkAddress limit) { 
            return std::string("Достигнут лимит количества связей в хранилище (").append(ToStr(limit)).append(").");
        }
    };

    // LinkWithSameValueAlreadyExistsException
    class LinkWithSameValueAlreadyExistsException : public std::exception
    {
        private: std::string _message;
        
        public: inline static std::string DefaultMessage = "Связь с таким же значением уже существует.";

        public: LinkWithSameValueAlreadyExistsException(std::string message, const std::exception& innerException) : _message(message) { }

        public: LinkWithSameValueAlreadyExistsException(std::string message) : _message(message) { }

        public: LinkWithSameValueAlreadyExistsException() : _message(DefaultMessage) { }
        
        public: const char* what() const noexcept override { return _message.c_str(); }
    };
}

using namespace Platform::Data::Exceptions;

int main() {
    std::cout << "Testing exception inheritance and functionality..." << std::endl;
    
    try {
        // Test ArgumentLinkDoesNotExistsException
        std::cout << "Testing ArgumentLinkDoesNotExistsException..." << std::endl;
        ArgumentLinkDoesNotExistsException<int> ex1(42, "testParam");
        std::cout << "Message: " << ex1.what() << std::endl;
        
        // Test inheritance
        std::exception* basePtr = &ex1;
        std::cout << "Can be cast to std::exception: " << (basePtr != nullptr ? "YES" : "NO") << std::endl;
        
        std::invalid_argument* invalidArgPtr = dynamic_cast<std::invalid_argument*>(&ex1);
        std::cout << "Can be cast to std::invalid_argument: " << (invalidArgPtr != nullptr ? "YES" : "NO") << std::endl;
        
        // Test ArgumentLinkHasDependenciesException
        std::cout << "\nTesting ArgumentLinkHasDependenciesException..." << std::endl;
        ArgumentLinkHasDependenciesException<int> ex2(123, "depParam");
        std::cout << "Message: " << ex2.what() << std::endl;
        
        // Test LinksLimitReachedException
        std::cout << "\nTesting LinksLimitReachedException..." << std::endl;
        LinksLimitReachedException<int> ex3(1000);
        std::cout << "Message: " << ex3.what() << std::endl;
        
        LinksLimitReachedExceptionBase* baseExPtr = &ex3;
        std::cout << "Can be cast to LinksLimitReachedExceptionBase: " << (baseExPtr != nullptr ? "YES" : "NO") << std::endl;
        
        // Test LinkWithSameValueAlreadyExistsException
        std::cout << "\nTesting LinkWithSameValueAlreadyExistsException..." << std::endl;
        LinkWithSameValueAlreadyExistsException ex4;
        std::cout << "Default message: " << ex4.what() << std::endl;
        
        // Test exception throwing and catching
        std::cout << "\nTesting exception throwing and catching..." << std::endl;
        
        try {
            throw ArgumentLinkDoesNotExistsException<int>(999, "throwTest");
        } catch (const std::invalid_argument& e) {
            std::cout << "Caught ArgumentLinkDoesNotExistsException as std::invalid_argument: " << e.what() << std::endl;
        }
        
        try {
            throw LinksLimitReachedException<int>("Custom limit message");
        } catch (const std::exception& e) {
            std::cout << "Caught LinksLimitReachedException as std::exception: " << e.what() << std::endl;
        }
        
        try {
            throw LinkWithSameValueAlreadyExistsException("Custom value message");
        } catch (const std::exception& e) {
            std::cout << "Caught LinkWithSameValueAlreadyExistsException as std::exception: " << e.what() << std::endl;
        }
        
        // Test polymorphic behavior
        std::cout << "\nTesting polymorphic behavior..." << std::endl;
        std::vector<std::unique_ptr<std::exception>> exceptions;
        exceptions.push_back(std::make_unique<ArgumentLinkDoesNotExistsException<int>>("poly test 1"));
        exceptions.push_back(std::make_unique<ArgumentLinkHasDependenciesException<int>>("poly test 2"));
        exceptions.push_back(std::make_unique<LinksLimitReachedException<int>>("poly test 3"));
        exceptions.push_back(std::make_unique<LinkWithSameValueAlreadyExistsException>("poly test 4"));
        
        for (const auto& ex : exceptions) {
            std::cout << "Polymorphic exception message: " << ex->what() << std::endl;
        }
        
        std::cout << "\nAll tests completed successfully!" << std::endl;
        return 0;
        
    } catch (const std::exception& e) {
        std::cout << "ERROR: Caught unexpected exception: " << e.what() << std::endl;
        return 1;
    } catch (...) {
        std::cout << "ERROR: Caught unknown exception" << std::endl;
        return 1;
    }
}