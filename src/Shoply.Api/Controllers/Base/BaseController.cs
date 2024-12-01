using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Infrastructure.Persistence.UnitOfWork;

namespace Shoply.Api.Controllers.Base;

public class BaseController<TService, TUnitOfWork, TContext, TOutput, TInputIdentifier, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete>(TService service, TUnitOfWork unitOfWork) : Controller
    where TContext : DbContext
    where TUnitOfWork : BaseUnitOfWork<TContext>
    where TService : IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
    where TOutput : BaseOutput<TOutput>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>, new()
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    public Guid _guidSessionDataRequest;
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
        Guid guidSessionDataRequest = SessionData.Initialize();
        SetGuid(guidSessionDataRequest);
    }

    private void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
        SessionHelper.SetGuidSessionDataRequest(this, guidSessionDataRequest);
    }
}