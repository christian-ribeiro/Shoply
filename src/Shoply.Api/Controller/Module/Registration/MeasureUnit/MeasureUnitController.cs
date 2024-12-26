using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class MeasureUnitController(IMeasureUnitService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IMeasureUnitService, IShoplyUnitOfWork, OutputMeasureUnit, InputIdentifierMeasureUnit, InputCreateMeasureUnit, InputUpdateMeasureUnit, InputIdentityUpdateMeasureUnit, InputIdentityDeleteMeasureUnit>(service, unitOfWork, userService) { }