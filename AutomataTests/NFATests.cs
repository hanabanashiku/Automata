using NUnit.Framework;
using System.Automata;
using NUnit.Framework.Internal;

namespace AutomataTests {
	[TestFixture]
	public class NfaTests {
		
		// The language over {0, 1}* of strings ending in 1
		[Test]
		public void EndsWithOneTest() {
			var q = new States(2);
			var f = new AcceptingStates(q[1]);
			var tf = new NondeterministicTransitionFunction() {
				new Transition(q[0], '0', q[0]),
				new Transition(q[0], '1', q[0]),
				new Transition(q[0], '1', q[1])
			};
			var m = new NondeterministicFiniteAutomaton(q, Alphabet.Binary, tf, q[0], f);
			
			Assert.True(m.Run("1"));
			Assert.True(m.Run("01"));
			Assert.True(m.Run("011010101011"));
			Assert.True(m.Run("000101110001"));
			Assert.False(m.Run("0"));
			Assert.False(m.Run("10"));
			Assert.False(m.Run("10010101100"));
		}

		// The language over {0, 1}* that contains even number of 0s and/or even number of 1's
		[Test]
		public void EvenZeroesOrOnesTest() {
			var q = new States(5);
			var f = new AcceptingStates(q[1], q[3]);
			var tf = new NondeterministicTransitionFunction() {
				new Transition(q[0], Alphabet.EmptyString, q[1]),
				new Transition(q[0], Alphabet.EmptyString, q[3]),
				new Transition(q[1], '1', q[1]),
				new Transition(q[1], '0', q[2]),
				new Transition(q[2], '1', q[2]),
				new Transition(q[2], '0', q[1]),
				new Transition(q[3], '0', q[3]),
				new Transition(q[3], '1', q[4]),
				new Transition(q[4], '0', q[4]),
				new Transition(q[4], '1', q[3])
			};
			var m = new NondeterministicFiniteAutomaton(q, Alphabet.Binary, tf, q[0], f);
			
			Assert.True(m.Run(""));
			Assert.True(m.Run("00"));
			Assert.True(m.Run("11"));
			Assert.True(m.Run("0011"));
			Assert.True(m.Run("00110101100"));
			Assert.True(m.Run("011010111"));
			Assert.False(m.Run("10"));
			Assert.False(m.Run("11101100"));
			Assert.False(m.Run("10001111"));
			
		}

		// The language a(bab)* union a(ba)*
		[Test]
		public void AbabuabaTest() {
			var q = new States(6);
			var f = new AcceptingStates(q[1], q[2]);
			var tf = new NondeterministicTransitionFunction() {
				new Transition(q[0], 'a', q[1]), 
				new Transition(q[1], 'b', q[3]), 
				new Transition(q[3], 'a', q[4]), 
				new Transition(q[4], 'b', q[1]), 
				new Transition(q[0], 'a', q[2]), 
				new Transition(q[2], 'b', q[5]), 
				new Transition(q[5], 'a', q[2])
			};
			var m = new NondeterministicFiniteAutomaton(q, Alphabet.Ab, tf, q[0], f);
			
			Assert.True(m.Run("ababbabbab"));
			Assert.True(m.Run("abab"));
			Assert.True(m.Run("abababa"));
			Assert.True(m.Run("aba"));
			Assert.False(m.Run("ba"));
			Assert.False(m.Run("bbbbb"));
			Assert.False(m.Run("abb"));
			Assert.False(m.Run("b"));
			Assert.False(m.Run(""));
			Assert.False(m.Run(new [] {Alphabet.EmptyString}));
		}
	}
}