#ifndef AUTOMATA_AUTOMATON_H
#define AUTOMATA_AUTOMATON_H

#include<string>

#include "State.h"
#include "States.h"
#include "Alphabet.h"
#include "TransitionFunction.h

namespace Automata{
	class Automaton{
	 public:
	  States* getStates() { return _states; };
	  State* getInitialState() { return _q0; }
	  Alphabet* getAlphabet() { return _alphabet; };
	  TransitionFunction* getTransitions() { return _tf; };
	  virtual bool run(char x[]);
	  virtual bool run(string x);

	 protected:
	  States* _states;
	  State* _q0;
	  Alphabet* _alphabet;
	  TransitionFunction* _tf;
	};
}

#endif //AUTOMATA_AUTOMATON_H
