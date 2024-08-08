using Byndyusoft.Core.Helpers.Abstract;

namespace Byndyusoft.Core.Helpers.Operations;

internal class PowerOperation : IOperation
{
    public string Symbol => "^";

    public double Apply(double operand1, double operand2)
    {
        return Math.Pow(operand1, operand2);
    }

    public int Precedence => 3;

    public bool IsLeftAssociative => false;
}
