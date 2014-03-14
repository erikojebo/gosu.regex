using System.Collections.Generic;
using System.Linq;

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

    class CharacterClassEdge : EdgeBase
    {
        private readonly RegexCharacterClass _characterClass;

        public CharacterClassEdge(RegexCharacterClass characterClass, State nextState) : base(nextState)
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
    }
}