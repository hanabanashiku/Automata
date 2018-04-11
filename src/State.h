#ifndef AUTOMATA_STATE_H
#define AUTOMATA_STATE_H

#include<string>

using namespace std;

namespace Automata{
    class State{
        public:
            string getName();
            State(const string& name);
            State(const int& n);
            bool operator ==(State* a, State* b);
            
        string _name;
    };
}


#endif //AUTOMATA_STATE_H
