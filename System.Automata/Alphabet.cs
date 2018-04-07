using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Automata {
	/// <summary>
	/// A automaton alphabet
	/// </summary>
	public class Alphabet : IEnumerable<char> {
		protected List<char> Chars;

		public const char EmptyString = '\0';

		/// <summary> {1} </summary>
		public static Alphabet Unary => new Alphabet('1');
		
		/// <summary> {0, 1} </summary>
		public static Alphabet Binary => new Alphabet('0', '1');
		
		/// <summary> {0, 1, ..., 9} </summary>
		public static Alphabet Decimal => new Alphabet(Enumerable.Range(48, 10).Select(c => (char)c).ToArray());
		
		/// <summary> {a, b} </summary>
		public static Alphabet Ab => new Alphabet('a', 'b');
		
		/// <summary> {a, b, c} </summary>
		public static Alphabet Abc => new Alphabet('a', 'b', 'c');
		
		/// <summary> {a, b, ..., z} </summary>
		public static Alphabet AzLower => new Alphabet(Enumerable.Range(65, 26).Select(c => (char)c).ToArray());
		
		/// <summary> {A, B, ..., Z} </summary>
		public static Alphabet AzUpper => new Alphabet(Enumerable.Range(97, 26).Select(c => (char)c).ToArray());
		
		/// <summary> {a, b, ..., Z} </summary>
		public static Alphabet AzAll => new Alphabet(AzLower.Concat(AzUpper).ToArray());
		
		/// <summary> All the printable ASCII characters </summary>
		public static Alphabet Ascii => new Alphabet(Enumerable.Range(32, 94).Select(c => (char)c).ToArray());

		/// <summary>
		/// Initialize the alphabet with the given symbols.
		/// </summary>
		/// <param name="s">The symbols to use.</param>
		public Alphabet(params char[] s) {
			Chars = new List<char>(s.Length);
			Chars.AddRange(s);
			if(Chars.Contains(EmptyString) || Chars.Contains(StackAlphabet.Z))
				throw new ArgumentException("Invalid symbol encountered");
		}

		public void Add(char s) {
			if(Chars.Exists(x => x == s))
				return;
			Chars.Add(s);
		}

		public bool Contains(char s) {
			return Chars.Contains(s);
		}

		public int Count => Chars.Count;

		public IEnumerator<char> GetEnumerator() {
			return Chars.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override string ToString() {
			var ret = "{";
			Chars.ForEach(x => ret += $"{x}, ");
			return $"{ret.Substring(0, ret.Length - 2)}}}";
		}

		public static Alphabet operator +(Alphabet a, Alphabet b) {
			var c = new Alphabet();
			foreach(var c1 in a) c.Add(c1);
			foreach(var c1 in b) c.Add(c1);
			return c;
		}

		public static Alphabet operator -(Alphabet a, Alphabet b) {
			var c = new Alphabet();
			foreach(var c1 in a)
				if(!b.Contains(c1))
					c.Add(c1);
			return c;
		}
		
	}
}