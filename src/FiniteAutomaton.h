#ifndef AUTOMATA_FINITEAUTOMATON_H
#define AUTOMATA_FINITEAUTOMATON_H

#include<stdexcept>
#include<string>

#include "Automaton.h"
#include "Transition.h"
#include "TransitionFunction.h"
#include "State.h"
#include "Alphabet.h"
#include "AcceptingStates.h"

namespace Automata{
	class FiniteAutomaton : Automaton{
	 public:
	  AcceptingStates* getAcceptingStates();
	  FiniteAutomaton(States* q, Alphabet* a, TransitionFunction* d, State* q0, AcceptingStates* f);
	  bool run(char x[]);
	  bool run(string x);

	 protected:
	  AcceptingStates* _f;
	};
}

#endif //AUTOMATA_FINITEAUTOMATON_H
