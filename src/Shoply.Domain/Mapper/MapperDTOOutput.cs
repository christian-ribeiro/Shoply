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
    }
}