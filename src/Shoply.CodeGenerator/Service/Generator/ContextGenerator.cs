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
            int lastOcurrency = (from i in originalFile where i.Contains("public DbSet") select originalFile.IndexOf(i)).LastOrDefault();
            originalFile.Insert(lastOcurrency + 1, $"    {dbSet}");

            FileService.WriteFile(GenerateFullPath.DbContext!, string.Join(Environment.NewLine, originalFile));
        }

        return true;
    }
}