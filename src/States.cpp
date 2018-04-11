#include "States.h"

namespace Automata{
    bool States::contains(const State *s) {
        for(auto const& i : _states){
            if(i == s) return true;
            if(*i == *s) return true;
        }
        return false;
    }

    void States::add(State* s) {
        if(!contains(s))
            _states.push_back(s);
    }

    State* States::operator [](const int& i){
        if(_states.size() >= i || i < 0)
            throw out_of_range(to_string(i));
        return _states.at(i);
    }

    States::States(const int &n) {
        for(int i = 0; i < n; i++){
            State s(n);
            _states.push_back(&s);
        }
    }

    States::States(State *states[]) {
        for(size_t i = 0; i < sizeof(states); i++)
            if(!contains(states[i]))
                _states.push_back(states[i]);
    }
}