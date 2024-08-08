using Byndyusoft.Core.Helpers.Abstract;
using System.Reflection;

namespace Byndyusoft.Core.Helpers.Implementations;

internal class OperationHelper : IOperationHelper
{
    public Dictionary<string, IOperation> GetOperations()
    {
        var operations = new Dictionary<string, IOperation>();

        Type interfaceType = typeof(IOperation);

        Assembly assembly = Assembly.GetExecutingAssembly();

        IEnumerable<Type> implementingTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(interfaceType) && !t.IsAbstract && !t.IsInterface);

        foreach (Type type in implementingTypes)
        {
            IOperation operationInstance = (IOperation)Activator.CreateInstance(type);

            operations[operationInstance.Symbol] = operationInstance;
        }

        return operations;
    }
}
