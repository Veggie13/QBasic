using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QBasic.Parsing
{
    public enum TokenType
    {
        Empty,
        Punctuation,
        Identifier,
        IntegerLiteral,
        DecimalLiteral,
        StringLiteral
    }

    public class Token
    {
        public Token(string data, TokenType type)
        {
            Data = data;
            Type = type;
        }

        public string Data { get; private set; }
        public TokenType Type { get; private set; }

        public static readonly Token Empty = new Token("", TokenType.Empty);

        public override string ToString()
        {
            return string.Format("Token( {0} : \"{1}\" )", Type, Data);
        }
    }

    class TokenGenerator
    {
        private Regex _pattern;
        private string _result;
        private TokenType _type;

        public TokenGenerator(string pattern, string result, TokenType type)
        {
            _pattern = new Regex("^" + pattern);
            _result = result;
            _type = type;
        }

        public Token GetNextToken(ref string source)
        {
            var match = _pattern.Match(source);
            if (!match.Success)
            {
                return null;
            }

            source = _pattern.Replace(source, "", 1);
            return new Token(match.Result(_result), _type);
        }
    }

    public static class Tokenizer
    {
        private static readonly TokenGenerator[] TokenGenerators = new[]
        {
            new TokenGenerator(@"'(.*?)[\r]?\n", "$0", TokenType.Empty),
            new TokenGenerator(@"[\r]?\n", "$0", TokenType.Empty),
            new TokenGenerator(@"""(.*?)""", "$1", TokenType.StringLiteral),
            new TokenGenerator(@"[a-zA-Z][_a-zA-Z0-9]*[!#$%&]?", "$0", TokenType.Identifier),
            new TokenGenerator(@"(-)?[0-9]+\.[0-9]+[!#]?", "$0", TokenType.DecimalLiteral),
            new TokenGenerator(@"(-)?[0-9]+[%&]?", "$0", TokenType.IntegerLiteral),
            new TokenGenerator(@"(\+|-|\*|/|\\|%|=|<|<=|>|>=|<>|\(|\)|,)", "$0", TokenType.Punctuation)
        };

        public static IEnumerable<Token> Tokenize(string source)
        {
            string str = source;
            while (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim(' ', '\t');

                var token = TokenGenerators.Select(g => g.GetNextToken(ref str)).FirstOrDefault(t => t != null);
                if (token == null)
                {
                    throw new Exception("Could not parse.");
                }

                yield return token;
            }

            yield return Token.Empty;
        }
    }
}
