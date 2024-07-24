namespace Byndyusoft.Core.Exceptions;

public class CalculationException : Exception
{
    public CalculationException()
    {
    }

    public CalculationException(string message)
        : base(message)
    {
    }

    public CalculationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
