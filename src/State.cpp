#include "State.h"

namespace Automata{
    string State::getName() {
        return _name;
    }

    State::State(const string& name) {
        _name = name;
    }

    State::State(const int& id){
        _name = "q" + to_string(id);
    }

    bool State::operator==(Automata::State *a, Automata::State *b) {
        return a->getName() == b->getName();
    }
}