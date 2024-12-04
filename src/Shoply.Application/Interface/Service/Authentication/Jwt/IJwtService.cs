using Shoply.Application.Argument.Authentication;
using System.Security.Claims;

namespace Shoply.Application.Interface.Service.Authentication
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(JwtUser jwtUser);
        Task<string> GenerateJwtToken(List<Claim> claims);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<string> GenerateRefreshToken();
    }
}