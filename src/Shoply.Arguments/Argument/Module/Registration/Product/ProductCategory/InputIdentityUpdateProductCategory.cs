using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateProductCategory(long id, InputUpdateProductCategory? inputUpdate) : BaseInputIdentityUpdate<InputUpdateProductCategory>(id, inputUpdate) { }