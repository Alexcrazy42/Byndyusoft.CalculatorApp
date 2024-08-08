using Byndyusoft.Core.Helpers.Abstract;

namespace Byndyusoft.Core.Helpers.Operations;

internal class AdditionOperation : IOperation
{
    public string Symbol => "+";

    public double Apply(double operand1, double operand2) => operand1 + operand2;

    public int Precedence => 1;

    public bool IsLeftAssociative => true;
}
