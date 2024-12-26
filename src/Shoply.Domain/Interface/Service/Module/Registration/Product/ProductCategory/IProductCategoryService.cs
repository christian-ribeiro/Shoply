using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IProductCategoryService : IBaseService<InputCreateProductCategory, InputUpdateProductCategory, InputIdentifierProductCategory, OutputProductCategory, InputIdentityUpdateProductCategory, InputIdentityDeleteProductCategory> { }