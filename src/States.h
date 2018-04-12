#ifndef AUTOMATA_STATES_H
#define AUTOMATA_STATES_H

#include<vector>
#include<stdexcept>
#include<iterator>

#include "State.h"

using namespace std;

namespace Automata{
    class States{
    public:
        void add(State *s);
        bool contains(const State *s);
        size_t size();
        bool empty();
        State* operator [](const int& i);
        States() = default;
        explicit States(State* states[]);
        explicit States(const int& n);
        __wrap_iter<vector<Automata::State *, std::__1::allocator<Automata::State *>>::pointer> begin();
        __wrap_iter<vector<Automata::State *, std::__1::allocator<Automata::State *>>::pointer> end();
      explicit operator string();

    protected:
        vector<State*> _states;
    };
}

#endif //AUTOMATA_STATES_H
