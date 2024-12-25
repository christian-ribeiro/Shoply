namespace Shoply.CodeGenerator.Classes;

public static class GeneratePath
{
    public static string Controller { get; set; } = Path.Combine(BasePath.Path, "Shoply.Api", "Controller", "Module");
    public static string Arguments { get; set; } = Path.Combine(BasePath.Path, "Shoply.Arguments", "Argument", "Module");
    public static string DTO { get; set; } = Path.Combine(BasePath.Path, "Shoply.Domain", "DTO", "Module");
    public static string InterfaceRepository { get; set; } = Path.Combine(BasePath.Path, "Shoply.Domain", "Interface", "Repository", "Module");
    public static string InterfaceService { get; set; } = Path.Combine(BasePath.Path, "Shoply.Domain", "Interface", "Service", "Module");
    public static string Service { get; set; } = Path.Combine(BasePath.Path, "Shoply.Domain", "Service", "Module");
    public static string Entity { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Persistence", "EFCore", "Entity", "Module");
    public static string Repository { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Persistence", "EFCore", "Repository", "Module");
    public static string MapperEntityDTO { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Mapper", "MapperEntityDTO.cs");
    public static string MapperDTOOutput { get; set; } = Path.Combine(BasePath.Path, "Shoply.Domain", "Mapper", "MapperDTOOutput.cs");
    public static string DbContext { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Persistence", "EFCore", "Context");
    public static string Mapping { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Persistence", "EFCore", "Mapping", "Module");
    public static string UserInheritance { get; set; } = Path.Combine(BasePath.Path, "Shoply.Infrastructure", "Persistence", "EFCore", "Entity", "Module", "Registration", "User", "User.cs");
}