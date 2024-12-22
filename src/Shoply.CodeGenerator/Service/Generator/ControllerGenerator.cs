using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class ControllerGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(TemplateFullPath.Controller!.Replace("{{TemplateName}}", "Controller_0"));

        template = template
        .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
        .Replace("{{EntityName}}", inputGenerate.EntityName);

        WriteFile(GenerateFullPath.Controller!, $"{inputGenerate.EntityName}Controller.cs", template);

        return true;
    }

    private static void WriteFile(string directoryPath, string fileName, string file)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText($"{directoryPath}\\{fileName}", file);
    }
}