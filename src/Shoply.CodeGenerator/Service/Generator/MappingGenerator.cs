using Shoply.Arguments.Extensions;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Classes;
using System.Collections.Generic;
using System.Text;

namespace Shoply.CodeGenerator.Service;

public static class MappingGenerator
{
    public static bool Generate(InputGenerate inputGenerate)
    {
        var template = File.ReadAllText(Path.Combine(TemplatePath.Mapping, "Mapping.txt"));

        var navigationProperty = BuildNavigationProperty(inputGenerate);
        var listProperty = string.Join(Environment.NewLine, (from i in inputGenerate.ListPropertyExternal.Union(inputGenerate.ListPropertyInternal) select BuildProperty(i)).ToList());
        var inheritanceProperty = BuildInheritanceProperty(inputGenerate);

        template = template
            .Replace("{{Module}}", inputGenerate.Module.GetMemberValue())
            .Replace("{{EntityName}}", inputGenerate.EntityName)
            .Replace("{{DatabaseName}}", inputGenerate.DatabaseName)
            .Replace("{{NavigationProperty}}", navigationProperty)
            .Replace("{{Property}}", listProperty)
            .Replace("{{InheritanceProperty}}", inheritanceProperty);

        IncludeUserInheritanceProperty(inputGenerate);

        FileService.WriteFile(GenerateFullPath.Mapping!, $"{inputGenerate.EntityName}Map.cs", template);

        return true;
    }

    private static string BuildNavigationProperty(InputGenerate inputGenerate)
    {
        bool hasCreate = inputGenerate.ListPropertyExternal.Count > 0;
        bool hasUpdate = inputGenerate.ListPropertyExternal.Any(x => x.HasUpdate);

        StringBuilder property = new();

        if (hasCreate)
            property.AppendLine($"        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUser{inputGenerate.EntityName}).HasForeignKey(x => x.CreationUserId).HasConstraintName(\"fkey_{inputGenerate.DatabaseName}_id_usuario_criacao\").OnDelete(DeleteBehavior.NoAction);");

        if (hasUpdate)
            property.AppendLine($"        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUser{inputGenerate.EntityName}).HasForeignKey(x => x.ChangeUserId).HasConstraintName(\"fkey_{inputGenerate.DatabaseName}_id_usuario_alteracao\").OnDelete(DeleteBehavior.NoAction);");

        return property.ToString();
    }

    private static string BuildProperty(InputGenerateProperty inputGenerateProperty)
    {
        StringBuilder property = new();

        property.AppendLine($"        builder.Property(x => x.{inputGenerateProperty.Name}).HasColumnName(\"{inputGenerateProperty.DatabaseName}\");");

        if (inputGenerateProperty.MaxLength != null)
            property.AppendLine($"        builder.Property(x => x.{inputGenerateProperty.Name}).HasMaxLength({inputGenerateProperty.MaxLength});");

        if (!inputGenerateProperty.Nullable)
            property.AppendLine($"        builder.Property(x => x.{inputGenerateProperty.Name}).IsRequired();");

        return property.ToString();
    }

    private static string BuildInheritanceProperty(InputGenerate inputGenerate)
    {
        bool hasCreate = inputGenerate.ListPropertyExternal.Count > 0;
        bool hasUpdate = inputGenerate.ListPropertyExternal.Any(x => x.HasUpdate);

        StringBuilder builder = new();

        if (hasCreate)
        {
            builder.AppendLine("        builder.Property(x => x.CreationDate).HasColumnName(\"data_criacao\");");
            builder.AppendLine("        builder.Property(x => x.CreationDate).IsRequired();");
            builder.AppendLine();
            builder.AppendLine("        builder.Property(x => x.CreationUserId).HasColumnName(\"id_usuario_criacao\");");
            builder.AppendLine("        builder.Property(x => x.CreationUserId).IsRequired();");
            builder.AppendLine();
        }

        if (hasUpdate)
        {
            builder.AppendLine("        builder.Property(x => x.ChangeDate).HasColumnName(\"data_alteracao\");");
            builder.AppendLine();
            builder.AppendLine("        builder.Property(x => x.ChangeUserId).HasColumnName(\"id_usuario_alteracao\");");
        }

        return builder.ToString();
    }

    private static void IncludeUserInheritanceProperty(InputGenerate inputGenerate)
    {
        bool hasCreate = inputGenerate.ListPropertyExternal.Count > 0;
        bool hasUpdate = inputGenerate.ListPropertyExternal.Any(x => x.HasUpdate);

        if (!hasCreate && !hasUpdate)
            return;

        StringBuilder property = new();

        property.AppendLine($"    #region {inputGenerate.EntityName}");

        if (hasCreate)
            property.AppendLine($"    public virtual List<{inputGenerate.EntityName}>? ListCreationUser{inputGenerate.EntityName} {{ get; private set; }}");

        if (hasUpdate)
            property.AppendLine($"    public virtual List<{inputGenerate.EntityName}>? ListChangeUser{inputGenerate.EntityName} {{ get; private set; }}");

        property.AppendLine("    #endregion");

        var teste = property.ToString();
        var originalFile = File.ReadAllLines(GeneratePath.UserInheritance!).ToList();

        if (!(from i in originalFile where i.Trim() == $"#region {inputGenerate.EntityName}" select i).Any())
        {
            int firstOcurrency = (from i in originalFile where i.Trim().Contains("#region Inheritance") select originalFile.IndexOf(i)).FirstOrDefault();
            originalFile.InsertRange(firstOcurrency + 1, property.ToString().Split(Environment.NewLine, StringSplitOptions.None));
        }

        FileService.WriteFile(GeneratePath.UserInheritance, originalFile);
    }
}