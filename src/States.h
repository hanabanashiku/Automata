//
// Created by maclm01 on 11/04/18.
//

#ifndef AUTOMATA_STATES_H
#define AUTOMATA_STATES_H

#include<string>
#include<vector>
#include<stdexcept>

#include "State.h"

using namespace std;

namespace Automata{
    class States{
    public:
        void add(State *s);
        bool contains(const State *s);
        State* operator [](const int& i);
        explicit States(State* states[]);
        explicit States(const int& n);

    private:
        vector<State*> _states;
    };
}

#endif //AUTOMATA_STATES_H
