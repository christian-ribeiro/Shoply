using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Base;

namespace Shoply.Domain.Interface.Repository.Module.Registration;

public interface IBrandRepository : IBaseRepository<InputCreateBrand, InputUpdateBrand, InputIdentifierBrand, OutputBrand, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO> { }