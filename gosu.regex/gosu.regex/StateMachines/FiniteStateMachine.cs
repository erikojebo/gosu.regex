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
            return IsMatch(input, _states.First());
        }

        private bool IsMatch(IEnumerable<char> input, State currentState)
        {
            if (!input.Any())
                return currentState.IsAccepting;

            var currentChar = input.First();

            if (currentState.AcceptsInput(currentChar))
                return IsMatch(input.Skip(1), currentState.NextState(currentChar));

            return false;
        }
    }
}