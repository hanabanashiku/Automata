using System.Collections.Generic;
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

        public new TuringTransitionFunction TransitionFunction { get; }
        
        public TuringMachine(States q, Alphabet a, TapeAlphabet g, TuringTransitionFunction tf, State q0) {
            States = q;
            Alphabet = a;
            TapeAlphabet = g;
            TransitionFunction = tf;
            InitialState = q0;
            
            if(States.Count == 0)
                throw new ArgumentException("The set of states can't be empty!");
            if(!States.Contains(InitialState))
                throw new ArgumentException("The initial state must be in the set of states!");

            foreach (var t in TransitionFunction) {
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
            return Run(tape, InitialState, 0).Result;
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
                var tr = TransitionFunction[q, tape[i]];

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
    }
}