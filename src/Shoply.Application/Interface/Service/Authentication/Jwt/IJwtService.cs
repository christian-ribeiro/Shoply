using Shoply.Application.Argument.Authentication;
using Shoply.Arguments.Argument.Base;
using System.Security.Claims;

namespace Shoply.Application.Interface.Service.Authentication;

public interface IJwtService
{
    Task<string> GenerateJwtToken(JwtUser jwtUser);
    Task<string> GenerateJwtToken(List<Claim> claims);
    Task<BaseResult<ClaimsPrincipal>> GetPrincipalFromExpiredToken(string token);
    Task<string> GenerateRefreshToken();
}