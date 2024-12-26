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
            .ConstructUsing(src => new UserDTO
            {
                InternalPropertiesDTO = new InternalPropertiesUserDTO(src.RefreshToken, src.LoginKey, src.PasswordRecoveryCode),
                ExternalPropertiesDTO = new ExternalPropertiesUserDTO(src.Name, src.Password, src.Email, src.Language),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesUserDTO()
            })
            .ReverseMap();

        CreateMap<User, InternalPropertiesUserDTO>()
            .ConstructUsing(src => new InternalPropertiesUserDTO(src.RefreshToken, src.LoginKey, src.PasswordRecoveryCode))
            .ReverseMap();

        CreateMap<User, ExternalPropertiesUserDTO>()
            .ConstructUsing(src => new ExternalPropertiesUserDTO(src.Name, src.Password, src.Email, src.Language))
            .ReverseMap();

        CreateMap<User, AuxiliaryPropertiesUserDTO>()
            .ConstructUsing(src => new AuxiliaryPropertiesUserDTO())
            .ReverseMap();
        #endregion

        #region Customer
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new CustomerDTO
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO(src.ListCustomerAddress?.Select(address => mapper.Mapper.Map<CustomerAddressDTO>(address)).ToList())
            })
            .ReverseMap();

        CreateMap<Customer, InternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerDTO())
            .ReverseMap();

        CreateMap<Customer, ExternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType))
            .ReverseMap();

        CreateMap<Customer, AuxiliaryPropertiesCustomerDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesCustomerDTO(src.ListCustomerAddress?.Select(address => mapper.Mapper.Map<CustomerAddressDTO>(address)).ToList()))
            .ReverseMap();
        #endregion

        #region CustomerAddress
        CreateMap<CustomerAddress, CustomerAddressDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new CustomerAddressDTO
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerAddressDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerAddressDTO(src.CustomerId, src.AddressType, src.PublicPlace, src.Number, src.Complement, src.Neighborhood, src.PostalCode, src.Reference, src.Observation),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerAddressDTO(mapper.Mapper.Map<CustomerDTO>(src.Customer))
            })
            .ReverseMap();

        CreateMap<CustomerAddress, InternalPropertiesCustomerAddressDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerAddressDTO())
            .ReverseMap();

        CreateMap<CustomerAddress, ExternalPropertiesCustomerAddressDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerAddressDTO(src.CustomerId, src.AddressType, src.PublicPlace, src.Number, src.Complement, src.Neighborhood, src.PostalCode, src.Reference, src.Observation))
            .ReverseMap();

        CreateMap<CustomerAddress, AuxiliaryPropertiesCustomerAddressDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesCustomerAddressDTO(mapper.Mapper.Map<CustomerDTO>(src.Customer)))
            .ReverseMap();
        #endregion

        #region Brand
        CreateMap<Brand, BrandDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new BrandDTO
            {
                InternalPropertiesDTO = new InternalPropertiesBrandDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesBrandDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesBrandDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<Brand, InternalPropertiesBrandDTO>()
            .ConstructUsing(src => new InternalPropertiesBrandDTO())
            .ReverseMap();

        CreateMap<Brand, ExternalPropertiesBrandDTO>()
            .ConstructUsing(src => new ExternalPropertiesBrandDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<Brand, AuxiliaryPropertiesBrandDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesBrandDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region ProductCategory
        CreateMap<ProductCategory, ProductCategoryDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new ProductCategoryDTO
            {
                InternalPropertiesDTO = new InternalPropertiesProductCategoryDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesProductCategoryDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductCategoryDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<ProductCategory, InternalPropertiesProductCategoryDTO>()
            .ConstructUsing(src => new InternalPropertiesProductCategoryDTO())
            .ReverseMap();

        CreateMap<ProductCategory, ExternalPropertiesProductCategoryDTO>()
            .ConstructUsing(src => new ExternalPropertiesProductCategoryDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<ProductCategory, AuxiliaryPropertiesProductCategoryDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesProductCategoryDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region MeasureUnit
        CreateMap<MeasureUnit, MeasureUnitDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new MeasureUnitDTO
            {
                InternalPropertiesDTO = new InternalPropertiesMeasureUnitDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesMeasureUnitDTO(src.Code, src.Description),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesMeasureUnitDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct))
            })
            .ReverseMap();

        CreateMap<MeasureUnit, InternalPropertiesMeasureUnitDTO>()
            .ConstructUsing(src => new InternalPropertiesMeasureUnitDTO())
            .ReverseMap();

        CreateMap<MeasureUnit, ExternalPropertiesMeasureUnitDTO>()
            .ConstructUsing(src => new ExternalPropertiesMeasureUnitDTO(src.Code, src.Description))
            .ReverseMap();

        CreateMap<MeasureUnit, AuxiliaryPropertiesMeasureUnitDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesMeasureUnitDTO(mapper.Mapper.Map<List<ProductDTO>>(src.ListProduct)))
            .ReverseMap();
        #endregion

        #region Product
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.InternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ExternalPropertiesDTO, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AuxiliaryPropertiesDTO, opt => opt.MapFrom(src => src))
            .ConstructUsing((src, mapper) => new ProductDTO
            {
                InternalPropertiesDTO = new InternalPropertiesProductDTO(src.Markup),
                ExternalPropertiesDTO = new ExternalPropertiesProductDTO(src.Code, src.Description, src.BarCode, src.CostValue, src.SaleValue, src.Status, src.ProductCategoryId, src.MeasureUnitId, src.BrandId),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductDTO(mapper.Mapper.Map<ProductCategoryDTO>(src.ProductCategory), mapper.Mapper.Map<MeasureUnitDTO>(src.MeasureUnit), mapper.Mapper.Map<BrandDTO>(src.Brand))
            })
            .ReverseMap();

        CreateMap<Product, InternalPropertiesProductDTO>()
            .ConstructUsing(src => new InternalPropertiesProductDTO(src.Markup))
            .ReverseMap();

        CreateMap<Product, ExternalPropertiesProductDTO>()
            .ConstructUsing(src => new ExternalPropertiesProductDTO(src.Code, src.Description, src.BarCode, src.CostValue, src.SaleValue, src.Status, src.ProductCategoryId, src.MeasureUnitId, src.BrandId))
            .ReverseMap();

        CreateMap<Product, AuxiliaryPropertiesProductDTO>()
            .ConstructUsing((src, mapper) => new AuxiliaryPropertiesProductDTO(mapper.Mapper.Map<ProductCategoryDTO>(src.ProductCategory), mapper.Mapper.Map<MeasureUnitDTO>(src.MeasureUnit), mapper.Mapper.Map<BrandDTO>(src.Brand)))
            .ReverseMap();
        #endregion
    }
}
