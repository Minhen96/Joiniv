using Microsoft.EntityFrameworkCore;
using Joiniv.Domain.Entities;
using Joiniv.Domain.Interfaces;

namespace Joiniv.Infrastructure.Data.Repositories
{
    // 1. Inherit from Repository<User> to get the CRUD for free!
    // 2. Implement IUserRepository to satisfy the email lookup.
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(JoinivDbContext context) : base(context){}

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}