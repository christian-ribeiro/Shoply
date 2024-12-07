using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.Interface.Repository.Base;

public interface IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO,
    TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    void SetGuid(Guid guidSessionDataRequest);
    Task<TDTO> Get(long id);
    Task<List<TDTO>> GetListByListId(List<long> listId);
    Task<List<TDTO>> GetAll();
    Task<TDTO?> GetByIdentifier(TInputIdentifier inputIdentifier);
    Task<List<TDTO>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier);
    Task<List<TDTO>> GetDynamic(string[] fields);
    Task<TDTO?> Create(TDTO dto);
    Task<List<TDTO>> Create(List<TDTO> listDTO);
    Task<TDTO?> Update(TDTO dto);
    Task<List<TDTO>> Update(List<TDTO> listDTO);
    Task<bool> Delete(TDTO dto);
    Task<bool> Delete(List<TDTO> listDTO);
}