#ifndef AUTOMATA_STATE_H
#define AUTOMATA_STATE_H

#include<string>

using namespace std;

namespace Automata{
    class State{
      friend class AcceptingStates;
        public:
            static const State* HA = new State("Ha");
            static const State* HR = new State("Hr");

            string getName();
            bool isAccepting();
            explicit State(const string& name);
            explicit State(const int& n);
            bool operator ==(State a, State b);
            bool operator ==(State* a, State* b);

     private:
        string _name;
        bool accepting;
        void setAccepting(bool value);
    };
}

#endif //AUTOMATA_STATE_H
