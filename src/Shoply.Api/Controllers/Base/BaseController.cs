using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Interface.UnitOfWork;

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
    public virtual async Task<ActionResult<TOutput>> Get([FromRoute] long id)
    {
        try
        {
            var result = await _service!.Get(id);
            if (result == null)
                return NotFound("Não encontrado");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("GetListByListId")]
    public virtual async Task<ActionResult<List<TOutput>>> GetListByListId([FromBody] List<long> listId)
    {
        try
        {
            var result = await _service!.GetListByListId(listId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    public virtual async Task<ActionResult<List<TOutput>>> GetAll()
    {
        try
        {
            var result = await _service!.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("GetByIdentifier")]
    public virtual async Task<ActionResult<TOutput>> GetByIdentifier([FromBody] TInputIdentifier inputIdentifier)
    {
        try
        {
            var result = await _service!.GetByIdentifier(inputIdentifier);
            if (result == null)
                return NotFound("Não encontrado");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("GetListByListIdentifier")]
    public virtual async Task<ActionResult<List<TOutput>>> GetListByListIdentifier([FromBody] List<TInputIdentifier> listInputIdentifier)
    {
        try
        {
            var result = await _service!.GetListByListIdentifier(listInputIdentifier);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    #endregion

    #region Create
    [HttpPost("Create")]
    public virtual async Task<ActionResult<TOutput>> Create([FromBody] TInputCreate inputCreate)
    {
        try
        {
            var result = await _service!.Create(inputCreate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("Create/Multiple")]
    public virtual async Task<ActionResult<TOutput>> Create([FromBody] List<TInputCreate> listInputCreate)
    {
        try
        {
            var result = await _service!.Create(listInputCreate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    #endregion

    #region Update
    [HttpPut("Update")]
    public virtual async Task<ActionResult<TOutput>> Update([FromBody] TInputIdentityUpdate inputIdentityUpdate)
    {
        try
        {
            var result = await _service!.Update(inputIdentityUpdate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPut("Update/Multiple")]
    public virtual async Task<ActionResult<TOutput>> Update([FromBody] List<TInputIdentityUpdate> listInputIdentityUpdate)
    {
        try
        {
            var result = await _service!.Update(listInputIdentityUpdate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    public virtual async Task<ActionResult<bool>> Delete([FromBody] TInputIdentityDelete inputIdentityDelete)
    {
        try
        {
            var result = await _service!.Delete(inputIdentityDelete);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpDelete("Delete/Multiple")]
    public virtual async Task<ActionResult<bool>> Delete([FromBody] List<TInputIdentityDelete> listInputIdentityDelete)
    {
        try
        {
            var result = await _service!.Delete(listInputIdentityDelete);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
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
            Console.WriteLine("OnActionExecuting -> " + DateTime.Now.ToString());
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
}