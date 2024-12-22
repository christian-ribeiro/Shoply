using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;
using System.ComponentModel;

namespace Shoply.CodeGenerator.Service;

public static class ArgumentGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        GenerateInputCreate(inputGenerate);
        GenerateInputIdentifier(inputGenerate);
        GenerateInputIdentityDelete(inputGenerate);
        GenerateInputIdentityUpdate(inputGenerate);
        GenerateInputUpdate(inputGenerate);
        GenerateOutput(inputGenerate);

        return true;
    }

    private static void GenerateInputCreate(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "InputCreate"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal select i.GenerateConstructor()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor);

        WriteFile(GenerateFullPath.Arguments!, $"InputCreate{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateInputIdentifier(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "InputIdentifier"));
        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() where i.Identifier select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() where i.Identifier select i.GenerateConstructor()).ToList());

        if (string.IsNullOrEmpty(properties))
            return;

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor);

        WriteFile(GenerateFullPath.Arguments!, $"InputIdentifier{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateInputIdentityDelete(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "InputIdentityDelete"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        WriteFile(GenerateFullPath.Arguments!, $"InputIdentityDelete{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateInputIdentityUpdate(InputGenerate inputGenerate)
    {
        bool hasUpdate = (from i in inputGenerate.ListPropertyExternal where i.HasUpdate select i.GenerateProperty()).Any();
        if (!hasUpdate)
            return;

        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "InputIdentityUpdate"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        WriteFile(GenerateFullPath.Arguments!, $"InputIdentityUpdate{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateInputUpdate(InputGenerate inputGenerate)
    {
        bool hasUpdate = (from i in inputGenerate.ListPropertyExternal where i.HasUpdate select i.GenerateProperty()).Any();
        if (!hasUpdate)
            return;

        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "InputUpdate"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal where i.HasUpdate select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal where i.HasUpdate select i.GenerateConstructor()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor);

        WriteFile(GenerateFullPath.Arguments!, $"InputUpdate{inputGenerate.EntityName}.cs", template);
    }

    private static void GenerateOutput(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.Arguments!.Replace("{{TemplateName}}", "Output"));

        string properties = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() select i.GenerateProperty()).ToList());
        string constructor = string.Join(", ", (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal).ToList() select i.GenerateConstructor()).ToList());

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{Properties}}", properties)
            .Replace("{{Constructor}}", constructor);

        WriteFile(GenerateFullPath.Arguments!, $"Output{inputGenerate.EntityName}.cs", template);
    }

    private static string GenerateProperty(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "    public {{PropertyType}}{{Nullable}} {{PropertyName}} { get; private set; } = {{PropertyNameLower}};";
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

    private static void WriteFile(string directoryPath, string fileName, string file)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText($"{directoryPath}\\{fileName}", file);
    }
}