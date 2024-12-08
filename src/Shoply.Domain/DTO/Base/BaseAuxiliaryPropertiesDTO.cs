using Shoply.Domain.DTO.Module.Registration;

namespace Shoply.Domain.DTO.Base;

public class BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO> where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    public UserDTO? CreationUser { get; set; }
    public UserDTO? ChangeUser { get; set; }
}

public class BaseAuxiliaryPropertiesDTO_0 : BaseAuxiliaryPropertiesDTO<BaseAuxiliaryPropertiesDTO_0> { }