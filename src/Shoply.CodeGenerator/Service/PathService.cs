using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class PathService
{
    public static void SetFullPath(InputGenerate inputGenerate)
    {
        GenerateFullPath.Controller = GeneratePath.Controller.ReplacePath(inputGenerate);
        GenerateFullPath.Arguments = GeneratePath.Arguments.ReplacePath(inputGenerate);
        GenerateFullPath.DTO = GeneratePath.DTO.ReplacePath(inputGenerate);
        GenerateFullPath.InterfaceRepository = GeneratePath.InterfaceRepository.ReplacePath(inputGenerate);
        GenerateFullPath.InterfaceService = GeneratePath.InterfaceService.ReplacePath(inputGenerate);
        GenerateFullPath.Service = GeneratePath.Service.ReplacePath(inputGenerate);
        GenerateFullPath.Entity = GeneratePath.Entity.ReplacePath(inputGenerate);
        GenerateFullPath.Repository = GeneratePath.Repository.ReplacePath(inputGenerate);
        GenerateFullPath.MapperEntityDTO = GeneratePath.MapperEntityDTO.ReplacePath(inputGenerate);
        GenerateFullPath.MapperDTOOutput = GeneratePath.MapperDTOOutput.ReplacePath(inputGenerate);
        GenerateFullPath.DbContext = GeneratePath.DbContext.ReplacePath(inputGenerate);

        TemplateFullPath.Controller = TemplatePath.Api.ReplacePath(inputGenerate);
        TemplateFullPath.Arguments = TemplatePath.Arguments.ReplacePath(inputGenerate);
        TemplateFullPath.DTO = TemplatePath.DTO.ReplacePath(inputGenerate);
        TemplateFullPath.InterfaceRepository = TemplatePath.InterfaceRepository.ReplacePath(inputGenerate);
        TemplateFullPath.InterfaceService = TemplatePath.InterfaceService.ReplacePath(inputGenerate);
        TemplateFullPath.Service = TemplatePath.Service.ReplacePath(inputGenerate);
        TemplateFullPath.Entity = TemplatePath.Entity.ReplacePath(inputGenerate);
        TemplateFullPath.Repository = TemplatePath.Repository.ReplacePath(inputGenerate);
        TemplateFullPath.MapperEntityDTO = TemplatePath.MapperEntityDTO.ReplacePath(inputGenerate);
        TemplateFullPath.MapperDTOOutput = TemplatePath.MapperDTOOutput.ReplacePath(inputGenerate);
    }

    private static string ReplacePath(this string path, InputGenerate inputGenerate)
    {
        string basePath = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
        string subPath = !string.IsNullOrEmpty(inputGenerate.SubPath) ? $"{inputGenerate.SubPath}\\{inputGenerate.EntityName}" : $"{inputGenerate.EntityName}";

        return path.Replace("{{BasePath}}", basePath).Replace("{{Module}}", inputGenerate.Module.GetMemberValue()).Replace("{{DbContext}}", inputGenerate.Context.GetMemberValue()).Replace("{{SubPath}}", subPath);
    }
}
