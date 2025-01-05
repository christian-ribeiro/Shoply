using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Filter;

namespace Shoply.Domain.Interface.Service.Base;

public interface IBaseService<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentifier, TOutput>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
{
    void SetGuid(Guid guidSessionDataRequest);
    Task<TOutput?> Get(long id);
    Task<List<TOutput>> GetListByListId(List<long> listId);
    Task<List<TOutput>> GetAll();
    Task<TOutput> GetByFilter(InputFilter inputFilter);
    Task<List<TOutput>> GetListByFilter(InputFilter inputFilter);
    Task<TOutput?> GetByIdentifier(TInputIdentifier inputIdentifier);
    Task<List<TOutput>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier);
    Task<BaseResult<TOutput?>> Create(TInputCreate inputCreate);
    Task<BaseResult<List<TOutput?>>> Create(List<TInputCreate> listInputCreate);
    Task<BaseResult<TOutput?>> Update(TInputIdentityUpdate inputIdentityUpdate);
    Task<BaseResult<List<TOutput?>>> Update(List<TInputIdentityUpdate> listInputIdentityUpdate);
    Task<BaseResult<bool>> Delete(TInputIdentityDelete inputIdentityDelete);
    Task<BaseResult<bool>> Delete(List<TInputIdentityDelete> listInputIdentityDelete);
}

public interface IBaseService<TInputCreate, TInputIdentityDelete, TInputIdentifier, TOutput> : IBaseService<TInputCreate, BaseInputUpdate_0, BaseInputIdentityUpdate_0, TInputIdentityDelete, TInputIdentifier, TOutput>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
        where TOutput : BaseOutput<TOutput>
{ }