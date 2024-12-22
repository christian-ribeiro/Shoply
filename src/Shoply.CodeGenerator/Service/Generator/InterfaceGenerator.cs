using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class InterfaceGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        GenerateRepository(inputGenerate);
        GenerateService(inputGenerate);
        return true;
    }

    private static void GenerateRepository(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.InterfaceRepository!.Replace("{{TemplateName}}", "IRepository_0"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        WriteFile(GenerateFullPath.InterfaceRepository!, $"I{inputGenerate.EntityName}Repository.cs", template);
    }

    private static void GenerateService(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.InterfaceService!.Replace("{{TemplateName}}", "IService_0"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        WriteFile(GenerateFullPath.InterfaceService!, $"I{inputGenerate.EntityName}Service.cs", template);
    }

    private static void WriteFile(string directoryPath, string fileName, string file)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText($"{directoryPath}\\{fileName}", file);
    }
}