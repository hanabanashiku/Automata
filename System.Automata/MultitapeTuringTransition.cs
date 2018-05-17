using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Automata {
	/// <summary>
	/// A transition for a Turing machine with multiple tapes.
	/// </summary>
	public class MultitapeTuringTransition : Transition {
		/// <summary>
		/// The list of current characters on each tape.
		/// </summary>
		public ReadOnlyCollection<char> InitialCharacters { get; }

		/// <summary>
		/// The list of moves for each tape.
		/// </summary>
		public ReadOnlyCollection<Tuple<char, TuringMachine.Direction>> FinalMoves { get; }

		public MultitapeTuringTransition(State p, char[] x, State q, Tuple<char, TuringMachine.Direction>[] y) : base(p, '0',
			q) {
			if(x.Length != y.Length)
				throw new ArgumentException("The initial and ending character arrays must be equal!");
			if(p == null || q == null)
				throw new NullReferenceException("Null state reference not allowed!");

			InitialCharacters = new List<char>(x).AsReadOnly();
			FinalMoves = new List<Tuple<char, TuringMachine.Direction>>(y).AsReadOnly();
		}

		public override string ToString() {
			var ret = $"δ({P}, (";
			foreach(var c in InitialCharacters)
				ret += c + ", ";
			ret = ret.Substring(0, ret.Length - 2) + $")) = ({Q}, (";
			foreach(var t in FinalMoves)
				ret += $"({t.Item1}, {t.Item2}), ";
			ret = ret.Substring(0, ret.Length - 2) + ")";
			return ret;
		}
	}
}