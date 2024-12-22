using System.Text.Json.Serialization;

namespace Shoply.CodeGenerator.Argument;

[method: JsonConstructor]
public class InputGenerateProperty(string name, string databaseName, EnumPropertyType propertyType, bool nullable, bool hasUpdate, bool identifier)
{
    public string Name { get; private set; } = name;
    public string DatabaseName { get; private set; } = databaseName;
    public EnumPropertyType PropertyType { get; private set; } = propertyType;
    public bool Nullable { get; private set; } = nullable;
    public bool HasUpdate { get; private set; } = hasUpdate;
    public bool Identifier { get; private set; } = identifier;
}