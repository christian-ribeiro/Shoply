using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Interface.UnitOfWork;
using System.Net;

namespace Shoply.Api.Controllers.Base;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public abstract class BaseController<TService, TUnitOfWork, TOutput, TInputIdentifier, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete>(TService service, TUnitOfWork unitOfWork, IUserService userService) : Controller
    where TUnitOfWork : IBaseUnitOfWork
    where TService : IBaseService<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TInputIdentityUpdate, TInputIdentityDelete>
    where TOutput : BaseOutput<TOutput>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    protected readonly Guid _guidSessionDataRequest = SessionData.Initialize();
    protected readonly TService _service = service;

    #region Read
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> Get([FromRoute] long id)
    {
        try
        {
            return await ResponseAsync(await _service!.Get(id));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("GetListByListId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<TOutput>>> GetListByListId([FromBody] List<long> listId)
    {
        try
        {
            return await ResponseAsync(await _service!.GetListByListId(listId));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<TOutput>>> GetAll()
    {
        try
        {
            return await ResponseAsync(await _service!.GetAll());
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("GetByIdentifier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> GetByIdentifier([FromBody] TInputIdentifier inputIdentifier)
    {
        try
        {
            return await ResponseAsync(await _service!.GetByIdentifier(inputIdentifier));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("GetListByListIdentifier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<TOutput>>> GetListByListIdentifier([FromBody] List<TInputIdentifier> listInputIdentifier)
    {
        try
        {
            return await ResponseAsync(await _service!.GetListByListIdentifier(listInputIdentifier));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
    #endregion

    #region Create
    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> Create([FromBody] TInputCreate inputCreate)
    {
        try
        {
            return await ResponseAsync(await _service!.Create(inputCreate));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("Create/Multiple")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> Create([FromBody] List<TInputCreate> listInputCreate)
    {
        try
        {
            return await ResponseAsync(await _service!.Create(listInputCreate));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
    #endregion

    #region Update
    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> Update([FromBody] TInputIdentityUpdate inputIdentityUpdate)
    {
        try
        {
            return await ResponseAsync(await _service!.Update(inputIdentityUpdate));

        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPut("Update/Multiple")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> Update([FromBody] List<TInputIdentityUpdate> listInputIdentityUpdate)
    {
        try
        {
            return await ResponseAsync(await _service!.Update(listInputIdentityUpdate));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<bool>> Delete([FromBody] TInputIdentityDelete inputIdentityDelete)
    {
        try
        {
            return await ResponseAsync(await _service!.Delete(inputIdentityDelete));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpDelete("Delete/Multiple")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<bool>> Delete([FromBody] List<TInputIdentityDelete> listInputIdentityDelete)
    {
        try
        {
            return await ResponseAsync(await _service!.Delete(listInputIdentityDelete));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
    #endregion

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool allowAnonymous = (from i in context.ActionDescriptor.EndpointMetadata
                               where i.GetType() == typeof(AllowAnonymousAttribute)
                               select i).Any();

        if (!allowAnonymous)
        {
            string email = User.FindFirst("user_email")!.Value;
            var loggedUser = await userService.GetByIdentifier(new InputIdentifierUser(email));
            if (loggedUser != null)
            {
                SetData();
                SessionData.SetLoggedUser(_guidSessionDataRequest, new LoggedUser(loggedUser.Id, loggedUser.Name, loggedUser.Email, loggedUser.Language));
            }
        }

        await base.OnActionExecutionAsync(context, next);
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

    protected async Task<ActionResult> ResponseAsync<T>(BaseResult<T> result)
    {
        if (!result.IsSuccess)
            return await Task.FromResult(StatusCode((int)HttpStatusCode.BadRequest, new BaseResponse<T> { ListError = result.ListDetailedError }));


        return await Task.FromResult(StatusCode((int)HttpStatusCode.OK, new BaseResponse<T>
        {
            Result = result.Value,
            ListError = result.ListDetailedError?.Count > 0 ? result.ListDetailedError : null,
            ListSuccess = result.ListDetailedSuccess?.Count > 0 ? result.ListDetailedSuccess : null,
        }));
    }

    protected async Task<ActionResult> ResponseAsync<ResponseType>(ResponseType result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return await Task.FromResult(StatusCode((int)statusCode, new BaseResponse<ResponseType> { Result = result }));
    }

    protected async Task<ActionResult> ResponseExceptionAsync(Exception ex)
    {
        return await Task.FromResult(StatusCode((int)HttpStatusCode.BadRequest, new BaseResponse<string> { ListError = [new DetailedError("", ex.Message)] }));
    }
}