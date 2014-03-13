using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex
{
    public class RegexCharacterClass
    {
        private readonly IList<char> _characters = new List<char>();
        private readonly bool _isNegated;

        public RegexCharacterClass(string classDefinition)
        {
            var classContent = classDefinition.Substring(1, classDefinition.Length - 2);

            _isNegated = classContent.First() == '^';

            for (int i = 0; i < classContent.Length; i++)
            {
                var currentChar = classContent[i];
                var isFirstChar = i == 0;

                if (currentChar == '-' && !isFirstChar)
                {
                    var rangeStartChar = classContent[i - 1];
                    var rangeEndChar = classContent[i + 1];

                    // Range start char has already been added last iteration of the loop, 
                    // so start from the second char in the range
                    for (int charInRange = rangeStartChar + 1; charInRange <= rangeEndChar; charInRange++)
                    {
                        _characters.Add((char)charInRange);
                    }

                    i++;
                    continue;
                }

                _characters.Add(currentChar);
            }
        }

        public bool Contains(char input)
        {
            return _characters.Contains(input) ^ _isNegated;
        }
    }
}