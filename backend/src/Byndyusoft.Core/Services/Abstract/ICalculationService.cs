using Byndyusoft.Core.Models;

namespace Byndyusoft.Core.Services.Abstract;

public interface ICalculationService
{
    public ExpressionResult Calculate(Expression expression);
}
