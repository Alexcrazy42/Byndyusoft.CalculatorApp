namespace Byndyusoft.Core.Services.Abstract;

public interface ITokenizeService
{
    public IReadOnlyCollection<string> TokenizeMathExpression(string input);
}
