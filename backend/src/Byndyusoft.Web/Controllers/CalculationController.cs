using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Models;
using Byndyusoft.Core.Services.Abstract;
using Byndyusoft.Web.Contracts.Requests;
using Byndyusoft.Web.Contracts.Responses;
using Byndyusoft.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Byndyusoft.Web.Controllers;

[ApiController]
[Route($"{ApiVersionManager.V1Prefix}/calculations")]

public class CalculationController : Controller
{
    private readonly ICalculationService calculationService;

    public CalculationController(ICalculationService calculationService)
    {
        this.calculationService = calculationService;
    }

    [HttpPost]
    public async Task<BaseResponse<CalculationResponse>> GetCalculationResult([FromBody] CalculationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Expression expression = new Expression(request.Expression);
            var result = calculationService.Calculate(expression);
            var calculationResponse = new CalculationResponse(result.Result);
            return new(data: calculationResponse);
        }
        catch (CalculationException ex)
        {
            return new(isError: true, isExceptedError: true, message: ex.Message);
        }
        catch (Exception)
        {
            return new(isError: true, isExceptedError: false, defaultMessage: "В вычислениях что-то пошло не так!");
        }
    }
}
