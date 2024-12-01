using Shoply.Arguments.Utils;

namespace Shoply.Domain.DTO.Base;

public class BaseExternalPropertiesDTO<TExternalPropertiesDTO> : BaseSetProperty<TExternalPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>
{ }

public class BaseExternalPropertiesDTO_0 : BaseExternalPropertiesDTO<BaseExternalPropertiesDTO_0> { }