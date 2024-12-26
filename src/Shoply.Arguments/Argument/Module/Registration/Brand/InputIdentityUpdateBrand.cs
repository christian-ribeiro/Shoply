using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateBrand(long id, InputUpdateBrand? inputUpdate) : BaseInputIdentityUpdate<InputUpdateBrand>(id, inputUpdate) { }