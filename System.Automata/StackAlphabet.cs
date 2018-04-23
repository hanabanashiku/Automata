using System.Collections.Generic;

namespace System.Automata {
	/// <summary>
	/// An alphabet for use with a pushdown stack
	/// </summary>
	public class StackAlphabet : Alphabet {

		public StackAlphabet(Alphabet a, params char[] c) {
			Chars = new List<char>();
			Chars.AddRange(a);
			if(!Contains(Z)) Chars.Add(Z);
			
			foreach(var s in c)
				if(!Chars.Contains(s) && s != EmptyString && s != Z)
					Chars.Add(s);
		}

		public StackAlphabet(params char[] c) : base(c){
			Chars.Add(Z);
		}

		public static StackAlphabet operator +(StackAlphabet a, Alphabet b) {
			var c = new StackAlphabet(a);
			foreach(var c1 in b)
				if(!a.Contains(c1))
					c.Add(c1);
			return c;
		}

		public static StackAlphabet operator +(Alphabet a, StackAlphabet b) {
			return b + a;
		}

		public static StackAlphabet operator +(StackAlphabet a, StackAlphabet b) {
			return a + (Alphabet)b;
		}

		public static StackAlphabet operator -(StackAlphabet a, Alphabet b) {
			var c = new StackAlphabet();
			foreach(var c1 in a)
				if(!b.Contains(c1))
					c.Add(c1);
			return c;
		}

		public static Alphabet operator -(Alphabet a, StackAlphabet b) {
			return a - (Alphabet)b;
		}

		public static StackAlphabet operator -(StackAlphabet a, StackAlphabet b) {
			return a - (Alphabet)b;
		}
	}
}