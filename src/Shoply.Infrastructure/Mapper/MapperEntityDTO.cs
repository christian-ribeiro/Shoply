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
    }
}