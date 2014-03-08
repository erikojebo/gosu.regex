using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex.StateMachines
{
    public class State
    {
        private readonly IList<Edge> _edges = new List<Edge>();

        public bool IsAccepting { get; set; }

        public void AddEdgeFor(char input, State nextState)
        {
            _edges.Add(new Edge(input, nextState));
        }

        public bool AcceptsInput(char input)
        {
            return _edges.Any(e => e.IsMatch(input));
        }

        public State NextState(char input)
        {
            return _edges.First(e => e.IsMatch(input)).NextState;
        }
    }
}