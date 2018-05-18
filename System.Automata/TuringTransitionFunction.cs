using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
    /// <summary>
    /// A collection of state transition mappings for PDAs
    /// </summary>
    public class TuringTransitionFunction : IEnumerable<TuringTransition> {
        private readonly List<TuringTransition> _trans;
		
        public TuringTransitionFunction(params TuringTransition[] t) {
            _trans = new List<TuringTransition>(t.Length);
            foreach(var tr in t)
                Add(tr);
        }
		
        public void Add(TuringTransition t) {
            if(Contains(t))
                return;
            _trans.Add(t);
        }

        public bool Contains(TuringTransition t) {
            return _trans.Contains(t);
        }

        public bool Exists(Predicate<TuringTransition> p) {
            return _trans.Exists(p);
        }

        public int Count => _trans.Count;

        public TuringTransition[] this[State p, char a] =>
            _trans.Where(x => x.P.Equals(p) && x.A == a).ToArray();

        public IEnumerator<TuringTransition> GetEnumerator() {
            return _trans.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}