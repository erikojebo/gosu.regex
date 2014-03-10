using System;

namespace Gosu.Regex.StateMachines
{
    public class InvalidStateMachineException : Exception
    {
        public InvalidStateMachineException(string message) : base(message)
        {
            
        }
    }
}