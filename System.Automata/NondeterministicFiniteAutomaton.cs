using System.Linq;
using System.Threading.Tasks;

namespace System.Automata {
	/// <summary>
	/// A nondeterministic Finite Automaton
	/// </summary>
	public class NondeterministicFiniteAutomaton : FiniteAutomaton {

		/// <summary>
		/// State transition mappings
		/// </summary>
		public new NondeterministicTransitionFunction Transitions { get; private set; }

		public NondeterministicFiniteAutomaton(States q, Alphabet a, NondeterministicTransitionFunction d, State q0,
			AcceptingStates f) : base(q, a, d, q0, f) {
			Transitions = d;
		}

		public new bool Run(char[] x) {
			return Run(x, InitialState, 0).Result;
		}

		public new bool Run(string x) {
			return Run(x.ToCharArray());
		}

		private async Task<bool> Run(char[] x, State p, int i) {
			// no more moves!
			if(i == x.Length) {
				// Check for lambda transitions
				var q = Transitions[p, Alphabet.EmptyString];
				
				if(q.Length == 0)
					return p.Accepting;

				var tasks = new Task<bool>[q.Length];
				for(var j = 0; j < q.Length; j++)
					tasks[j] = Run(x, q[j].Q, i);

				var result = false;
				foreach(var t in tasks)
					result = result || await t;
				return result;
			}
			

			for(; i < x.Length; i++) {
				if(!Alphabet.Contains(x[i]))
					return false;
				if(x[i] == Alphabet.EmptyString)
					continue;
				
				// get possible transitions
				var q = Transitions[p, x[i]];
				
				if(q.Length == 0) // nothing to do, stop
					return false;
				
				if(q.Length == 1){
					p = q[0].Q; // switch states
					if(q[0].A == Alphabet.EmptyString) i--; // Don't increment on null transition!
				}
				
				else { // we have a choice
					var tasks = new Task<bool>[q.Length];
					// check each path asynchronously
					for(var j = 0; j < q.Length; j++)
						tasks[j] = Run(x, q[j].Q, q[j].A == Alphabet.EmptyString ? i : i+1);
					
					var result = false;
					// check the result
					foreach(var t in tasks)
						result = result || await t;
					return result; // if one of them is successful, this branch accepts
				}
			}

			// we have reached the end of the string. Do we accept?
			return p.Accepting; 
		}
	}
}