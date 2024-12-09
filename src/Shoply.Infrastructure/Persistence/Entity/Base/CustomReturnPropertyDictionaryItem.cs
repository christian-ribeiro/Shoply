namespace Shoply.Infrastructure.Persistence.Entity.Base;

public class CustomReturnPropertyDictionaryItem(string propertyName)
{
    public string PropertyName { get; private set; } = propertyName;
}