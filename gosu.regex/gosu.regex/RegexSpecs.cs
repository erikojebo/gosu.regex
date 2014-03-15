using NUnit.Framework;

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
        public void Single_character_with_star_operator_accepts_same_char_repeated_an_odd_number_of_times()
        {
            "A*".ShouldMatch("AAA");
        }
        
        [Test]
        public void Single_character_with_star_operator_accepts_same_char_repeated_an_even_number_of_times()
        {
            "A*".ShouldMatch("AAAA");
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

        [Test]
        public void Character_with_star_operator_followed_by_other_character_should_accept_second_character_only()
        {
            "A*B".ShouldMatch("B");
        }

        [Test]
        public void Character_with_plus_operator_does_not_accept_empty_input()
        {
            "A+".ShouldNotMatch("");
        }

        [Test]
        public void Character_with_plus_operator_does_not_accept_other_character()
        {
            "A+".ShouldNotMatch("B");
        }

        [Test]
        public void Character_with_plus_operator_accept_single_instance_of_character()
        {
            "A+".ShouldMatch("A");
        }

        [Test]
        public void Character_with_plus_operator_accept_repeated_instances_of_character()
        {
            "A+".ShouldMatch("AAA");
        }

        [Test]
        public void Character_with_plus_operator_followed_by_other_character_accepts_input_consisting_of_first_char_followed_by_second_character()
        {
            "A+B".ShouldMatch("AB");
        }
        
        [Test]
        public void Character_with_plus_operator_followed_by_other_character_accepts_input_consisting_of_first_char_repeated_and_then_followed_by_second_character()
        {
            "A+B".ShouldMatch("AAAAB");
        }
        
        [Test]
        public void Character_with_plus_operator_followed_by_other_character_should_not_accept_first_character_only()
        {
            "A+B".ShouldNotMatch("A");
        }

        [Test]
        public void Character_with_plus_operator_followed_by_other_character_should_not_accept_second_character_only()
        {
            "A+B".ShouldNotMatch("B");
        }

        [Test]
        public void Character_with_question_marks_operator_accepts_empty_input()
        {
            "A?".ShouldMatch("");
        }
        
        [Test]
        public void Character_with_question_marks_operator_does_not_accept_other_character()
        {
            "A?".ShouldNotMatch("B");
        }
        
        [Test]
        public void Character_with_question_marks_operator_accept_single_instance_of_character()
        {
            "A?".ShouldMatch("A");
        }

        [Test]
        public void Character_with_question_marks_operator_does_not_accept_repeated_instances_of_character()
        {
            "A?".ShouldNotMatch("AAA");
        }

        [Test]
        public void Character_with_question_marks_operator_followed_by_other_character_accepts_input_consisting_of_first_char_followed_by_second_character()
        {
            "A?B".ShouldMatch("AB");
        }

        [Test]
        public void Character_with_question_marks_operator_followed_by_other_character_should_not_accept_input_consisting_of_first_char_repeated_and_then_followed_by_second_character()
        {
            "A?B".ShouldNotMatch("AAB");
        }

        [Test]
        public void Character_with_question_marks_operator_followed_by_other_character_should_not_accept_first_character_only()
        {
            "A?B".ShouldNotMatch("A");
        }

        [Test]
        public void Character_with_question_marks_operator_followed_by_other_character_should_accept_second_character_only()
        {
            "A?B".ShouldMatch("B");
        }

        [Test]
        public void Dot_operator_matches_lowercase_letter()
        {
            ".".ShouldMatch("a");
        }

        [Test]
        public void Dot_operator_matches_uppercase_letter()
        {
            ".".ShouldMatch("A");
        }

        [Test]
        public void Dot_operator_matches_digit()
        {
            ".".ShouldMatch("A");
        }

        [Test]
        public void Dot_operator_matches_space_character()
        {
            ".".ShouldMatch(" ");
        }
        
        [Test]
        public void Dot_operator_matches_special_character()
        {
            ".".ShouldMatch("&");
        }

        [Test]
        public void Dot_operator_does_not_match_newline_character()
        {
            ".".ShouldNotMatch("\n");
        }

        [Test]
        public void Dot_operator_does_not_match_empty_string()
        {
            ".".ShouldNotMatch("");
        }

        [Test]
        public void Escaped_dot_followed_by_dot_operator_matches_dot_followed_by_letter()
        {
            "\\..".ShouldMatch(".A");
        }

        [Test]
        public void Escaped_dot_matches_dot_character()
        {
            "\\.".ShouldMatch(".");
        }
        
        [Test]
        public void Escaped_dot_does_not_match_letter()
        {
            "\\.".ShouldNotMatch("A");
        }

        [Test]
        public void Character_followed_by_escaped_question_mark_matches_same_character_followed_by_literal_question_mark()
        {
            "A\\?".ShouldMatch("A?");
        }
        
        [Test]
        public void Character_followed_by_escaped_question_mark_does_not_match_same_character()
        {
            "A\\?".ShouldNotMatch("A");
        }
        
        [Test]
        public void Character_followed_by_escaped_question_mark_does_not_match_empty_string()
        {
            "A\\?".ShouldNotMatch("");
        }

        [Test]
        public void Character_followed_by_escaped_asterisk_matches_same_character_followed_by_literal_asterisk()
        {
            "A\\*".ShouldMatch("A*");
        }

        [Test]
        public void Character_followed_by_escaped_asterisk_does_not_match_same_character()
        {
            "A\\*".ShouldNotMatch("A");
        }

        [Test]
        public void Character_followed_by_escaped_asterisk_does_not_match_empty_string()
        {
            "A\\*".ShouldNotMatch("");
        }

        [Test]
        public void Character_class_should_match_character_in_the_class()
        {
            "[0-9]".ShouldMatch("8");
        }
        
        [Test]
        public void Character_class_with_all_digits_but_one_should_not_match_the_one_remaining_digit()
        {
            "[1-9]".ShouldNotMatch("0");
        }
        
        [Test]
        public void Character_class_with_all_digits_should_not_match_letter()
        {
            "[0-9]".ShouldNotMatch("a");
        }

        [Test]
        public void A_DIGIT_CLASS_star_B_matches_A12B()
        {
            "A[0-9]*B".ShouldMatch("A12B");
        }
        
        [Test]
        public void A_DIGIT_CLASS_star_B_matches_A1B()
        {
            "A[0-9]*B".ShouldMatch("A1B");
        }
        
        [Test]
        public void A_DIGIT_CLASS_star_B_matches_AB()
        {
            "A[0-9]*B".ShouldMatch("AB");
        }

        [Test]
        public void A_DIGIT_CLASS_question_mark_B_matches_A1B()
        {
            "A[0-9]?B".ShouldMatch("A1B");
        }
        
        [Test]
        public void A_DIGIT_CLASS_question_mark_B_does_not_match_A12B()
        {
            "A[0-9]?B".ShouldNotMatch("A12B");
        }

        [Test]
        public void A_DIGIT_CLASS_question_mark_B_matches_AB()
        {
            "A[0-9]?B".ShouldMatch("AB");
        }

        [Test]
        public void A_NEGATED_DIGIT_CLASS_question_mark_B_matches_AZB()
        {
            "A[^0-9]?B".ShouldMatch("AZB");
        }
        
        [Test]
        public void A_NEGATED_DIGIT_CLASS_question_mark_B_matches_AB()
        {
            "A[^0-9]?B".ShouldMatch("AB");
        }

        [Test]
        public void A_DIGIT_CLASS_plus_B_does_not_match_AB()
        {
            "A[0-9]+B".ShouldNotMatch("AB");
        }

        [Test]
        public void A_DIGIT_CLASS_plus_B_matches_A1B()
        {
            "A[0-9]+B".ShouldMatch("A1B");
        }

        [Test]
        public void A_DIGIT_CLASS_plus_B_matches_A12B()
        {
            "A[0-9]+B".ShouldMatch("A12B");
        }

        [Test]
        public void A_NEGATED_DIGIT_CLASS_plus_B_does_not_match_AB()
        {
            "A[^0-9]+B".ShouldNotMatch("AB");
        }

        [Test]
        public void A_NEGATED_DIGIT_CLASS_plus_B_matches_AZB()
        {
            "A[^0-9]+B".ShouldMatch("AZB");
        }
        
        [Test]
        public void A_NEGATED_DIGIT_CLASS_plus_B_matches_A_SPECIAL_CHARS_B()
        {
            "A[^0-9]+B".ShouldMatch("A%&)B");
        }

        [Test]
        public void Char_followed_by_starred_parentheis_accepts_first_char_only()
        {
            "A(BC)*".ShouldMatch("A");
        }
        
        [Test]
        public void Char_followed_by_starred_parentheis_accepts_first_char_followed_by_parenthesized_expression_repeated_once()
        {
            "A(BC)*".ShouldMatch("ABC");
        }
        
        [Test]
        public void Char_followed_by_starred_parentheis_accepts_first_char_followed_by_parenthesized_expression_repeated_twice()
        {
            "A(BC)*".ShouldMatch("ABCBC");
        }

        [Test]
        public void Char_followed_by_starred_parentheis_does_not_accept_first_char_followed_by_only_part_of_the_parenthesized_expression()
        {
            "A(BC)*".ShouldNotMatch("AB");
        }
        
        [Test]
        public void Char_followed_by_starred_parentheis_does_not_accept_first_char_followed_other_chars_not_matching_parenthesized_expression()
        {
            "A(BC)*".ShouldNotMatch("A123");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_plus_operator_does_not_accept_first_char_only()
        {
            "A(BC)+".ShouldNotMatch("A");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_plus_operator_accepts_first_char_followed_by_parenthesized_expression_repeated_once()
        {
            "A(BC)+".ShouldMatch("ABC");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_plus_operator_accepts_first_char_followed_by_parenthesized_expression_repeated_twice()
        {
            "A(BC)+".ShouldMatch("ABCBC");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_plus_operator_does_not_accept_first_char_followed_by_only_part_of_the_parenthesized_expression()
        {
            "A(BC)+".ShouldNotMatch("AB");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_plus_operator_does_not_accept_first_char_followed_other_chars_not_matching_parenthesized_expression()
        {
            "A(BC)+".ShouldNotMatch("A123");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_question_mark_operator_accepts_first_char_only()
        {
            "A(BC)?".ShouldMatch("A");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_question_mark_operator_accepts_first_char_followed_by_parenthesized_expression_repeated_once()
        {
            "A(BC)?".ShouldMatch("ABC");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_question_mark_operator_does_not_accept_first_char_followed_by_parenthesized_expression_repeated_twice()
        {
            "A(BC)?".ShouldNotMatch("ABCBC");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_question_mark_operator_does_not_accept_first_char_followed_by_only_part_of_the_parenthesized_expression()
        {
            "A(BC)?".ShouldNotMatch("AB");
        }

        [Test]
        public void Char_followed_by_parenthesis_with_question_mark_operator_does_not_accept_first_char_followed_other_chars_not_matching_parenthesized_expression()
        {
            "A(BC)?".ShouldNotMatch("A123");
        }
    }
}