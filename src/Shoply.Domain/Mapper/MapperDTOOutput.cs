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
            .ReverseMap();
        CreateMap<OutputUser, InternalPropertiesUserDTO>().ReverseMap();
        CreateMap<OutputUser, ExternalPropertiesUserDTO>().ReverseMap();
        CreateMap<OutputUser, AuxiliaryPropertiesUserDTO>().ReverseMap();
        #endregion

        #region Customer
        CreateMap<OutputCustomer, CustomerDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputCustomer, InternalPropertiesCustomerDTO>().ReverseMap();
        CreateMap<OutputCustomer, ExternalPropertiesCustomerDTO>().ReverseMap();
        CreateMap<OutputCustomer, AuxiliaryPropertiesCustomerDTO>().ReverseMap();
        #endregion

        #region CustomerAddress
        CreateMap<OutputCustomerAddress, CustomerAddressDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputCustomerAddress, InternalPropertiesCustomerAddressDTO>().ReverseMap();
        CreateMap<OutputCustomerAddress, ExternalPropertiesCustomerAddressDTO>().ReverseMap();
        CreateMap<OutputCustomerAddress, AuxiliaryPropertiesCustomerAddressDTO>().ReverseMap();
        #endregion

        #region Brand
        CreateMap<OutputBrand, BrandDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputBrand, InternalPropertiesBrandDTO>().ReverseMap();
        CreateMap<OutputBrand, ExternalPropertiesBrandDTO>().ReverseMap();
        CreateMap<OutputBrand, AuxiliaryPropertiesBrandDTO>().ReverseMap();
        #endregion

        #region ProductCategory
        CreateMap<OutputProductCategory, ProductCategoryDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputProductCategory, InternalPropertiesProductCategoryDTO>().ReverseMap();
        CreateMap<OutputProductCategory, ExternalPropertiesProductCategoryDTO>().ReverseMap();
        CreateMap<OutputProductCategory, AuxiliaryPropertiesProductCategoryDTO>().ReverseMap();
        #endregion

        #region MeasureUnit
        CreateMap<OutputMeasureUnit, MeasureUnitDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputMeasureUnit, InternalPropertiesMeasureUnitDTO>().ReverseMap();
        CreateMap<OutputMeasureUnit, ExternalPropertiesMeasureUnitDTO>().ReverseMap();
        CreateMap<OutputMeasureUnit, AuxiliaryPropertiesMeasureUnitDTO>().ReverseMap();
        #endregion

        #region Product
        CreateMap<OutputProduct, ProductDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputProduct, InternalPropertiesProductDTO>().ReverseMap();
        CreateMap<OutputProduct, ExternalPropertiesProductDTO>().ReverseMap();
        CreateMap<OutputProduct, AuxiliaryPropertiesProductDTO>().ReverseMap();
        #endregion

        #region ProductImage
        CreateMap<OutputProductImage, ProductImageDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ReverseMap();
        CreateMap<OutputProductImage, InternalPropertiesProductImageDTO>().ReverseMap();
        CreateMap<OutputProductImage, ExternalPropertiesProductImageDTO>().ReverseMap();
        CreateMap<OutputProductImage, AuxiliaryPropertiesProductImageDTO>().ReverseMap();
        #endregion
    }
}
