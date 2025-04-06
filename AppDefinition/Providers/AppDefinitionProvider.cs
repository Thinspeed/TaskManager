using AppDefinition.Abstractions;

namespace AppDefinition.Providers;

public class AppDefinitionProvider : IAppDefinitionProvider
{
    private readonly Dictionary<Type, OrderedDefinition> _appDefinitions = [];

    public void Add(IAppDefinition definition)
    {
        int count = _appDefinitions.Count;
        
        _appDefinitions.Add(definition.GetType(), new OrderedDefinition()
        {
            Definition = definition,
            Order = count
        });
    }
    
    public bool Contains(Type type)
    {
        return _appDefinitions.ContainsKey(type);
    }

    public IEnumerable<IAppDefinition> GetAppDefinitions()
    {
        return _appDefinitions
            .OrderBy(x => x.Value.Order)
            .Select(x => x.Value.Definition)
            .ToList();
    }

    private struct OrderedDefinition
    {
        public required IAppDefinition Definition { get; init; }
        
        public required int Order { get; init; }
    }
}