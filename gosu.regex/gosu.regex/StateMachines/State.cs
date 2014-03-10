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
            _edges.Add(new Edge(input, nextState));
        }

        public void AddFreeEdgeTo(State nextState)
        {
            if (nextState == this)
                throw new InvalidStateMachineException("Cannot add epsilon transision from a given state to itself, since that would open up for infinite loops in the state machine");

            _edges.Add(new FreeEdge(nextState));
        }

        public bool IsMatch(IEnumerable<char> input)
        {
            if (!input.Any())
                return IsAccepting;
            
            var currentChar = input.First();

            var matchingEdges = _edges.Where(x => x.Accepts(currentChar));

            return matchingEdges.Any(x => x.IsMatch(input));
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(_name))
                return _name;

            return base.ToString();
        }
    }
}