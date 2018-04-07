namespace System.Automata {
	public abstract class Automaton {
		
		/// <summary>
		/// The set of states.
		/// </summary>
		public States States { get; protected set; }
		
		/// <summary>
		/// The machine's alphabet.
		/// </summary>
		public Alphabet Alphabet { get; protected set; }
		
		/// <summary>
		/// The initial state.
		/// </summary>
		public State InitialState { get; protected set; }

		/// <summary>
		/// The state transition mappings
		/// </summary>
		public TransitionFunction Transitions { get; protected set; }
	}
}