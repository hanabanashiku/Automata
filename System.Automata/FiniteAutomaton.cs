namespace System.Automata {
	/// <summary>
	/// A deterministic Finite Automaton
	/// </summary>
	public class FiniteAutomaton : Automaton {
		
		public FiniteAutomaton(States q, Alphabet a, TransitionFunction d, State q0, AcceptingStates f) {
			States = q;
			Alphabet = a;
			Transitions = d;
			InitialState = q0;
			AcceptingStates = f;
			
			if(States.Count == 0)
				throw new ArgumentException("The set of states cannot be empty.");
			if(!States.Contains(InitialState))
				throw new ArgumentException("The initial state does not exist in the set of states!");
			foreach(var s in AcceptingStates)
				if(!States.Contains(s))
					throw new ArgumentException($"The accepting state {s} does not exist in the set of states!");
			foreach(var t in Transitions)
				if(!States.Contains(t.P) || !States.Contains(t.Q) || (t.A != Alphabet.EmptyString && !Alphabet.Contains(t.A)))
					throw new ArgumentException($"Invalid transition {t}");
		}
		
		/// <summary>
		/// Run the machine using the given input.
		/// </summary>
		/// <param name="x">The input string.</param>
		/// <returns>True if the machine accepts the input string.</returns>
		public bool Run(char[] x) {
			var i = 0;
			var current = InitialState;

			while(i < x.Length) {
				if(!Alphabet.Contains(x[i]))
					return false;
				if(x[i] == Alphabet.EmptyString) {
					i++;
					continue;
				}
				var q = Transitions[current, x[i]];
				if(q == null)
					return false;
				current = q.Q;
				i++;
			}
			return current.Accepting;
		}

		/// <summary>
		/// Run the machine using the given input.
		/// </summary>
		/// <param name="x">The input string.</param>
		/// <returns>True if the machine accepts the input string.</returns>
		public bool Run(string x) {
			return Run(x.ToCharArray());
		}

	}
}