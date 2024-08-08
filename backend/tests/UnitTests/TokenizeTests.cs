using Byndyusoft.Core;
using Byndyusoft.Core.Services.Abstract;
using Byndyusoft.Core.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests;

public class TokenizeTests
{
    private readonly ITokenizeService tokenizeService;

    public TokenizeTests()
    {
        var services = new ServiceCollection();

        services.AddServices();
        var _serviceProvider = services.BuildServiceProvider();
        tokenizeService = _serviceProvider.GetService<ITokenizeService>();
    }

    [Theory]
    [InlineData("8 + 4", new[] { "8", "+", "4" })]
    [InlineData("3 * (2 + 5)", new[] { "3", "*", "(", "2", "+", "5", ")" })]
    [InlineData("(7 - 2) * 4", new[] { "(", "7", "-", "2", ")", "*", "4" })]
    [InlineData("10 / (2 + 3) - 1", new[] { "10", "/", "(", "2", "+", "3", ")", "-", "1" })]
    [InlineData("5 * 6 / (2 + 4) - 3", new[] { "5", "*", "6", "/", "(", "2", "+", "4", ")", "-", "3" })]
    [InlineData("(1 + 2) * (3 - 4)", new[] { "(", "1", "+", "2", ")", "*", "(", "3", "-", "4", ")" })]
    [InlineData("3.5 * (2.1 + 1.2)", new[] { "3.5", "*", "(", "2.1", "+", "1.2", ")" })]
    [InlineData("2 ^ 3", new[] { "2", "^", "3" })]
    [InlineData("5 ^ 2 * 3", new[] { "5", "^", "2", "*", "3" })]
    [InlineData("(2 ^ 3) + 4", new[] { "(", "2", "^", "3", ")", "+", "4" })]
    [InlineData("3 * (2 ^ 2) - 1", new[] { "3", "*", "(", "2", "^", "2", ")", "-", "1" })]
    [InlineData("10 / (5 ^ 2) + 3", new[] { "10", "/", "(", "5", "^", "2", ")", "+", "3" })]
    [InlineData("(2 + 3) ^ 2", new[] { "(", "2", "+", "3", ")", "^", "2" })]
    [InlineData("3.5 * (2 ^ 1.5)", new[] { "3.5", "*", "(", "2", "^", "1.5", ")" })]
    [InlineData("2 ^ (3 + 1)", new[] { "2", "^", "(", "3", "+", "1", ")" })]
    [InlineData("(3 ^ 2) * (2 ^ 3)", new[] { "(", "3", "^", "2", ")", "*", "(", "2", "^", "3", ")" })]
    [InlineData("((2 ^ 2) - 1) ^ 2", new[] { "(", "(", "2", "^", "2", ")", "-", "1", ")", "^", "2" })]
    public void TokenizeMathExpressions_TokenizeCorrect(string expression, string[] tokens)
    {
        var tokensFromMethod = tokenizeService.TokenizeMathExpression(expression).ToArray();
        var isArraysEquals = IsArraysEquals(tokensFromMethod, tokens);
        Assert.True(isArraysEquals);
    }

    private bool IsArraysEquals(string[] a, string[] b)
    {
        if (a.Length != b.Length) return false;

        for(int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}