using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityUpdateMeasureUnit(long id, InputUpdateMeasureUnit? inputUpdate) : BaseInputIdentityUpdate<InputUpdateMeasureUnit>(id, inputUpdate) { }