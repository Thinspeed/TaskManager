using System.Reflection;
using AppDefinition.Abstractions;
using AppDefinition.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppDefinition.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static void AddAppDefinitions(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        IEnumerable<Type> appDefinitions = assembly.GetTypes()
            .Where(t => !t.IsAbstract && typeof(IAppDefinition).IsAssignableFrom(t));
        
        var appDefinitionProvider = new AppDefinitionProvider();

        foreach (var definition in appDefinitions)
        {
            builder.AddDefinition(definition, appDefinitionProvider);
        }
        
        builder.Services.AddSingleton<IAppDefinitionProvider>(appDefinitionProvider);
    }

    private static void AddDefinition(this IHostApplicationBuilder builder, Type definition, AppDefinitionProvider handledDefinitions)
    {
        if (handledDefinitions.Contains(definition))
        {
            return;
        }
        
        IAppDefinition instance = Activator.CreateInstance(definition) as IAppDefinition
            ?? throw new Exception($"Failed to create definition of type {definition.FullName}");
        
        foreach (var dependency in instance.DependsOn)
        {
            AddDefinition(builder, dependency, handledDefinitions);
        }
            
        instance.RegisterDefinition(builder);
        handledDefinitions.Add(instance);
    }
}