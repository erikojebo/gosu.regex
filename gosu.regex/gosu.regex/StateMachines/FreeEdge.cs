using System.Collections.Generic;

namespace Gosu.Regex.StateMachines
{
    public class FreeEdge : EdgeBase
    {
        public FreeEdge(State startState, State nextState) : base(startState, nextState)
        {
            if (startState == nextState)
                throw new InvalidStateMachineException("Cannot add epsilon transision from a given state to itself, since that would open up for infinite loops in the state machine");
        }

        public override bool Accepts(IEnumerable<char> input)
        {
            return true;
        }

        public override IEnumerable<char> Consume(IEnumerable<char> input)
        {
            return input;
        }

        public override string ToString()
        {
            return "Free";
        }
    }
}