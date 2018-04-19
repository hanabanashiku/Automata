#include "catch.hpp"
#include "States.h"

using namespace Automata;

	TEST_CASE("States Test"){
		States s(3);
		REQUIRE(s[0]->getName() == "q0");
		REQUIRE(s[1]->getName() == "q1");
		REQUIRE(s[2]->getName() == "q2");
	}
