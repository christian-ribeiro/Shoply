namespace Shoply.CodeGenerator.Classes;

public static class GeneratePath
{
    public static string Controller { get; set; } = "{{BasePath}}\\Shoply.Api\\Controller\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string Arguments { get; set; } = "{{BasePath}}\\Shoply.Arguments\\Argument\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string DTO { get; set; } = "{{BasePath}}\\Shoply.Domain\\DTO\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string InterfaceRepository { get; set; } = "{{BasePath}}\\Shoply.Domain\\Interface\\Repository\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string InterfaceService { get; set; } = "{{BasePath}}\\Shoply.Domain\\Interface\\Service\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string Service { get; set; } = "{{BasePath}}\\Shoply.Domain\\Service\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string Entity { get; set; } = "{{BasePath}}\\Shoply.Infrastructure\\Persistence\\EFCore\\Entity\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string Repository { get; set; } = "{{BasePath}}\\Shoply.Infrastructure\\Persistence\\EFCore\\Repository\\Module\\{{Module}}\\{{SubPath}}\\";
    public static string MapperEntityDTO { get; set; } = "{{BasePath}}\\Shoply.Infrastructure\\Mapper\\MapperEntityDTO.cs";
    public static string MapperDTOOutput { get; set; } = "{{BasePath}}\\Shoply.Domain\\Mapper\\MapperDTOOutput.cs";
}