using Shoply.Arguments.Argument.Base;

namespace Shoply.Domain.Interface.Service.Base;

public interface IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    void SetGuid(Guid guidSessionDataRequest);
    Task<TOutput?> Get(long id);
    Task<List<TOutput>> GetListByListId(List<long> listId);
    Task<List<TOutput>> GetAll();
    Task<TOutput?> GetByIdentifier(TInputIdentifier inputIdentifier);
    Task<List<TOutput>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier);
    Task<BaseResult<TOutput?>> Create(TInputCreate inputCreate);
    Task<BaseResult<List<TOutput?>>> Create(List<TInputCreate> listInputCreate);
    Task<BaseResult<TOutput?>> Update(TInputIdentityUpdate inputIdentityUpdate);
    Task<BaseResult<List<TOutput?>>> Update(List<TInputIdentityUpdate> listInputIdentityUpdate);
    Task<BaseResult<bool>> Delete(TInputIdentityDelete inputIdentityDelete);
    Task<BaseResult<bool>> Delete(List<TInputIdentityDelete> listInputIdentityDelete);
}