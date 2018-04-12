cmake_minimum_required(VERSION 3.10)
project(Automata)

set(CMAKE_CXX_STANDARD 11)

include_directories(src)

add_executable(Automata
        src/AcceptingStates.cpp
        src/AcceptingStates.h
        src/Alphabet.cpp
        src/Alphabet.h
        src/Automaton.h
        src/FiniteAutomaton.cpp
        src/FiniteAutomaton.h
        src/State.cpp
        src/State.h
        src/States.cpp
        src/States.h
        src/Transition.cpp
        src/Transition.h
        src/TransitionFunction.cpp
        src/TransitionFunction.h)
