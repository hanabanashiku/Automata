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

	Alphabet::Alphabet(char s[], size_t size) {
		for(int i = 0; i < size; i++)
			if(!contains(s[i]) && s[i] != EMPTY_STRING)
				_symbols.push_back(s[i]);
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

	Alphabet Alphabet::operator+(Alphabet *a) {
		Alphabet ret;

		for(auto s : *this)
			ret.add(s);

		for(auto s : *a)
			if(!ret.contains(s))
				ret.add(s);
		return ret;
	}

	Alphabet Alphabet::operator-(Alphabet *a) {
		Alphabet ret;

		for(auto s : *this)
			if(!a->contains(s))
				ret.add(s);
		return ret;
	}
}
