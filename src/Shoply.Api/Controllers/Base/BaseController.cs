using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Domain.Interface.UnitOfWork;

namespace Shoply.Api.Controllers.Base;

public abstract class BaseController<TService, TUnitOfWork, TOutput, TInputIdentifier, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete>(TService service, TUnitOfWork unitOfWork) : Controller
    where TUnitOfWork : IBaseUnitOfWork
    where TService : IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
    where TOutput : BaseOutput<TOutput>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>, new()
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    protected readonly Guid _guidSessionDataRequest = SessionData.Initialize();
    protected readonly TService _service = service;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        bool allowAnonymous = (from i in context.ActionDescriptor.EndpointMetadata
                               where i.GetType() == typeof(AllowAnonymousAttribute)
                               select i).Any();

        if (!allowAnonymous)
        {
            SetData();
        }

        base.OnActionExecuting(context);
    }

    public override async void OnActionExecuted(ActionExecutedContext context)
    {
        await unitOfWork.CommitAsync();
        base.OnActionExecuted(context);
    }

    private void SetData()
    {
        SetGuid();
    }

    private void SetGuid()
    {
        SessionHelper.SetGuidSessionDataRequest(this, _guidSessionDataRequest);
    }
}