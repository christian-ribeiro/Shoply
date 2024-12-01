using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Service.Base;

public abstract class BaseService<TRepository, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(TRepository repository) : IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
        where TRepository : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>
        where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>
        where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>
{
    public Guid _guidSessionDataRequest;
    protected readonly TRepository _repository = repository;

    #region Read
    public async Task<TOutput?> Get(long id)
    {
        return FromDTOToOutput(await _repository.Get(id));
    }

    public async Task<List<TOutput>> GetListByListId(List<long> listId)
    {
        return FromDTOToOutput(await _repository.GetListByListId(listId));
    }

    public async Task<List<TOutput>> GetAll()
    {
        return FromDTOToOutput(await _repository.GetAll());
    }

    public async Task<TOutput?> GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        return FromDTOToOutput(await _repository.GetByIdentifier(inputIdentifier));
    }

    public async Task<List<TOutput>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier)
    {
        return FromDTOToOutput(await _repository.GetListByListIdentifier(listInputIdentifier));
    }
    #endregion

    #region Create
    public async Task<TOutput?> Create(TInputCreate inputCreate)
    {
        var result = await Create([inputCreate])!;
        return result?.FirstOrDefault();
    }

    public virtual async Task<List<TOutput?>> Create(List<TInputCreate> listInputCreate)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public async Task<TOutput?> Update(TInputIdentityUpdate inputIdentityUpdate)
    {
        var result = await Update([inputIdentityUpdate]);
        return result?.FirstOrDefault();
    }

    public virtual async Task<List<TOutput>> Update(List<TInputIdentityUpdate> listInputIdentityUpdate)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public async Task<bool> Delete(TInputIdentityDelete inputIdentityDelete)
    {
        return await Delete([inputIdentityDelete]);
    }

    public virtual async Task<bool> Delete(List<TInputIdentityDelete> listInputIdentityDelete)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Internal
    public void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
        SessionHelper.SetGuidSessionDataRequest(this, guidSessionDataRequest);
    }
    #endregion

    #region Custom
    internal static TOutput FromDTOToOutput(TDTO? dto)
    {
        return SessionData.Mapper!.MapperDTOOutput.Map<TDTO, TOutput>(dto!);
    }

    internal static List<TOutput> FromDTOToOutput(List<TDTO> listDTO)
    {
        return SessionData.Mapper!.MapperDTOOutput.Map<List<TDTO>, List<TOutput>>(listDTO);
    }
    #endregion
}