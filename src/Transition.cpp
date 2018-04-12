#include "Transition.h"

namespace Automata {
	State* Transition::getCurrent() {
		return _p;
	}

	State* Transition::getNext() {
		return _q;
	}

	char Transition::getSymbol() {
		return _a;
	}

	Transition::Transition(State *p, State *q, char s) {
		_p = p;
		_q = q;
		_a = s;
	}

	Transition::operator string(){
		stringstream ret;
		ret << "δ(" << _p << ", ";
		if(_a == EMPTY_STRING) ret << "ε";
		else ret << _a;
		ret << ") = " << _q;
		return ret.str();
	}

	bool Transition::operator==(Automata::Transition *a, Automata::Transition *b) {
		if(a == b) return true;
		return a->getCurrent() == b->getCurrent()
		       && a->getNext() == b->getNext()
		       && a->getSymbol() == b->getSymbol();
	}
}
