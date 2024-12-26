using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Base;

namespace Shoply.Domain.Interface.Repository.Module.Registration;

public interface IProductImageRepository : IBaseRepository<InputCreateProductImage, InputIdentifierProductImage, OutputProductImage, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO> { }