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
        var template = File.ReadAllText(Path.Combine(TemplatePath.InterfaceRepository, "IRepository_0.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.InterfaceRepository!, $"I{inputGenerate.EntityName}Repository.cs", template);
    }

    private static void GenerateService(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.InterfaceService, "IService_0.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.InterfaceService!, $"I{inputGenerate.EntityName}Service.cs", template);
    }
}