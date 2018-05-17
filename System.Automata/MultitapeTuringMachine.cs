﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Automata {
	public class MultitapeTuringMachine : TuringMachine {

		/// <summary>
		/// 
		/// </summary>
		public new MultitapeTuringTransitionFunction Transitions { get; }

		/// <summary>
		/// The number of tapes
		/// </summary>
		public int Tapes { get; }

		public MultitapeTuringMachine(States q, Alphabet a, TapeAlphabet g, MultitapeTuringTransitionFunction tf, State q0,
			int k = 1) {
			States = q;
			Alphabet = a;
			TapeAlphabet = g;
			Transitions = tf;
			InitialState = q0;
			Tapes = k;

			if(States.Count == 0)
				throw new ArgumentException("The set of states can't be empty!");
			if(!States.Contains(InitialState))
				throw new ArgumentException("The initial state must be in the set of states!");
			if(k < 1)
				throw new ArgumentException("The machine must have at least one tape!");

			foreach(var t in Transitions) {
				if((!States.Contains(t.P) && !t.P.Equals(State.Ha) && !t.P.Equals(State.Hr))
				   || (!States.Contains(t.Q) && !t.Q.Equals(State.Ha) && !t.Q.Equals(State.Hr)))
					throw new ArgumentException($"Invalid transition {t}");
				if(t.InitialCharacters.Count != Tapes || t.FinalMoves.Count != Tapes)
					throw new ArgumentException("The size of transition's arrays must both equal the number of tapes!");
				foreach(var c in t.InitialCharacters)
					if(!TapeAlphabet.Contains(c))
						throw new ArgumentException("All characters in the inital array must be in the tape alphabet!");
				foreach(var p in t.FinalMoves)
					if(!TapeAlphabet.Contains(p.Item1))
						throw new ArgumentException("All characters in the final tuple array must be in the tape alphabet!");
			}
		}

		public new bool Run(string x) {
			return Run(x.ToCharArray());
		}
		
		public new bool Run(string x, out char[] o) { throw new NotSupportedException(); }

		/// <summary>
		/// Run the machine
		/// </summary>
		/// <param name="x">The input to run the machine on.</param>
		/// <returns>True if the machine halts in the accept state.</returns>
		/// <remarks>The first move the machine makes is to fill the first tape (tape 0) with the blank symbol followed by the input.</remarks>
		public new bool Run(IEnumerable<char> x) {
			var tapes = Enumerable.Range(0, Tapes).Select(n => new List<char>(Alphabet.Blank)).ToList();
			// Fill the first tape with the input
			foreach(var c in x) {
				if(!Alphabet.Contains(c))
					return false;
				if(c == Alphabet.EmptyString)
					continue;
				tapes[0].Add(c);
			}

			try {
				return Run(tapes, InitialState, Enumerable.Range(0, Tapes).Select(n => 0).ToArray()).Result;
			}
			catch(StackOverflowException) {
				return false; // infinite loop, reject
			}
		}
		
		public new bool Run(IEnumerable<char> x, out char[] o) { throw new NotSupportedException(); }

		// tapes contains each tape, q is the current state, i contains the current index for each tape.
		private async Task<bool> Run(List<List<char>> tapes, State q, IList<int> i) {
			while(true) {
				// Check tape bound
				if(i.Any(j => j < 0))
					return false; // move to hr

				// Add blanks as needed to avoid out of bound exceptions
				for(var j = 0; j < Tapes; j++)
					while(tapes[j].Count <= i[j])
						tapes[j].Add(Alphabet.Blank);

				// Check for halts
				if(q.Equals(State.Ha))
					return true;
				if(q.Equals(State.Hr))
					return false;

				// get set of current symbols
				var c = new char[Tapes];
				for(var j = 0; j < Tapes; j++)
					c[j] = tapes[j][i[j]];

				// fetch possible moves
				var tr = Transitions[q, c];

				// No more moves
				if(tr.Length == 0)
					q = State.Hr;

				// one move
				else if(tr.Length == 1) {
					var move = tr[0];
					q = move.Q;
					for(var j = 0; j < Tapes; j++) {
						// replace the tape contents and move
						tapes[j][i[j]] = move.FinalMoves[j].Item1;
						i[j] = MoveTapeHead(i[j], move.FinalMoves[j].Item2);
					}
				}

				// we have choices!
				else { // j is the move choice, k is the current tape
					var tasks = new Task<bool>[tr.Length];
					// set up each task
					for(var j = 0; j < tr.Length; j++) {
						// copy tapes and indices
						var tapeCopy = DeepCopyTapes(tapes);
						var icopy = new List<int>(i);
						// make first move 
						for(var k = 0; k < Tapes; k++) {
							tapeCopy[k][i[k]] = tr[j].FinalMoves[i[k]].Item1;
							icopy[k] = MoveTapeHead(i[k], tr[j].FinalMoves[k].Item2);
						}

						tasks[j] = Run(tapeCopy, tr[j].Q, icopy); // start task
					}

					var result = false;
					foreach(var task in tasks)
						result = result || await task;

					return result;
				}
			}
		}

		private List<List<char>> DeepCopyTapes(List<List<char>> x) {
			var copy = new List<List<char>>(x.Count);
			for(var i = 0; i < x.Count; i++)
				copy[i] = new List<char>(x[i]);
			return copy;
		}
	}
}