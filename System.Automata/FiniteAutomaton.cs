namespace System.Automata {
	/// <summary>
	/// A deterministic Finite Automaton
	/// </summary>
	public class FiniteAutomaton : Automaton {
		/// <summary>
		/// The set of accepting states.
		/// </summary>
		public AcceptingStates AcceptingStates { get; }
		
		/// <summary>
		/// The state transition mappings
		/// </summary>
		public TransitionFunction Transitions { get; }

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
			if(Transitions.Exists(x => x.A == Alphabet.EmptyString))
				throw new ArgumentException("Null transitions are not allowed!");
			foreach(var t in Transitions)
				if(!States.Contains(t.P) || !States.Contains(t.Q) || !Alphabet.Contains(t.A))
					throw new ArgumentException($"Invalid transition {t}");
		}

		/// <summary>
		/// Run the machine using the given input.
		/// </summary>
		/// <param name="x">The input string.</param>
		/// <returns>True if the machine accepts the input string.</returns>
		public bool Run(char?[] x) {
			Running = true;
			var i = 0;
			CurrentState = InitialState;

			while(i < x.Length) {
				if(!Alphabet.Contains(x[i])) {
					Running = false;
					return false;
				}
				if(x[i] == Alphabet.EmptyString) {
					i++;
					continue;
				}
				var t = Transitions.Get(CurrentState, x[i]);
				if(t == null)
					return false;
				CurrentState = ((Transition)t).Q;
				i++;
			}
			Running = false;
			return CurrentState.Accepting;
		}

		/// <summary>
		/// Run the machine using the given input.
		/// </summary>
		/// <param name="x">The input string.</param>
		/// <returns>True if the machine accepts the input string.</returns>
		public bool Run(string x) {
			var input = new char?[x.Length];
			for(var i = 0; i < x.Length; i++)
				input[i] = x[i];
			return Run(input);
		}

	}
}