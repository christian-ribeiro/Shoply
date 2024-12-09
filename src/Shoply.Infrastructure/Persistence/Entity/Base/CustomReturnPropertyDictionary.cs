namespace Shoply.Infrastructure.Persistence.Entity.Base;

public class CustomReturnPropertyDictionary(List<CustomReturnPropertyDictionaryItem> listProperty)
{
    public List<CustomReturnPropertyDictionaryItem> ListProperty { get; private set; } = listProperty;
}