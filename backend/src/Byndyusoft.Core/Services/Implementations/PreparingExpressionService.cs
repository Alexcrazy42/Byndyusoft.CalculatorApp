using Byndyusoft.Core.Helpers.Abstract;
using Byndyusoft.Core.Services.Abstract;
using System.Text;

namespace Byndyusoft.Core.Services.Implementations;

public class PreparingExpressionService : IPreparingExpressionService
{
    private readonly List<string> operations = new();

    public PreparingExpressionService(IOperationHelper opHelper)
    {
        operations = opHelper.GetOperations()
            .Select(x => x.Key)
            .ToList();
    }


    public string GetPreparedExpression(string expression)
    {
        expression = expression.Replace(" ", "");
        StringBuilder sb = new StringBuilder();

        var isAfterOperation = true;


        foreach (char c in expression)
        {
            var s = c.ToString();
            if (operations.Contains(s))
            {
                if (isAfterOperation)
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(' ');
                    sb.Append(c);
                    sb.Append(' ');
                }
                isAfterOperation = true;
            }
            else if (c != ' ')
            {
                sb.Append(s);
                isAfterOperation = false;

            }
        }

        return sb.ToString();
    }
}
