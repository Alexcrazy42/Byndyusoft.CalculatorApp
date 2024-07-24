using Byndyusoft.Core.Services.Abstract;

namespace Byndyusoft.Core.Services.Implementations;

public class TokenizeService : ITokenizeService
{
    public IReadOnlyCollection<string> TokenizeMathExpression(string input)
    {
        string pattern = @"([-+]?\d*\.?\d+|[()+\-*/])";
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
