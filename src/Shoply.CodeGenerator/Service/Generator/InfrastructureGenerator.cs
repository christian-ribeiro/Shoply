using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class InfrastructureGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        GenerateEntity(inputGenerate);
        GenerateRepository(inputGenerate);
        return true;
    }

    private static void GenerateEntity(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.Entity, "Entity_0.txt"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() select i.GenerateConstructor()).ToList());
        string constructorProperty = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() select i.GenerateConstructorProperty()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor)
            .Replace("{{ConstructorProperties}}", constructorProperty);

        FileService.WriteFile(GenerateFullPath.Entity!, $"{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateRepository(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.Repository, "Repository_0.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.Repository!, $"{inputGenerate.EntityName}Repository.cs", template);
    }

    private static string GenerateProperty(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "    public {{PropertyType}}{{Nullable}} {{PropertyName}} { get; private set; }";
        return template
            .Replace("{{PropertyType}}", inputGenerateProperty.PropertyType.GetMemberValue())
            .Replace("{{Nullable}}", inputGenerateProperty.Nullable ? "?" : "")
            .Replace("{{PropertyName}}", inputGenerateProperty.Name)
            .Replace("{{PropertyNameLower}}", Char.ToLower(inputGenerateProperty.Name[0]).ToString() + inputGenerateProperty.Name[1..]);
    }

    private static string GenerateConstructor(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "{{PropertyType}}{{Nullable}} {{PropertyName}}";

        return template
            .Replace("{{PropertyType}}", inputGenerateProperty.PropertyType.GetMemberValue())
            .Replace("{{Nullable}}", inputGenerateProperty.Nullable ? "?" : "")
            .Replace("{{PropertyName}}", Char.ToLower(inputGenerateProperty.Name[0]).ToString() + inputGenerateProperty.Name[1..]);
    }

    private static string GenerateConstructorProperty(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "        {{PropertyName}} = {{ConstructorPropertyName}};";

        return template
            .Replace("{{PropertyName}}", inputGenerateProperty.Name)
            .Replace("{{ConstructorPropertyName}}", Char.ToLower(inputGenerateProperty.Name[0]).ToString() + inputGenerateProperty.Name[1..]);
    }
}