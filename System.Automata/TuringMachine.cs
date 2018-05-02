using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Automata {
    /// <summary>
    /// A nondeterministic Turing machine
    /// </summary>
    public class TuringMachine : Automaton {
        /// <summary>
        /// The direction to move the tape
        /// </summary>
        public enum Direction {
            /// <summary>
            /// Move the tape head left one cell.
            /// </summary>
            L,
            /// <summary>
            /// Move the tape head right one cell.
            /// </summary>
            R,
            /// <summary>
            /// Do not move the tape head.
            /// </summary>
            S
        };
        
        /// <summary>
        /// The tape alphabet.
        /// </summary>
        public TapeAlphabet TapeAlphabet { get; }

        public new static readonly AcceptingStates AcceptingStates = new AcceptingStates(State.Ha);

        public new TuringTransitionFunction Transitions { get; }
        
        public TuringMachine(States q, Alphabet a, TapeAlphabet g, TuringTransitionFunction tf, State q0) {
            States = q;
            Alphabet = a;
            TapeAlphabet = g;
            Transitions = tf;
            InitialState = q0;
            
            if(States.Count == 0)
                throw new ArgumentException("The set of states can't be empty!");
            if(!States.Contains(InitialState))
                throw new ArgumentException("The initial state must be in the set of states!");

            foreach (var t in Transitions) {
               if((!States.Contains(t.P) && !t.P.Equals(State.Ha) && !t.P.Equals(State.Hr)) 
                  || (!States.Contains(t.Q) && !t.Q.Equals(State.Ha) && !t.Q.Equals(State.Hr)) || !TapeAlphabet.Contains(t.A) 
                  || !TapeAlphabet.Contains(t.B))
                   throw new ArgumentException($"Invalid transition {t}");
            }
        }

        /// <summary>
        /// Run the machine
        /// </summary>
        /// <param name="x">The input to run the machine on.</param>
        /// <returns>True if the machine halts in the accept state.</returns>
        /// <remarks>The first move the machine makes is to fill the tape with the blank symbol and the input.</remarks>
        public bool Run(char[] x) {
            var tape = new List<char>() { Alphabet.Blank };
            // Fill the tape with the input
            foreach (var c in x) {
                if (!Alphabet.Contains(c))
                    return false;
                if (c == Alphabet.EmptyString)
                    continue;
                tape.Add(c);
            }

            try {
                return Run(tape, InitialState, 0).Result;
            }
            catch(StackOverflowException) {
                return false; // infinite loop, reject
            }
        }

        public bool Run(string x) {
            return Run(x.ToCharArray());
        }

        private async Task<bool> Run(List<char> tape, State q, int i) {
            while (true) {
                // Check tape bound
                if (i < 0) 
                    return false; // move to hr
                while(tape.Count <= i)
                    tape.Add(Alphabet.Blank);
                
                // check for halt
                if (q.Equals(State.Ha))
                    return true;
                if (q.Equals(State.Hr))
                    return false;
                
                // get possible moves
                var tr = Transitions[q, tape[i]];

                if (tr.Length == 0)
                    q = State.Hr;

                // one move
                else if(tr.Length == 1) {
                    tape[i] = tr[0].B;
                    q = tr[0].Q;
                    i = MoveTapeHead(i, tr[0].Direction);
                }

                // we have choices!
                else {
                    var tasks = new Task<bool>[tr.Length];
                    for (var j = 0; j < tr.Length; j++) {
                        var tapeCopy = new List<char>(tape);
                        tapeCopy[i] = tr[j].B;
                        tasks[j] = Run(tapeCopy, tr[j].Q, MoveTapeHead(i, tr[j].Direction));
                    }

                    var result = false;
                    foreach (var task in tasks)
                        result = result || await task;

                    return result;
                }
            }
        }

        private int MoveTapeHead(int i, Direction d) {
            switch (d) {
                case Direction.L:
                    return i - 1;
                case Direction.R:
                    return i + 1;
                case Direction.S:
                    return i;
                default:
                    throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
        }

        /// <param name="x">The number to encode</param>
        /// <returns>The integer as a binary number</returns>
        /// <remarks>32-bits with two's complement for negatives, without leading 0s</remarks>
        public static string BinEncode(int x) {
            return Convert.ToString(x, 2);
        }

        /// <param name="x">The character to encode</param>
        /// <returns>The character as an ASCII binary number without leading 0s</returns>
        public static string BinEncode(char x) {
            return BinEncode((int)x);
        }

        /// <param name="x">The string to encode</param>
        /// <returns>A string of the form e(x0)Δe(x1)Δ..Δe(xk)Δ where e(xk) is the binary encoding of a single character.</returns>
        public static string BinEncode(char[] x) {
            if(x.Length == 0) return $"{Alphabet.Blank}";
            return x.Aggregate("", (current, c) => current + (BinEncode(c) + Alphabet.Blank));
        }
        /// <param name="x">The string to encode</param>
        /// <returns>A string of the form e(x0)Δe(x1)Δ..Δe(xk)Δ where e(xk) is the binary encoding of a single character.</returns>
        public static string BinEncode(string x) {
            return BinEncode(x.ToCharArray());
        }

        /// <param name="n">The number to encode</param>
        /// <returns>The number n returned as the string 1^n</returns>
        public static string UnaryEncode(uint n) {
            var result = "";

            while(n > 0) {
                result += "1";
                n--;
            }
            return result;
        }

        /// <summary>
        /// Encode a turing machine
        /// </summary>
        /// <param name="t">The turing machine to encode</param>
        /// <param name="x">The input to encode</param>
        /// <returns>
        /// A string ∈ {0, 1, Δ}* representing a turing machine in the form
        /// Δe(m0)Δe(m1)Δ..Δe(mk)Δe(x)
        /// Where mk is a transition and e is the binary encoding function.
        /// </returns>
        /// <remarks>See BinEncode(TuringTransition) and BinEncode(char[]) for specific encodings.</remarks>
        public static string BinEncode(TuringMachine t, char[] x = null) {
            var result = $"{Alphabet.Blank}";

            foreach(var tr in t.Transitions)
                result += BinEncode(tr, t.States) + Alphabet.Blank;

            if(x != null)
                result += BinEncode(x);

            return result;
        }

        /// <summary>
        /// Encode a turing machine move 
        /// </summary>
        /// <param name="t">The transition</param>
        /// <param name="s">The set of states</param>
        /// <returns>
        /// A string ∈ {0, 1, Δ}* representing a turing transition
        /// 𝛿(p, σ) = (q, τ, D) where p,q ∈ S, σ,τ ∈ Σ
        /// in the form
        /// n(indexof(p))Δn(σ)Δn(indexof(q))Δn(τ)Δn(D)Δ
        /// where n(x) reperesents the binary integer or UTF representation of x.
        /// n(D) = { L->00, R->01, S->10, err->11 }
        /// </returns>
        /// <exception cref="ArgumentException">If a state referenced is not contained in s</exception>
        public static string BinEncode(TuringTransition t, States s) {
            string d;
    
            // get the binary representation of the state index
            var p = EncodeState(t.P, s); // current state
            var q = EncodeState(t.Q, s); // next state
            var sigma = BinEncode(t.A); // current tape symbol
            var tao = BinEncode(t.B); // new tape symbol
            switch(t.Direction) { // the direction
                case Direction.L:
                    d = "00";
                    break;
                case Direction.R:
                    d = "01";
                    break;
                case Direction.S:
                    d = "10";
                    break;
                default:
                    d = "11";
                    break;
            }

            return $"{p}{Alphabet.Blank}{sigma}{Alphabet.Blank}{q}{Alphabet.Blank}{tao}{Alphabet.Blank}{d}{Alphabet.Blank}";
        }

        private static string EncodeState(State p, States s) {
            if(!s.Contains(p) && !p.Equals(State.Ha) && !p.Equals(State.Hr))
                throw new ArgumentException("The state must exist in the set of states!");
            
            if(p.Equals(State.Ha))
                return "0";
            if(p.Equals(State.Hr))
                return "1";
            return BinEncode(s.Select(((state, i) => new {state, i})).First(x => x.state.Equals(p)).i + 2);
        }
    }
}