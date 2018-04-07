using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	/// <summary>
	/// A collection of deterministic state transition mappings.
	/// </summary>
	public class TransitionFunction : IEnumerable<Transition> {
		private readonly List<Transition> _trans;

		public TransitionFunction(params Transition[] t) {
			_trans = new List<Transition>(t.Length);
			_trans.AddRange(t);
		}

		public void Add(Transition t) {
			if(_trans.Contains(t))
				return;
			_trans.Add(t);
		}

		public bool Contains(Transition t) {
			return _trans.Contains(t);
		}
		
		public bool Exists(Predicate<Transition> p) {
			return _trans.Exists(p);
		}

		public int Count => _trans.Count;

		public State Get(State p, char s) {
			if(!_trans.Exists(x => Equals(x.P, p) && Equals(x.A, s)))
				return null;
			return _trans.First(x => x.P.Equals(p) && x.A == s).Q;
		}

		public IEnumerator<Transition> GetEnumerator() {
			return _trans.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}