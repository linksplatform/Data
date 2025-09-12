#pragma once
#include <sstream>

// Try to include Platform.Converters, but provide fallback if not available
#ifdef __has_include
    #if __has_include(<Platform.Converters.h>)
        #include <Platform.Converters.h>
        #define HAS_PLATFORM_CONVERTERS
    #endif
#endif

// Fallback converter if Platform.Converters is not available
#ifndef HAS_PLATFORM_CONVERTERS
namespace Platform { namespace Converters {
    template<typename TTarget, typename TSource>
    TTarget To(const TSource& source) {
        std::stringstream ss;
        ss << source;
        return ss.str();
    }
}}
#endif