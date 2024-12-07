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
                ExternalPropertiesDTO = new ExternalPropertiesUserDTO(src.Name, src.Password, src.Email, src.Language),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesUserDTO()
            })
            .ReverseMap();

        CreateMap<OutputUser, InternalPropertiesUserDTO>()
            .ConstructUsing(src => new InternalPropertiesUserDTO(src.RefreshToken, src.LoginKey, src.PasswordRecoveryCode))
            .ReverseMap();

        CreateMap<OutputUser, ExternalPropertiesUserDTO>()
            .ConstructUsing(src => new ExternalPropertiesUserDTO(src.Name, src.Password, src.Email, src.Language))
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
            .ConstructUsing(src => new CustomerDTO()
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO()
            })
            .ReverseMap();

        CreateMap<OutputCustomer, InternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerDTO())
            .ReverseMap();

        CreateMap<OutputCustomer, ExternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerDTO(src.Code, src.FirstName, src.LastName, src.BirthDate, src.Document, src.PersonType))
            .ReverseMap();

        CreateMap<OutputCustomer, AuxiliaryPropertiesCustomerDTO>()
            .ConstructUsing(src => new AuxiliaryPropertiesCustomerDTO())
            .ReverseMap();
        #endregion
    }
}