using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class DTOGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        GenerateAuxiliaryDTO(inputGenerate);
        GenerateDTO(inputGenerate);
        GenerateExternalDTO(inputGenerate);
        GenerateInternalDTO(inputGenerate);
        GeneratePropertyValidateDTO(inputGenerate);
        GenerateValidateDTO(inputGenerate);
        return true;
    }

    private static void GenerateAuxiliaryDTO(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, "AuxiliaryPropertiesDTO.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.DTO!, $"AuxiliaryProperties{inputGenerate.EntityName}DTO.cs", template);
    }

    private static void GenerateDTO(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, "DTO.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.DTO!, $"{inputGenerate.EntityName}DTO.cs", template);
    }

    private static void GenerateExternalDTO(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, "ExternalPropertiesDTO.txt"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal select i.GenerateConstructor()).ToList());
        string constructorProperty = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal select i.GenerateConstructorProperty()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor)
            .Replace("{{ConstructorProperties}}", constructorProperty);

        FileService.WriteFile(GenerateFullPath.DTO!, $"ExternalProperties{inputGenerate.EntityName}DTO.cs", template);
    }

    private static void GenerateInternalDTO(InputGenerate inputGenerate)
    {
        bool hasInternal = inputGenerate.ListPropertyInternal.Count > 0;

        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, hasInternal ? "InternalPropertiesDTO_1.txt" : "InternalPropertiesDTO_0.txt"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyInternal select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyInternal select i.GenerateConstructor()).ToList());
        string constructorProperty = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyInternal select i.GenerateConstructorProperty()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor)
            .Replace("{{ConstructorProperties}}", constructorProperty);

        FileService.WriteFile(GenerateFullPath.DTO!, $"InternalProperties{inputGenerate.EntityName}DTO.cs", template);
    }

    private static void GeneratePropertyValidateDTO(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, "PropertyValidateDTO.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.DTO!, $"{inputGenerate.EntityName}PropertyValidateDTO.cs", template);
    }

    private static void GenerateValidateDTO(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.DTO, "ValidateDTO.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.DTO!, $"{inputGenerate.EntityName}ValidateDTO.cs", template);
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