
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	/// <summary>
	/// A collection of state transition mappings for PDAs
	/// </summary>
	public class PushdownTransitionFunction : IEnumerable<PushdownTransition> {
		private readonly List<PushdownTransition> _trans;
		
		public PushdownTransitionFunction(params PushdownTransition[] t) {
			_trans = new List<PushdownTransition>(t.Length);
			foreach(var tr in t)
				Add(tr);
		}
		
		public void Add(PushdownTransition t) {
			if(t.P == null || t.Q == null)
				throw new NullReferenceException("Null state not allowed!");
			if(Contains(t))
				return;
			_trans.Add(t);
		}

		public bool Contains(PushdownTransition t) {
			return _trans.Contains(t);
		}

		public bool Exists(Predicate<Transition> p) {
			return _trans.Exists(p);
		}

		public int Count => _trans.Count;

		public PushdownTransition[] this[State p, char a, char alpha] =>
			_trans.Where(x => x.P.Equals(p) && x.A == a && (x.Top == alpha || x.Top == Alphabet.Wildcard)).ToArray();

		public IEnumerator<PushdownTransition> GetEnumerator() {
			return _trans.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}