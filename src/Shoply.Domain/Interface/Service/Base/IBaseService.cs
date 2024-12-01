using Shoply.Arguments.Argument.Base;

namespace Shoply.Domain.Interface.Service.Base;

public interface IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputUpdate : BaseInputUpdate<TInputUpdate>
        where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>, new()
        where TOutput : BaseOutput<TOutput>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{ }