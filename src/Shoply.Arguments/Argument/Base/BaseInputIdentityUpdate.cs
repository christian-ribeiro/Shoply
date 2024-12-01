using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Base;

public class BaseInputIdentityUpdate<TInputUpdate> where TInputUpdate : BaseInputUpdate<TInputUpdate>
{
    public long Id { get; private set; }
    public TInputUpdate? InputUpdate { get; private set; }

    public BaseInputIdentityUpdate() { }

    [JsonConstructor]
    public BaseInputIdentityUpdate(long id, TInputUpdate? inputUpdate)
    {
        Id = id;
        InputUpdate = inputUpdate;
    }
}

public class BaseInputIdentityUpdate_0 : BaseInputIdentityUpdate<BaseInputUpdate_0> { }