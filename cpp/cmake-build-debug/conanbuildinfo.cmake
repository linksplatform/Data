include(CMakeParseArguments)

macro(conan_find_apple_frameworks FRAMEWORKS_FOUND FRAMEWORKS SUFFIX BUILD_TYPE)
    if(APPLE)
        if(CMAKE_BUILD_TYPE)
            set(_BTYPE ${CMAKE_BUILD_TYPE})
        elseif(NOT BUILD_TYPE STREQUAL "")
            set(_BTYPE ${BUILD_TYPE})
        endif()
        if(_BTYPE)
            if(${_BTYPE} MATCHES "Debug|_DEBUG")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_DEBUG} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_DEBUG} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "Release|_RELEASE")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_RELEASE} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_RELEASE} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "RelWithDebInfo|_RELWITHDEBINFO")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_RELWITHDEBINFO} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_RELWITHDEBINFO} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "MinSizeRel|_MINSIZEREL")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_MINSIZEREL} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_MINSIZEREL} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            endif()
        endif()
        foreach(_FRAMEWORK ${FRAMEWORKS})
            # https://cmake.org/pipermail/cmake-developers/2017-August/030199.html
            find_library(CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND NAME ${_FRAMEWORK} PATHS ${CONAN_FRAMEWORK_DIRS${SUFFIX}} CMAKE_FIND_ROOT_PATH_BOTH)
            if(CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND)
                list(APPEND ${FRAMEWORKS_FOUND} ${CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND})
            else()
                message(FATAL_ERROR "Framework library ${_FRAMEWORK} not found in paths: ${CONAN_FRAMEWORK_DIRS${SUFFIX}}")
            endif()
        endforeach()
    endif()
endmacro()


#################
###  GTEST
#################
set(CONAN_GTEST_ROOT "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4")
set(CONAN_INCLUDE_DIRS_GTEST "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/include")
set(CONAN_LIB_DIRS_GTEST "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/lib")
set(CONAN_BIN_DIRS_GTEST )
set(CONAN_RES_DIRS_GTEST )
set(CONAN_SRC_DIRS_GTEST )
set(CONAN_BUILD_DIRS_GTEST "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/")
set(CONAN_FRAMEWORK_DIRS_GTEST )
set(CONAN_LIBS_GTEST gtest_main gmock_main gmock gtest)
set(CONAN_PKG_LIBS_GTEST gtest_main gmock_main gmock gtest)
set(CONAN_SYSTEM_LIBS_GTEST pthread)
set(CONAN_FRAMEWORKS_GTEST )
set(CONAN_FRAMEWORKS_FOUND_GTEST "")  # Will be filled later
set(CONAN_DEFINES_GTEST )
set(CONAN_BUILD_MODULES_PATHS_GTEST )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_GTEST )

set(CONAN_C_FLAGS_GTEST "")
set(CONAN_CXX_FLAGS_GTEST "")
set(CONAN_SHARED_LINKER_FLAGS_GTEST "")
set(CONAN_EXE_LINKER_FLAGS_GTEST "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_GTEST_LIST "")
set(CONAN_CXX_FLAGS_GTEST_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_GTEST_LIST "")
set(CONAN_EXE_LINKER_FLAGS_GTEST_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_GTEST "${CONAN_FRAMEWORKS_GTEST}" "_GTEST" "")
# Append to aggregated values variable
set(CONAN_LIBS_GTEST ${CONAN_PKG_LIBS_GTEST} ${CONAN_SYSTEM_LIBS_GTEST} ${CONAN_FRAMEWORKS_FOUND_GTEST})


#################
###  PLATFORM.RANGES
#################
set(CONAN_PLATFORM.RANGES_ROOT "/home/freephoenix888/.conan/data/platform.ranges/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.RANGES "/home/freephoenix888/.conan/data/platform.ranges/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.RANGES )
set(CONAN_BIN_DIRS_PLATFORM.RANGES )
set(CONAN_RES_DIRS_PLATFORM.RANGES )
set(CONAN_SRC_DIRS_PLATFORM.RANGES )
set(CONAN_BUILD_DIRS_PLATFORM.RANGES "/home/freephoenix888/.conan/data/platform.ranges/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.RANGES )
set(CONAN_LIBS_PLATFORM.RANGES )
set(CONAN_PKG_LIBS_PLATFORM.RANGES )
set(CONAN_SYSTEM_LIBS_PLATFORM.RANGES )
set(CONAN_FRAMEWORKS_PLATFORM.RANGES )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.RANGES )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.RANGES )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES )

set(CONAN_C_FLAGS_PLATFORM.RANGES "")
set(CONAN_CXX_FLAGS_PLATFORM.RANGES "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.RANGES_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.RANGES_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES "${CONAN_FRAMEWORKS_PLATFORM.RANGES}" "_PLATFORM.RANGES" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.RANGES ${CONAN_PKG_LIBS_PLATFORM.RANGES} ${CONAN_SYSTEM_LIBS_PLATFORM.RANGES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES})


#################
###  PLATFORM.SETTERS
#################
set(CONAN_PLATFORM.SETTERS_ROOT "/home/freephoenix888/.conan/data/platform.setters/0.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.SETTERS "/home/freephoenix888/.conan/data/platform.setters/0.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.SETTERS )
set(CONAN_BIN_DIRS_PLATFORM.SETTERS )
set(CONAN_RES_DIRS_PLATFORM.SETTERS )
set(CONAN_SRC_DIRS_PLATFORM.SETTERS )
set(CONAN_BUILD_DIRS_PLATFORM.SETTERS "/home/freephoenix888/.conan/data/platform.setters/0.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.SETTERS )
set(CONAN_LIBS_PLATFORM.SETTERS )
set(CONAN_PKG_LIBS_PLATFORM.SETTERS )
set(CONAN_SYSTEM_LIBS_PLATFORM.SETTERS )
set(CONAN_FRAMEWORKS_PLATFORM.SETTERS )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.SETTERS )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.SETTERS )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS )

set(CONAN_C_FLAGS_PLATFORM.SETTERS "")
set(CONAN_CXX_FLAGS_PLATFORM.SETTERS "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.SETTERS_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.SETTERS_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS "${CONAN_FRAMEWORKS_PLATFORM.SETTERS}" "_PLATFORM.SETTERS" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.SETTERS ${CONAN_PKG_LIBS_PLATFORM.SETTERS} ${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS})


#################
###  PLATFORM.INTERFACES
#################
set(CONAN_PLATFORM.INTERFACES_ROOT "/home/freephoenix888/.conan/data/platform.interfaces/0.1.4/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES "/home/freephoenix888/.conan/data/platform.interfaces/0.1.4/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.INTERFACES )
set(CONAN_BIN_DIRS_PLATFORM.INTERFACES )
set(CONAN_RES_DIRS_PLATFORM.INTERFACES )
set(CONAN_SRC_DIRS_PLATFORM.INTERFACES )
set(CONAN_BUILD_DIRS_PLATFORM.INTERFACES "/home/freephoenix888/.conan/data/platform.interfaces/0.1.4/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.INTERFACES )
set(CONAN_LIBS_PLATFORM.INTERFACES )
set(CONAN_PKG_LIBS_PLATFORM.INTERFACES )
set(CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES )
set(CONAN_FRAMEWORKS_PLATFORM.INTERFACES )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.INTERFACES )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.INTERFACES )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES )

set(CONAN_C_FLAGS_PLATFORM.INTERFACES "")
set(CONAN_CXX_FLAGS_PLATFORM.INTERFACES "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.INTERFACES_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.INTERFACES_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES "${CONAN_FRAMEWORKS_PLATFORM.INTERFACES}" "_PLATFORM.INTERFACES" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.INTERFACES ${CONAN_PKG_LIBS_PLATFORM.INTERFACES} ${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES})


#################
###  PLATFORM.CONVERTERS
#################
set(CONAN_PLATFORM.CONVERTERS_ROOT "/home/freephoenix888/.conan/data/platform.converters/0.1.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS "/home/freephoenix888/.conan/data/platform.converters/0.1.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.CONVERTERS )
set(CONAN_BIN_DIRS_PLATFORM.CONVERTERS )
set(CONAN_RES_DIRS_PLATFORM.CONVERTERS )
set(CONAN_SRC_DIRS_PLATFORM.CONVERTERS )
set(CONAN_BUILD_DIRS_PLATFORM.CONVERTERS "/home/freephoenix888/.conan/data/platform.converters/0.1.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.CONVERTERS )
set(CONAN_LIBS_PLATFORM.CONVERTERS )
set(CONAN_PKG_LIBS_PLATFORM.CONVERTERS )
set(CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS )
set(CONAN_FRAMEWORKS_PLATFORM.CONVERTERS )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.CONVERTERS )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.CONVERTERS )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS )

set(CONAN_C_FLAGS_PLATFORM.CONVERTERS "")
set(CONAN_CXX_FLAGS_PLATFORM.CONVERTERS "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.CONVERTERS_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS "${CONAN_FRAMEWORKS_PLATFORM.CONVERTERS}" "_PLATFORM.CONVERTERS" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.CONVERTERS ${CONAN_PKG_LIBS_PLATFORM.CONVERTERS} ${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS})


#################
###  PLATFORM.HASHING
#################
set(CONAN_PLATFORM.HASHING_ROOT "/home/freephoenix888/.conan/data/platform.hashing/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.HASHING "/home/freephoenix888/.conan/data/platform.hashing/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.HASHING )
set(CONAN_BIN_DIRS_PLATFORM.HASHING )
set(CONAN_RES_DIRS_PLATFORM.HASHING )
set(CONAN_SRC_DIRS_PLATFORM.HASHING )
set(CONAN_BUILD_DIRS_PLATFORM.HASHING "/home/freephoenix888/.conan/data/platform.hashing/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.HASHING )
set(CONAN_LIBS_PLATFORM.HASHING )
set(CONAN_PKG_LIBS_PLATFORM.HASHING )
set(CONAN_SYSTEM_LIBS_PLATFORM.HASHING )
set(CONAN_FRAMEWORKS_PLATFORM.HASHING )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.HASHING )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.HASHING )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING )

set(CONAN_C_FLAGS_PLATFORM.HASHING "")
set(CONAN_CXX_FLAGS_PLATFORM.HASHING "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.HASHING_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.HASHING_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING "${CONAN_FRAMEWORKS_PLATFORM.HASHING}" "_PLATFORM.HASHING" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.HASHING ${CONAN_PKG_LIBS_PLATFORM.HASHING} ${CONAN_SYSTEM_LIBS_PLATFORM.HASHING} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING})


#################
###  PLATFORM.EXCEPTIONS
#################
set(CONAN_PLATFORM.EXCEPTIONS_ROOT "/home/freephoenix888/.conan/data/platform.exceptions/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS "/home/freephoenix888/.conan/data/platform.exceptions/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS )
set(CONAN_BIN_DIRS_PLATFORM.EXCEPTIONS )
set(CONAN_RES_DIRS_PLATFORM.EXCEPTIONS )
set(CONAN_SRC_DIRS_PLATFORM.EXCEPTIONS )
set(CONAN_BUILD_DIRS_PLATFORM.EXCEPTIONS "/home/freephoenix888/.conan/data/platform.exceptions/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.EXCEPTIONS )
set(CONAN_LIBS_PLATFORM.EXCEPTIONS )
set(CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS )
set(CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS )
set(CONAN_FRAMEWORKS_PLATFORM.EXCEPTIONS )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.EXCEPTIONS )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.EXCEPTIONS )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS )

set(CONAN_C_FLAGS_PLATFORM.EXCEPTIONS "")
set(CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS "${CONAN_FRAMEWORKS_PLATFORM.EXCEPTIONS}" "_PLATFORM.EXCEPTIONS" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.EXCEPTIONS ${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS} ${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS})


#################
###  PLATFORM.DELEGATES
#################
set(CONAN_PLATFORM.DELEGATES_ROOT "/home/freephoenix888/.conan/data/platform.delegates/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES "/home/freephoenix888/.conan/data/platform.delegates/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_PLATFORM.DELEGATES )
set(CONAN_BIN_DIRS_PLATFORM.DELEGATES )
set(CONAN_RES_DIRS_PLATFORM.DELEGATES )
set(CONAN_SRC_DIRS_PLATFORM.DELEGATES )
set(CONAN_BUILD_DIRS_PLATFORM.DELEGATES "/home/freephoenix888/.conan/data/platform.delegates/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_PLATFORM.DELEGATES )
set(CONAN_LIBS_PLATFORM.DELEGATES )
set(CONAN_PKG_LIBS_PLATFORM.DELEGATES )
set(CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES )
set(CONAN_FRAMEWORKS_PLATFORM.DELEGATES )
set(CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES "")  # Will be filled later
set(CONAN_DEFINES_PLATFORM.DELEGATES )
set(CONAN_BUILD_MODULES_PATHS_PLATFORM.DELEGATES )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES )

set(CONAN_C_FLAGS_PLATFORM.DELEGATES "")
set(CONAN_CXX_FLAGS_PLATFORM.DELEGATES "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_PLATFORM.DELEGATES_LIST "")
set(CONAN_CXX_FLAGS_PLATFORM.DELEGATES_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_LIST "")
set(CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES "${CONAN_FRAMEWORKS_PLATFORM.DELEGATES}" "_PLATFORM.DELEGATES" "")
# Append to aggregated values variable
set(CONAN_LIBS_PLATFORM.DELEGATES ${CONAN_PKG_LIBS_PLATFORM.DELEGATES} ${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES})


### Definition of global aggregated variables ###

set(CONAN_PACKAGE_NAME None)
set(CONAN_PACKAGE_VERSION None)

set(CONAN_SETTINGS_ARCH "x86_64")
set(CONAN_SETTINGS_ARCH_BUILD "x86_64")
set(CONAN_SETTINGS_BUILD_TYPE "Release")
set(CONAN_SETTINGS_COMPILER "gcc")
set(CONAN_SETTINGS_COMPILER_LIBCXX "libstdc++11")
set(CONAN_SETTINGS_COMPILER_VERSION "10")
set(CONAN_SETTINGS_OS "Linux")
set(CONAN_SETTINGS_OS_BUILD "Linux")

set(CONAN_DEPENDENCIES gtest platform.ranges platform.setters platform.interfaces platform.converters platform.hashing platform.exceptions platform.delegates)
# Storing original command line args (CMake helper) flags
set(CONAN_CMD_CXX_FLAGS ${CONAN_CXX_FLAGS})

set(CONAN_CMD_SHARED_LINKER_FLAGS ${CONAN_SHARED_LINKER_FLAGS})
set(CONAN_CMD_C_FLAGS ${CONAN_C_FLAGS})
# Defining accumulated conan variables for all deps

set(CONAN_INCLUDE_DIRS "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/include"
			"/home/freephoenix888/.conan/data/platform.ranges/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.setters/0.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.interfaces/0.1.4/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.converters/0.1.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.hashing/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.exceptions/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/freephoenix888/.conan/data/platform.delegates/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include" ${CONAN_INCLUDE_DIRS})
set(CONAN_LIB_DIRS "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/lib" ${CONAN_LIB_DIRS})
set(CONAN_BIN_DIRS  ${CONAN_BIN_DIRS})
set(CONAN_RES_DIRS  ${CONAN_RES_DIRS})
set(CONAN_FRAMEWORK_DIRS  ${CONAN_FRAMEWORK_DIRS})
set(CONAN_LIBS gtest_main gmock_main gmock gtest ${CONAN_LIBS})
set(CONAN_PKG_LIBS gtest_main gmock_main gmock gtest ${CONAN_PKG_LIBS})
set(CONAN_SYSTEM_LIBS pthread ${CONAN_SYSTEM_LIBS})
set(CONAN_FRAMEWORKS  ${CONAN_FRAMEWORKS})
set(CONAN_FRAMEWORKS_FOUND "")  # Will be filled later
set(CONAN_DEFINES  ${CONAN_DEFINES})
set(CONAN_BUILD_MODULES_PATHS  ${CONAN_BUILD_MODULES_PATHS})
set(CONAN_CMAKE_MODULE_PATH "/home/freephoenix888/.conan/data/gtest/cci.20210126/_/_/package/e019a06362b932ca5d1b082b6c112aa150c88de4/"
			"/home/freephoenix888/.conan/data/platform.ranges/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.setters/0.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.interfaces/0.1.4/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.converters/0.1.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.hashing/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.exceptions/0.2.0/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/freephoenix888/.conan/data/platform.delegates/0.1.3/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/" ${CONAN_CMAKE_MODULE_PATH})

set(CONAN_CXX_FLAGS " ${CONAN_CXX_FLAGS}")
set(CONAN_SHARED_LINKER_FLAGS " ${CONAN_SHARED_LINKER_FLAGS}")
set(CONAN_EXE_LINKER_FLAGS " ${CONAN_EXE_LINKER_FLAGS}")
set(CONAN_C_FLAGS " ${CONAN_C_FLAGS}")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND "${CONAN_FRAMEWORKS}" "" "")
# Append to aggregated values variable: Use CONAN_LIBS instead of CONAN_PKG_LIBS to include user appended vars
set(CONAN_LIBS ${CONAN_LIBS} ${CONAN_SYSTEM_LIBS} ${CONAN_FRAMEWORKS_FOUND})


###  Definition of macros and functions ###

macro(conan_define_targets)
    if(${CMAKE_VERSION} VERSION_LESS "3.1.2")
        message(FATAL_ERROR "TARGETS not supported by your CMake version!")
    endif()  # CMAKE > 3.x
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${CONAN_CMD_CXX_FLAGS}")
    set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${CONAN_CMD_C_FLAGS}")
    set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} ${CONAN_CMD_SHARED_LINKER_FLAGS}")


    set(_CONAN_PKG_LIBS_GTEST_DEPENDENCIES "${CONAN_SYSTEM_LIBS_GTEST} ${CONAN_FRAMEWORKS_FOUND_GTEST} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GTEST_DEPENDENCIES "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GTEST}" "${CONAN_LIB_DIRS_GTEST}"
                                  CONAN_PACKAGE_TARGETS_GTEST "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES}"
                                  "" gtest)
    set(_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_GTEST_DEBUG} ${CONAN_FRAMEWORKS_FOUND_GTEST_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GTEST_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GTEST_DEBUG}" "${CONAN_LIB_DIRS_GTEST_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_GTEST_DEBUG "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_DEBUG}"
                                  "debug" gtest)
    set(_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_GTEST_RELEASE} ${CONAN_FRAMEWORKS_FOUND_GTEST_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GTEST_RELEASE}" "${CONAN_LIB_DIRS_GTEST_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_GTEST_RELEASE "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELEASE}"
                                  "release" gtest)
    set(_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_GTEST_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_GTEST_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GTEST_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_GTEST_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_GTEST_RELWITHDEBINFO "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" gtest)
    set(_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_GTEST_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_GTEST_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GTEST_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GTEST_MINSIZEREL}" "${CONAN_LIB_DIRS_GTEST_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_GTEST_MINSIZEREL "${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" gtest)

    add_library(CONAN_PKG::gtest INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::gtest PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_GTEST} ${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GTEST_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_GTEST_RELEASE} ${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GTEST_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_GTEST_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GTEST_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_GTEST_MINSIZEREL} ${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GTEST_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_GTEST_DEBUG} ${_CONAN_PKG_LIBS_GTEST_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GTEST_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GTEST_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::gtest PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_GTEST}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_GTEST_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_GTEST_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_GTEST_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_GTEST_DEBUG}>)
    set_property(TARGET CONAN_PKG::gtest PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_GTEST}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_GTEST_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_GTEST_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_GTEST_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_GTEST_DEBUG}>)
    set_property(TARGET CONAN_PKG::gtest PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_GTEST_LIST} ${CONAN_CXX_FLAGS_GTEST_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_GTEST_RELEASE_LIST} ${CONAN_CXX_FLAGS_GTEST_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_GTEST_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_GTEST_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_GTEST_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_GTEST_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_GTEST_DEBUG_LIST}  ${CONAN_CXX_FLAGS_GTEST_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.RANGES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES} CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.RANGES}" "${CONAN_LIB_DIRS_PLATFORM.RANGES}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.RANGES "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES}"
                                  "" platform.ranges)
    set(_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.RANGES_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES_DEBUG} CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.RANGES_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.RANGES_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_DEBUG}"
                                  "debug" platform.ranges)
    set(_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.RANGES_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES_RELEASE} CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.RANGES_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.RANGES_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELEASE}"
                                  "release" platform.ranges)
    set(_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.RANGES_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES_RELWITHDEBINFO} CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.RANGES_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.RANGES_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.ranges)
    set(_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.RANGES_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.RANGES_MINSIZEREL} CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.RANGES_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.RANGES_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.ranges)

    add_library(CONAN_PKG::platform.ranges INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.ranges PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.RANGES} ${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.RANGES_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.RANGES_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.RANGES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.RANGES_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.ranges PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.RANGES}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.RANGES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.RANGES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.RANGES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.RANGES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.ranges PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.RANGES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.ranges PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.RANGES_LIST} ${CONAN_CXX_FLAGS_PLATFORM.RANGES_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.RANGES_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.RANGES_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.RANGES_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.RANGES_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.RANGES_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.RANGES_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.RANGES_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.RANGES_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS} CONAN_PKG::platform.interfaces")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.SETTERS}" "${CONAN_LIB_DIRS_PLATFORM.SETTERS}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES}"
                                  "" platform.setters)
    set(_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS_DEBUG} CONAN_PKG::platform.interfaces")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.SETTERS_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.SETTERS_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_DEBUG}"
                                  "debug" platform.setters)
    set(_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS_RELEASE} CONAN_PKG::platform.interfaces")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.SETTERS_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.SETTERS_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELEASE}"
                                  "release" platform.setters)
    set(_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS_RELWITHDEBINFO} CONAN_PKG::platform.interfaces")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.SETTERS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.SETTERS_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.setters)
    set(_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.SETTERS_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.SETTERS_MINSIZEREL} CONAN_PKG::platform.interfaces")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.SETTERS_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.SETTERS_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.setters)

    add_library(CONAN_PKG::platform.setters INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.setters PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS} ${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.SETTERS_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.SETTERS_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.SETTERS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.SETTERS_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.setters PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.SETTERS}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.SETTERS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.SETTERS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.SETTERS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.SETTERS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.setters PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.SETTERS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.setters PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.SETTERS_LIST} ${CONAN_CXX_FLAGS_PLATFORM.SETTERS_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.SETTERS_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.SETTERS_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.SETTERS_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.SETTERS_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.SETTERS_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.SETTERS_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.SETTERS_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.SETTERS_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.INTERFACES}" "${CONAN_LIB_DIRS_PLATFORM.INTERFACES}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES}"
                                  "" platform.interfaces)
    set(_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.INTERFACES_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_DEBUG}"
                                  "debug" platform.interfaces)
    set(_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.INTERFACES_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.INTERFACES_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELEASE}"
                                  "release" platform.interfaces)
    set(_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.INTERFACES_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.INTERFACES_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.interfaces)
    set(_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.INTERFACES_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.INTERFACES_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.INTERFACES_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.INTERFACES_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.interfaces)

    add_library(CONAN_PKG::platform.interfaces INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.interfaces PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES} ${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.INTERFACES_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.INTERFACES_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.INTERFACES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.INTERFACES_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.interfaces PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.INTERFACES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.interfaces PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.INTERFACES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.interfaces PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.INTERFACES_LIST} ${CONAN_CXX_FLAGS_PLATFORM.INTERFACES_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.INTERFACES_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.INTERFACES_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.INTERFACES_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.INTERFACES_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.INTERFACES_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.INTERFACES_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.INTERFACES_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.INTERFACES_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.CONVERTERS}" "${CONAN_LIB_DIRS_PLATFORM.CONVERTERS}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES}"
                                  "" platform.converters)
    set(_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.CONVERTERS_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_DEBUG}"
                                  "debug" platform.converters)
    set(_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.CONVERTERS_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.CONVERTERS_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELEASE}"
                                  "release" platform.converters)
    set(_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.CONVERTERS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.CONVERTERS_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.converters)
    set(_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.CONVERTERS_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.CONVERTERS_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.CONVERTERS_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.CONVERTERS_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.converters)

    add_library(CONAN_PKG::platform.converters INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.converters PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS} ${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.CONVERTERS_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.CONVERTERS_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.CONVERTERS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.CONVERTERS_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.converters PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.CONVERTERS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.converters PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.CONVERTERS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.converters PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.CONVERTERS_LIST} ${CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.CONVERTERS_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.CONVERTERS_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.CONVERTERS_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.CONVERTERS_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.CONVERTERS_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.HASHING} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.HASHING}" "${CONAN_LIB_DIRS_PLATFORM.HASHING}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.HASHING "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES}"
                                  "" platform.hashing)
    set(_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.HASHING_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.HASHING_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.HASHING_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_DEBUG}"
                                  "debug" platform.hashing)
    set(_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.HASHING_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.HASHING_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.HASHING_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELEASE}"
                                  "release" platform.hashing)
    set(_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.HASHING_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.HASHING_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.HASHING_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.hashing)
    set(_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.HASHING_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.HASHING_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.HASHING_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.HASHING_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.hashing)

    add_library(CONAN_PKG::platform.hashing INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.hashing PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.HASHING} ${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.HASHING_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.HASHING_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.HASHING_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.HASHING_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.hashing PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.HASHING}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.HASHING_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.HASHING_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.HASHING_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.HASHING_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.hashing PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.HASHING_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.hashing PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.HASHING_LIST} ${CONAN_CXX_FLAGS_PLATFORM.HASHING_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.HASHING_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.HASHING_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.HASHING_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.HASHING_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.HASHING_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.HASHING_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.HASHING_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.HASHING_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS} CONAN_PKG::platform.delegates")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS}" "${CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES}"
                                  "" platform.exceptions)
    set(_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS_DEBUG} CONAN_PKG::platform.delegates")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_DEBUG}"
                                  "debug" platform.exceptions)
    set(_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS_RELEASE} CONAN_PKG::platform.delegates")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELEASE}"
                                  "release" platform.exceptions)
    set(_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS_RELWITHDEBINFO} CONAN_PKG::platform.delegates")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.exceptions)
    set(_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.EXCEPTIONS_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.EXCEPTIONS_MINSIZEREL} CONAN_PKG::platform.delegates")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.EXCEPTIONS_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.exceptions)

    add_library(CONAN_PKG::platform.exceptions INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.exceptions PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS} ${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.EXCEPTIONS_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.EXCEPTIONS_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.EXCEPTIONS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.EXCEPTIONS_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.exceptions PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.EXCEPTIONS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.exceptions PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.EXCEPTIONS_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.exceptions PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_LIST} ${CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.EXCEPTIONS_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.EXCEPTIONS_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES "${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.DELEGATES}" "${CONAN_LIB_DIRS_PLATFORM.DELEGATES}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES}"
                                  "" platform.delegates)
    set(_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES_DEBUG} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEBUG}" "${CONAN_LIB_DIRS_PLATFORM.DELEGATES_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_DEBUG "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_DEBUG}"
                                  "debug" platform.delegates)
    set(_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES_RELEASE} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.DELEGATES_RELEASE}" "${CONAN_LIB_DIRS_PLATFORM.DELEGATES_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_RELEASE "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELEASE}"
                                  "release" platform.delegates)
    set(_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.DELEGATES_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_PLATFORM.DELEGATES_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" platform.delegates)
    set(_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_PLATFORM.DELEGATES_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_PLATFORM.DELEGATES_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_PLATFORM.DELEGATES_MINSIZEREL}" "${CONAN_LIB_DIRS_PLATFORM.DELEGATES_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_MINSIZEREL "${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" platform.delegates)

    add_library(CONAN_PKG::platform.delegates INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::platform.delegates PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES} ${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_RELEASE} ${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_MINSIZEREL} ${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_PLATFORM.DELEGATES_DEBUG} ${_CONAN_PKG_LIBS_PLATFORM.DELEGATES_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_PLATFORM.DELEGATES_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_PLATFORM.DELEGATES_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::platform.delegates PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_PLATFORM.DELEGATES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.delegates PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_PLATFORM.DELEGATES_DEBUG}>)
    set_property(TARGET CONAN_PKG::platform.delegates PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_PLATFORM.DELEGATES_LIST} ${CONAN_CXX_FLAGS_PLATFORM.DELEGATES_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_PLATFORM.DELEGATES_RELEASE_LIST} ${CONAN_CXX_FLAGS_PLATFORM.DELEGATES_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_PLATFORM.DELEGATES_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_PLATFORM.DELEGATES_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_PLATFORM.DELEGATES_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_PLATFORM.DELEGATES_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_PLATFORM.DELEGATES_DEBUG_LIST}  ${CONAN_CXX_FLAGS_PLATFORM.DELEGATES_DEBUG_LIST}>)

    set(CONAN_TARGETS CONAN_PKG::gtest CONAN_PKG::platform.ranges CONAN_PKG::platform.setters CONAN_PKG::platform.interfaces CONAN_PKG::platform.converters CONAN_PKG::platform.hashing CONAN_PKG::platform.exceptions CONAN_PKG::platform.delegates)

endmacro()


macro(conan_basic_setup)
    set(options TARGETS NO_OUTPUT_DIRS SKIP_RPATH KEEP_RPATHS SKIP_STD SKIP_FPIC)
    cmake_parse_arguments(ARGUMENTS "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN} )

    if(CONAN_EXPORTED)
        conan_message(STATUS "Conan: called by CMake conan helper")
    endif()

    if(CONAN_IN_LOCAL_CACHE)
        conan_message(STATUS "Conan: called inside local cache")
    endif()

    if(NOT ARGUMENTS_NO_OUTPUT_DIRS)
        conan_message(STATUS "Conan: Adjusting output directories")
        conan_output_dirs_setup()
    endif()

    if(NOT ARGUMENTS_TARGETS)
        conan_message(STATUS "Conan: Using cmake global configuration")
        conan_global_flags()
    else()
        conan_message(STATUS "Conan: Using cmake targets configuration")
        conan_define_targets()
    endif()

    if(ARGUMENTS_SKIP_RPATH)
        # Change by "DEPRECATION" or "SEND_ERROR" when we are ready
        conan_message(WARNING "Conan: SKIP_RPATH is deprecated, it has been renamed to KEEP_RPATHS")
    endif()

    if(NOT ARGUMENTS_SKIP_RPATH AND NOT ARGUMENTS_KEEP_RPATHS)
        # Parameter has renamed, but we keep the compatibility with old SKIP_RPATH
        conan_set_rpath()
    endif()

    if(NOT ARGUMENTS_SKIP_STD)
        conan_set_std()
    endif()

    if(NOT ARGUMENTS_SKIP_FPIC)
        conan_set_fpic()
    endif()

    conan_check_compiler()
    conan_set_libcxx()
    conan_set_vs_runtime()
    conan_set_find_paths()
    conan_include_build_modules()
    conan_set_find_library_paths()
endmacro()


macro(conan_set_find_paths)
    # CMAKE_MODULE_PATH does not have Debug/Release config, but there are variables
    # CONAN_CMAKE_MODULE_PATH_DEBUG to be used by the consumer
    # CMake can find findXXX.cmake files in the root of packages
    set(CMAKE_MODULE_PATH ${CONAN_CMAKE_MODULE_PATH} ${CMAKE_MODULE_PATH})

    # Make find_package() to work
    set(CMAKE_PREFIX_PATH ${CONAN_CMAKE_MODULE_PATH} ${CMAKE_PREFIX_PATH})

    # Set the find root path (cross build)
    set(CMAKE_FIND_ROOT_PATH ${CONAN_CMAKE_FIND_ROOT_PATH} ${CMAKE_FIND_ROOT_PATH})
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_PROGRAM)
        set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_PROGRAM})
    endif()
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_LIBRARY)
        set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_LIBRARY})
    endif()
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_INCLUDE)
        set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_INCLUDE})
    endif()
endmacro()


macro(conan_set_find_library_paths)
    # CMAKE_INCLUDE_PATH, CMAKE_LIBRARY_PATH does not have Debug/Release config, but there are variables
    # CONAN_INCLUDE_DIRS_DEBUG/RELEASE CONAN_LIB_DIRS_DEBUG/RELEASE to be used by the consumer
    # For find_library
    set(CMAKE_INCLUDE_PATH ${CONAN_INCLUDE_DIRS} ${CMAKE_INCLUDE_PATH})
    set(CMAKE_LIBRARY_PATH ${CONAN_LIB_DIRS} ${CMAKE_LIBRARY_PATH})
endmacro()


macro(conan_set_vs_runtime)
    if(CONAN_LINK_RUNTIME)
        conan_get_policy(CMP0091 policy_0091)
        if(policy_0091 STREQUAL "NEW")
            if(CONAN_LINK_RUNTIME MATCHES "MTd")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDebug")
            elseif(CONAN_LINK_RUNTIME MATCHES "MDd")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDebugDLL")
            elseif(CONAN_LINK_RUNTIME MATCHES "MT")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded")
            elseif(CONAN_LINK_RUNTIME MATCHES "MD")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDLL")
            endif()
        else()
            foreach(flag CMAKE_C_FLAGS_RELEASE CMAKE_CXX_FLAGS_RELEASE
                         CMAKE_C_FLAGS_RELWITHDEBINFO CMAKE_CXX_FLAGS_RELWITHDEBINFO
                         CMAKE_C_FLAGS_MINSIZEREL CMAKE_CXX_FLAGS_MINSIZEREL)
                if(DEFINED ${flag})
                    string(REPLACE "/MD" ${CONAN_LINK_RUNTIME} ${flag} "${${flag}}")
                endif()
            endforeach()
            foreach(flag CMAKE_C_FLAGS_DEBUG CMAKE_CXX_FLAGS_DEBUG)
                if(DEFINED ${flag})
                    string(REPLACE "/MDd" ${CONAN_LINK_RUNTIME} ${flag} "${${flag}}")
                endif()
            endforeach()
        endif()
    endif()
endmacro()


macro(conan_flags_setup)
    # Macro maintained for backwards compatibility
    conan_set_find_library_paths()
    conan_global_flags()
    conan_set_rpath()
    conan_set_vs_runtime()
    conan_set_libcxx()
endmacro()


function(conan_message MESSAGE_OUTPUT)
    if(NOT CONAN_CMAKE_SILENT_OUTPUT)
        message(${ARGV${0}})
    endif()
endfunction()


function(conan_get_policy policy_id policy)
    if(POLICY "${policy_id}")
        cmake_policy(GET "${policy_id}" _policy)
        set(${policy} "${_policy}" PARENT_SCOPE)
    else()
        set(${policy} "" PARENT_SCOPE)
    endif()
endfunction()


function(conan_find_libraries_abs_path libraries package_libdir libraries_abs_path)
    foreach(_LIBRARY_NAME ${libraries})
        find_library(CONAN_FOUND_LIBRARY NAME ${_LIBRARY_NAME} PATHS ${package_libdir}
                     NO_DEFAULT_PATH NO_CMAKE_FIND_ROOT_PATH)
        if(CONAN_FOUND_LIBRARY)
            conan_message(STATUS "Library ${_LIBRARY_NAME} found ${CONAN_FOUND_LIBRARY}")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${CONAN_FOUND_LIBRARY})
        else()
            conan_message(STATUS "Library ${_LIBRARY_NAME} not found in package, might be system one")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIBRARY_NAME})
        endif()
        unset(CONAN_FOUND_LIBRARY CACHE)
    endforeach()
    set(${libraries_abs_path} ${CONAN_FULLPATH_LIBS} PARENT_SCOPE)
endfunction()


function(conan_package_library_targets libraries package_libdir libraries_abs_path deps build_type package_name)
    unset(_CONAN_ACTUAL_TARGETS CACHE)
    unset(_CONAN_FOUND_SYSTEM_LIBS CACHE)
    foreach(_LIBRARY_NAME ${libraries})
        find_library(CONAN_FOUND_LIBRARY NAME ${_LIBRARY_NAME} PATHS ${package_libdir}
                     NO_DEFAULT_PATH NO_CMAKE_FIND_ROOT_PATH)
        if(CONAN_FOUND_LIBRARY)
            conan_message(STATUS "Library ${_LIBRARY_NAME} found ${CONAN_FOUND_LIBRARY}")
            set(_LIB_NAME CONAN_LIB::${package_name}_${_LIBRARY_NAME}${build_type})
            add_library(${_LIB_NAME} UNKNOWN IMPORTED)
            set_target_properties(${_LIB_NAME} PROPERTIES IMPORTED_LOCATION ${CONAN_FOUND_LIBRARY})
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIB_NAME})
            set(_CONAN_ACTUAL_TARGETS ${_CONAN_ACTUAL_TARGETS} ${_LIB_NAME})
        else()
            conan_message(STATUS "Library ${_LIBRARY_NAME} not found in package, might be system one")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIBRARY_NAME})
            set(_CONAN_FOUND_SYSTEM_LIBS "${_CONAN_FOUND_SYSTEM_LIBS};${_LIBRARY_NAME}")
        endif()
        unset(CONAN_FOUND_LIBRARY CACHE)
    endforeach()

    # Add all dependencies to all targets
    string(REPLACE " " ";" deps_list "${deps}")
    foreach(_CONAN_ACTUAL_TARGET ${_CONAN_ACTUAL_TARGETS})
        set_property(TARGET ${_CONAN_ACTUAL_TARGET} PROPERTY INTERFACE_LINK_LIBRARIES "${_CONAN_FOUND_SYSTEM_LIBS};${deps_list}")
    endforeach()

    set(${libraries_abs_path} ${CONAN_FULLPATH_LIBS} PARENT_SCOPE)
endfunction()


macro(conan_set_libcxx)
    if(DEFINED CONAN_LIBCXX)
        conan_message(STATUS "Conan: C++ stdlib: ${CONAN_LIBCXX}")
        if(CONAN_COMPILER STREQUAL "clang" OR CONAN_COMPILER STREQUAL "apple-clang")
            if(CONAN_LIBCXX STREQUAL "libstdc++" OR CONAN_LIBCXX STREQUAL "libstdc++11" )
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libstdc++")
            elseif(CONAN_LIBCXX STREQUAL "libc++")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libc++")
            endif()
        endif()
        if(CONAN_COMPILER STREQUAL "sun-cc")
            if(CONAN_LIBCXX STREQUAL "libCstd")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=Cstd")
            elseif(CONAN_LIBCXX STREQUAL "libstdcxx")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stdcxx4")
            elseif(CONAN_LIBCXX STREQUAL "libstlport")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stlport4")
            elseif(CONAN_LIBCXX STREQUAL "libstdc++")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stdcpp")
            endif()
        endif()
        if(CONAN_LIBCXX STREQUAL "libstdc++11")
            add_definitions(-D_GLIBCXX_USE_CXX11_ABI=1)
        elseif(CONAN_LIBCXX STREQUAL "libstdc++")
            add_definitions(-D_GLIBCXX_USE_CXX11_ABI=0)
        endif()
    endif()
endmacro()


macro(conan_set_std)
    conan_message(STATUS "Conan: Adjusting language standard")
    # Do not warn "Manually-specified variables were not used by the project"
    set(ignorevar "${CONAN_STD_CXX_FLAG}${CONAN_CMAKE_CXX_STANDARD}${CONAN_CMAKE_CXX_EXTENSIONS}")
    if (CMAKE_VERSION VERSION_LESS "3.1" OR
        (CMAKE_VERSION VERSION_LESS "3.12" AND ("${CONAN_CMAKE_CXX_STANDARD}" STREQUAL "20" OR "${CONAN_CMAKE_CXX_STANDARD}" STREQUAL "gnu20")))
        if(CONAN_STD_CXX_FLAG)
            conan_message(STATUS "Conan setting CXX_FLAGS flags: ${CONAN_STD_CXX_FLAG}")
            set(CMAKE_CXX_FLAGS "${CONAN_STD_CXX_FLAG} ${CMAKE_CXX_FLAGS}")
        endif()
    else()
        if(CONAN_CMAKE_CXX_STANDARD)
            conan_message(STATUS "Conan setting CPP STANDARD: ${CONAN_CMAKE_CXX_STANDARD} WITH EXTENSIONS ${CONAN_CMAKE_CXX_EXTENSIONS}")
            set(CMAKE_CXX_STANDARD ${CONAN_CMAKE_CXX_STANDARD})
            set(CMAKE_CXX_EXTENSIONS ${CONAN_CMAKE_CXX_EXTENSIONS})
        endif()
    endif()
endmacro()


macro(conan_set_rpath)
    conan_message(STATUS "Conan: Adjusting default RPATHs Conan policies")
    if(APPLE)
        # https://cmake.org/Wiki/CMake_RPATH_handling
        # CONAN GUIDE: All generated libraries should have the id and dependencies to other
        # dylibs without path, just the name, EX:
        # libMyLib1.dylib:
        #     libMyLib1.dylib (compatibility version 0.0.0, current version 0.0.0)
        #     libMyLib0.dylib (compatibility version 0.0.0, current version 0.0.0)
        #     /usr/lib/libc++.1.dylib (compatibility version 1.0.0, current version 120.0.0)
        #     /usr/lib/libSystem.B.dylib (compatibility version 1.0.0, current version 1197.1.1)
        # AVOID RPATH FOR *.dylib, ALL LIBS BETWEEN THEM AND THE EXE
        # SHOULD BE ON THE LINKER RESOLVER PATH (./ IS ONE OF THEM)
        set(CMAKE_SKIP_RPATH 1 CACHE BOOL "rpaths" FORCE)
        # Policy CMP0068
        # We want the old behavior, in CMake >= 3.9 CMAKE_SKIP_RPATH won't affect the install_name in OSX
        set(CMAKE_INSTALL_NAME_DIR "")
    endif()
endmacro()


macro(conan_set_fpic)
    if(DEFINED CONAN_CMAKE_POSITION_INDEPENDENT_CODE)
        conan_message(STATUS "Conan: Adjusting fPIC flag (${CONAN_CMAKE_POSITION_INDEPENDENT_CODE})")
        set(CMAKE_POSITION_INDEPENDENT_CODE ${CONAN_CMAKE_POSITION_INDEPENDENT_CODE})
    endif()
endmacro()


macro(conan_output_dirs_setup)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/bin)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})

    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/lib)
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELEASE ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_DEBUG ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})

    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/lib)
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
endmacro()


macro(conan_split_version VERSION_STRING MAJOR MINOR)
    #make a list from the version string
    string(REPLACE "." ";" VERSION_LIST "${VERSION_STRING}")

    #write output values
    list(LENGTH VERSION_LIST _version_len)
    list(GET VERSION_LIST 0 ${MAJOR})
    if(${_version_len} GREATER 1)
        list(GET VERSION_LIST 1 ${MINOR})
    endif()
endmacro()


macro(conan_error_compiler_version)
    message(FATAL_ERROR "Detected a mismatch for the compiler version between your conan profile settings and CMake: \n"
                        "Compiler version specified in your conan profile: ${CONAN_COMPILER_VERSION}\n"
                        "Compiler version detected in CMake: ${VERSION_MAJOR}.${VERSION_MINOR}\n"
                        "Please check your conan profile settings (conan profile show [default|your_profile_name])\n"
                        "P.S. You may set CONAN_DISABLE_CHECK_COMPILER CMake variable in order to disable this check."
           )
endmacro()

set(_CONAN_CURRENT_DIR ${CMAKE_CURRENT_LIST_DIR})

function(conan_get_compiler CONAN_INFO_COMPILER CONAN_INFO_COMPILER_VERSION)
    conan_message(STATUS "Current conanbuildinfo.cmake directory: " ${_CONAN_CURRENT_DIR})
    if(NOT EXISTS ${_CONAN_CURRENT_DIR}/conaninfo.txt)
        conan_message(STATUS "WARN: conaninfo.txt not found")
        return()
    endif()

    file (READ "${_CONAN_CURRENT_DIR}/conaninfo.txt" CONANINFO)

    # MATCHALL will match all, including the last one, which is the full_settings one
    string(REGEX MATCH "full_settings.*" _FULL_SETTINGS_MATCHED ${CONANINFO})
    string(REGEX MATCH "compiler=([-A-Za-z0-9_ ]+)" _MATCHED ${_FULL_SETTINGS_MATCHED})
    if(DEFINED CMAKE_MATCH_1)
        string(STRIP "${CMAKE_MATCH_1}" _CONAN_INFO_COMPILER)
        set(${CONAN_INFO_COMPILER} ${_CONAN_INFO_COMPILER} PARENT_SCOPE)
    endif()

    string(REGEX MATCH "compiler.version=([-A-Za-z0-9_.]+)" _MATCHED ${_FULL_SETTINGS_MATCHED})
    if(DEFINED CMAKE_MATCH_1)
        string(STRIP "${CMAKE_MATCH_1}" _CONAN_INFO_COMPILER_VERSION)
        set(${CONAN_INFO_COMPILER_VERSION} ${_CONAN_INFO_COMPILER_VERSION} PARENT_SCOPE)
    endif()
endfunction()


function(check_compiler_version)
    conan_split_version(${CMAKE_CXX_COMPILER_VERSION} VERSION_MAJOR VERSION_MINOR)
    if(DEFINED CONAN_SETTINGS_COMPILER_TOOLSET)
       conan_message(STATUS "Conan: Skipping compiler check: Declared 'compiler.toolset'")
       return()
    endif()
    if(CMAKE_CXX_COMPILER_ID MATCHES MSVC)
        # MSVC_VERSION is defined since 2.8.2 at least
        # https://cmake.org/cmake/help/v2.8.2/cmake.html#variable:MSVC_VERSION
        # https://cmake.org/cmake/help/v3.14/variable/MSVC_VERSION.html
        if(
            # 1930 = VS 17.0 (v143 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "17" AND NOT MSVC_VERSION EQUAL 1930) OR
            # 1920-1929 = VS 16.0 (v142 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "16" AND NOT((MSVC_VERSION GREATER 1919) AND (MSVC_VERSION LESS 1930))) OR
            # 1910-1919 = VS 15.0 (v141 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "15" AND NOT((MSVC_VERSION GREATER 1909) AND (MSVC_VERSION LESS 1920))) OR
            # 1900      = VS 14.0 (v140 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "14" AND NOT(MSVC_VERSION EQUAL 1900)) OR
            # 1800      = VS 12.0 (v120 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "12" AND NOT VERSION_MAJOR STREQUAL "18") OR
            # 1700      = VS 11.0 (v110 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "11" AND NOT VERSION_MAJOR STREQUAL "17") OR
            # 1600      = VS 10.0 (v100 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "10" AND NOT VERSION_MAJOR STREQUAL "16") OR
            # 1500      = VS  9.0 (v90 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "9" AND NOT VERSION_MAJOR STREQUAL "15") OR
            # 1400      = VS  8.0 (v80 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "8" AND NOT VERSION_MAJOR STREQUAL "14") OR
            # 1310      = VS  7.1, 1300      = VS  7.0
            (CONAN_COMPILER_VERSION STREQUAL "7" AND NOT VERSION_MAJOR STREQUAL "13") OR
            # 1200      = VS  6.0
            (CONAN_COMPILER_VERSION STREQUAL "6" AND NOT VERSION_MAJOR STREQUAL "12") )
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "gcc")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        set(_CHECK_VERSION ${VERSION_MAJOR}.${VERSION_MINOR})
        set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 5.0)
            conan_message(STATUS "Conan: Compiler GCC>=5, checking major version ${CONAN_COMPILER_VERSION}")
            conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
            if("${CONAN_COMPILER_MINOR}" STREQUAL "")
                set(_CHECK_VERSION ${VERSION_MAJOR})
                set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR})
            endif()
        endif()
        conan_message(STATUS "Conan: Checking correct version: ${_CHECK_VERSION}")
        if(NOT ${_CHECK_VERSION} VERSION_EQUAL ${_CONAN_VERSION})
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "clang")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        set(_CHECK_VERSION ${VERSION_MAJOR}.${VERSION_MINOR})
        set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 8.0)
            conan_message(STATUS "Conan: Compiler Clang>=8, checking major version ${CONAN_COMPILER_VERSION}")
            if("${CONAN_COMPILER_MINOR}" STREQUAL "")
                set(_CHECK_VERSION ${VERSION_MAJOR})
                set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR})
            endif()
        endif()
        conan_message(STATUS "Conan: Checking correct version: ${_CHECK_VERSION}")
        if(NOT ${_CHECK_VERSION} VERSION_EQUAL ${_CONAN_VERSION})
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "apple-clang" OR CONAN_COMPILER STREQUAL "sun-cc" OR CONAN_COMPILER STREQUAL "mcst-lcc")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        if(NOT ${VERSION_MAJOR}.${VERSION_MINOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
           conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "intel")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 19.1)
            if(NOT ${VERSION_MAJOR}.${VERSION_MINOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
               conan_error_compiler_version()
            endif()
        else()
            if(NOT ${VERSION_MAJOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR})
               conan_error_compiler_version()
            endif()
        endif()
    else()
        conan_message(STATUS "WARN: Unknown compiler '${CONAN_COMPILER}', skipping the version check...")
    endif()
endfunction()


function(conan_check_compiler)
    if(CONAN_DISABLE_CHECK_COMPILER)
        conan_message(STATUS "WARN: Disabled conan compiler checks")
        return()
    endif()
    if(NOT DEFINED CMAKE_CXX_COMPILER_ID)
        if(DEFINED CMAKE_C_COMPILER_ID)
            conan_message(STATUS "This project seems to be plain C, using '${CMAKE_C_COMPILER_ID}' compiler")
            set(CMAKE_CXX_COMPILER_ID ${CMAKE_C_COMPILER_ID})
            set(CMAKE_CXX_COMPILER_VERSION ${CMAKE_C_COMPILER_VERSION})
        else()
            message(FATAL_ERROR "This project seems to be plain C, but no compiler defined")
        endif()
    endif()
    if(NOT CMAKE_CXX_COMPILER_ID AND NOT CMAKE_C_COMPILER_ID)
        # This use case happens when compiler is not identified by CMake, but the compilers are there and work
        conan_message(STATUS "*** WARN: CMake was not able to identify a C or C++ compiler ***")
        conan_message(STATUS "*** WARN: Disabling compiler checks. Please make sure your settings match your environment ***")
        return()
    endif()
    if(NOT DEFINED CONAN_COMPILER)
        conan_get_compiler(CONAN_COMPILER CONAN_COMPILER_VERSION)
        if(NOT DEFINED CONAN_COMPILER)
            conan_message(STATUS "WARN: CONAN_COMPILER variable not set, please make sure yourself that "
                          "your compiler and version matches your declared settings")
            return()
        endif()
    endif()

    if(NOT CMAKE_HOST_SYSTEM_NAME STREQUAL ${CMAKE_SYSTEM_NAME})
        set(CROSS_BUILDING 1)
    endif()

    # If using VS, verify toolset
    if (CONAN_COMPILER STREQUAL "Visual Studio")
        if (CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "LLVM" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "llvm" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "clang" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "Clang")
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "Clang")
        elseif (CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "Intel")
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "Intel")
        else()
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "MSVC")
        endif()

        if (NOT CMAKE_CXX_COMPILER_ID MATCHES ${EXPECTED_CMAKE_CXX_COMPILER_ID})
            message(FATAL_ERROR "Incorrect '${CONAN_COMPILER}'. Toolset specifies compiler as '${EXPECTED_CMAKE_CXX_COMPILER_ID}' "
                                "but CMake detected '${CMAKE_CXX_COMPILER_ID}'")
        endif()

    # Avoid checks when cross compiling, apple-clang crashes because its APPLE but not apple-clang
    # Actually CMake is detecting "clang" when you are using apple-clang, only if CMP0025 is set to NEW will detect apple-clang
    elseif((CONAN_COMPILER STREQUAL "gcc" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "GNU") OR
        (CONAN_COMPILER STREQUAL "apple-clang" AND NOT CROSS_BUILDING AND (NOT APPLE OR NOT CMAKE_CXX_COMPILER_ID MATCHES "Clang")) OR
        (CONAN_COMPILER STREQUAL "clang" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "Clang") OR
        (CONAN_COMPILER STREQUAL "sun-cc" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "SunPro") )
        message(FATAL_ERROR "Incorrect '${CONAN_COMPILER}', is not the one detected by CMake: '${CMAKE_CXX_COMPILER_ID}'")
    endif()


    if(NOT DEFINED CONAN_COMPILER_VERSION)
        conan_message(STATUS "WARN: CONAN_COMPILER_VERSION variable not set, please make sure yourself "
                             "that your compiler version matches your declared settings")
        return()
    endif()
    check_compiler_version()
endfunction()


macro(conan_set_flags build_type)
    set(CMAKE_CXX_FLAGS${build_type} "${CMAKE_CXX_FLAGS${build_type}} ${CONAN_CXX_FLAGS${build_type}}")
    set(CMAKE_C_FLAGS${build_type} "${CMAKE_C_FLAGS${build_type}} ${CONAN_C_FLAGS${build_type}}")
    set(CMAKE_SHARED_LINKER_FLAGS${build_type} "${CMAKE_SHARED_LINKER_FLAGS${build_type}} ${CONAN_SHARED_LINKER_FLAGS${build_type}}")
    set(CMAKE_EXE_LINKER_FLAGS${build_type} "${CMAKE_EXE_LINKER_FLAGS${build_type}} ${CONAN_EXE_LINKER_FLAGS${build_type}}")
endmacro()


macro(conan_global_flags)
    if(CONAN_SYSTEM_INCLUDES)
        include_directories(SYSTEM ${CONAN_INCLUDE_DIRS}
                                   "$<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_RELEASE}>"
                                   "$<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_RELWITHDEBINFO}>"
                                   "$<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_MINSIZEREL}>"
                                   "$<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DEBUG}>")
    else()
        include_directories(${CONAN_INCLUDE_DIRS}
                            "$<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_RELEASE}>"
                            "$<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_RELWITHDEBINFO}>"
                            "$<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_MINSIZEREL}>"
                            "$<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DEBUG}>")
    endif()

    link_directories(${CONAN_LIB_DIRS})

    conan_find_libraries_abs_path("${CONAN_LIBS_DEBUG}" "${CONAN_LIB_DIRS_DEBUG}"
                                  CONAN_LIBS_DEBUG)
    conan_find_libraries_abs_path("${CONAN_LIBS_RELEASE}" "${CONAN_LIB_DIRS_RELEASE}"
                                  CONAN_LIBS_RELEASE)
    conan_find_libraries_abs_path("${CONAN_LIBS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_RELWITHDEBINFO}"
                                  CONAN_LIBS_RELWITHDEBINFO)
    conan_find_libraries_abs_path("${CONAN_LIBS_MINSIZEREL}" "${CONAN_LIB_DIRS_MINSIZEREL}"
                                  CONAN_LIBS_MINSIZEREL)

    add_compile_options(${CONAN_DEFINES}
                        "$<$<CONFIG:Debug>:${CONAN_DEFINES_DEBUG}>"
                        "$<$<CONFIG:Release>:${CONAN_DEFINES_RELEASE}>"
                        "$<$<CONFIG:RelWithDebInfo>:${CONAN_DEFINES_RELWITHDEBINFO}>"
                        "$<$<CONFIG:MinSizeRel>:${CONAN_DEFINES_MINSIZEREL}>")

    conan_set_flags("")
    conan_set_flags("_RELEASE")
    conan_set_flags("_DEBUG")

endmacro()


macro(conan_target_link_libraries target)
    if(CONAN_TARGETS)
        target_link_libraries(${target} ${CONAN_TARGETS})
    else()
        target_link_libraries(${target} ${CONAN_LIBS})
        foreach(_LIB ${CONAN_LIBS_RELEASE})
            target_link_libraries(${target} optimized ${_LIB})
        endforeach()
        foreach(_LIB ${CONAN_LIBS_DEBUG})
            target_link_libraries(${target} debug ${_LIB})
        endforeach()
    endif()
endmacro()


macro(conan_include_build_modules)
    if(CMAKE_BUILD_TYPE)
        if(${CMAKE_BUILD_TYPE} MATCHES "Debug")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_DEBUG} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "Release")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_RELEASE} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "RelWithDebInfo")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_RELWITHDEBINFO} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "MinSizeRel")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_MINSIZEREL} ${CONAN_BUILD_MODULES_PATHS})
        endif()
    endif()

    foreach(_BUILD_MODULE_PATH ${CONAN_BUILD_MODULES_PATHS})
        include(${_BUILD_MODULE_PATH})
    endforeach()
endmacro()


### Definition of user declared vars (user_info) ###

set(CONAN_USER_PLATFORM.HASHING_suggested_flags "-march=haswell")