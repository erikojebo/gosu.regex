namespace Gosu.Regex.StateMachines
{
    public class Edge
    {
        public readonly char Input;
        public readonly State NextState;

        public Edge(char input, State nextState)
        {
            Input = input;
            NextState = nextState;
        }

        public bool IsMatch(char input)
        {
            return input == Input;
        }
    }
}