using System;
using Microsoft.EntityFrameworkCore;
using SOSRequestsAPIService.Models;

namespace SOSRequestsAPIService.Models
{
    public class SOSDbContext : DbContext
    {
        public SOSDbContext(DbContextOptions<SOSDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SOSRequest> SOSRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
        }
    }
}

