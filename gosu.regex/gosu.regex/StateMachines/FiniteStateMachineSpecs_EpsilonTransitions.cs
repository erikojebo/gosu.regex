using NUnit.Framework;

namespace Gosu.Regex.StateMachines
{
    [TestFixture]
    public class FiniteStateMachine_with_state_for_A_and_then_B_then_C_with_epsilon_transition_from_B_to_start
    {
        private FiniteStateMachine _stateMachine;

        [SetUp]
        public void SetUp()
        {
            var state1 = new State("start");
            var state2 = new State("1");
            var state3 = new State("2");
            var state4 = new State("3") { IsAccepting = true };

            state1.AddEdgeFor('A', state2);
            state2.AddEdgeFor('B', state3);
            state3.AddEdgeFor('C', state4);

            state3.AddFreeEdgeTo(state1);

            _stateMachine = new FiniteStateMachine(state1, state2);
        }

        [Test]
        public void Does_not_accept_empty_input()
        {
            Assert.IsFalse(_stateMachine.IsMatch(""));
        }

        [Test]
        public void Does_not_accept_A()
        {
            Assert.IsFalse(_stateMachine.IsMatch("A"));
        }
        
        [Test]
        public void Does_not_accept_AB()
        {
            Assert.IsFalse(_stateMachine.IsMatch("AB"));
        }

        [Test]
        public void Accepts_ABC()
        {
            Assert.IsTrue(_stateMachine.IsMatch("ABC"));
        }
        
        [Test]
        public void Accepts_ABABC()
        {
            Assert.IsTrue(_stateMachine.IsMatch("ABABC"));
        }
        
        [Test]
        public void Does_not_accept_ABA()
        {
            Assert.IsFalse(_stateMachine.IsMatch("ABA"));
        }
        
        [Test]
        public void Does_not_accept_ABAC()
        {
            Assert.IsFalse(_stateMachine.IsMatch("ABAC"));
        }
        
        [Test]
        public void Does_not_accept_ABCABC()
        {
            Assert.IsFalse(_stateMachine.IsMatch("ABCABC"));
        }

        [Test]
        public void Does_not_accept_ABB()
        {
            Assert.IsFalse(_stateMachine.IsMatch("ABB"));
        }

        [Test]
        public void Does_not_accept_other_character()
        {
            Assert.IsFalse(_stateMachine.IsMatch("D"));
        }
    }
}