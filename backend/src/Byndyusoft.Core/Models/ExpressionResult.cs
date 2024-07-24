namespace Byndyusoft.Core.Models;

public class ExpressionResult
{
    public double Result { get; private set; }

	public ExpressionResult()
	{ }

	public ExpressionResult(double result)
	{
		Result = result;
	}
}
