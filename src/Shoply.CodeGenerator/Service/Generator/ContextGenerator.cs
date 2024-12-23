using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;

namespace Shoply.CodeGenerator.Service;

public static class ContextGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        var originalFile = File.ReadAllLines(GenerateFullPath.DbContext!).ToList();

        string dbSet = $"public DbSet<{inputGenerate.EntityName}>? {inputGenerate.EntityName} {{ get; set; }}";

        if (!(from i in originalFile where i.Trim() == dbSet select i).Any())
        {
            int lastOcurrency = (from i in originalFile select i.Trim()).ToList().LastIndexOf($"#endregion {inputGenerate.Module.GetMemberValue()}");
            originalFile.Insert(lastOcurrency, $"    {dbSet}");

            WriteFile(GenerateFullPath.DbContext!, string.Join(Environment.NewLine, originalFile));
        }

        return true;
    }


    private static void WriteFile(string path, string file)
    {
        File.WriteAllText(path, file);
    }
}