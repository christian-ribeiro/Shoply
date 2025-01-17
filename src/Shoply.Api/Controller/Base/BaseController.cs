﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shoply.Application.Extensions;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Filter;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.DataAnnotation;
using Shoply.Arguments.Enum.Base;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Interface.UnitOfWork;
using Shoply.Domain.Utils;
using System.Net;
using System.Text.Json;

namespace Shoply.Api.Controller.Base;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public abstract class BaseController<TService, TUnitOfWork, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentifier, TOutput>(TService service, TUnitOfWork unitOfWork, IUserService userService) : Microsoft.AspNetCore.Mvc.Controller
    where TUnitOfWork : IBaseUnitOfWork
    where TService : IBaseService<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentifier, TOutput>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
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
            return await ResponseAsync(PrepareReturn(await _service!.Get(id)));
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
            return await ResponseAsync(PrepareReturn(await _service!.GetListByListId(listId)));
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
            return await ResponseAsync(PrepareReturn(await _service!.GetAll()));
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
            return await ResponseAsync(PrepareReturn(await _service!.GetByIdentifier(inputIdentifier)));
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
            return await ResponseAsync(PrepareReturn(await _service!.GetListByListIdentifier(listInputIdentifier)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("GetByFilter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> GetByFilter([FromBody] InputFilter inputFilter)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.GetByFilter(inputFilter)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("GetListByFilter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TOutput>> GetListByFilter([FromBody] InputFilter inputFilter)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.GetListByFilter(inputFilter)));
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
            return await ResponseAsync(PrepareReturn(await _service!.Create(inputCreate)));
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
            return await ResponseAsync(PrepareReturn(await _service!.Create(listInputCreate)));
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
            return await ResponseAsync(PrepareReturn(await _service!.Update(inputIdentityUpdate)));

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
            return await ResponseAsync(PrepareReturn(await _service!.Update(listInputIdentityUpdate)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
    #endregion

    #region Delete
    [HeaderIgnore]
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

    [HeaderIgnore]
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
        await unitOfWork.BeginTransactionAsync();

        bool allowAnonymous = (from i in context.ActionDescriptor.EndpointMetadata
                               where i.GetType() == typeof(AllowAnonymousAttribute)
                               select i).Any();

        SetGuid();

        if (!allowAnonymous)
        {
            var response = await SetData();
            if (response.Redirect)
            {
                context.Result = response.Action;
                return;
            }
        }

        SetReturnProperty();

        await base.OnActionExecutionAsync(context, next);
    }

    public override async void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        SessionData.RemoveSessionDataRequest(_guidSessionDataRequest);
        NotificationHelper.Remove(_guidSessionDataRequest);
        await unitOfWork.CommitAsync();
    }

    private async Task<(bool Redirect, ActionResult? Action)> SetData()
    {
        string email = User.FindFirst("user_email")!.Value;
        Guid loginKey = Guid.Parse(User.FindFirst("login_key")!.Value);

        var loggedUser = await userService.GetByIdentifier(new InputIdentifierUser(email));
        if (loggedUser != null)
        {
            if (loginKey != loggedUser.LoginKey)
            {
                var action = Unauthorized(new BaseResponse<string> { ListNotification = [new(loggedUser.Email, ["Houve outro acesso com este usuário. Realize o login novamente"], EnumNotificationType.Error)] });
                return (true, action);
            }

            SessionData.SetLoggedUser(_guidSessionDataRequest, new LoggedUser(loggedUser.Id, loggedUser.Name, loggedUser.Email, loggedUser.Language));
        }

        return (false, default);
    }

    private void SetGuid()
    {
        SessionHelper.SetGuidSessionDataRequest(this, _guidSessionDataRequest);
    }

    private void SetReturnProperty()
    {
        string? returnProperty = Request.HttpContext.GetHeader<string>("RETURN-PROPERTY");
        if (!string.IsNullOrEmpty(returnProperty))
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(returnProperty, new JsonDocumentOptions { AllowTrailingCommas = true });
                if (document.RootElement.ValueKind == JsonValueKind.Array)
                    SessionData.SetReturnProperty(_guidSessionDataRequest, JsonSerializer.Deserialize<List<string>>(returnProperty));
            }
            catch { }
        }
    }

    protected async Task<ActionResult> ResponseAsync<T>(BaseResult<T>? result)
    {
        if (result == null)
            return await Task.FromResult(StatusCode((int)HttpStatusCode.InternalServerError));

        if (!result.IsSuccess)
            return await Task.FromResult(StatusCode((int)HttpStatusCode.BadRequest, new BaseResponse<T> { ListNotification = result.ListNotification }));


        return await Task.FromResult(StatusCode((int)HttpStatusCode.OK, new BaseResponse<T>
        {
            Result = result.Value,
            ListNotification = result.ListNotification?.Count > 0 ? result.ListNotification : null
        }));
    }

    protected async Task<ActionResult> ResponseAsync<ResponseType>(ResponseType result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return await Task.FromResult(StatusCode((int)statusCode, new BaseResponse<ResponseType> { Result = result }));
    }

    protected async Task<ActionResult> ResponseAsync(BaseResult<object> result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (!result.IsSuccess)
            return await Task.FromResult(StatusCode((int)HttpStatusCode.BadRequest, new BaseResponse<object> { ListNotification = result.ListNotification }));


        return await Task.FromResult(StatusCode((int)HttpStatusCode.OK, new BaseResponse<object>
        {
            Result = result.Value,
            ListNotification = result.ListNotification?.Count > 0 ? result.ListNotification : null
        }));
    }

    protected async Task<ActionResult> ResponseExceptionAsync(Exception ex)
    {
        return await Task.FromResult(StatusCode((int)HttpStatusCode.BadRequest, new BaseResponse<string> { ListNotification = [new DetailedNotification("", [ex.Message], EnumNotificationType.Error)] }));
    }

    public BaseResult<object>? PrepareReturn<T>(BaseResult<T>? input)
    {
        return PrepareResponse.PrepareReturn(_guidSessionDataRequest, input);
    }

    public object? PrepareReturn<T>(T input)
    {
        return PrepareResponse.PrepareReturn(_guidSessionDataRequest, input);
    }
}

public abstract class BaseController<TService, TUnitOfWork, TInputCreate, TInputIdentityDelete, TInputIdentifier, TOutput>(TService service, TUnitOfWork unitOfWork, IUserService userService) : BaseController<TService, TUnitOfWork, TInputCreate, BaseInputUpdate_0, BaseInputIdentityUpdate_0, TInputIdentityDelete, TInputIdentifier, TOutput>(service, unitOfWork, userService)
    where TUnitOfWork : IBaseUnitOfWork
    where TService : IBaseService<TInputCreate, TInputIdentityDelete, TInputIdentifier, TOutput>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<TOutput>> Update([FromBody] BaseInputIdentityUpdate_0 inputIdentityUpdate)
    {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<TOutput>> Update([FromBody] List<BaseInputIdentityUpdate_0> listInputIdentityUpdate)
    {
        throw new NotImplementedException();
    }
}