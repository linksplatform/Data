#pragma once
#include <vector>
#include <concepts>

namespace Platform::Data
{
    template<typename T>
    concept CLinks = requires(T& links, const T& constLinks, 
                             const std::vector<typename T::LinkAddressType>& restriction,
                             const std::vector<typename T::LinkAddressType>& substitution,
                             const typename T::ReadHandlerType& readHandler,
                             const typename T::WriteHandlerType& writeHandler)
    {
        // Type aliases that must exist
        typename T::LinkAddressType;
        typename T::LinksOptionsType;
        typename T::LinkType;
        typename T::ReadHandlerType;
        typename T::WriteHandlerType;
        
        // Constants must exist
        T::Constants;
        
        // Core methods that must exist
        { constLinks.Count(restriction) } -> std::convertible_to<typename T::LinkAddressType>;
        { constLinks.Each(restriction, readHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        { links.Create(substitution, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        { links.Update(restriction, substitution, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        { links.Delete(restriction, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
    };
}