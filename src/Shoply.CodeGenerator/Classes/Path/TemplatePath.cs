namespace Shoply.CodeGenerator.Classes;

public static class TemplatePath
{
    public static string Controller { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Controller");
    public static string Arguments { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Argument");
    public static string DTO { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Domain", "DTO");
    public static string InterfaceRepository { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Domain", "Interface", "Repository");
    public static string InterfaceService { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Domain", "Interface", "Service");
    public static string Service { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Domain", "Service");
    public static string Entity { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Infrastructure", "Persistence", "EFCore", "Entity");
    public static string Repository { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Infrastructure", "Persistence", "EFCore", "Repository");
    public static string MapperEntityDTO { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Mapper", "MapperEntityDTO");
    public static string MapperDTOOutput { get; private set; } = Path.Combine(BasePath.Path, "Shoply.CodeGenerator", "Template", "Mapper", "MapperDTOOutput");
}