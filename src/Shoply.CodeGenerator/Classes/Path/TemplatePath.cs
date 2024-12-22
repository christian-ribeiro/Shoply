namespace Shoply.CodeGenerator.Classes;

public static class TemplatePath
{
    public static string Api { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Controller\\{{TemplateName}}.txt";
    public static string Arguments { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Argument\\{{TemplateName}}.txt";
    public static string DTO { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Domain\\DTO\\{{TemplateName}}.txt";
    public static string InterfaceRepository { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Domain\\Interface\\Repository\\{{TemplateName}}.txt";
    public static string InterfaceService { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Domain\\Interface\\Service\\{{TemplateName}}.txt";
    public static string Service { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Domain\\Service\\{{TemplateName}}.txt";
    public static string Entity { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Infrastructure\\Persistence\\EFCore\\Entity\\{{TemplateName}}.txt";
    public static string Repository { get; private set; } = "{{BasePath}}\\Shoply.CodeGenerator\\Template\\Infrastructure\\Persistence\\EFCore\\Repository\\{{TemplateName}}.txt";
}