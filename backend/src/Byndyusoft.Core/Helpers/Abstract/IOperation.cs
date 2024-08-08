namespace Byndyusoft.Core.Helpers.Abstract;

public interface IOperation
{
    string Symbol { get; }

    double Apply(double operand1, double operand2);

    int Precedence { get; }

    bool IsLeftAssociative { get; }
}
