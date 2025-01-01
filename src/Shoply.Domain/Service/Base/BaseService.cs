using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Base;

public abstract class BaseService<TRepository, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentifier, TOutput, TValidateDTO, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(TRepository repository, ITranslationService translationService) : BaseValidate<TValidateDTO>(translationService), IBaseService<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentifier, TOutput>
        where TRepository : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
        where TValidateDTO : BaseValidateDTO
        where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
        where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
        where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    public new Guid _guidSessionDataRequest;
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
    public new void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
        SessionHelper.SetGuidSessionDataRequest(this, guidSessionDataRequest);
        base.SetGuid(guidSessionDataRequest);
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

public abstract class BaseService<TRepository, TInputCreate, TInputIdentifier, TOutput, TInputIdentityDelete, TValidateDTO, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(TRepository repository, ITranslationService translationService) : BaseService<TRepository, TInputCreate, BaseInputUpdate_0, BaseInputIdentityUpdate_0, TInputIdentityDelete, TInputIdentifier, TOutput, TValidateDTO, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(repository, translationService), IBaseService<TInputCreate, TInputIdentityDelete, TInputIdentifier, TOutput>
        where TRepository : IBaseRepository<TInputCreate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
        where TValidateDTO : BaseValidateDTO
        where TDTO : BaseDTO<TInputCreate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
        where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
        where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
        where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{ }