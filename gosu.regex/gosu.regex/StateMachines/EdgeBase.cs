using System.Collections.Generic;

namespace Gosu.Regex.StateMachines
{
    public abstract class EdgeBase
    {
        public readonly State NextState;

        protected EdgeBase(State nextState)
        {
            NextState = nextState;
        }

        public abstract bool Accepts(IEnumerable<char> input);
        
        public abstract IEnumerable<char> Consume(IEnumerable<char> input);

        public bool IsMatch(IEnumerable<char> input)
        {
            return NextState.IsMatch(Consume(input));
        }
    }
}