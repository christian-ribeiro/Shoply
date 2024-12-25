using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class PathService
{
    public static void SetFullPath(InputGenerate inputGenerate)
    {
        string subPath = Path.Combine(inputGenerate.SubPath, inputGenerate.EntityName);

        GenerateFullPath.Controller = Path.Combine(GeneratePath.Controller, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.Arguments = Path.Combine(GeneratePath.Arguments, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.DTO = Path.Combine(GeneratePath.DTO, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.InterfaceRepository = Path.Combine(GeneratePath.InterfaceRepository, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.InterfaceService = Path.Combine(GeneratePath.InterfaceService, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.Service = Path.Combine(GeneratePath.Service, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.Entity = Path.Combine(GeneratePath.Entity, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.Repository = Path.Combine(GeneratePath.Repository, inputGenerate.Module.GetMemberValue(), subPath);
        GenerateFullPath.DbContext = Path.Combine(GeneratePath.DbContext, $"{inputGenerate.Context.GetMemberValue()}.cs");
        GenerateFullPath.Mapping = Path.Combine(GeneratePath.Mapping, inputGenerate.Module.GetMemberValue(), subPath);
    }
}
