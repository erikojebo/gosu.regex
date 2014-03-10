using System.Collections.Generic;

namespace Gosu.Regex.StateMachines
{
    public class FreeEdge : EdgeBase
    {
        public FreeEdge(State nextState) : base(nextState)
        {
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