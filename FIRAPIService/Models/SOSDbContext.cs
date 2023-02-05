using System;
using Microsoft.EntityFrameworkCore;

namespace FIRAPIService.Models
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Priority>().ToTable(nameof(Priorities), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Status>().ToTable(nameof(Statuses), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<SOSRequest>().ToTable(nameof(SOSRequests), t => t.ExcludeFromMigrations());
        }
    }
}

