using Shoply.Arguments.Argument.Base;

namespace Shoply.Domain.DTO.Base;

public class BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TOutput : BaseOutput<TOutput>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>
{
    public TInternalPropertiesDTO? InternalPropertiesDTO { get; set; }
    public TExternalPropertiesDTO? ExternalPropertiesDTO { get; set; }
    public TAuxiliaryPropertiesDTO? AuxiliaryPropertiesDTO { get; set; }
}