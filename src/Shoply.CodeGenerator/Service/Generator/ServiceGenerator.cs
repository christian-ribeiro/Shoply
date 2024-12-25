using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class ServiceGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.Service, "Service_0.txt"));

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.Service!, $"{inputGenerate.EntityName}Service.cs", template);

        return true;
    }
}