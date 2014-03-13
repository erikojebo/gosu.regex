using NUnit.Framework;

namespace Gosu.Regex
{
    [TestFixture]
    public class RegexCharacterClassSpecs
    {
        [Test]
        public void Class_with_single_character_matches_same_character()
        {
            var characterClass = new RegexCharacterClass("[a]");

            Assert.IsTrue(characterClass.Contains('a'));
        }

        [Test]
        public void Class_with_single_character_does_not_match_other_character()
        {
            var characterClass = new RegexCharacterClass("[a]");

            Assert.IsFalse(characterClass.Contains('b'));
        }
        
        [Test]
        public void Class_with_multiple_characters_matches_those_characters()
        {
            var characterClass = new RegexCharacterClass("[abc]");

            Assert.IsTrue(characterClass.Contains('a'));
            Assert.IsTrue(characterClass.Contains('b'));
            Assert.IsTrue(characterClass.Contains('c'));
        }

        [Test]
        public void Class_with_multiple_characters_does_not_match_other_character()
        {
            var characterClass = new RegexCharacterClass("[abc]");

            Assert.IsFalse(characterClass.Contains('d'));
        }

        [Test]
        public void Negated_class_with_single_character_does_not_match_same_character()
        {
            var characterClass = new RegexCharacterClass("[^a]");

            Assert.IsFalse(characterClass.Contains('a'));
        }
        
        [Test]
        public void Negated_class_with_single_character_matches_other_character()
        {
            var characterClass = new RegexCharacterClass("[^a]");

            Assert.IsTrue(characterClass.Contains('b'));
        }

        [Test]
        public void Class_with_letter_range_contains_all_characters_in_the_range()
        {
            var characterClass = new RegexCharacterClass("[a-c]");

            Assert.IsTrue(characterClass.Contains('a'));
            Assert.IsTrue(characterClass.Contains('b'));
            Assert.IsTrue(characterClass.Contains('c'));
        }
        
        [Test]
        public void Class_with_letter_range_does_not_contain_character_outside_of_the_range()
        {
            var characterClass = new RegexCharacterClass("[a-c]");

            Assert.IsFalse(characterClass.Contains('d'));
        }
        
        [Test]
        public void Class_with_letter_range_does_not_contain_hyphen_character()
        {
            var characterClass = new RegexCharacterClass("[a-c]");

            Assert.IsFalse(characterClass.Contains('-'));
        }
        
        [Test]
        public void Class_with_range_including_all_uppercase_letters_contains_uppercase_letters()
        {
            var characterClass = new RegexCharacterClass("[A-Z]");

            Assert.IsTrue(characterClass.Contains('A'));
            Assert.IsTrue(characterClass.Contains('B'));
            Assert.IsTrue(characterClass.Contains('Z'));
        }
        
        [Test]
        public void Class_with_range_including_all_uppercase_letters_does_not_contain_lowercase_letters()
        {
            var characterClass = new RegexCharacterClass("[A-Z]");

            Assert.IsFalse(characterClass.Contains('a'));
            Assert.IsFalse(characterClass.Contains('b'));
            Assert.IsFalse(characterClass.Contains('z'));
        }

        [Test]
        public void Class_with_multiple_range_definitions_directly_after_each_other_includes_characters_in_both_ranges()
        {
            var characterClass = new RegexCharacterClass("[A-Z0-9]");

            Assert.IsTrue(characterClass.Contains('A'));
            Assert.IsTrue(characterClass.Contains('B'));
            Assert.IsTrue(characterClass.Contains('Z'));

            Assert.IsTrue(characterClass.Contains('0'));
            Assert.IsTrue(characterClass.Contains('1'));
            Assert.IsTrue(characterClass.Contains('9'));
        }

        [Test]
        public void Class_with_regex_operator_characters_contains_literal_operator_characters()
        {
            var characterClass = new RegexCharacterClass("[*+?.]");

            Assert.IsTrue(characterClass.Contains('*'));
            Assert.IsTrue(characterClass.Contains('+'));
            Assert.IsTrue(characterClass.Contains('?'));
            Assert.IsTrue(characterClass.Contains('.'));
        }
        
        [Test]
        public void Class_with_regex_operator_characters_does_not_contain_other_characters()
        {
            var characterClass = new RegexCharacterClass("[*+?.]");

            Assert.IsFalse(characterClass.Contains('1'));
            Assert.IsFalse(characterClass.Contains('a'));
            Assert.IsFalse(characterClass.Contains(' '));
            Assert.IsFalse(characterClass.Contains('\\'));
        }
    }
}