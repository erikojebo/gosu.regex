using System;
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

            var isInEscapedState = false;
            List<char> currentCharacterClassDefinition = null;

            for (int index = 0; index < chars.Length; index++)
            {
                var isLastChar = index == chars.Length - 1;
                var currentChar = expression[index];

                if (currentChar == '\\')
                {
                    isInEscapedState = true;
                    continue;
                }

                if (currentChar == '[')
                {
                    currentCharacterClassDefinition = chars.Skip(index).TakeWhile(x => x != ']')
                        .Concat(new[] { ']' })
                        .ToList();

                    index += currentCharacterClassDefinition.Count - 1;
                }

                if (currentChar == '*' && !isInEscapedState)
                {
                    var secondLastState = _states[_states.Count - 2];

                    previousState.AddFreeEdgeTo(secondLastState);

                    secondLastState.IsAccepting = isLastChar;
                    previousState.IsAccepting = isLastChar;

                    previousState = secondLastState;
                }
                else if (currentChar == '?' && !isInEscapedState)
                {
                    var secondLastState = _states[_states.Count - 2];

                    secondLastState.AddFreeEdgeTo(previousState);

                    secondLastState.IsAccepting = isLastChar;
                    previousState.IsAccepting = isLastChar;
                }
                else if (currentChar == '+' && !isInEscapedState)
                {
                    if (currentCharacterClassDefinition != null)
                        previousState.AddCharacterClassEdgeFor(currentCharacterClassDefinition, previousState);
                    else
                        previousState.AddEdgeFor(chars[index - 1], previousState);

                    previousState.IsAccepting = isLastChar;
                }
                else
                {
                    var currentState = new State();

                    if (currentChar == '.' && !isInEscapedState)
                    {
                        previousState.AddWildcardEdgeTo(currentState);
                    }
                    else if (currentCharacterClassDefinition != null)
                    {
                        previousState.AddCharacterClassEdgeFor(currentCharacterClassDefinition, currentState);
                    }
                    else
                    {
                        previousState.AddEdgeFor(currentChar, currentState);                        
                    }

                    _states.Add(currentState);

                    previousState = currentState;
                }

                isInEscapedState = false;

                if (!IsNextChar('+', expression, index))
                    currentCharacterClassDefinition = null;
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
