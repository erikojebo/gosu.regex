﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gosu.Regex.StateMachines;

namespace Gosu.Regex
{
    public class Regex
    {
        private readonly FiniteStateMachine _stateMachine;
        private readonly IList<State> _states = new List<State>();

        public Regex(string expression)
        {
            CreateStates(expression);
            
            _stateMachine = new FiniteStateMachine(_states);
        }

        private void CreateStates(string expression)
        {
            var startingState = new State();

            _states.Add(startingState);

            var previousState = startingState;

            var chars = expression.ToArray();

            for (int index = 0; index < chars.Length; index++)
            {
                var currentChar = expression[index];

                if (currentChar == '*')
                    continue;

                
                if (IsNextChar('*', expression, index))
                {
                    previousState.AddEdgeFor(chars[index], previousState);
                    previousState.IsAccepting = true;
                }
                else
                {
                    var currentState = new State();

                    previousState.AddEdgeFor(currentChar, currentState);

                    _states.Add(currentState);

                    previousState = currentState;
                }
            }

            _states.Last().IsAccepting = true;
        }

        private bool IsNextChar(char c, string expression, int index)
        {
            var nextIndex = index + 1;

            if (nextIndex >= expression.Length)
                return false;

            return expression[nextIndex] == c;
        }

        public bool IsMatch(string input)
        {
            return _stateMachine.IsMatch(input);
        }
    }
}