using System.Collections.Generic;

namespace System.Automata {
	public class PushdownTransition : Transition{
		
		/// <summary>
		/// The top stack character
		/// </summary>
		public char Top { get; }
		
		/// <summary>
		/// The sequebnce to replace the top stack character with
		/// </summary>
		public char[] Replace { get; }

		public PushdownTransition(State p, char a, char s, State q, char[] alpha) : base(p, a, q) {
			Top = s;
			Replace = alpha;
		}

		public PushdownTransition(State p, char a, char s, State q, string alpha) : base(p, a, q) {
			Top = s;
			Replace = alpha.ToCharArray();
		}

		public override string ToString() {
			return $"({P}, {A}, {Top}, {Q}, {Replace})";
		}
	}
}