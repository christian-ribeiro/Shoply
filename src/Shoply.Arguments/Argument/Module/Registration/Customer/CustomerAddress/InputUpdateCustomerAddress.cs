using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateCustomerAddress(string publicPlace, string number, string? complement, string neighborhood, string postalCode, string? reference, string? observation) : BaseInputUpdate<InputUpdateCustomerAddress>
{
    public string PublicPlace { get; set; } = publicPlace;
    public string Number { get; set; } = number;
    public string? Complement { get; set; } = complement;
    public string Neighborhood { get; set; } = neighborhood;
    public string PostalCode { get; set; } = postalCode;
    public string? Reference { get; set; } = reference;
    public string? Observation { get; set; } = observation;
}