namespace System.Automata {
    /// <summary>
    /// A transition for a Turing machine
    /// </summary>
    public class TuringTransition : Transition{
        
        /// <summary>
        /// The character to replace the current tape cell with.
        /// </summary>
        public char B { get; }
        
        /// <summary>
        /// The direction to move the tape head in.
        /// </summary>
        public TuringMachine.Direction Direction { get; }
        
        /// <param name="p">The current state.</param>
        /// <param name="x">The current tape cell contents.</param>
        /// <param name="q">The next state.</param>
        /// <param name="y">The character to replace the tape cell with.</param>
        /// <param name="d">The direction to move the tape head after transitioning.</param>
        public TuringTransition(State p, char x, State q, char y, TuringMachine.Direction d) : base(p, x, q) {
            B = y;
            Direction = d;
            if(p == null || q == null)
                throw new NullReferenceException("Null reference not allowed!");
        }

        public override string ToString() {
            return $"δ({P}, {A}) = ({Q}, {B}, {Direction.ToString()})";
        }
    }
}