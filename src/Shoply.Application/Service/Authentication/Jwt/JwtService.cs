using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shoply.Application.Service.Authentication
{
    public class JwtService(IOptions<JwtConfiguration> options) : IJwtService
    {
        private readonly JwtConfiguration _jwtConfiguration = options.Value ?? throw new ArgumentNullException(nameof(JwtConfiguration));

        public Task<string> GenerateJwtToken(JwtUser jwtUser)
        {
            List<Claim> claims =
                [
                    new Claim(JwtRegisteredClaimNames.Sub, jwtUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, jwtUser.Name),
                    new Claim("user_email", jwtUser.Email),
                    new Claim("language", ((int)jwtUser.Language).ToString()),
                ];
            return GenerateJwtToken(claims);
        }

        public Task<string> GenerateJwtToken(List<Claim> claims)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfiguration.Issuer,
                ValidAudience = _jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key)),
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return Task.FromResult(principal);
        }

        public Task<string> GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Task.FromResult(Convert.ToBase64String(randomNumber));
        }
    }
}