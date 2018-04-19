using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	/// <summary>
	/// A deterministic collection of  state transition mappings.
	/// </summary>
	public class TransitionFunction : IEnumerable<Transition> {
		protected List<Transition> Trans;

		public TransitionFunction(params Transition[] t) {
			Trans = new List<Transition>(t.Length);
			foreach(var tr in t)
				Add(tr);
		}

		public void Add(Transition t) {
			if(t.P == null || t.Q == null)
				throw new NullReferenceException("Null state not allowed!");
			if(t.A == Alphabet.EmptyString)
				throw new ArgumentException("Null transitions not allowed!");
			if(Contains(t))
				return;
			if(Get(t.P, t.A) != null)
				throw new ArgumentException("Cannot have multiple choices for transitions!");
			Trans.Add(t);
		}

		public bool Contains(Transition t) {
			return Trans.Contains(t);
		}
		
		public bool Exists(Predicate<Transition> p) {
			return Trans.Exists(p);
		}

		public int Count => Trans.Count;

		public State this[State p, char s] => Get(p, s);
		
		private State Get(State p, char s) {
			if(!Trans.Exists(x => Equals(x.P, p) && Equals(x.A, s)))
				return null;
			return Trans.First(x => x.P.Equals(p) && x.A == s).Q;
		}

		public IEnumerator<Transition> GetEnumerator() {
			return Trans.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}