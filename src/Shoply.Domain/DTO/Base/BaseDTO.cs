using Shoply.Arguments.Argument.Base;
using System.Reflection;

namespace Shoply.Domain.DTO.Base;

public class BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TOutput : BaseOutput<TOutput>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    public TInternalPropertiesDTO InternalPropertiesDTO { get; set; } = new TInternalPropertiesDTO();
    public TExternalPropertiesDTO ExternalPropertiesDTO { get; set; } = new TExternalPropertiesDTO();
    public TAuxiliaryPropertiesDTO AuxiliaryPropertiesDTO { get; set; } = new TAuxiliaryPropertiesDTO();

    public TDTO Create(TInputCreate inputCreate, TInternalPropertiesDTO? internalPropertiesDTO = default)
    {
        List<PropertyInfo>? listExternalProperties = [.. typeof(TExternalPropertiesDTO).GetProperties()];
        if (listExternalProperties == null)
            return (TDTO)this;

        foreach (PropertyInfo item in listExternalProperties)
        {
            var propertyValue = inputCreate.GetType().GetProperty(item.Name)?.GetValue(inputCreate);

            if (propertyValue != null)
                item.SetValue(ExternalPropertiesDTO, propertyValue);
        }

        if (internalPropertiesDTO != null)
            InternalPropertiesDTO = internalPropertiesDTO;

        return (TDTO)this;
    }

    public TDTO Create(TExternalPropertiesDTO externalPropertiesDTO, TInternalPropertiesDTO? internalPropertiesDTO = default)
    {
        ExternalPropertiesDTO = externalPropertiesDTO;

        if (internalPropertiesDTO != null)
            InternalPropertiesDTO = internalPropertiesDTO;

        return (TDTO)this;
    }

    public TDTO Update(TInputUpdate inputUpdate)
    {
        List<PropertyInfo>? listExternalProperties = [.. typeof(TExternalPropertiesDTO).GetProperties()];
        if (listExternalProperties == null)
            return (TDTO)this;

        foreach (PropertyInfo item in listExternalProperties)
        {
            var propertyValue = inputUpdate.GetType().GetProperty(item.Name)?.GetValue(inputUpdate);
            if (propertyValue != null)
                item.SetValue(ExternalPropertiesDTO, propertyValue);
        }

        return (TDTO)this;
    }

    public TDTO Update(TExternalPropertiesDTO externalPropertiesDTO, TInternalPropertiesDTO? internalPropertiesDTO = default)
    {
        ExternalPropertiesDTO = externalPropertiesDTO;

        if (internalPropertiesDTO != null)
            InternalPropertiesDTO = internalPropertiesDTO;

        return (TDTO)this;
    }
}

public class BaseDTO<TInputCreate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO> : BaseDTO<TInputCreate, BaseInputUpdate_0, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TOutput : BaseOutput<TOutput>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TDTO : BaseDTO<TInputCreate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{ }