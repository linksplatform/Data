#include <iostream>
#include <stdexcept>
#include <exception>
#include <string>

// Mock Platform::Converters::To for testing
namespace Platform {
namespace Converters {
    template<typename T>
    std::string To(int value) {
        return std::to_string(value);
    }
}
}

// Include our exception headers
#include "../cpp/Platform.Data/Exceptions/ArgumentLinkDoesNotExistsException.h"
#include "../cpp/Platform.Data/Exceptions/ArgumentLinkHasDependenciesException.h"
#include "../cpp/Platform.Data/Exceptions/LinksLimitReachedExceptionBase.h"
#include "../cpp/Platform.Data/Exceptions/LinksLimitReachedException.h"
#include "../cpp/Platform.Data/Exceptions/LinkWithSameValueAlreadyExistsException.h"

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