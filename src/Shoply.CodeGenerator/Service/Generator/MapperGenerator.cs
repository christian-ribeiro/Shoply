using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class MapperGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        GenerateEntityDTO(inputGenerate);
        GenerateDTOOutput(inputGenerate);
        return true;
    }

    public static void GenerateEntityDTO(InputGenerate inputGenerate)
    {
        bool hasExternal = inputGenerate.ListPropertyExternal.Count > 0;

        var template = File.ReadAllText(TemplateFullPath.MapperEntityDTO!.Replace("{{TemplateName}}", hasExternal ? "MapperEntityDTO_0" : "MapperEntityDTO_1"));

        string externalProperties = string.Join(", ", (from i in inputGenerate.ListPropertyExternal select i.GenerateProperty()).ToList());
        string internalProperties = string.Join(", ", (from i in inputGenerate.ListPropertyInternal select i.GenerateProperty()).ToList());

        template = template
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{InternalProperties}}", internalProperties)
            .Replace("{{ExternalProperties}}", externalProperties);

        var originalFile = File.ReadAllLines(GenerateFullPath.MapperEntityDTO!).ToList();
        if (!(from i in originalFile where i.Trim() == $"#region {inputGenerate.EntityName}" select i).Any())
        {
            int lastOcurrency = (from i in originalFile select i.Trim()).ToList().LastIndexOf("#endregion");
            originalFile.InsertRange(lastOcurrency + 1, template.Split(Environment.NewLine));

            WriteFile(GenerateFullPath.MapperEntityDTO!, string.Join(Environment.NewLine, originalFile));
        }
    }

    public static void GenerateDTOOutput(InputGenerate inputGenerate)
    {
        bool hasExternal = inputGenerate.ListPropertyExternal.Count > 0;

        var template = File.ReadAllText(TemplateFullPath.MapperDTOOutput!.Replace("{{TemplateName}}", hasExternal ? "MapperDTOOutput_0" : "MapperDTOOutput_1"));

        string externalProperties = string.Join(", ", (from i in inputGenerate.ListPropertyExternal select i.GenerateProperty()).ToList());
        string internalProperties = string.Join(", ", (from i in inputGenerate.ListPropertyInternal select i.GenerateProperty()).ToList());

        template = template
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{InternalProperties}}", internalProperties)
            .Replace("{{ExternalProperties}}", externalProperties);

        var originalFile = File.ReadAllLines(GenerateFullPath.MapperDTOOutput!).ToList();
        if (!(from i in originalFile where i.Trim() == $"#region {inputGenerate.EntityName}" select i).Any())
        {
            int lastOcurrency = (from i in originalFile select i.Trim()).ToList().LastIndexOf("#endregion");
            originalFile.InsertRange(lastOcurrency + 1, template.Split(Environment.NewLine));

            WriteFile(GenerateFullPath.MapperDTOOutput!, string.Join(Environment.NewLine, originalFile));
        }
    }

    private static string GenerateProperty(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "src.{{PropertyName}}";
        return template
            .Replace("{{PropertyName}}", inputGenerateProperty.Name);
    }

    private static void WriteFile(string path, string file)
    {
        File.WriteAllText(path, file);
    }
}