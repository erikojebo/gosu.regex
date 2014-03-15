using System.Collections.Generic;
using System.Linq;

namespace Gosu.Regex
{
    public class RegexCharacterClass
    {
        private readonly IEnumerable<char> _characters;
        private readonly bool _isNegated;
        private string _classDefinition;

        private RegexCharacterClass(IEnumerable<char> classDefinition, IEnumerable<char> characters, bool isNegated)
        {
            _classDefinition = string.Join("", classDefinition);
            _characters = characters;
            _isNegated = isNegated;
        }

        public bool Contains(char input)
        {
            return _characters.Contains(input) ^ _isNegated;
        }

        public override string ToString()
        {
            return _classDefinition;
        }

        public static RegexCharacterClass Parse(IEnumerable<char> classDefinition)
        {
            var characters = new List<char>();
            var isNegated = false;

            var length = classDefinition.Count();

            var classContent = classDefinition.Skip(1).Take(length - 2).ToList();

            // If the first character is a negation operator, skip it so that it is not included in the actual
            // character class
            if (classContent.First() == '^')
            {
                isNegated = true;
                classContent = classContent.Skip(1).ToList();
            }
            
            for (int i = 0; i < classContent.Count; i++)
            {
                char? previousChar = i > 0 ? classContent[i - 1] : (char?)null;
                var currentChar = classContent[i];
                var isFirstChar = i == 0;

                var isEscaped = previousChar == '\\';

                if (currentChar == '-' && !isFirstChar && !isEscaped)
                {
                    var rangeStartChar = classContent[i - 1];
                    var rangeEndChar = classContent[i + 1];

                    // Range start char has already been added last iteration of the loop, 
                    // so start from the second char in the range
                    for (int charInRange = rangeStartChar + 1; charInRange <= rangeEndChar; charInRange++)
                    {
                        characters.Add((char)charInRange);
                    }

                    i++;
                    continue;
                }

                characters.Add(currentChar);
            }

            return new RegexCharacterClass(classDefinition, characters, isNegated);
        }
    }
}