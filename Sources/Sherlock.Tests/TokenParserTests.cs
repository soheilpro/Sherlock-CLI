using System;
using Xunit;

namespace Sherlock.Tests
{
    public class TokenParserTests
    {
        [Fact]
        public void SingleValue()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("x");
            var expected = new[] { "x" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LeadingAndTrailingSpaces()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse(" x ");
            var expected = new[] { "x" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TwoValues()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("x y");
            var expected = new[] { "x", "y" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LeadingTrailingAndInsideSpaces()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse(" x  y ");
            var expected = new[] { "x", "y" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoubleQuotedValue()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x a\"");
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SingleQuotedValue()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("'x a'");
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoubleAndSingleQuotedValues()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x a\" 'y b'"); // "x a" 'y b'
            var expected = new[] { "x a", "y b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MixedQuotes()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"'x a'\" '\"y\" b'"); // "'x a'" '"y" b'
            var expected = new[] { "'x a'", "\"y\" b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuotesWithNoSpaces()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x a\"\"y b\""); // "x a""y b"
            var expected = new[] { "x ay b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MixedQuotesWithNoSpaces()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x a\"'y b'"); // "x a"'y b'
            var expected = new[] { "x ay b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuotesAndValuesWithNoSpaces()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x\"z\"y\""); // "x "a" y"
            var expected = new[] { "xzy" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnclosedDoubleQuote()
        {
            var tokenParser = new TokenParser();

            Assert.Throws<TokenParser.UnclosedQuotationMarkException>(() => tokenParser.Parse("\"a"));
        }

        [Fact]
        public void UnclosedDoubleQuoteIngnored()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("\"x a", ignoreUnclosedQuotes: true); // "x a
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnclosedSingleQuote()
        {
            var tokenParser = new TokenParser();

            Assert.Throws<TokenParser.UnclosedQuotationMarkException>(() => tokenParser.Parse("'a"));
        }

        [Fact]
        public void UnclosedSingleQuoteIgnored()
        {
            var tokenParser = new TokenParser();
            var actual = tokenParser.Parse("'x a", ignoreUnclosedQuotes: true); // 'x a
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }
    }
}
