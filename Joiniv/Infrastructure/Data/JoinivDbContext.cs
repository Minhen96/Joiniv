using Joiniv.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Joiniv.Infrastructure.Data
{
    public class JoinivDbContext : DbContext
    {
        public JoinivDbContext(DbContextOptions<JoinivDbContext> options) : base(options)
        {
        }

        // Standard practice to include these for easy querying
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventImage> EventImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // This tells EF Core: "Go find every class that implements IEntityTypeConfiguration and apply it"
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JoinivDbContext).Assembly);
        }
    }
}
