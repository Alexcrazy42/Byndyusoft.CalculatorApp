using Byndyusoft.Core.Helpers;
using Byndyusoft.Core.Services.Abstract;
using Byndyusoft.Core.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Byndyusoft.Core;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ICalculationService, CalculationService>();
        services.AddTransient<IPreparingExpressionService, PreparingExpressionService>();
        services.AddTransient<ITokenizeService, TokenizeService>();
        services.AddTransient<IMathHelper, MathHelper>();
        return services;
    }
}
