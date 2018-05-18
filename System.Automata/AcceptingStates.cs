namespace System.Automata {
	/// <summary>
	/// The set of all accepting states.
	/// </summary>
	/// <remarks>These states should also be in the set of all states.</remarks>
	public class AcceptingStates : States {
		/// <inheritdoc />
		public AcceptingStates(params State[] q) : base(q) {
			foreach(var p in q)
				p.Accepting = true;
			
		}
	}
}