using Microsoft.EntityFrameworkCore;
using Joiniv.Domain.Interfaces;

namespace Joiniv.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly JoinivDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(JoinivDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // This is the magic that switches the table based on T
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}