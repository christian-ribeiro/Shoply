using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Base;

public abstract class BaseService<TRepository, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete, TValidateDTO, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO, TProcessType>(TRepository repository, ITranslationService translationService) : BaseValidate<TValidateDTO, TProcessType>(translationService), IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
        where TRepository : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TValidateDTO : BaseValidateDTO
        where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
        where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
        where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
        where TProcessType : Enum
{
    public Guid _guidSessionDataRequest;
    protected readonly TRepository _repository = repository;

    #region Read
    public async Task<TOutput?> Get(long id)
    {
        return FromDTOToOutput(await _repository.Get(id, true));
    }

    public async Task<List<TOutput>> GetListByListId(List<long> listId)
    {
        return FromDTOToOutput(await _repository.GetListByListId(listId, true));
    }

    public async Task<List<TOutput>> GetAll()
    {
        return FromDTOToOutput(await _repository.GetAll(true))!;
    }

    public async Task<TOutput?> GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        return FromDTOToOutput(await _repository.GetByIdentifier(inputIdentifier, true));
    }

    public async Task<List<TOutput>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier)
    {
        return FromDTOToOutput(await _repository.GetListByListIdentifier(listInputIdentifier, true));
    }
    #endregion

    #region Create
    public async Task<BaseResult<TOutput?>> Create(TInputCreate inputCreate)
    {
        var result = await Create([inputCreate])!;
        return result.IsSuccess ? BaseResult<TOutput?>.Success(result.Value?.FirstOrDefault(), result.ListNotification) : BaseResult<TOutput?>.Failure(result.ListNotification);
    }

    public virtual Task<BaseResult<List<TOutput?>>> Create(List<TInputCreate> listInputCreate)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public async Task<BaseResult<TOutput?>> Update(TInputIdentityUpdate inputIdentityUpdate)
    {
        var result = await Update([inputIdentityUpdate]);
        return result.IsSuccess ? BaseResult<TOutput?>.Success(result.Value?.FirstOrDefault(), result.ListNotification) : BaseResult<TOutput?>.Failure(result.ListNotification);
    }

    public virtual Task<BaseResult<List<TOutput?>>> Update(List<TInputIdentityUpdate> listInputIdentityUpdate)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public async Task<BaseResult<bool>> Delete(TInputIdentityDelete inputIdentityDelete)
    {
        return await Delete([inputIdentityDelete]);
    }

    public virtual Task<BaseResult<bool>> Delete(List<TInputIdentityDelete> listInputIdentityDelete)
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
        return SessionData.Mapper!.MapperDTOOutput.Map<List<TDTO>, List<TOutput>>(listDTO)!;
    }
    #endregion
}