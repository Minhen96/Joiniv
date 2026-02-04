using Joiniv.Domain.Entities;

namespace Joiniv.Application.Interfaces
{
    public interface IJwtTokenService{
        string GenerateToken(User user);
    }
}