using QBasic.Program;
using QBasic.Program.Expressions;
using QBasic.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QBasic.Parsing
{
    public static class ExpressionParser
    {
        private static readonly IReadOnlyDictionary<char, Binary.Operators> OperatorMap;

        static ExpressionParser()
        {
            OperatorMap = new Dictionary<char, Binary.Operators>()
            {
                { '+', Binary.Operators.Plus },
                { '-', Binary.Operators.Minus },
                { '*', Binary.Operators.Asterisk },
                { '/', Binary.Operators.Slash },
                { '\\', Binary.Operators.Backslash },
                { '%', Binary.Operators.Percent }
            };
        }

        public static Expression Parse(string expressionString)
        {
            var components = new List<string>() { expressionString.Trim() };
            tokenizeStringLiterals(0, components);
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith("\""))
                {
                    continue;
                }
                tokenizeParentheses(i, components);
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith("\""))
                {
                    continue;
                }
                tokenizeBinaryExpression(i, components);
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith("\""))
                {
                    continue;
                }
                tokenizeNumericLiterals(i, components);
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith("\""))
                {
                    continue;
                }
                tokenizeSymbols(i, components);
            }

            var results = Enumerable.Range(0, components.Count).Select(n => (Expression)null).ToList();
            return parseExpression(0, components, results);
        }

        private static readonly Regex StringRegex = new Regex("\"[^\"]*\"");
        private static void tokenizeStringLiterals(int index, List<string> components)
        {
            string parcel = components[index];
            while (StringRegex.IsMatch(parcel))
            {
                var match = StringRegex.Matches(parcel)[0];
                parcel = StringRegex.Replace(parcel, string.Format("{{{0}}}", components.Count), 1);
                components.Add(match.Value);
            }
            components[index] = parcel;
        }

        private static readonly Regex NumericRegex = new Regex(@"[\s]*[^\{\+\-]([\+\-]?)[\s]*([0-9]+(\.[0-9]+)?[!#%&]?)[^\}][\s]*");
        private static void tokenizeNumericLiterals(int index, List<string> components)
        {
            string parcel = " " + components[index] + " ";
            while (NumericRegex.IsMatch(parcel))
            {
                var match = NumericRegex.Matches(parcel)[0];
                parcel = NumericRegex.Replace(parcel, string.Format("{{{0}}}", components.Count), 1);
                if (isRawElement(parcel))
                {
                    components[index] = match.Groups[1].Value + match.Groups[2].Value;
                    return;
                }
                components.Add(match.Groups[1].Value + match.Groups[2].Value);
            }
            components[index] = parcel.Trim();
        }

        private static readonly Regex SymbolRegex = new Regex(@"[\s]*([_a-zA-Z][_a-zA-Z0-9]*[!#\$%&]?)[\s]*");
        private static void tokenizeSymbols(int index, List<string> components)
        {
            string parcel = components[index];
            while (SymbolRegex.IsMatch(parcel))
            {
                var match = SymbolRegex.Matches(parcel)[0];
                parcel = SymbolRegex.Replace(parcel, string.Format("{{{0}}}", components.Count), 1);
                if (isRawElement(parcel))
                {
                    components[index] = match.Groups[1].Value;
                    return;
                }
                components.Add(match.Groups[1].Value);
            }
            components[index] = parcel.Trim();
        }

        private static readonly Regex ParenthesesRegex = new Regex(@"\([\s]*([^\(\)]*)[\s]*\)");
        private static void tokenizeParentheses(int index, List<string> components)
        {
            string parcel = components[index];
            while (ParenthesesRegex.IsMatch(parcel))
            {
                var match = ParenthesesRegex.Matches(parcel)[0];
                parcel = ParenthesesRegex.Replace(parcel, string.Format("{{{0}}}", components.Count), 1);
                components.Add(match.Groups[1].Value);
            }
            components[index] = parcel;
        }

        private static readonly Regex BinaryRegex = new Regex(@"([^\s]+)[\s]*([\+\-\*/\\%])[\s]*([^\s]+)");
        private static void tokenizeBinaryExpression(int index, List<string> components)
        {
            string parcel = components[index].Trim();
            if (BinaryRegex.IsMatch(parcel))
            {
                var match = BinaryRegex.Matches(parcel)[0];
                string left = match.Groups[1].Value;
                if (!isRawElement(left))
                {
                    left = string.Format("{{{0}}}", components.Count);
                    components.Add(match.Groups[1].Value);
                }
                string right = match.Groups[3].Value;
                if (!isRawElement(right))
                {
                    right = string.Format("{{{0}}}", components.Count);
                    components.Add(match.Groups[3].Value);
                }

                parcel = string.Format("{0}{1}{2}",
                    left,
                    match.Groups[2].Value,
                    right);
            }
            components[index] = parcel;
        }

        private static readonly Regex RawRegex = new Regex(@"^\{[0-9]+\}$");
        private static bool isRawElement(string token)
        {
            return RawRegex.IsMatch(token);
        }

        private static readonly Regex ParseBinaryRegex = new Regex(@"^\{([0-9]+)\}([\+\-\*/\\%])\{([0-9]+)\}$");
        private static readonly Regex ParseStringRegex = new Regex("^\"(.*)\"$");
        private static readonly Regex ParseIntegerRegex = new Regex(@"^([\+\-]?[0-9]+)%$");
        private static readonly Regex ParseLongRegex = new Regex(@"^([\+\-]?[0-9]+)&$");
        private static readonly Regex ParseDoubleRegex = new Regex(@"^([\+\-]?[0-9]+(\.[0-9]+)?)#$");
        private static readonly Regex ParseSingleRegex = new Regex(@"^([\+\-]?[0-9]+(\.[0-9]+)?)[!]?$");
        private static readonly Regex ParseSymbolRegex = new Regex(@"^([_a-zA-Z][_a-zA-Z0-9]*([!#\$%&]?))$");
        private static Expression parseExpression(int component, List<string> components, List<Expression> results)
        {
            if (results[component] != null)
            {
                return results[component];
            }

            string expressionString = components[component];

            if (ParseBinaryRegex.IsMatch(expressionString))
            {
                var match = ParseBinaryRegex.Match(expressionString);
                char op = match.Groups[2].Value[0];
                int leftValue = int.Parse(match.Groups[1].Value);
                int rightValue = int.Parse(match.Groups[3].Value);

                if (results[leftValue] == null)
                {
                    results[leftValue] = parseExpression(leftValue, components, results);
                }
                if (results[rightValue] == null)
                {
                    results[rightValue] = parseExpression(rightValue, components, results);
                }

                return new Binary()
                {
                    Left = results[leftValue],
                    Operator = OperatorMap[op],
                    Right = results[rightValue]
                };
            }
            if (ParseStringRegex.IsMatch(expressionString))
            {
                return new Constant()
                {
                    DataType = Primitives.String,
                    Value = ParseStringRegex.Match(expressionString).Groups[1].Value
                };
            }
            if (ParseIntegerRegex.IsMatch(expressionString))
            {
                return new Constant()
                {
                    DataType = Primitives.Integer,
                    Value = Int16.Parse(ParseIntegerRegex.Match(expressionString).Groups[1].Value)
                };
            }
            if (ParseLongRegex.IsMatch(expressionString))
            {
                return new Constant()
                {
                    DataType = Primitives.Long,
                    Value = Int32.Parse(ParseLongRegex.Match(expressionString).Groups[1].Value)
                };
            }
            if (ParseDoubleRegex.IsMatch(expressionString))
            {
                return new Constant()
                {
                    DataType = Primitives.Double,
                    Value = double.Parse(ParseDoubleRegex.Match(expressionString).Groups[1].Value)
                };
            }
            if (ParseSingleRegex.IsMatch(expressionString))
            {
                return new Constant()
                {
                    DataType = Primitives.Single,
                    Value = float.Parse(ParseSingleRegex.Match(expressionString).Groups[1].Value)
                };
            }
            if (ParseSymbolRegex.IsMatch(expressionString))
            {
                var match = ParseSymbolRegex.Match(expressionString);
                DataType type;
                switch (match.Groups[2].Value)
                {
                    case "$":
                        type = Primitives.String;
                        break;
                    case "%":
                        type = Primitives.Integer;
                        break;
                    case "&":
                        type = Primitives.Long;
                        break;
                    case "#":
                        type = Primitives.Double;
                        break;
                    case "!":
                    case "":
                        type = Primitives.Single;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                return new Variable()
                {
                    DataType = type,
                    Name = match.Groups[1].Value
                };
            }

            throw new ArgumentException();
        }
    }
}
