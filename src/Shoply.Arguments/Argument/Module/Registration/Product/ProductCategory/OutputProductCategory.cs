using Shoply.Arguments.Argument.Base;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputProductCategory(string code, string description, List<OutputProduct>? listProduct) : BaseOutput<OutputProductCategory>
{
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public virtual List<OutputProduct>? ListProduct { get; private set; } = listProduct;
}