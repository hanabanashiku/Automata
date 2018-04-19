#ifndef AUTOMATA_ACCEPTINGSTATES_H
#define AUTOMATA_ACCEPTINGSTATES_H

#include "States.h"

namespace Automata{
	class AcceptingStates : public States{
	 public:
	  explicit AcceptingStates (State* states[], size_t size);
	};
}

#endif //AUTOMATA_ACCEPTINGSTATES_H
