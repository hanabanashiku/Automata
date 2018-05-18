using System.Collections.Generic;

namespace System.Automata {
    /// <summary>
    /// An alphabet for use with a Turing tape
    /// </summary>
    public class TapeAlphabet : Alphabet {

        public TapeAlphabet(Alphabet a, params char[] c) {
            Chars = new List<char>();
            Chars.AddRange(a);
            if(!Contains(Blank)) Chars.Add(Blank);
			
            foreach(var s in c)
                if(!Chars.Contains(s) && s != EmptyString && s != Z)
                    Chars.Add(s);
        }

        public TapeAlphabet(params char[] c) : base(c){
            Chars.Add(Blank);
        }

        public static TapeAlphabet operator +(TapeAlphabet a, Alphabet b) {
            var c = new TapeAlphabet(a);
            foreach(var c1 in b)
                if(!a.Contains(c1))
                    c.Add(c1);
            return c;
        }

        public static TapeAlphabet operator +(Alphabet a, TapeAlphabet b) {
            return b + a;
        }

        public static TapeAlphabet operator +(TapeAlphabet a, TapeAlphabet b) {
            return a + (Alphabet)b;
        }

        public static TapeAlphabet operator -(TapeAlphabet a, Alphabet b) {
            var c = new TapeAlphabet();
            foreach(var c1 in a)
                if(!b.Contains(c1))
                    c.Add(c1);
            return c;
        }

        public static Alphabet operator -(Alphabet a, TapeAlphabet b) {
            return a - (Alphabet)b;
        }

        public static TapeAlphabet operator -(TapeAlphabet a, TapeAlphabet b) {
            return a - (Alphabet)b;
        }
    }
}