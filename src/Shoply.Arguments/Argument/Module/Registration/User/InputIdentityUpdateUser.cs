using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateUser(long id, InputUpdateUser inputUpdate) : BaseInputIdentityUpdate<InputUpdateUser>(id, inputUpdate) { }