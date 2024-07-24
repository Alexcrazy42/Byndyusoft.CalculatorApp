using Byndyusoft.Core.Exceptions;
using Byndyusoft.Core.Helpers;
using Byndyusoft.Core.Models;
using Byndyusoft.Core.Services.Abstract;
using System.Globalization;

namespace Byndyusoft.Core.Services.Implementations;

public class CalculationService : ICalculationService
{
    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

    private readonly IPreparingExpressionService preparingExpressionService;
    private readonly IMathHelper mathHelper;
    private readonly ITokenizeService tokenizeService;

    public CalculationService(IPreparingExpressionService preparingExpressionService,
        IMathHelper mathHelper,
        ITokenizeService tokenizeService)
    {
        this.preparingExpressionService = preparingExpressionService;
        this.mathHelper = mathHelper;
        this.tokenizeService = tokenizeService;
    }

    public ExpressionResult Calculate(Expression expression)
    {
        var preparedInput = preparingExpressionService.GetPreparedExpression(expression.Input);
        Queue<string> postfix = ShuntingYardAlgorithm(preparedInput);
        var result = EvaluatePostfix(postfix);
        return new ExpressionResult(result);
    }

    private Queue<string> ShuntingYardAlgorithm(string mathExpression)
    {
        Queue<string> outputQueue = new Queue<string>();
        Stack<string> operatorStack = new Stack<string>();

        string[] tokens = tokenizeService.TokenizeMathExpression(mathExpression).ToArray();

        foreach (string token in tokens)
        {
            if (double.TryParse(token, NumberStyles.Float, culture, out double number))
            {
                outputQueue.Enqueue(token);
            }
            else if (mathHelper.IsOperator(token))
            {
                while (operatorStack.Count > 0 && mathHelper.IsOperator(operatorStack.Peek()) &&
                       (mathHelper.IsLeftAssociative(token) && mathHelper.ComparePrecedence(token, operatorStack.Peek()) <= 0 ||
                        mathHelper.ComparePrecedence(token, operatorStack.Peek()) < 0))
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                if (operatorStack.Count == 0)
                {
                    throw new CalculationException("Несогласованные скобки в выражении.");
                }
                operatorStack.Pop();
            }
            else
            {
                throw new CalculationException("Недопустимый токен в выражении: " + token);
            }
        }

        while (operatorStack.Count > 0)
        {
            string topOperator = operatorStack.Peek();
            if (topOperator == "(" || topOperator == ")")
            {
                throw new CalculationException("Несогласованные скобки в выражении.");
            }
            outputQueue.Enqueue(operatorStack.Pop());
        }

        return outputQueue;
    }

    private double EvaluatePostfix(Queue<string> postfix)
    {
        Stack<double> evaluationStack = new Stack<double>();

        while (postfix.Count > 0)
        {
            string token = postfix.Dequeue();

            if (double.TryParse(token, NumberStyles.Float, culture, out double number))
            {
                evaluationStack.Push(number);
            }
            else if (mathHelper.IsOperator(token))
            {
                if (evaluationStack.Count < 2)
                {
                    throw new CalculationException("Недостаточно операндов для оператора " + token);
                }

                double operand2 = evaluationStack.Pop();
                double operand1 = evaluationStack.Pop();
                double result = mathHelper.ApplyOperation(token, operand1, operand2);
                evaluationStack.Push(result);
            }
            else
            {
                throw new CalculationException("Недопустимый токен в постфиксной записи: " + token);
            }
        }

        if (evaluationStack.Count != 1)
        {
            throw new CalculationException("Ошибка вычисления: некорректное количество операндов и операторов.");
        }

        return evaluationStack.Pop();
    }
}