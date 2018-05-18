using System.Collections;
using System.Collections.Generic;

namespace System.Automata {
	/// <summary>
	/// The set of all states
	/// </summary>
	public class States : IEnumerable<State> {
		private readonly List<State> _states;

		/// <summary>
		/// Initialize the set with n states name qi for each <![CDATA[i < n]]>.
		/// </summary>
		/// <param name="n">The number of states</param>
		public States(int n) {
			_states = new List<State>(n);
			for(var i = 0; i < n; i++)
				_states.Add(new State(i));
		}
		
		/// <summary>
		/// Initialize the set with the given states.
		/// </summary>
		/// <param name="q">The states to include</param>
		public States(params State[] q) {
			_states = new List<State>(q.Length);
			_states.AddRange(q);
		}
		
		/// <summary>
		/// Add a state to the set.
		/// </summary>
		/// <param name="q">The state to add</param>
		public void Add(State q) {
			if(_states.Exists(x => x.Name == q.Name))
				return;
			_states.Add(q);
		}
		
		/// <summary>
		/// Check if the state exists in the set
		/// </summary>
		/// <param name="q">The state to check</param>
		/// <returns>True if it exists in the set.</returns>
		public bool Contains(State q) {
			return _states.Contains(q);
		}

		public int Count => _states.Count;
		
		public State this[int i] => _states[i];

		public IEnumerator<State> GetEnumerator() {
			return _states.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override string ToString() {
			var ret = "{";
			_states.ForEach(x => ret += $"{x.Name}, ");
			return $"{ret.Substring(0, ret.Length - 2)}}}";
		}
	}
}