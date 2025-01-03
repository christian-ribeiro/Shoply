using AutoMapper;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

namespace Shoply.Infrastructure.Mapper;

public class MapperEntityDTO : Profile
{
    public MapperEntityDTO()
    {
        #region User
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<User, InternalPropertiesUserDTO>().ReverseMap();
        CreateMap<User, ExternalPropertiesUserDTO>().ReverseMap();
        CreateMap<User, AuxiliaryPropertiesUserDTO>().ReverseMap();
        #endregion

        #region Customer
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<Customer, InternalPropertiesCustomerDTO>().ReverseMap();
        CreateMap<Customer, ExternalPropertiesCustomerDTO>().ReverseMap();
        CreateMap<Customer, AuxiliaryPropertiesCustomerDTO>().ReverseMap();
        #endregion

        #region CustomerAddress
        CreateMap<CustomerAddress, CustomerAddressDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<CustomerAddress, InternalPropertiesCustomerAddressDTO>().ReverseMap();
        CreateMap<CustomerAddress, ExternalPropertiesCustomerAddressDTO>().ReverseMap();
        CreateMap<CustomerAddress, AuxiliaryPropertiesCustomerAddressDTO>().ReverseMap();
        #endregion

        #region Brand
        CreateMap<Brand, BrandDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<Brand, InternalPropertiesBrandDTO>().ReverseMap();
        CreateMap<Brand, ExternalPropertiesBrandDTO>().ReverseMap();
        CreateMap<Brand, AuxiliaryPropertiesBrandDTO>().ReverseMap();
        #endregion

        #region ProductCategory
        CreateMap<ProductCategory, ProductCategoryDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<ProductCategory, InternalPropertiesProductCategoryDTO>().ReverseMap();
        CreateMap<ProductCategory, ExternalPropertiesProductCategoryDTO>().ReverseMap();
        CreateMap<ProductCategory, AuxiliaryPropertiesProductCategoryDTO>().ReverseMap();
        #endregion

        #region MeasureUnit
        CreateMap<MeasureUnit, MeasureUnitDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<MeasureUnit, InternalPropertiesMeasureUnitDTO>().ReverseMap();
        CreateMap<MeasureUnit, ExternalPropertiesMeasureUnitDTO>().ReverseMap();
        CreateMap<MeasureUnit, AuxiliaryPropertiesMeasureUnitDTO>().ReverseMap();
        #endregion

        #region Product
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<Product, InternalPropertiesProductDTO>().ReverseMap();
        CreateMap<Product, ExternalPropertiesProductDTO>().ReverseMap();
        CreateMap<Product, AuxiliaryPropertiesProductDTO>().ReverseMap();
        #endregion

        #region ProductImage
        CreateMap<ProductImage, ProductImageDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<ProductImage, InternalPropertiesProductImageDTO>().ReverseMap();
        CreateMap<ProductImage, ExternalPropertiesProductImageDTO>().ReverseMap();
        CreateMap<ProductImage, AuxiliaryPropertiesProductImageDTO>().ReverseMap();
        #endregion
    }
}
