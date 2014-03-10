using System;
using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    public class FiniteStateMachine
    {
        private readonly IEnumerable<State> _states;

        public FiniteStateMachine(params State[] states) : this((IEnumerable<State>)states)
        {
        }
        
        public FiniteStateMachine(IEnumerable<State> states)
        {
            _states = states;
        }

        public bool IsMatch(string input)
        {
            return _states.First().IsMatch(input);
        }
    }
}