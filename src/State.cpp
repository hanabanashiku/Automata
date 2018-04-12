#include "State.h"

namespace Automata{
    string State::getName() {
        return _name;
    }

    bool State::isAccepting() {
        return accepting;
    }

    void State::setAccepting (bool value) {
        accepting = value;
    }

    State::State(const string& name) {
        _name = name;
    }

    State::State(const int& id){
        _name = "q" + to_string(id);
    }

    bool State::operator==(State a, State b){
        return a.getName() == b.getName();
    }

    bool State::operator==(Automata::State *a, Automata::State *b) {
        return a == b || a->getName() == b->getName();
    }
}