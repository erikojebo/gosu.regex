using System.Collections.Generic;
using System.Linq;
using Gosu.Regex.StateMachines;

namespace Gosu.Regex
{
    public class Regex
    {
        private readonly FiniteStateMachine _stateMachine;
        private readonly IList<State> _states = new List<State>();

        public Regex(IEnumerable<char> expression) : this(string.Join("", expression))
        {
        }

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
                var currentChar = expression[index];

                Regex innerExpression = null;

                if (currentChar == '(')
                {
                    var parenthesizedExpression = ConsumeTo(')', chars, index).ToList();
                    var innerExpressionChars = parenthesizedExpression.Skip(1).Take(parenthesizedExpression.Count - 2);

                    innerExpression = new Regex(innerExpressionChars);

                    index += parenthesizedExpression.Count;
                    currentChar = expression[index];
                }

                var isLastChar = index == chars.Length - 1;

                if (currentChar == '\\')
                {
                    isInEscapedState = true;
                    continue;
                }

                if (currentChar == '[')
                {
                    currentCharacterClassDefinition = ConsumeTo(']', chars, index);

                    index += currentCharacterClassDefinition.Count - 1;
                }

                if (currentChar == '*' && !isInEscapedState)
                {
                    if (innerExpression != null)
                    {
                        var innerStartState = innerExpression._states.First();
                        var innerEndStates = innerExpression._states.Where(x => x.IsAccepting);

                        foreach (var state in innerExpression._states)
                        {
                            _states.Add(state);
                        }

                        previousState.AddFreeEdgeTo(innerStartState);

                        foreach (var innerEndState in innerEndStates)
                        {
                            innerEndState.AddFreeEdgeTo(previousState);
                            innerEndState.IsAccepting = false;
                        }

                        previousState.IsAccepting = isLastChar;
                    }
                    else
                    {
                        var secondLastState = _states[_states.Count - 2];

                        previousState.AddFreeEdgeTo(secondLastState);

                        secondLastState.IsAccepting = isLastChar;
                        previousState.IsAccepting = isLastChar;

                        previousState = secondLastState;
                    }
                }
                else if (currentChar == '?' && !isInEscapedState)
                {
                    if (innerExpression != null)
                    {
                        var innerStartState = innerExpression._states.First();
                        var innerEndStates = innerExpression._states.Where(x => x.IsAccepting);

                        foreach (var state in innerExpression._states)
                        {
                            _states.Add(state);
                        }

                        previousState.AddFreeEdgeTo(innerStartState);

                        var currentState = new State();

                        _states.Add(currentState);

                        foreach (var innerEndState in innerEndStates)
                        {
                            innerEndState.AddFreeEdgeTo(currentState);
                            innerEndState.IsAccepting = false;
                        }

                        previousState.AddFreeEdgeTo(currentState);

                        previousState = currentState;
                    }
                    else
                    {
                        var secondLastState = _states[_states.Count - 2];

                        secondLastState.AddFreeEdgeTo(previousState);

                        secondLastState.IsAccepting = isLastChar;
                    }
                    previousState.IsAccepting = isLastChar;
                }
                else if (currentChar == '+' && !isInEscapedState)
                {
                    if (innerExpression != null)
                    {
                        var innerStartState = innerExpression._states.First();
                        var innerEndStates = innerExpression._states.Where(x => x.IsAccepting);

                        foreach (var state in innerExpression._states)
                        {
                            _states.Add(state);
                        }

                        previousState.AddFreeEdgeTo(innerStartState);
                        
                        var currentState = new State();

                        _states.Add(currentState);

                        foreach (var innerEndState in innerEndStates)
                        {
                            innerEndState.AddFreeEdgeTo(currentState);
                            innerEndState.IsAccepting = false;
                        }

                        currentState.AddFreeEdgeTo(previousState);

                        previousState = currentState;
                    }
                    else
                    {
                        if (currentCharacterClassDefinition != null)
                            previousState.AddCharacterClassEdgeFor(currentCharacterClassDefinition, previousState);
                        else
                            previousState.AddEdgeFor(chars[index - 1], previousState);
                    }
                    
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

        private static List<char> ConsumeTo(char endChar, IEnumerable<char> chars, int index)
        {
            return chars.Skip(index).TakeWhile(x => x != endChar)
                .Concat(new[] { endChar })
                .ToList();
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