using User.Api.DTOs;

namespace User.Api.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto model);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshRequest request);

    }
}
