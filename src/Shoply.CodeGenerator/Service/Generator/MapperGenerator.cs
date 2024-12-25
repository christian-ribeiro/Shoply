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

        var template = File.ReadAllText(Path.Combine(TemplatePath.MapperEntityDTO, hasExternal ? "MapperEntityDTO_0.txt" : "MapperEntityDTO_1.txt"));

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

            FileService.WriteFile(GenerateFullPath.MapperEntityDTO!, originalFile);
        }
    }

    public static void GenerateDTOOutput(InputGenerate inputGenerate)
    {
        bool hasExternal = inputGenerate.ListPropertyExternal.Count > 0;

        var template = File.ReadAllText(Path.Combine(TemplatePath.MapperDTOOutput, hasExternal ? "MapperDTOOutput_0.txt" : "MapperDTOOutput_1.txt"));

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

            FileService.WriteFile(GenerateFullPath.MapperDTOOutput!, originalFile);
        }
    }

    private static string GenerateProperty(this InputGenerateProperty inputGenerateProperty)
    {
        string template = "src.{{PropertyName}}";
        return template
            .Replace("{{PropertyName}}", inputGenerateProperty.Name);
    }
}