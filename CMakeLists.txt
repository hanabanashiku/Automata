cmake_minimum_required(VERSION 3.10)
project(Automata)

set(CMAKE_CXX_STANDARD 11)

include_directories(src)

add_executable(Automata
        src/State.cpp
        src/State.h src/States.cpp src/States.h)
