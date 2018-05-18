using NUnit.Framework;
using System.Automata;

namespace AutomataTests {
	[TestFixture]
	public class PdaTests {
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

		[Test]
		public void SimplePalTest() {
			var q = new States(3);
			var f = new AcceptingStates(q[2]);
			var tf = new PushdownTransitionFunction() {
				new PushdownTransition(q[0], 'a', Alphabet.Z, q[0], $"a{Alphabet.Z}"),
				new PushdownTransition(q[0], 'b', Alphabet.Z, q[0], $"b{Alphabet.Z}"),
				new PushdownTransition(q[0], 'a', 'a', q[0], "aa"),
				new PushdownTransition(q[0], 'b', 'b', q[0], "bb"),
				new PushdownTransition(q[0], 'b', 'a', q[0], "ba"),
				new PushdownTransition(q[0], 'a', 'b', q[0], "ab"),
				new PushdownTransition(q[0], 'c', 'a', q[1], "a"),
				new PushdownTransition(q[0], 'c', 'b', q[1], "b"),
				new PushdownTransition(q[0], 'c', Alphabet.Z, q[1], $"{Alphabet.Z}"),
				new PushdownTransition(q[1], 'a', 'a', q[1], Alphabet.EmptyString.ToString()),
				new PushdownTransition(q[1], 'b', 'b', q[1], Alphabet.EmptyString.ToString()),
				new PushdownTransition(q[1], Alphabet.EmptyString, Alphabet.Z, q[2], Alphabet.Z.ToString())
				
			};
			var m = new PushdownAutomaton(q, Alphabet.Abc, new StackAlphabet(Alphabet.Abc), tf, q[0], f);
			
			Assert.True(m.Run("aca"));
			Assert.True(m.Run("abacaba"));
			Assert.True(m.Run("c"));
			Assert.True(m.Run("bacab"));
			Assert.False(m.Run("ab"));
			Assert.False(m.Run("babb"));
			Assert.False(m.Run("acab"));
			Assert.False(m.Run("bacba"));
		}
	}
}