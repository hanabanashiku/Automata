#include "Alphabet.h"

namespace Automata{

	bool Alphabet::contains(char s){
		for(auto i : _symbols)
			if(i == s) return true;
		return false;
	}

	void Alphabet::add(char s){
		if(!contains(s))
			_symbols.push_back(s);
	}

	size_t Alphabet::size(){
		return _symbols.size();
	}

	bool Alphabet::empty(){
		return _symbols.empty();
	}

	explicit Alphabet::Alphabet(char* s[]) {
		for(int i = 0; i < sizeof(s); i++)
			if(!contains(*s[i]) && s != EMPTY_STRING)
				_symbols.push_back(*s[i]);
	}

	Alphabet::operator string(){
		if(empty()) return "{ }";
		string ret = "{ " + to_string(_symbols[0]);
		for(int i = 1; i < _symbols.size(); i++)
			ret += ", " + to_string(_symbols[i]);
		return ret + " }";
	}

	__wrap_iter<vector<char, std::__1::allocator<char >>::pointer> Alphabet::begin() {
		return _symbols.begin();
	}

	__wrap_iter<vector<char, std::__1::allocator<char >>::pointer> Alphabet::end() {
		return _symbols.end();
	}

	Alphabet Alphabet::operator+(Automata::Alphabet *a, Automata::Alphabet *b) {
		Alphabet ret;

		for(auto s : *a)
			ret.add(s);

		for(auto s : *b)
			if(!ret.contains(s))
				ret.add(s);
		return ret;
	}

	Alphabet Alphabet::operator-(Automata::Alphabet *a, Automata::Alphabet *b) {
		Alphabet ret;

		for(auto s : *a)
			if(!b->contains(s))
				ret.add(s);
		return ret;
	}
}
