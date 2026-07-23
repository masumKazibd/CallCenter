using CallCenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agent> Agents => Set<Agent>(); 
        public DbSet<Queue> Queues => Set<Queue>();
        public DbSet<Call> Calls => Set<Call>();
        public DbSet<CallEvent> CallEvents => Set<CallEvent>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Agent>().HasIndex(a => a.Email).IsUnique();
            b.Entity<Agent>().HasIndex(a => a.Extension).IsUnique();

            b.Entity<Call>().HasIndex(c => c.StartedAt);
            b.Entity<Call>().HasIndex(c => c.Status);

            b.Entity<Call>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Call)
                .HasForeignKey(e => e.CallId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<Agent>().Property(a => a.Status).HasConversion<string>();
            b.Entity<Call>().Property(c => c.Direction).HasConversion<string>();
            b.Entity<Call>().Property(c => c.Status).HasConversion<string>();

            base.OnModelCreating(b);
        }
    }
}
