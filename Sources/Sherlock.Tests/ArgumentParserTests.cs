using System;
using Xunit;

namespace Sherlock.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SingleValue()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("x");
            var expected = new[] { "x" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LeadingAndTrailingSpaces()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse(" x ");
            var expected = new[] { "x" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TwoValues()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("x y");
            var expected = new[] { "x", "y" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LeadingTrailingAndInsideSpaces()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse(" x  y ");
            var expected = new[] { "x", "y" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoubleQuotedValue()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x a\"");
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SingleQuotedValue()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("'x a'");
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoubleAndSingleQuotedValues()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x a\" 'y b'"); // "x a" 'y b'
            var expected = new[] { "x a", "y b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MixedQuotes()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"'x a'\" '\"y\" b'"); // "'x a'" '"y" b'
            var expected = new[] { "'x a'", "\"y\" b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuotesWithNoSpaces()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x a\"\"y b\""); // "x a""y b"
            var expected = new[] { "x ay b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MixedQuotesWithNoSpaces()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x a\"'y b'"); // "x a"'y b'
            var expected = new[] { "x ay b" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuotesAndValuesWithNoSpaces()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x\"z\"y\""); // "x "a" y"
            var expected = new[] { "xzy" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnclosedDoubleQuote()
        {
            var argumentParser = new ArgumentParser();

            Assert.Throws<ArgumentParser.UnclosedQuotationMarkException>(() => argumentParser.Parse("\"a"));
        }

        [Fact]
        public void UnclosedDoubleQuoteIngnored()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("\"x a", true); // "x a
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnclosedSingleQuote()
        {
            var argumentParser = new ArgumentParser();

            Assert.Throws<ArgumentParser.UnclosedQuotationMarkException>(() => argumentParser.Parse("'a"));
        }

        [Fact]
        public void UnclosedSingleQuoteIgnored()
        {
            var argumentParser = new ArgumentParser();
            var actual = argumentParser.Parse("'x a", true); // 'x a
            var expected = new[] { "x a" };

            Assert.Equal(expected, actual);
        }
    }
}
