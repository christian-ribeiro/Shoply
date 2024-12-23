using System.Text.Json.Serialization;

namespace Shoply.CodeGenerator.Argument;

[method: JsonConstructor]
public class InputGenerate(EnumDbContext context, EnumModule module, string subPath, string entityName, List<InputGenerateProperty> listPropertyExternal, List<InputGenerateProperty> listPropertyInternal)
{
    public EnumDbContext Context { get; private set; } = context;
    public EnumModule Module { get; private set; } = module;
    public string SubPath { get; private set; } = subPath;
    public string EntityName { get; private set; } = entityName;

    public List<InputGenerateProperty> ListPropertyExternal { get; private set; } = listPropertyExternal;
    public List<InputGenerateProperty> ListPropertyInternal { get; private set; } = listPropertyInternal;
}