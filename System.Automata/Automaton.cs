namespace System.Automata {
	public abstract class Automaton {
		
		/// <summary>
		/// True if the automaton is currently running.
		/// </summary>
		public bool Running { get; protected set; }

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

		protected State CurrentState;
	}
}