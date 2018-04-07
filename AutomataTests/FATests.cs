using System;
using NUnit.Framework;
using System.Automata;

namespace AutomataTests {
	[TestFixture]
	public class FaTests {
		[Test]
		public void StateSetTest() {
			var s = new States(3);
			Assert.True(s[0].Name == "q0");
			Assert.True(s[1].Name == "q1");
			Assert.True(s[2].Name == "q2");
		}

		[Test]
		public void GetStateTest() {
			var q = new States(3);
			var tf = new TransitionFunction() {
				new Transition(q[0], '1', q[1]),
				new Transition(q[1], '1', q[2]),
				new Transition(q[2], '1', q[0])
			};
			Assert.AreEqual(tf.Get(q[0], '1'), q[1]);
			Assert.AreEqual(tf.Get(q[1], '1'), q[2]);
			Assert.AreEqual(tf.Get(q[2], '1'), q[0]);
		}
		
		[Test]
		public void MultiplesOfThreeTest() {
			var q = new States(3);
			var f = new AcceptingStates(q[0]);
			var tf = new TransitionFunction() {
				new Transition(q[0], '0', q[0]),
				new Transition(q[0], '1', q[1]),
				new Transition(q[1], '1', q[0]),
				new Transition(q[1], '0', q[2]),
				new Transition(q[2], '0', q[1]),
				new Transition(q[2], '1', q[2])
			};
			var m = new FiniteAutomaton(q, Alphabet.Binary, tf, q[0], f);
			
			Assert.True(m.Run("0"));
			Assert.True(m.Run("11"));
			Assert.True(m.Run("110"));
			Assert.True(m.Run("1001"));
			Assert.False(m.Run("1"));
			Assert.False(m.Run("10"));
			Assert.False(m.Run("100"));
			Assert.True(m.Run("1011101110000"));
		}

		[Test]
		public void EvenZeroesTest() {
			var q = new States(2);
			var f = new AcceptingStates(q[0]);
			var tf = new TransitionFunction() {
				new Transition(q[0], '1', q[0]),
				new Transition(q[0], '0', q[1]),
				new Transition(q[1], '0', q[0]),
				new Transition(q[1], '1', q[1])
			};
			var m = new FiniteAutomaton(q, Alphabet.Binary, tf, q[0], f);
			
			Assert.True(m.Run("1111"));
			Assert.True(m.Run("1100"));
			Assert.True(m.Run("10101"));
			Assert.True(m.Run("00"));
			Assert.True(m.Run("10111111110"));
			Assert.False(m.Run("101"));
			Assert.False(m.Run("000"));
			Assert.False(m.Run("11111011111"));
		}
	}
}