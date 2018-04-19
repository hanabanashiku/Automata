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

	bool Transition::operator==(Transition* a) {
		if(a == this) return true;
		return a->getCurrent() == this->getCurrent()
		       && a->getNext() == this->getNext()
		       && a->getSymbol() == this->getSymbol();
	}
}
