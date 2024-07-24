using AutoMapper;
using Byndyusoft.Presentation.Contracts.Requests;
using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Byndyusoft.Core;
using Byndyusoft.Core.Services.Abstract;

namespace Byndyusoft.Presentation;

public class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();
        services.AddServices();
        services.AddAutoMapper(typeof(Program).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        var calculatorService = serviceProvider.GetRequiredService<ICalculationService>();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        while (true)
        {
            Console.WriteLine("Введите выражение (для выхода введите 'exit'):");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                var calculationRequest = new CalculationRequest(input);
                var expression = mapper.Map<Expression>(calculationRequest);
                var result = calculatorService.Calculate(expression).Result;
                Console.WriteLine("Результат: " + result);
            }
            catch (CalculationException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.WriteLine();
        }
    }
}
