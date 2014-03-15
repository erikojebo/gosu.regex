using NUnit.Framework;

namespace Gosu.Regex
{
    public static class RegexSpecExtensions
    {
        public static void ShouldMatch(this string expression, string input)
        {
            ShouldMatch(new Regex(expression), input);
        }

        public static void ShouldMatch(this Regex expression, string input)
        {
            AssertMatch(expression, input, true);
        }
        
        public static void ShouldNotMatch(this string expression, string input)
        {
            ShouldNotMatch(new Regex(expression), input);
        }

        public static void ShouldNotMatch(this Regex expression, string input)
        {
            AssertMatch(expression, input, false);
        }

        private static void AssertMatch(Regex expression, string input, bool expected)
        {
            var actual = expression.IsMatch(input);

            Assert.AreEqual(expected, actual);
        }
    }
}