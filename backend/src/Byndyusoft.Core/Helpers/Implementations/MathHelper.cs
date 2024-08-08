using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Helpers.Abstract;

namespace Byndyusoft.Core.Helpers.Implementations;

internal class MathHelper : IMathHelper
{
    public readonly Dictionary<string, IOperation> _operations;

    public MathHelper(IOperationHelper opHelper)
    {
        _operations = opHelper.GetOperations();
    }

    public bool IsOperator(string token)
    {
        return _operations.ContainsKey(token);
    }

    public bool IsLeftAssociative(string token)
    {
        return _operations.ContainsKey(token) && _operations[token].IsLeftAssociative;
    }

    public int ComparePrecedence(string token1, string token2)
    {
        if (!_operations.ContainsKey(token1) || !_operations.ContainsKey(token2))
        {
            return 0;
        }

        var precedence1 = _operations[token1].Precedence;
        var precedence2 = _operations[token2].Precedence;

        return precedence1.CompareTo(precedence2);
    }

    public double ApplyOperation(string operation, double operand1, double operand2)
    {
        if (!_operations.ContainsKey(operation))
        {
            throw new CalculationException("Недопустимая операция: " + operation);
        }

        return _operations[operation].Apply(operand1, operand2);
    }
}
