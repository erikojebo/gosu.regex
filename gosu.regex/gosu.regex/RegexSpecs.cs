﻿using NUnit.Framework;

namespace Gosu.Regex
{
    public class RegexSpecs
    {
        [Test]
        public void Empty_expression_should_match_empty_string()
        {
            "".ShouldMatch("");
        }

        [Test]
        public void Empty_expression_should_not_match_non_empty_input()
        {
            "".ShouldNotMatch("a");
        }

        [Test]
        public void Single_character_expression_should_not_match_empty_string()
        {
            "a".ShouldNotMatch("");
        }
        
        [Test]
        public void Single_character_expression_should_match_same_character()
        {
            "a".ShouldMatch("a");
        }

        [Test]
        public void Expression_string_without_operators_matches_same_string()
        {
            "This is a valid regular expression without operators"
                .ShouldMatch("This is a valid regular expression without operators");
        }

        [Test]
        public void Single_character_with_star_operator_accepts_empty_input()
        {
            "A*".ShouldMatch("");
        }
        
        [Test]
        public void Single_character_with_star_operator_accepts_same_char()
        {
            "A*".ShouldMatch("A");
        }
        
        [Test]
        public void Single_character_with_star_operator_accepts_same_char_repeated_multiple_times()
        {
            "A*".ShouldMatch("AAA");
        }

        [Test]
        public void Character_with_star_operator_followed_by_other_character_accepts_input_consisting_of_only_second_character()
        {
            "A*B".ShouldMatch("B");
        }
        
        [Test]
        public void Character_with_star_operator_followed_by_other_character_accepts_input_consisting_of_first_char_followed_by_second_character()
        {
            "A*B".ShouldMatch("AB");
        }
        
        [Test]
        public void Character_with_star_operator_followed_by_other_character_accepts_input_consisting_of_first_char_repeated_and_then_followed_by_second_character()
        {
            "A*B".ShouldMatch("AAAAB");
        }
        
        [Test]
        public void Character_with_star_operator_followed_by_other_character_should_not_accept_first_character_only()
        {
            "A*B".ShouldNotMatch("A");
        }
    }

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

            Assert.AreEqual(actual, expected);
        }
    }
}