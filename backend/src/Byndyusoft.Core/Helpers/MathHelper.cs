using Byndyusoft.Core.Exceptions;

namespace Byndyusoft.Core.Helpers;

public class MathHelper : IMathHelper
{
    public bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    public bool IsLeftAssociative(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    public int ComparePrecedence(string token1, string token2)
    {
        Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                {"+", 1}, {"-", 1},
                {"*", 2}, {"/", 2}
            };

        int precedence1 = precedence.ContainsKey(token1) ? precedence[token1] : 0;
        int precedence2 = precedence.ContainsKey(token2) ? precedence[token2] : 0;

        return precedence1.CompareTo(precedence2);
    }

    public double ApplyOperation(string operation, double operand1, double operand2)
    {
        switch (operation)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            case "*":
                return operand1 * operand2;
            case "/":
                if (operand2 == 0)
                    throw new CalculationException("Деление на ноль.");
                return operand1 / operand2;
            default:
                throw new CalculationException("Недопустимая операция: " + operation);
        }
    }
}
