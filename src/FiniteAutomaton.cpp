#include "FiniteAutomaton.h"

namespace Automata{
	AcceptingStates* FiniteAutomaton::getAcceptingStates() {
		return _f;
	}

	FiniteAutomaton::FiniteAutomaton(States *q, Alphabet *a, TransitionFunction *d, State *q0, AcceptingStates *f) {
		_states = q;
		_alphabet = a;
		_tf = d;
		_q0 = q0;
		_f = f;

		if(_states->empty())
			throw length_error("Set of states cannot be empty!");
		if(!_states->contains(q0))
			throw invalid_argument("The initial state must be a part of the set of states!");
		for(auto i = _f->begin(); i != _f->end(); i++)
			if(!_states->contains(*i))
				throw invalid_argument("The accepting state " + (*i)->getName() + " is not in the set of states!");
		for(int i = 0; i < _tf->size(); i++){
			Transition* t = (*_tf)[i];
			if(t->getSymbol() == EMPTY_STRING)
				throw invalid_argument("Null transitions are not allowed!");
			if(!_states->contains(t->getCurrent()) || !_states->contains(t->getNext()))
				throw invalid_argument("A transition contains a state that is not in the set of states!");
			if(!_alphabet->contains(t->getSymbol()))
				throw invalid_argument("Invalid symbol '" + to_string(t->getSymbol()) + "' encountered.");
		}
	}

	bool FiniteAutomaton::run(char x[], size_t size) {
		State* current = _q0;

		for(int i = 0; i < size; i++){
			if(_alphabet->contains(x[i]))
				return false;

			if(x[i] == EMPTY_STRING)
				continue;

			State* q = _tf->getNext(current, x[i]);
			if(q == nullptr)
				return false;
			current = q;
		}

		return current->isAccepting();
	}

	bool FiniteAutomaton::run(string x){
		char* input = &x[0];
		return run(input, x.size());
	}
}