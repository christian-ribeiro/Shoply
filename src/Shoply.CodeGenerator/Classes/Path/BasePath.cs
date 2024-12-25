namespace Shoply.CodeGenerator.Classes;

public static class BasePath
{
    public static string Path = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
}