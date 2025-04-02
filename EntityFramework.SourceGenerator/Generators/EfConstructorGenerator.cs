﻿using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace EntityFramework.SourceGenerator.Generators;

[Generator]
public class EfConstructorGenerator : IIncrementalGenerator
{
    private const string AttributeFileName = "EfConstructorAttribute.g.cs";
    private const string Version = "1.0.0";
    private const string GeneratedFileName = "EfConstructors.g.cs";
    
    private const string EfConstructor = $$"""
                                            // <auto-generated/>

                                            namespace {{SourceGenerationHelper.AttributesNamespace}}
                                            {
                                                [System.AttributeUsage(System.AttributeTargets.Class)]
                                                public sealed class {{nameof(EfConstructor)}}Attribute : System.Attribute
                                                {
                                                }
                                            }
                                            """;
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            AttributeFileName,
            SourceText.From(EfConstructor, Encoding.UTF8)));
        
        IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => SourceGenerationHelper.IsSyntaxTargetClassWithAttribute(s, nameof(EfConstructor)),
                transform: static (ctx, _) => SourceGenerationHelper.GetTargetClassForGeneration(ctx));
        
        IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClasses
            = context.CompilationProvider.Combine(classDeclarations.Collect());
        
        context.RegisterSourceOutput(compilationAndClasses,
                 (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    private void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        
        var builder = new StringBuilder();
        builder.Append("// <auto-generated/>");
         
        foreach (var classSyntax in classes)
        {
            SemanticModel model = compilation.GetSemanticModel(classSyntax.SyntaxTree);
            ISymbol symbol = model.GetDeclaredSymbol(classSyntax)!;
            
            string className = symbol.Name;
            string classNamespace = symbol.ContainingNamespace?.ToDisplayString() ?? string.Empty;

            string generatedClass = $$"""

                                      namespace {{classNamespace}}
                                      {
                                          {{SourceGenerationHelper.GetGeneratedCodeAttribute(nameof(EfConstructorGenerator), Version)}}
                                          partial class {{className}}
                                          {
                                              private {{className}}()
                                              {
                                              }
                                          }
                                      }

                                      """;
             
            builder.Append(generatedClass);
        }
         
        context.AddSource(GeneratedFileName, SourceText.From(builder.ToString(), Encoding.UTF8));
    }
}