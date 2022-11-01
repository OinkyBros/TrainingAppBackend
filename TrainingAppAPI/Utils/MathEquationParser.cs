using System.Text.RegularExpressions;

namespace Oinky.TrainingAppAPI.Utils
{
    public class MathExpressionParser
    {
        private enum TokenType
        {
            Number,
            Variable,
            Operator,
            Parenthesis,
            Invalid
        }

        public bool Calculate(string expression, out double result)
        {
            try
            {
                var tokens = Tokenize(expression);
                tokens = ConvertToRPN(tokens);
                result = Calculate(tokens);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = Double.MinValue;
                return false;
            }
        }

        private double Calculate(List<Token> rpn)
        {
            Stack<double> output = new Stack<double>();
            foreach (Token token in rpn)
            {
                switch (token.TokenType)
                {
                    case TokenType.Number:
                        output.Push(double.Parse(token.Value));
                        break;

                    case TokenType.Operator:
                        double tempValue;
                        switch (token.Value)
                        {
                            case "*":
                                output.Push(output.Pop() * output.Pop());
                                break;

                            case "/":
                                tempValue = output.Pop();
                                output.Push(output.Pop() / tempValue);
                                break;

                            case "+":
                                output.Push(output.Pop() + output.Pop());
                                break;

                            case "-":
                                tempValue = output.Pop();
                                output.Push(output.Pop() - tempValue);
                                break;

                            case "^":
                                tempValue = output.Pop();
                                output.Push(Math.Pow(output.Pop(), tempValue));
                                break;
                        }
                        break;
                }
            }

            return output.Pop();
        }

        private TokenType CheckType(string str)
        {
            if (m_availableOperators.ContainsKey(str))
                return TokenType.Operator;
            if (double.TryParse(str, out double value))
                return TokenType.Number;
            if (m_parenthesis.Contains(str))
                return TokenType.Parenthesis;
            return TokenType.Invalid;
        }

        private List<Token> ConvertToRPN(List<Token> tokens)
        {
            Queue<Token> outputQueue = new Queue<Token>();
            Stack<Token> operatorStack = new Stack<Token>();
            foreach (Token token in tokens)
            {
                switch (token.TokenType)
                {
                    case TokenType.Number:
                        outputQueue.Enqueue(token);
                        break;

                    case TokenType.Operator:
                        if (operatorStack.Count == 0)
                            operatorStack.Push(token);
                        else
                        {
                            Operator o1 = m_availableOperators[token.Value];
                            Token operatorToken = operatorStack.Peek();
                            Operator o2 = null;
                            if (operatorToken.TokenType == TokenType.Operator)
                                o2 = m_availableOperators[operatorToken.Value];
                            while (o2 != null && (o2.Precedence > o1.Precedence || ((o2.Precedence == o1.Precedence) && !o1.RightAssociative)))
                            {
                                outputQueue.Enqueue(operatorStack.Pop());
                                if (operatorStack.Count == 0)
                                    break;
                                operatorToken = operatorStack.Peek();
                                if (operatorToken.TokenType == TokenType.Operator)
                                    o2 = m_availableOperators[operatorToken.Value];
                            }
                            operatorStack.Push(token);
                        }
                        break;

                    case TokenType.Parenthesis:
                        if (token.Value == "(")
                            operatorStack.Push(token);
                        else
                        {
                            Token operatorToken = operatorStack.Pop();
                            while (operatorToken.Value != "(")
                            {
                                outputQueue.Enqueue(operatorToken);
                                operatorToken = operatorStack.Pop();
                            }
                        }
                        break;
                }
            }

            while (operatorStack.Count > 0)
                outputQueue.Enqueue(operatorStack.Pop());

            var output = outputQueue.ToList();
            foreach (var item in output)
                Console.Write(item.Value + " ");

            return output;
        }

        private List<Token> Tokenize(string expression)
        {
            List<Token> tokens = new List<Token>();
            string[] formulaSplit = Regex.Split(expression, @"([\+\-*\/()\^{}])").Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (string str in formulaSplit)
            {
                TokenType type = CheckType(str);
                tokens.Add(new Token(tokenType: type, value: str));
                Console.WriteLine("Value: " + str + "; TokenType: " + type);
            }
            return tokens;
        }

        private static Dictionary<string, Operator> m_availableOperators = new Dictionary<string, Operator>()
            {
                {"+" , new Operator("+", 2) },
                {"-" , new Operator("-", 2) },
                {"*" , new Operator("*", 3) },
                {"/" , new Operator("/", 3) },
                {"^" , new Operator("^", 4, true) }
            };

        private List<string> m_parenthesis = new List<string>()
            {
                "(", ")", "{", "}"
            };

        private class Operator
        {
            public string Name { get; }
            public int Precedence { get; }
            public bool RightAssociative { get; } = false;

            public Operator(string name, int precedence, bool rightAssociative = false)
            {
                Name = name;
                Precedence = precedence;
                RightAssociative = rightAssociative;
            }
        }

        private class Token
        {
            public TokenType TokenType { get; }
            public string Value { get; }

            public Token(TokenType tokenType, string value)
            {
                TokenType = tokenType;
                Value = value;
            }
        }
    }
}