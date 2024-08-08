using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Helpers.Abstract;

namespace Byndyusoft.Core.Helpers.Operations;

internal class DivisionOperation : IOperation
{
    public string Symbol => "/";

    public double Apply(double operand1, double operand2)
    {
        if (operand2 == 0)
            throw new CalculationException("Деление на ноль.");
        return operand1 / operand2;
    }

    public int Precedence => 2;

    public bool IsLeftAssociative => true;
}
