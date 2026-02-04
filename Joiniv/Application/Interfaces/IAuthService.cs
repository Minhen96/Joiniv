using Joiniv.Contracts.Auth;

namespace Joiniv.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}