using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using User.Api.DbConnection;
using User.Api.DTOs;
using User.Api.Utility;

namespace User.Api.Services
{
    public class AuthService 
    {
        private readonly DumriCommerceCollegeContext _context;

        private readonly JwtOptions _jwtOptions;

        public AuthService(DumriCommerceCollegeContext context, IOptions<JwtOptions> options)
        {
            _context = context;

            _jwtOptions = options.Value;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto model)
        {
            var user = await (
                            from u in _context.Users
                            join ur in _context.UserRoles on u.Id equals ur.UserId
                            join r in _context.MRoles on ur.RoleId equals r.RoleId
                            where u.Email == model.Email
                            select u
                        ).FirstOrDefaultAsync();

            if (user == null) return null;

            // Validate password (hash comparison)
            if (!PasswordHelper.ValidatePassword(model.Password, user.PasswordHash)) return null;


            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user); // Save changes to DB

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };


        }

        public string GenerateJwtToken(User.Api.DbEntities.User user)
        {
            var authClaims = new List<Claim>
                                { new Claim(ClaimTypes.Name, user.Name),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                };

            // Add role claims
                            var roles = (
                                            from u in _context.Users
                                            join ur in _context.UserRoles on u.Id equals ur.UserId
                                            join r in _context.MRoles on ur.RoleId equals r.RoleId
                                            where u.Email == user.Email
                                            select r.RoleName
                                        ).ToList();
            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

            var creds = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptions.ExpireMinutes)),
                claims: authClaims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshRequest request)
        {

            var principal = GetPrincipalFromExpiredToken(request.AccessToken);

            if (principal == null)
                return null;

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FindAsync(Convert.ToInt32(userId));
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                //return Unauthorized("Refresh token expired or invalid.");
                return null;

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }


        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = authSigningKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }

    }
}
