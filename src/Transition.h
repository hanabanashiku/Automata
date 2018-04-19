//
#ifndef AUTOMATA_TRANSITION_H
#define AUTOMATA_TRANSITION_H

#include "State.h"
#include "Alphabet.h"
#include <sstream>

namespace Automata{
	class Transition{
	 public:
	  State* getCurrent();
	  State* getNext();
	  char getSymbol();
	  Transition(State* p, State* q, char s);
	  explicit operator string();
	  bool operator ==(Transition* a);

	 private:
	  State* _p;
	  State* _q;
	  char _a;
	};
}

#endif //AUTOMATA_TRANSITION_H
