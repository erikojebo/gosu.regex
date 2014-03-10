using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    public class Edge : EdgeBase
    {
        public readonly char Input;

        public Edge(char input, State nextState) : base(nextState)
        {
            Input = input;
        }

        public override bool Accepts(IEnumerable<char> input)
        {
            return input.FirstOrDefault() == Input;
        }

        public override IEnumerable<char> Consume(IEnumerable<char> input)
        {
            return input.Skip(1);
        }

        public override string ToString()
        {
            return Input.ToString();
        }
    }
}