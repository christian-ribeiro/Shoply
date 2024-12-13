namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Base;

public class CustomReturnPropertyDictionaryItem(string propertyName)
{
    public string PropertyName { get; private set; } = propertyName;
}