using KyrsachAPI.Context;
using KyrsachAPI.Entities;
using KyrsachAPI.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KyrsachAPI.Service
{
    public interface ITokenService
    {
        string GenerateNewToken(User user);
    }

    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly AppSettings _appSettings;
        private readonly TaskTrackContext _context;

        public TokenService(ILogger<TokenService> logger, TaskTrackContext context, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }

        private TimeSpan tokenExpTime = TimeSpan.FromHours(12);

        public string GenerateNewToken(User user)
        {
            string JWTKey = _appSettings.SecretJWTKey;

            var userRole = _context.UserRoles.SingleOrDefault(r => r.RoleId == user.UserId);
            var claims = new List<Claim>();
            claims.Add(new Claim("userId", user.UserId.ToString()));
            claims.Add(new Claim("role", userRole.RoleName));
            var jwt = new JwtSecurityToken(
                issuer: "TastTreakServer",
                audience: "TastTreakServerUser",
                claims: claims,
                expires: DateTime.UtcNow.Add(tokenExpTime),
                signingCredentials: new SigningCredentials( new SymmetricSecurityKey( Encoding.UTF8.GetBytes(JWTKey)), SecurityAlgorithms.HmacSha256));
            var Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Token;
        }
    }
}
