#ifndef AUTOMATA_TRANSITIONFUNCTION_H
#define AUTOMATA_TRANSITIONFUNCTION_H

#include<vector>
#include<stdexcept>
#include<iterator>

#include "Transition.h"
#include "State.h"

namespace Automata{
	class TransitionFunction{
	 public:
	  bool contains(Transition* t);
	  void add(Transition* t);
	  State* getNext(State* p, char s);
	  size_t size();
	  bool empty();
	  explicit TransitionFunction(Transition* t[]);
	  explicit Transition* operator[](int i);
	  TransitionFunction() = default;
	  __wrap_iter<vector<Transition*, std::__1::allocator<Transition* >>::pointer> begin();
	  __wrap_iter<vector<Transition*, std::__1::allocator<Transition* >>::pointer> end();

	 protected:
	  vector<Transition*> _trans;
	};
}

#endif //AUTOMATA_TRANSITIONFUNCTION_H
