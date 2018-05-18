using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	public class MultitapeTuringTransitionFunction : IEnumerable<MultitapeTuringTransition> {
		private readonly List<MultitapeTuringTransition> _trans;

		public MultitapeTuringTransitionFunction(params MultitapeTuringTransition[] t) {
			_trans = new List<MultitapeTuringTransition>(t.Length);
			foreach(var tr in t)
				Add(tr);
		}

		public void Add(MultitapeTuringTransition t) {
			if(Contains(t))
				return;
			_trans.Add(t);
		}

		public bool Contains(MultitapeTuringTransition t) {
			return _trans.Contains(t);
		}

		public bool Exists(Predicate<MultitapeTuringTransition> p) {
			return _trans.Exists(p);
		}

		public int Count => _trans.Count;

		public MultitapeTuringTransition[] this[State p, char[] a] =>
			_trans.Where(x => x.P.Equals(p) && x.InitialCharacters.Count == a.Length && x.InitialCharacters.SequenceEqual(a))
				.ToArray();

		public IEnumerator<MultitapeTuringTransition> GetEnumerator() {
			return _trans.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}