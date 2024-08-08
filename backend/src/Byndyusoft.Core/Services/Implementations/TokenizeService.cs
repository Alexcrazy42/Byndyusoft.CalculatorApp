using Byndyusoft.Core.Helpers.Abstract;
using Byndyusoft.Core.Services.Abstract;
using System.Text;

namespace Byndyusoft.Core.Services.Implementations;

public class TokenizeService : ITokenizeService
{
    private readonly string operationsPattern;

    public TokenizeService(IOperationHelper opHelper)
    {
        var operations = opHelper.GetOperations();
        StringBuilder sb = new StringBuilder();
        foreach (var operation in operations)
        {
            if (operation.Key == "-")
            {
                sb.Append(@"\-");
            }
            else
            {
                sb.Append(operation.Key);
            }
        }
        operationsPattern = sb.ToString();
    }

    public IReadOnlyCollection<string> TokenizeMathExpression(string input)
    {
        string pattern = @$"([-+]?\d*\.?\d+|[(){operationsPattern}])";
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
        var matches = regex.Matches(input);

        List<string> tokens = new List<string>();
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            tokens.Add(match.Value);
        }

        return tokens.ToList();
    }
}
