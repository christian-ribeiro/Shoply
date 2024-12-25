namespace Shoply.CodeGenerator.Service;

public static class FileService
{
    public static void WriteFile(string directoryPath, string fileName, string file)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText(Path.Combine(directoryPath, fileName), file);
    }

    public static void WriteFile(string path, string file)
    {
        File.WriteAllText(path, file);
    }

    public static void WriteFile(string path, List<string> file)
    {
        File.WriteAllLines(path, file);
    }
}