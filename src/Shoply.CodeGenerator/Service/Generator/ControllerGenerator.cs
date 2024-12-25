using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class ControllerGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.Controller, "Controller_0.txt"));

        template = template
        .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
        .Replace("{{EntityName}}", inputGenerate.EntityName);

        FileService.WriteFile(GenerateFullPath.Controller!, $"{inputGenerate.EntityName}Controller.cs", template);

        return true;
    }
}