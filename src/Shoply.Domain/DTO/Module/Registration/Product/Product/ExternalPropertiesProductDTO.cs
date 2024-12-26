using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesProductDTO : BaseExternalPropertiesDTO<ExternalPropertiesProductDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string? BarCode { get; private set; }
    public decimal CostValue { get; private set; }
    public decimal SaleValue { get; private set; }
    public EnumProductStatus Status { get; private set; }
    public long? ProductCategoryId { get; private set; }
    public long MeasureUnitId { get; private set; }
    public long BrandId { get; private set; }

    public ExternalPropertiesProductDTO() { }

    public ExternalPropertiesProductDTO(string code, string description, string? barCode, decimal costValue, decimal saleValue, EnumProductStatus status, long? productCategoryId, long measureUnitId, long brandId)
    {
        Code = code;
        Description = description;
        BarCode = barCode;
        CostValue = costValue;
        SaleValue = saleValue;
        Status = status;
        ProductCategoryId = productCategoryId;
        MeasureUnitId = measureUnitId;
        BrandId = brandId;
    }
}