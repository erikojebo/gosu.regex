using NUnit.Framework;

namespace Gosu.Regex
{
    [TestFixture]
    public class RegexAcceptanceSpecs
    {
        [Test]
        public void Swedish_phone_number_with_country_code()
        {
            const string phoneNumberExpression = "\\+[0-9]+-[0-9]+";

            phoneNumberExpression.ShouldMatch("+4670-1234567");
            phoneNumberExpression.ShouldNotMatch("070-1234567");
            phoneNumberExpression.ShouldNotMatch("+4670-1234-567");
            phoneNumberExpression.ShouldNotMatch("+4670");
        }

        [Test]
        public void CSharp_identifier()
        {
            const string expression = "[a-zA-Z_][a-zA-Z_0-9]*";

            expression.ShouldMatch("a");
            expression.ShouldMatch("abc");
            expression.ShouldMatch("a_b_c");
            expression.ShouldMatch("m_member");
            expression.ShouldMatch("_member");
            expression.ShouldMatch("__member");
            expression.ShouldMatch("_member1");
            
            expression.ShouldNotMatch("1");
            expression.ShouldNotMatch("1member");
            expression.ShouldNotMatch("_membe&r");
            expression.ShouldNotMatch("mem ber");
        }

        [Test]
        [Ignore("not implemented yet")]
        public void Nested_parenthesis()
        {
            const string expression = "(abc(d(ef)?)*)+";

            expression.ShouldMatch("abcdefdefdddddabc");
            expression.ShouldMatch("abc");
            expression.ShouldMatch("abcabc");
            expression.ShouldMatch("abcddddd");
            expression.ShouldMatch("abcdefddef");

            expression.ShouldNotMatch("");
            expression.ShouldNotMatch("ab");
            expression.ShouldNotMatch("def");
            expression.ShouldNotMatch("abcdede");
        }
    }
}