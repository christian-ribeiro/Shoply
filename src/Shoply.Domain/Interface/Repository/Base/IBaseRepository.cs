using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.Interface.Repository.Base;

public interface IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>, IBaseDTO<TDTO, TOutput>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    void SetGuid(Guid guidSessionDataRequest);
    Task<TDTO> Get(long id, bool useCustomReturnProperty = false);
    Task<List<TDTO>> GetListByListId(List<long> listId, bool useCustomReturnProperty = false);
    Task<List<TDTO>> GetAll(bool useCustomReturnProperty = false);
    Task<TDTO?> GetByIdentifier(TInputIdentifier inputIdentifier, bool useCustomReturnProperty = false);
    Task<List<TDTO>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier, bool useCustomReturnProperty = false);
    Task<List<TDTO>> GetDynamic(string[] fields, bool useCustomReturnProperty = false);
    Task<TDTO?> Create(TDTO dto);
    Task<List<TDTO>> Create(List<TDTO> listDTO);
    Task<TDTO?> Update(TDTO dto);
    Task<List<TDTO>> Update(List<TDTO> listDTO);
    Task<bool> Delete(TDTO dto);
    Task<bool> Delete(List<TDTO> listDTO);
}

public interface IBaseRepository<TInputCreate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO> : IBaseRepository<TInputCreate, BaseInputUpdate_0, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>, IBaseDTO<TDTO, TOutput>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{ }