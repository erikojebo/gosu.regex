using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    public class State
    {
        private readonly string _name;
        private readonly IList<EdgeBase> _edges = new List<EdgeBase>();

        public State() : this(null)
        {
            
        }

        public State(string name)
        {
            _name = name;
        }

        public bool IsAccepting { get; set; }

        public void AddEdgeFor(char input, State nextState)
        {
            _edges.Add(new Edge(input, this, nextState));
        }

        public void AddFreeEdgeTo(State nextState)
        {
            _edges.Add(new FreeEdge(this, nextState));
        }

        public void AddWildcardEdgeTo(State nextState)
        {
            _edges.Add(new WildcardEdge(this, nextState));
        }

        public void AddCharacterClassEdgeFor(IEnumerable<char> characterClassDefinition, State nextState)
        {
            var characterClass = RegexCharacterClass.Parse(characterClassDefinition);
            _edges.Add(new CharacterClassEdge(characterClass, this, nextState));
        }

        public bool IsMatch(IEnumerable<char> input)
        {
            if (!input.Any() && IsAccepting)
                return true;

            var matchingEdges = _edges.Where(x => x.Accepts(input));

            var isMatch = matchingEdges.Any(x => x.IsMatch(input));

            return isMatch;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(_name))
                return _name;

            return base.ToString();
        }
    }
}