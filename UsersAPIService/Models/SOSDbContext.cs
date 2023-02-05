using System;
using Microsoft.EntityFrameworkCore;

namespace UsersAPIService.Models
{
	public class SOSDbContext: DbContext
	{
		public SOSDbContext(DbContextOptions<SOSDbContext> options): base(options)
		{
		}

		public DbSet<User> Users { get; set; }
    }
}

