#ifndef AUTOMATA_ACCEPTINGSTATES_H
#define AUTOMATA_ACCEPTINGSTATES_H

#include "States.h"

namespace Automata{
	class AcceptingStates : States{
	 public:
	  explicit AcceptingStates (State* states[]);
	};
}

#endif //AUTOMATA_ACCEPTINGSTATES_H
