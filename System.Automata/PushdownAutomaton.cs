namespace System.Automata {
	/// <summary>
	/// A deterministic pushdown automaton utilizing a stack.
	/// </summary>
	public class PushdownAutomaton : Automaton {
		/// <summary>
		/// The stack alphabet.
		/// </summary>
		public StackAlphabet StackAlphabet { get; }

		/// <summary>
		/// The set of accepting states.
		/// </summary>
		public AcceptingStates AcceptingStates { get; }

		/// <param name="q">The set of states</param>
		/// <param name="a">The language alphabet</param>
		/// <param name="g">The stack alphabet</param>
		/// <param name="d">The transition function containing Transition and PushdownTransition objects</param>
		/// <param name="q0">The initial state</param>
		/// <param name="f">The set of accepting states</param>
		/// <exception cref="ArgumentException"></exception>
		public PushdownAutomaton(States q, Alphabet a, StackAlphabet g, TransitionFunction d, State q0, AcceptingStates f) {
			States = q;
			Alphabet = a;
			StackAlphabet = g;
			Transitions = d;
			InitialState = q0;
			AcceptingStates = f;

			if(States.Count == 0)
				throw new ArgumentException("The set of states cannot be empty.");
			if(!States.Contains(InitialState))
				throw new ArgumentException("The inital state does not exist in the set of states!");
			if(Transitions.Exists(x => x.A == Alphabet.EmptyString))
				throw new ArgumentException("Null transitions are not allowed!");

			foreach(var t in Transitions) {
				if(!States.Contains(t.P) || !States.Contains(t.Q) || !Alphabet.Contains(t.A))
					throw new ArgumentException($"Invalid transition {t}");
				if(t.GetType() == typeof(PushdownTransition)) {
					var pt = (PushdownTransition)t;
					if(!StackAlphabet.Contains(pt.Top) || System.Array.Exists(pt.Replace, x => !StackAlphabet.Contains(x)))
						throw new ArgumentException("Invalid transition {pt}");
				}
				else if(t.GetType() != typeof(Transition))
					throw new ArgumentException("Invalid transition type found");
			}
		}
	}
}