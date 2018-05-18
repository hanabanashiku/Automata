using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	/// <summary>
	/// A nondeterministic collection of state transition mappings.
	/// </summary>
	public class NondeterministicTransitionFunction : TransitionFunction {

		public NondeterministicTransitionFunction(params Transition[] t) {
			Trans = new List<Transition>(t.Length);
			foreach(var tr in t)
				Add(tr);
		}

		public new void Add(Transition t) {
			if(t.P == null || t.Q == null)
				throw new NullReferenceException("Null state not allowed!");
			if(Contains(t))
				return;
			
			Trans.Add(t);
		}

		public new Transition[] this[State p, char s] => Get(p, s);

		private Transition[] Get(State p, char s) {
			return Trans.Where(x => x.P.Equals(p) && (x.A == s || x.A == Alphabet.EmptyString)).ToArray();
		}
	}
}