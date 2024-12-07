using AutoMapper;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;

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
            .ConstructUsing(src => new CustomerDTO
            {
                InternalPropertiesDTO = new InternalPropertiesCustomerDTO(),
                ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(),
                AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO()
            })
            .ReverseMap();

        CreateMap<Customer, InternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new InternalPropertiesCustomerDTO())
            .ReverseMap();

        CreateMap<Customer, ExternalPropertiesCustomerDTO>()
            .ConstructUsing(src => new ExternalPropertiesCustomerDTO())
            .ReverseMap();

        CreateMap<Customer, AuxiliaryPropertiesCustomerDTO>()
            .ConstructUsing(src => new AuxiliaryPropertiesCustomerDTO())
            .ReverseMap();
        #endregion
    }
}