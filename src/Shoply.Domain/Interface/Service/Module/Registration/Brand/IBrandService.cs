using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IBrandService : IBaseService<InputCreateBrand, InputUpdateBrand, InputIdentifierBrand, OutputBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand> { }