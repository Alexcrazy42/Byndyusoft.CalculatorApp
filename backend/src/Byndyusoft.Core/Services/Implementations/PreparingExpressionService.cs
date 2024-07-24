using Byndyusoft.Core.Services.Abstract;
using System.Text;

namespace Byndyusoft.Core.Services.Implementations;

public class PreparingExpressionService : IPreparingExpressionService
{
    public string GetPreparedExpression(string expression)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in expression)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                sb.Append(' ');
                sb.Append(c);
                sb.Append(' ');
            }
            else if (c != ' ')
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}
