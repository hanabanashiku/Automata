#ifndef AUTOMATA_STATE_H
#define AUTOMATA_STATE_H

#include<string>

using namespace std;

namespace Automata{
    class State{
      friend class AcceptingStates;
        public:
            string getName();
            bool isAccepting();
            explicit State(const string& name);
            explicit State(const int& n);
            bool operator ==(State a);
            bool operator ==(State* a);

     private:
        string _name;
        bool accepting;
        void setAccepting(bool value);
    };

    static const State* HA = new State("Ha");
    static const State* HR = new State("Hr");
}

#endif //AUTOMATA_STATE_H
