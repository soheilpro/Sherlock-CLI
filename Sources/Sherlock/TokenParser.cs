using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sherlock
{
    internal class TokenParser
    {
        public string[] Parse(string text, char separator = ' ', bool ignoreUnclosedQuotes = false)
        {
            return ParseCore(text, separator, ignoreUnclosedQuotes).ToArray();
        }

        private IEnumerable<string> ParseCore(string text, char separator, bool ignoreUnclosedQuotes)
        {
            var token = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var currentChar = text[i];

                if (currentChar == separator)
                {
                    if (token.Length > 0)
                    {
                        yield return token.ToString();
                        token.Clear();
                    }

                    continue;
                }

                if (currentChar == '\'' || currentChar == '\"')
                {
                    var quoteChar = currentChar;
                    int j;

                    for (j = i + 1; j < text.Length; j++)
                    {
                        currentChar = text[j];

                        if (currentChar == quoteChar)
                        {
                            i = j;
                            break;
                        }

                        token.Append(currentChar);
                    }

                    if (j == text.Length)
                    {
                        if (ignoreUnclosedQuotes)
                            break;
                        else
                            throw new UnclosedQuotationMarkException();
                    }

                    continue;
                }

                token.Append(currentChar);
            }

            if (token.Length > 0)
                yield return token.ToString();
        }

        public class UnclosedQuotationMarkException : Exception
        {
        }
    }
}
