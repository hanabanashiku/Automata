using NUnit.Framework;
using System.Automata;

namespace AutomataTests {
	[TestFixture]
	public class PDATests {
		[Test]
		// {0^n1^n | n >= 0}
		public void ZeroNOneNTest() {
			var q = new States(3);
			var f = new AcceptingStates(q[0], q[2]);
			var tf = new PushdownTransitionFunction() {
				new PushdownTransition(q[0], '0', Alphabet.Z, q[0], $"0{Alphabet.Z}"),
				new PushdownTransition(q[0], '0', '0', q[0], "00"),
				new PushdownTransition(q[0], '1', '0', q[1], new [] {Alphabet.EmptyString}),
				new PushdownTransition(q[1], '1', '0', q[1], new [] {Alphabet.EmptyString}),
				new PushdownTransition(q[1], Alphabet.EmptyString, Alphabet.Z, q[2], new [] {Alphabet.Z})
			};
			var m = new PushdownAutomaton(q, Alphabet.Binary, new StackAlphabet(Alphabet.Binary), tf, q[0],  f);
			
			Assert.True(m.Run(""));
			Assert.True(m.Run("0011"));
			Assert.True(m.Run("01"));
			Assert.True(m.Run("0000011111"));
			Assert.False(m.Run("011010"));
			Assert.False(m.Run("101010001"));
			Assert.False(m.Run("111000"));
		}
	}
}