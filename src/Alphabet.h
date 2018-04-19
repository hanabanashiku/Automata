//
// Created by Michael MacLean on 4/11/18.
//

#ifndef AUTOMATA_ALPHABET_H
#define AUTOMATA_ALPHABET_H

#include<string>
#include<vector>
#include<iterator>

using namespace std;

namespace Automata{
	const char EMPTY_STRING = '\0';

	class Alphabet {
	 public:

	  bool contains(char s);
	  void add(char s);
	  size_t size();
	  bool empty();
	  Alphabet() = default;
	  explicit Alphabet(char s[], size_t size);
	  explicit operator string();
	  __wrap_iter<vector<char, std::__1::allocator<char >>::pointer> begin();
	  __wrap_iter<vector<char, std::__1::allocator<char >>::pointer> end();
	  Alphabet operator +(Alphabet* a);
	  Alphabet operator -(Alphabet* a);

	 protected:
	  vector<char> _symbols;
	};
}

#endif //AUTOMATA_ALPHABET_H
