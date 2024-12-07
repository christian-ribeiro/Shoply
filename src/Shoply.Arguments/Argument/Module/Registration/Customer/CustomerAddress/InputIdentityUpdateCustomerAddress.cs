using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateCustomerAddress(long id, InputUpdateCustomerAddress? inputUpdate) : BaseInputIdentityUpdate<InputUpdateCustomerAddress>(id, inputUpdate) { }