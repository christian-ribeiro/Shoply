using Shoply.Arguments.Argument.Base;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputMeasureUnit(string code, string description, List<OutputProduct>? listProduct) : BaseOutput<OutputMeasureUnit>
{
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public virtual List<OutputProduct>? ListProduct { get; private set; } = listProduct;
}