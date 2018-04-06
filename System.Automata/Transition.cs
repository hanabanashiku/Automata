namespace System.Automata {
	/// <summary>
	/// A state transition
	/// </summary>
	public struct Transition {
		/// <summary>
		/// The current state.
		/// </summary>
		public State P { get; }
		
		/// <summary>
		/// The next state.
		/// </summary>
		public State Q { get; }
		
		/// <summary>
		/// The symbol to transition on
		/// </summary>
		public char? A { get; }
		

		/// <param name="p">The current state</param>
		/// <param name="c">The current input symbol</param>
		/// <param name="q">The next state</param>
		/// <remarks>For a epsilon-transition, use Alphabet.<see cref="Alphabet.EmptyString" /></remarks>
		public Transition(State p, char? c, State q) {
			P = p;
			A = c;
			Q = q;
		}

		public override string ToString() {
			return $"δ({P}, {A.ToString().Replace('\0'.ToString(), "ε")}) = {Q}";
		}
	}
}