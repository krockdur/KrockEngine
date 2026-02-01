cmake_minimum_required(VERSION 3.28)
project(kengine LANGUAGES CXX)

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

include(FetchContent)
FetchContent_Declare(SFML
    GIT_REPOSITORY https://github.com/SFML/SFML.git
    GIT_TAG 3.0.2
    GIT_SHALLOW ON
    EXCLUDE_FROM_ALL
    SYSTEM)
FetchContent_MakeAvailable(SFML)

add_executable(kengine src/sources/main.cpp
        src/sources/Atlas.cpp
        src/headers/Atlas.h
        src/sources/Map.cpp
        src/headers/Map.h
        src/sources/Sprite.cpp
        src/headers/Sprite.h
        src/sources/Tile.cpp
        src/headers/Tile.h
        src/sources/Game.cpp
        src/headers/Game.h)
#target_include_directories(kengine PRIVATE include/)
target_compile_features(kengine PRIVATE cxx_std_17)
target_link_libraries(kengine PRIVATE SFML::Graphics)

set_property(DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR} PROPERTY VS_STARTUP_PROJECT kengine)