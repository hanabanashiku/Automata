#include "AcceptingStates.h"

using namespace std;

namespace Automata{
	AcceptingStates::AcceptingStates(State* states[]) : States(states){
		for(auto s : _states)
			s->setAccepting(true);
	}
}