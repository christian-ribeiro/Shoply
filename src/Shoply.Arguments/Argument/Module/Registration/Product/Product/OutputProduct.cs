using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputProduct(string code, string description, string? barCode, decimal costValue, decimal saleValue, EnumProductStatus status, long? productCategoryId, long measureUnitId, long brandId, decimal markup, OutputProductCategory? productCategory, OutputMeasureUnit? measureUnit, OutputBrand? brand, List<OutputProductImage>? listProductImage) : BaseOutput<OutputProduct>
{
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public string? BarCode { get; private set; } = barCode;
    public decimal CostValue { get; private set; } = costValue;
    public decimal SaleValue { get; private set; } = saleValue;
    public EnumProductStatus Status { get; private set; } = status;
    public long? ProductCategoryId { get; private set; } = productCategoryId;
    public long MeasureUnitId { get; private set; } = measureUnitId;
    public long BrandId { get; private set; } = brandId;
    public decimal Markup { get; private set; } = markup;
    public OutputProductCategory? ProductCategory { get; private set; } = productCategory;
    public OutputMeasureUnit? MeasureUnit { get; private set; } = measureUnit;
    public OutputBrand? Brand { get; private set; } = brand;
    public List<OutputProductImage>? ListProductImage { get; private set; } = listProductImage;
}