using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    class CharacterClassEdge : EdgeBase
    {
        private readonly RegexCharacterClass _characterClass;

        public CharacterClassEdge(RegexCharacterClass characterClass, State startState, State nextState) : base(startState, nextState)
        {
            _characterClass = characterClass;
        }

        public override bool Accepts(IEnumerable<char> input)
        {
            return _characterClass.Contains(input.First());
        }

        public override IEnumerable<char> Consume(IEnumerable<char> input)
        {
            return input.Skip(1);
        }

        public override string ToString()
        {
            return _characterClass.ToString();
        }
    }
}