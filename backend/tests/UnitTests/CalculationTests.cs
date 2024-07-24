using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Models;
using Byndyusoft.Core.Services.Abstract;
using Byndyusoft.Core.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests;

public class CalculationTests
{
    private readonly ICalculationService calculationService;

    public CalculationTests()
    {
        var services = new ServiceCollection();

        services.AddTransient<ICalculationService, CalculationService>();
        var _serviceProvider = services.BuildServiceProvider();
        calculationService = _serviceProvider.GetService<ICalculationService>();
    }

    [Theory]
    [InlineData("8 + 4", 12)]
    [InlineData("8 - 4", 4)]
    [InlineData("8 * 4", 32)]
    [InlineData("8 / 4", 2)]
    [InlineData("3 / 2", 1.5)]
    [InlineData("3.5 + 2.1", 5.6)]
    [InlineData("2.5 * 1.5", 3.75)]
    [InlineData("7.0 / 2.0", 3.5)]
    public void Calculate_SimpleOperations_CalculateCorrect(string input, double res)
    {
        var expression = new Expression(input);
        Assert.Equal(calculationService.Calculate(expression).Result, res);
    }

    [Theory]
    [InlineData("8 / 0")]
    [InlineData("8 / (1 - 1)")]
    [InlineData("(3 - 2) / (3 - 3)")]
    public void Calculate_ZeroDivision_ThrowCalculationException(string input)
    {
        var expression = new Expression(input);
        Action calculateRes = delegate ()
        {
            calculationService.Calculate(expression);
        };
        Assert.Throws<CalculationException>(calculateRes);
    }

    [Theory]
    [InlineData("(8 + 4) * 2", 24)]
    [InlineData("8 + 4 * 2", 16)]
    [InlineData("(8 - 4) / 2", 2)]
    [InlineData("(8 * 4 + 1) / 3", 11)]
    [InlineData("(10 - (3 * 2)) / 2", 2)]
    [InlineData("(5 + 3) * (7 - 4)", 24)]
    [InlineData("(12 / 3) * (8 - 5)", 12)]
    [InlineData("((8 - 2) * 3) / (10 - 5)", 3.6)]
    [InlineData("((6 + 2) * 4) - (3 * (5 - 2))", 23)]
    [InlineData("(10 - 3 * 2) / (4 - 2)", 2)]

    public void Calculate_WithBrackets_CalculateCorrect(string input, double res)
    {
        var expression = new Expression(input);
        Assert.Equal(calculationService.Calculate(expression).Result, res);
    }
}