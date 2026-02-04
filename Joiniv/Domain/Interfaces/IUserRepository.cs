using Joiniv.Domain.Entities;

namespace Joiniv.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}