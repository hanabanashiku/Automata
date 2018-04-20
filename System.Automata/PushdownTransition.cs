using System.Linq;

namespace System.Automata {
	public class PushdownTransition : Transition{
		
		/// <summary>
		/// The top stack character
		/// </summary>
		public char Top { get; }
		
		/// <summary>
		/// The sequence to replace the top stack character with.
		/// Use Alphabet.EmptyString to delete the top stack character.
		/// </summary>
		public char[] Replace { get; }

		public PushdownTransition(State p, char a, char s, State q, char[] alpha) : base(p, a, q) {
			Top = s;
			Replace = alpha;
			if(Replace.Length > 1 && Replace.Contains(Alphabet.Wildcard))
				throw new ArgumentException("Can't use wildcard as a symbol!");
			if(Replace.Length == 1 && Replace[0] == Alphabet.Wildcard && Top != Alphabet.Wildcard)
				throw new ArgumentException("Can't have Replace be wildcard if top symbol is not!");
		}

		public PushdownTransition(State p, char a, char s, State q, string alpha) : base(p, a, q) {
			Top = s;
			Replace = alpha.ToCharArray();
			if(Replace.Length > 1 && Replace.Contains(Alphabet.Wildcard))
				throw new ArgumentException("Can't use wildcard as a symbol!");
			if(Replace.Length == 1 && Replace[0] == Alphabet.Wildcard && Top != Alphabet.Wildcard)
				throw new ArgumentException("Can't have Replace be wildcard if top symbol is not!");
		}

		public override string ToString() {
			return $"({P}, {A}, {Top}, {Q}, {Replace})";
		}
	}
}