﻿using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    public class WildcardEdge : EdgeBase
    {
        public WildcardEdge(State startState, State nextState) : base(startState, nextState)
        {
        }

        public override bool Accepts(IEnumerable<char> input)
        {
            return input.Any() && input.First() != '\n';
        }

        public override IEnumerable<char> Consume(IEnumerable<char> input)
        {
            return input.Skip(1);
        }

        public override string ToString()
        {
            return ".";
        }
    }
}