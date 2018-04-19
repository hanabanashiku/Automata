#include "AcceptingStates.h"

using namespace std;

namespace Automata{
	AcceptingStates::AcceptingStates(State* states[], size_t size) : States(states, size){
		for(auto s : _states)
			s->setAccepting(true);
	}
}