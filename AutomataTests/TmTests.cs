using System.Automata;
using NUnit.Framework;

namespace AutomataTests {
    [TestFixture]
    public class TmTests {
        // {xx | x in {a, b}*}
        [Test]
        public void XxTest() {
            var q = new States(10);
            var g = new TapeAlphabet('A', 'B') + Alphabet.Ab;
            var tf = new TuringTransitionFunction() {
                new TuringTransition(q[0], Alphabet.Blank, q[1], Alphabet.Blank, TuringMachine.Direction.R),
                new TuringTransition(q[1], 'a', q[2], 'A', TuringMachine.Direction.R),
                new TuringTransition(q[1], 'b', q[2], 'B', TuringMachine.Direction.R),
                new TuringTransition(q[2], 'a', q[2], 'a', TuringMachine.Direction.R), 
                new TuringTransition(q[2], 'b', q[2], 'b', TuringMachine.Direction.R), 
                new TuringTransition(q[2], Alphabet.Blank, q[3], Alphabet.Blank, TuringMachine.Direction.L),
                new TuringTransition(q[2], 'A', q[3], 'A', TuringMachine.Direction.L),
                new TuringTransition(q[2], 'B', q[3], 'B', TuringMachine.Direction.L),
                new TuringTransition(q[3], 'a', q[4], 'A', TuringMachine.Direction.L),
                new TuringTransition(q[3], 'b', q[4], 'B', TuringMachine.Direction.L),
                new TuringTransition(q[4], 'a', q[4], 'a', TuringMachine.Direction.L),
                new TuringTransition(q[4], 'b', q[4], 'b', TuringMachine.Direction.L),
                new TuringTransition(q[4], 'A', q[1], 'A', TuringMachine.Direction.R),
                new TuringTransition(q[4], 'B', q[1], 'B', TuringMachine.Direction.R),
                new TuringTransition(q[1], 'A', q[5], 'A', TuringMachine.Direction.L),
                new TuringTransition(q[1], 'B', q[5], 'B', TuringMachine.Direction.L),
                new TuringTransition(q[1], Alphabet.Blank, State.Ha, Alphabet.Blank, TuringMachine.Direction.S),
                new TuringTransition(q[5], 'A', q[5], 'a', TuringMachine.Direction.L),
                new TuringTransition(q[5], 'B', q[5], 'b', TuringMachine.Direction.L),
                new TuringTransition(q[5], Alphabet.Blank, q[6], Alphabet.Blank, TuringMachine.Direction.R),
                new TuringTransition(q[6], Alphabet.Blank, State.Ha, Alphabet.Blank, TuringMachine.Direction.S),
                new TuringTransition(q[6], 'b', q[7], 'B', TuringMachine.Direction.R),
                new TuringTransition(q[7], 'a', q[7], 'a', TuringMachine.Direction.R),
                new TuringTransition(q[7], 'b', q[7], 'b', TuringMachine.Direction.R),
                new TuringTransition(q[7], Alphabet.Blank, q[7], Alphabet.Blank, TuringMachine.Direction.R),
                new TuringTransition(q[7], 'B', q[9], Alphabet.Blank, TuringMachine.Direction.L),
                new TuringTransition(q[9], Alphabet.Blank, q[9], Alphabet.Blank, TuringMachine.Direction.L),
                new TuringTransition(q[9], 'a', q[9], 'a', TuringMachine.Direction.L),
                new TuringTransition(q[9], 'b', q[9], 'b', TuringMachine.Direction.L),
                new TuringTransition(q[6], 'a', q[8], 'A', TuringMachine.Direction.R),
                new TuringTransition(q[8], 'a', q[8], 'a', TuringMachine.Direction.R),
                new TuringTransition(q[8], 'b', q[8], 'b', TuringMachine.Direction.R),
                new TuringTransition(q[8], Alphabet.Blank, q[8], Alphabet.Blank, TuringMachine.Direction.R),
                new TuringTransition(q[8], 'A', q[9], Alphabet.Blank, TuringMachine.Direction.L),
                new TuringTransition(q[9], 'A', q[6], 'A', TuringMachine.Direction.R),
                new TuringTransition(q[9], 'B', q[6], 'B', TuringMachine.Direction.R)
            };
            var m = new TuringMachine(q, Alphabet.Ab, g, tf, q[0]);
            
            Assert.True(m.Run("aa"));
            Assert.True(m.Run("bb"));
            Assert.True(m.Run("aaabbbaaabbb"));
            Assert.True(m.Run("ababbababb"));
            Assert.True(m.Run("babbab"));
            Assert.True(m.Run("abab"));
            Assert.False(m.Run("babb"));
            Assert.False(m.Run("abb"));
            Assert.False(m.Run("a"));
            Assert.False(m.Run("b"));
        }
    }
}