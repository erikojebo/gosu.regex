using NUnit.Framework;

namespace Gosu.Regex.StateMachines
{
    [TestFixture]
    public class FiniteStateMachineSpecs
    {
        [Test]
        public void Machine_with_single_accepting_state_accepts_empty_input()
        {
            var state = new State { IsAccepting = true };

            var machine = new FiniteStateMachine(state);

            Assert.IsTrue(machine.IsMatch(""));
        }

        [Test]
        public void Machine_with_starting_state_connected_to_accepting_state_by_edge_for_A_matches_A()
        {
            var state1 = new State();
            var state2 = new State { IsAccepting = true };

            state1.AddEdgeFor('A', state2);

            var machine = new FiniteStateMachine(state1, state2);

            Assert.IsTrue(machine.IsMatch("A"));
        }
        
        [Test]
        public void Machine_with_starting_state_connected_to_accepting_state_by_edge_for_A_does_not_match_B()
        {
            var state1 = new State();
            var state2 = new State { IsAccepting = true };

            state1.AddEdgeFor('A', state2);

            var machine = new FiniteStateMachine(state1, state2);

            Assert.IsFalse(machine.IsMatch("B"));
        }

        [Test]
        public void Machine_where_starting_state_is_not_accepting_does_not_accept_empty_input()
        {
            var state1 = new State();

            var machine = new FiniteStateMachine(state1);

            Assert.IsFalse(machine.IsMatch(""));
        }
    }
}