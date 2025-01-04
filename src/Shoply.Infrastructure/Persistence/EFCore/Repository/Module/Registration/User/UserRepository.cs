using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Context;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Repository.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Repository.Module.Registration;

public class UserRepository(ShoplyDbContext context) : BaseRepository<ShoplyDbContext, User, InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>(context), IUserRepository { }