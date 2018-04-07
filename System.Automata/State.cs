namespace System.Automata {
	/// <summary>
	/// A automaton state.
	/// </summary>
	public class State {
		
		/// <summary>
		/// The Turing halting accept state.
		/// </summary>
		public static readonly State Hr = new State("Hr") { Accepting = true} ;

		/// <summary>
		/// The Turing halting reject state.
		/// </summary>
		public static readonly State Ha = new State("Ha");
		
		/// <summary>
		/// The name of the state
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Is the state an accepting state?
		/// </summary>
		public bool Accepting { get; internal set; }
		

		/// <param name="name">The name of the state</param>
		public State(string name) {
			Name = name;
			Accepting = false;
		}

		public State(int id) {
			Name = $"q{id}";
			Accepting = false;
		}

		public override string ToString() => Name;

		public override bool Equals(object obj) {
			if(obj == null) return false;
			return obj.GetType() == typeof(State) && Equals((State)obj);
		}

		public bool Equals(State s) {
			if(s.Name != Name) return false;
			if(s.Accepting != Accepting) return false;
			return true;
		}

		public override int GetHashCode() {
			return Name.GetHashCode();
		}
	}
}