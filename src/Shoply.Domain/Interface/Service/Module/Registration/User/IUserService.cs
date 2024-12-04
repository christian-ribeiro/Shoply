﻿using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IUserService : IBaseService<InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, InputIdentityUpdateUser, InputIdentityDeleteUser>
{
    Task<string> Authenticate(InputAuthenticateUser inputAuthenticateUser);
}