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
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>
{
    void SetGuid(Guid guidSessionDataRequest);
    Task<TDTO> Get(long id);
    Task<List<TDTO>> GetListByListId(List<long> listId);
    Task<List<TDTO>> GetAll();
    Task<TDTO?> GetByIdentifier(TInputIdentifier inputIdentifier);
    Task<List<TDTO>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier);
    Task<List<TDTO?>> Create(List<TDTO> listDTO);
    Task<List<TDTO?>> Update(List<TDTO> listDTO);
    Task<bool> Delete(List<TDTO> listDTO);
    Task<bool> Delete(List<long> listId);
}