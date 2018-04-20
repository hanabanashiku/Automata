using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Automata {
	/// <summary>
	/// A deterministic pushdown automaton utilizing a stack.
	/// </summary>
	public class PushdownAutomaton : Automaton {
		/// <summary>
		/// The stack alphabet.
		/// </summary>
		public StackAlphabet StackAlphabet { get; }

		/// <summary>
		/// The set of accepting states.
		/// </summary>
		public AcceptingStates AcceptingStates { get; }
		
		/// <summary>
		/// The transition function
		/// </summary>
		public new PushdownTransitionFunction Transitions { get; }

		/// <param name="q">The set of states</param>
		/// <param name="a">The language alphabet</param>
		/// <param name="g">The stack alphabet</param>
		/// <param name="d">The transition function containing Transition and PushdownTransition objects</param>
		/// <param name="q0">The initial state</param>
		/// <param name="f">The set of accepting states</param>
		/// <exception cref="ArgumentException"></exception>
		public PushdownAutomaton(States q, Alphabet a, StackAlphabet g, PushdownTransitionFunction d, State q0, AcceptingStates f) {
			States = q;
			Alphabet = a;
			StackAlphabet = g;
			Transitions = d;
			InitialState = q0;
			AcceptingStates = f;

			if(States.Count == 0)
				throw new ArgumentException("The set of states cannot be empty.");
			if(!States.Contains(InitialState))
				throw new ArgumentException("The inital state does not exist in the set of states!");
			
			foreach(var s in AcceptingStates)
				if(!States.Contains(s))
					throw new ArgumentException($"The accepting state {s} is not in the set of states!");

			foreach(var t in Transitions) {
				if(!States.Contains(t.P) || !States.Contains(t.Q) 
				                         || (!Alphabet.Contains(t.A) && t.A != Alphabet.EmptyString) || 
				   (!StackAlphabet.Contains(t.Top) && t.Top != Alphabet.Wildcard && t.Top != Alphabet.EmptyString))
					throw new ArgumentException($"Invalid transition {t}");
				if(t.Replace.Length == 1 && ((t.Replace[0] != Alphabet.Wildcard && t.Replace[0] != Alphabet.EmptyString) && !StackAlphabet.Contains(t.Replace[0])))
					throw new ArgumentException($"Invalid replace char for transition {t}");
				if(t.Replace.Length > 1 && Array.Exists(t.Replace, x => !StackAlphabet.Contains(x)))
					throw new ArgumentException($"Invalid replace char sequence for transition {t}");
			}
		}

		public bool Run(char[] x) {
			var stack = new Stack<char>();
			stack.Push(Alphabet.Z);
			return Run(x, InitialState, 0, stack).Result;
		}

		public bool Run(string x) {
			return Run(x.ToCharArray());
		}

		private async Task<bool> Run(char[] x, State p, int i, Stack<char> stack) {
			for(; i < x.Length; i++) {
				if(!Alphabet.Contains(x[i]))
					return false;
				if(x[i] == Alphabet.EmptyString)
					continue;

				// Get possible transitions
				var q = Transitions[p, x[i], stack.Peek()];

				if(q.Length == 0) // nothing to do, stop
					return false;

				if(q.Length == 1) {
					// try to update the stack. if we are out of bounds, reject
					try { stack = updateStack(stack, q[0].Top, q[0].Replace); }
					catch(Exception) { return false; }

					p = q[0].Q;
					if(q[0].A == Alphabet.EmptyString) i--; // Don't increment on null transition!
				}

				else {
					var tasks = new Task<bool>[q.Length];
					// check each path asynchronously
					for(var j = 0; j < q.Length; j++) {
						var s = new Stack<char>(stack);
						try { s = updateStack(s, q[j].Top, q[j].Replace); }
						catch(Exception) { continue; }
						
						tasks[j] = Run(x, q[j].Q, q[j].A == Alphabet.EmptyString ? i : i + 1, s);
					}

					var result = false;
					foreach(var t in tasks)
						result = result || (t != null && await t);
					return result;
				}
			}
			
			// No more moves!
			if(p.Accepting)
				return true;
			
			// check for lambda transitions
			var r = Transitions[p, Alphabet.EmptyString, stack.Peek()];
			var lambdas = new Task<bool>[r.Length];
			for(var k = 0; k < r.Length; k++) {
				var s = new Stack<char>(stack);
				try { s = updateStack(s, r[k].Top, r[k].Replace); }
				catch(Exception) { continue; }

				lambdas[k] = Run(x, r[k].Q, i, s);
			}
			var accept = false;
			foreach(var t in lambdas)
				accept = accept || (t != null && await t);
			return accept;
		}
		

		private Stack<char> updateStack(Stack<char> stack, char top, char[] replace) {
			// We shouldn't modify the stack if the top character doesn't match
			// or if we are keeping the top symbol the same (wildcard)
			if(stack.Peek() != top || top == Alphabet.Wildcard)
				return stack;

			stack.Pop(); // pop the top symbol off
			// if we are replacing with nothing, we're done
			if(replace.Length == 1 && replace[0] == Alphabet.EmptyString)
				return stack;
			
			// We do this backwards: if abc is the replace string,
			// then a should be on top.
			for(var i = replace.Length - 1; i >= 0; i--)
				if(replace[i] != Alphabet.EmptyString)
					stack.Push(replace[i]);
			return stack;
		}
	}
}