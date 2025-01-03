using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class UserDTO : BaseDTO<InputCreateUser, InputUpdateUser, OutputUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>, IBaseDTO<UserDTO, OutputUser>
{
    public UserDTO GetDTO(OutputUser output)
    {
        return new UserDTO
        {
            InternalPropertiesDTO = new InternalPropertiesUserDTO(output.RefreshToken, output.LoginKey, output.PasswordRecoveryCode).SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesUserDTO(output.Name, output.Email, output.Password, output.Language),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesUserDTO().SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }

    public OutputUser GetOutput(UserDTO dto)
    {
        return new OutputUser(dto.ExternalPropertiesDTO.Name, dto.ExternalPropertiesDTO.Email, dto.ExternalPropertiesDTO.Password, dto.ExternalPropertiesDTO.Language, dto.InternalPropertiesDTO.RefreshToken, dto.InternalPropertiesDTO.LoginKey, dto.InternalPropertiesDTO.PasswordRecoveryCode)
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator UserDTO?(OutputUser output)
    {
        return output == null ? default : new UserDTO().GetDTO(output);
    }

    public static implicit operator OutputUser?(UserDTO dto)
    {
        return dto == null ? default : new UserDTO().GetOutput(dto);
    }
}