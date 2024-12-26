using AutoMapper;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;

namespace Shoply.Domain.Mapper;

public class MapperDTOOutput : Profile
{
    public MapperDTOOutput()
    {
        #region User
        CreateMap<OutputUser, UserDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing(src => new UserDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesUserDTO(src.RefreshToken, src.LoginKey, src.PasswordRecoveryCode),
                ExternalPropertiesDTO = new ExternalPropertiesUserDTO(src.Name, src.Email, src.Password, src.Language),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesUserDTO()
            })
            .ReverseMap();

        CreateMap<OutputUser, InternalPropertiesUserDTO>()
            .ConstructUsing(src => new InternalPropertiesUserDTO(src.RefreshToken, src.LoginKey, src.PasswordRecoveryCode))
            .ReverseMap();

        CreateMap<OutputUser, ExternalPropertiesUserDTO>()
            .ConstructUsing(src => new ExternalPropertiesUserDTO(src.Name, src.Email, src.Password, src.Language))
            .ReverseMap();

        CreateMap<OutputUser, AuxiliaryPropertiesUserDTO>()
            .ConstructUsing(src => new AuxiliaryPropertiesUserDTO())
            .ReverseMap();
        #endregion

        #region Customer
        CreateMap<OutputCustomer, CustomerDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new CustomerDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO(src.ListCustomerAddress?.Select(address => mapper.Mapper.Map<CustomerAddressDTO>(address)).ToList())
            })
            .ReverseMap();

        CreateMap<OutputCustomer, InternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerDTO())
            .ReverseMap();

        CreateMap<OutputCustomer, ExternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType))
            .ReverseMap();

        CreateMap<OutputCustomer, AuxiliaryPropertiesCustomerDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesCustomerDTO(src.ListCustomerAddress?.Select(address => mapper.Mapper.Map<CustomerAddressDTO>(address)).ToList()))
            .ReverseMap();
        #endregion

        #region CustomerAddress
        CreateMap<OutputCustomerAddress, CustomerAddressDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new CustomerAddressDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerAddressDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerAddressDTO(src.CustomerId, src.AddressType, src.PublicPlace, src.Number, src.Complement, src.Neighborhood, src.PostalCode, src.Reference, src.Observation),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerAddressDTO(mapper.Mapper.Map<CustomerDTO>(src.Customer))
            })
            .ReverseMap();

        CreateMap<OutputCustomerAddress, InternalPropertiesCustomerAddressDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerAddressDTO())
            .ReverseMap();

        CreateMap<OutputCustomerAddress, ExternalPropertiesCustomerAddressDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerAddressDTO(src.CustomerId, src.AddressType, src.PublicPlace, src.Number, src.Complement, src.Neighborhood, src.PostalCode, src.Reference, src.Observation))
            .ReverseMap();

        CreateMap<OutputCustomerAddress, AuxiliaryPropertiesCustomerAddressDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesCustomerAddressDTO(mapper.Mapper.Map<CustomerDTO>(src.Customer)))
            .ReverseMap();
        #endregion

        #region Brand
        CreateMap<OutputBrand, BrandDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new BrandDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesBrandDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesBrandDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesBrandDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<OutputBrand, InternalPropertiesBrandDTO>()
            .ConstructUsing(src => new InternalPropertiesBrandDTO())
            .ReverseMap();

        CreateMap<OutputBrand, ExternalPropertiesBrandDTO>()
            .ConstructUsing(src => new ExternalPropertiesBrandDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<OutputBrand, AuxiliaryPropertiesBrandDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesBrandDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region ProductCategory
        CreateMap<OutputProductCategory, ProductCategoryDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new ProductCategoryDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesProductCategoryDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesProductCategoryDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductCategoryDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<OutputProductCategory, InternalPropertiesProductCategoryDTO>()
            .ConstructUsing(src => new InternalPropertiesProductCategoryDTO())
            .ReverseMap();

        CreateMap<OutputProductCategory, ExternalPropertiesProductCategoryDTO>()
            .ConstructUsing(src => new ExternalPropertiesProductCategoryDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<OutputProductCategory, AuxiliaryPropertiesProductCategoryDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesProductCategoryDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region MeasureUnit
        CreateMap<OutputMeasureUnit, MeasureUnitDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new MeasureUnitDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesMeasureUnitDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesMeasureUnitDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesMeasureUnitDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<OutputMeasureUnit, InternalPropertiesMeasureUnitDTO>()
            .ConstructUsing(src => new InternalPropertiesMeasureUnitDTO())
            .ReverseMap();

        CreateMap<OutputMeasureUnit, ExternalPropertiesMeasureUnitDTO>()
            .ConstructUsing(src => new ExternalPropertiesMeasureUnitDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<OutputMeasureUnit, AuxiliaryPropertiesMeasureUnitDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesMeasureUnitDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region Product
        CreateMap<OutputProduct, ProductDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new ProductDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesProductDTO(src.Markup),
                ExternalPropertiesDTO = new ExternalPropertiesProductDTO(src.Code, src.Description, src.BarCode, src.CostValue, src.SaleValue, src.Status, src.ProductCategoryId, src.MeasureUnitId, src.BrandId),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductDTO(mapper.Mapper.Map<ProductCategoryDTO>(src.ProductCategory), mapper.Mapper.Map<MeasureUnitDTO>(src.MeasureUnit), mapper.Mapper.Map<BrandDTO>(src.Brand), mapper.Mapper.Map<List<ProductImageDTO>>(src.ListProductImage))
            })
            .ReverseMap();

        CreateMap<OutputProduct, InternalPropertiesProductDTO>()
            .ConstructUsing(src => new InternalPropertiesProductDTO(src.Markup))
            .ReverseMap();

        CreateMap<OutputProduct, ExternalPropertiesProductDTO>()
            .ConstructUsing(src => new ExternalPropertiesProductDTO(src.Code, src.Description, src.BarCode, src.CostValue, src.SaleValue, src.Status, src.ProductCategoryId, src.MeasureUnitId, src.BrandId))
            .ReverseMap();

        CreateMap<OutputProduct, AuxiliaryPropertiesProductDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesProductDTO(mapper.Mapper.Map<ProductCategoryDTO>(src.ProductCategory), mapper.Mapper.Map<MeasureUnitDTO>(src.MeasureUnit), mapper.Mapper.Map<BrandDTO>(src.Brand), mapper.Mapper.Map<List<ProductImageDTO>>(src.ListProductImage)))
            .ReverseMap();
        #endregion
   
        #region ProductImage
        CreateMap<OutputProductImage, ProductImageDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new ProductImageDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesProductImageDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesProductImageDTO(src.FileName, src.FileLength, src.ImageUrl, src.ProductId),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductImageDTO(mapper.Mapper.Map<ProductDTO>(src.Product))
            })
            .ReverseMap();
        
        CreateMap<OutputProductImage, InternalPropertiesProductImageDTO>()
            .ConstructUsing(src => new InternalPropertiesProductImageDTO())
            .ReverseMap();
        
        CreateMap<OutputProductImage, ExternalPropertiesProductImageDTO>()
            .ConstructUsing(src => new ExternalPropertiesProductImageDTO(src.FileName, src.FileLength, src.ImageUrl, src.ProductId))
            .ReverseMap();
        
        CreateMap<OutputProductImage, AuxiliaryPropertiesProductImageDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesProductImageDTO(mapper.Mapper.Map<ProductDTO>(src.Product)))
            .ReverseMap();
        #endregion
    }
}
