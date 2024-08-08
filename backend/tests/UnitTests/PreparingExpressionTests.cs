using Byndyusoft.Core;
using Byndyusoft.Core.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests;

public class PreparingExpressionTests
{
    private readonly IPreparingExpressionService preparingExpressionService;

    public PreparingExpressionTests()
    {
        var services = new ServiceCollection();

        services.AddServices();
        var _serviceProvider = services.BuildServiceProvider();
        preparingExpressionService = _serviceProvider.GetService<IPreparingExpressionService>();
    }

    [Theory]
    [InlineData("8 +4", "8 + 4")]
    [InlineData("3-2", "3 - 2")]
    [InlineData("5*7", "5 * 7")]
    [InlineData("10/2", "10 / 2")]
    [InlineData("1.5+0.5", "1.5 + 0.5")]
    [InlineData("4-1.5", "4 - 1.5")]
    [InlineData("3.7*2.5", "3.7 * 2.5")]
    [InlineData("8.2/4.1", "8.2 / 4.1")]
    [InlineData("6+(2-1)", "6 + (2 - 1)")]
    [InlineData("(8-2)/3", "(8 - 2) / 3")]
    [InlineData("(4.5+1)*2", "(4.5 + 1) * 2")]
    [InlineData("7-(3.5*2)", "7 - (3.5 * 2)")]
    [InlineData("1+(2*(3-1))/4", "1 + (2 * (3 - 1)) / 4")]
    [InlineData("3-(2+1)/(4-2)", "3 - (2 + 1) / (4 - 2)")]
    [InlineData("(1.2+3.4)*(5.6-7.8)", "(1.2 + 3.4) * (5.6 - 7.8)")]
    [InlineData("(8.5 - 2.3) / (1.1 + 0.9)", "(8.5 - 2.3) / (1.1 + 0.9)")]
    [InlineData("((3.5 + 2.1) * 2.5) / 1.5", "((3.5 + 2.1) * 2.5) / 1.5")]
    [InlineData("(10.5 - (3.2 * 2.1)) / 2.4", "(10.5 - (3.2 * 2.1)) / 2.4")]
    [InlineData("(5.3 + 3.1) * (7.6 - 4.2)", "(5.3 + 3.1) * (7.6 - 4.2)")]
    [InlineData("(12.6 / 3.2) * (8.1 - 5.4)", "(12.6 / 3.2) * (8.1 - 5.4)")]
    [InlineData("3+-2", "3 + -2")]
    [InlineData("-3+-2", "-3 + -2")]
    public void PreparingExpressions_PreparingCorrect(string input, string expected)
    {
        var actual = preparingExpressionService.GetPreparedExpression(input);
        Assert.Equal(actual, expected);
    }
}
