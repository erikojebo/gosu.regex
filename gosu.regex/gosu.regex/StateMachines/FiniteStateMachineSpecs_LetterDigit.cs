using NUnit.Framework;

namespace Gosu.Regex.StateMachines
{
    [TestFixture]
    public class FiniteStateMachine_accepting_zero_or_more_pairs_of_letter_A_or_B_followed_by_digit_1_or_2
    {
        private FiniteStateMachine _letterDigitMachine;

        [SetUp]
        public void SetUp()
        {
            var letterDigitState1 = new State { IsAccepting = true };
            var letterDigitState2 = new State();

            letterDigitState1.AddEdgeFor('A', letterDigitState2);
            letterDigitState1.AddEdgeFor('B', letterDigitState2);

            letterDigitState2.AddEdgeFor('1', letterDigitState1);
            letterDigitState2.AddEdgeFor('2', letterDigitState1);

            _letterDigitMachine = new FiniteStateMachine(letterDigitState1, letterDigitState2);
        }

        [Test]
        public void Accepts_empty_input()
        {
            Assert.IsTrue(_letterDigitMachine.IsMatch(""));
        }

        [Test]
        public void Does_not_accept_A()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("A"));
        }

        [Test]
        public void Accepts_A1()
        {
            Assert.IsTrue(_letterDigitMachine.IsMatch("A1"));
        }

        [Test]
        public void Accepts_A2()
        {
            Assert.IsTrue(_letterDigitMachine.IsMatch("A2"));
        }
   
        [Test]
        public void Accepts_B1()
        {
            Assert.IsTrue(_letterDigitMachine.IsMatch("B1"));
        }

        [Test]
        public void Accepts_B2()
        {
            Assert.IsTrue(_letterDigitMachine.IsMatch("B2"));
        }

        [Test]
        public void Does_not_accept_C1()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("C1"));
        }
        
        [Test]
        public void Does_not_accept_C2()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("C2"));
        }

        [Test]
        public void Does_not_accept_1C()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("1C"));
        }
        
        [Test]
        public void Does_not_accept_2A()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("2A"));
        }
        
        [Test]
        public void Does_not_accept_1A()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("1A"));
        }
        
        [Test]
        public void Does_not_accept_A1B1B()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("A1B1B"));
        }
        
        [Test]
        public void Accepts_A1B1B2()
        {
            Assert.IsFalse(_letterDigitMachine.IsMatch("A1B1B"));
        }
    }
}