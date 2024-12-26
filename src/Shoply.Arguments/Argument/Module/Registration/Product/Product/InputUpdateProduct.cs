using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateProduct(string description, string? barCode, decimal costValue, decimal saleValue, EnumProductStatus status, long? productCategoryId, long measureUnitId, long brandId) : BaseInputUpdate<InputUpdateProduct>
{
    public string Description { get; private set; } = description;
    public string? BarCode { get; private set; } = barCode;
    public decimal CostValue { get; private set; } = costValue;
    public decimal SaleValue { get; private set; } = saleValue;
    public EnumProductStatus Status { get; private set; } = status;
    public long? ProductCategoryId { get; private set; } = productCategoryId;
    public long MeasureUnitId { get; private set; } = measureUnitId;
    public long BrandId { get; private set; } = brandId;
}