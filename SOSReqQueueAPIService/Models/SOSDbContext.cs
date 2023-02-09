using System;
using Microsoft.EntityFrameworkCore;

namespace SOSReqQueueAPIService.Models
{
    public class SOSDbContext : DbContext
    {
        public SOSDbContext(DbContextOptions<SOSDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<SOSRequest> SOSRequests { get; set; }
        public virtual DbSet<FIR> FIRs { get; set; }
        public DbSet<SOSReqQueue> SOSReqQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Priority>().ToTable(nameof(Priorities), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Status>().ToTable(nameof(Statuses), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<SOSRequest>().ToTable(nameof(SOSRequests), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<FIR>().ToTable(nameof(FIRs), t => t.ExcludeFromMigrations());
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is TimeStamps && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((TimeStamps)entity.Entity).CreatedAt = now;
                }
                ((TimeStamps)entity.Entity).UpdatedAt = now;
            }
        }

    }
}

