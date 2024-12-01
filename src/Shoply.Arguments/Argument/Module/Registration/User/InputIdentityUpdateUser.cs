using Shoply.Arguments.Argument.Base;

namespace Shoply.Arguments.Argument.Module.Registration;

public class InputIdentityUpdateUser : BaseInputIdentityUpdate<InputUpdateUser>
{
    public InputIdentityUpdateUser() { }

    public InputIdentityUpdateUser(long id, InputUpdateUser? inputUpdate) : base(id, inputUpdate) { }
}