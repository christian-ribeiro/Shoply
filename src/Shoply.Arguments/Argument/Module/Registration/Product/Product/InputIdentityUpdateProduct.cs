using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateProduct(long id, InputUpdateProduct? inputUpdate) : BaseInputIdentityUpdate<InputUpdateProduct>(id, inputUpdate) { }