using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateCustomer(long id, InputUpdateCustomer? inputUpdate) : BaseInputIdentityUpdate<InputUpdateCustomer>(id, inputUpdate) { }