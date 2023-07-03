cmake_minimum_required(VERSION 3.15)
project(Platform.Data)
set(CMAKE_CXX_STANDARD 20)
set(LINKS_PLATFORM_TESTS OFF CACHE BOOL "Whether to compile tests")
set(LINKS_PLATFORM_EXTRA_FLAGS "" CACHE STRING "Extra compiler flags")

set(CONAN_DISABLE_CHECK_COMPILER TRUE)
set(LINKS_PLATFORM_TESTS TRUE)

find_package(GTest)
find_package(Platform.Interfaces)
find_package(Platform.Setters)
find_package(Platform.Ranges)

add_library(${PROJECT_NAME}.Library INTERFACE)
target_include_directories(${PROJECT_NAME}.Library INTERFACE ${PROJECT_NAME})

target_link_libraries(${PROJECT_NAME}.Library INTERFACE Platform.Interfaces::Platform.Interfaces)
target_link_libraries(${PROJECT_NAME}.Library INTERFACE Platform.Ranges::Platform.Ranges)
target_link_libraries(${PROJECT_NAME}.Library INTERFACE Platform.Setters::Platform.Setters)

if(${LINKS_PLATFORM_TESTS})
    add_executable(${PROJECT_NAME}.Tests ${PROJECT_NAME}.Tests/AllTests.cpp)
    target_link_libraries(${PROJECT_NAME}.Tests PRIVATE ${PROJECT_NAME}.Library)
    target_link_libraries(${PROJECT_NAME}.Tests PRIVATE GTest::gtest)
    target_link_libraries(${PROJECT_NAME}.Tests PRIVATE GTest::gtest_main)
    set_target_properties(${PROJECT_NAME}.Tests PROPERTIES CXX_STANDARD 20)
endif()
