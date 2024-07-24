namespace Byndyusoft.Presentation.Contracts.Requests;

public class CalculationRequest
{
    public string Expression { get; set; }

	public CalculationRequest()
	{ }

	public CalculationRequest(string expression)
	{
        Expression = expression;
    }
}
