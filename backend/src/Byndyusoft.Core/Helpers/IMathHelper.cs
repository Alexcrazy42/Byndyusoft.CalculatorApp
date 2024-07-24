namespace Byndyusoft.Core.Helpers;

public interface IMathHelper
{
    public bool IsOperator(string token);

    public bool IsLeftAssociative(string token);

    public int ComparePrecedence(string token1, string token2);

    public double ApplyOperation(string operation, double operand1, double operand2);
}
