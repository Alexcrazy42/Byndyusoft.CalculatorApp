using Byndyusoft.Core;
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

        services.AddServices();
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
    [InlineData("2 ^ 3", 8)]
    [InlineData("5 ^ 0", 1)]
    [InlineData("10 ^ 2", 100)]
    [InlineData("2 ^ 4", 16)]
    [InlineData("3 ^ 3", 27)]
    public void Calculate_SimpleOperations_CalculateCorrect(string input, double res)
    {
        var expression = new Expression(input);
        Assert.Equal(calculationService.Calculate(expression).Result, res);
    }

    [Theory]
    [InlineData("8 / 0")]
    [InlineData("8 / (1 - 1)")]
    [InlineData("(3 - 2) / (3 - 3)")]
    [InlineData("(3 ^ 2) / (3 - 3)")]
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
    [InlineData("2 ^ 3 * 4", 32)]
    [InlineData("5 + 3 ^ 2", 14)]
    [InlineData("10 - 2 ^ 3 + 4", 6)]
    [InlineData("2 ^ (3 + 1)", 16)]
    [InlineData("2 ^ 2 * 3 ^ 2", 36)]
    [InlineData("(2 ^ 3 - 4) / 2", 2)]
    [InlineData("2 ^ (4 - 1)", 8)]
    [InlineData("5 * 2 ^ 3", 40)]
    [InlineData("(3 + 2) ^ 2", 25)]
    [InlineData("2 ^ (3 ^ 2)", 512)]
    [InlineData("(2 ^ 2) ^ 2", 16)]

    public void Calculate_WithBrackets_CalculateCorrect(string input, double res)
    {
        var expression = new Expression(input);
        Assert.Equal(calculationService.Calculate(expression).Result, res);
    }
}