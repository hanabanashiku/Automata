#include "TransitionFunction.h"

namespace Automata{
	bool TransitionFunction::contains(Transition* t){
		for(auto i : _trans)
			if(i == t) return true;
		return false;
	}

	void TransitionFunction::add(Transition* t) {
		if(!contains(t))
			_trans.push_back(t);
	}

	State* TransitionFunction::getNext(State* p, char s){
		for(auto i : _trans)
			if(i->getCurrent() == p && i->getSymbol() == s)
				return i->getNext();
		return nullptr;
	}

	size_t TransitionFunction::size(){
		return _trans.size();
	}

	bool TransitionFunction::empty(){
		return _trans.empty();
	}

	TransitionFunction::TransitionFunction(Transition* t[], size_t size) {
		for(int i = 0; i < size; i++)
			if(!contains(t[i]))
				_trans.push_back(t[i]);
	}

	Transition* TransitionFunction::operator[](int i) {
		if(i < 0 || i >= _trans.size())
			throw out_of_range(to_string(i));
		return _trans.at(i);
	}

	__wrap_iter<vector<Transition*, std::__1::allocator<Transition* >>::pointer> TransitionFunction::begin() {
		return _trans.begin();
	}

	__wrap_iter<vector<Transition*, std::__1::allocator<Transition* >>::pointer> TransitionFunction::end() {
		return _trans.end();
	}
}