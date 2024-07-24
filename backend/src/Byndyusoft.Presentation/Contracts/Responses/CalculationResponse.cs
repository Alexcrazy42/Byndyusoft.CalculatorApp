namespace Byndyusoft.Presentation.Contracts.Responses;

public class CalculationResponse
{
    public double Result { get; private set; }

	public CalculationResponse()
	{ }

	public CalculationResponse(double result)
	{
		Result = result;
	}
}
