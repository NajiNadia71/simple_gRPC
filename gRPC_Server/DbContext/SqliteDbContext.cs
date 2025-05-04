
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using gRPC_Server.Entities;

#region Database context
namespace gRPC_Server.DbContexts
{
	public class SqliteDbContext : DbContext
	{
		protected readonly IConfiguration Configuration;
		public SqliteDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}
	
		public DbSet<ProductionType> ProductionTypes { get; set; } 
		public DbSet<Production> Productions { get; set; } 
		public DbSet<gRPC_Server.Entities.Ad> Ads { get; set; } 
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(Configuration.GetConnectionString("connectionString"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new AdConfiguration());
			modelBuilder.ApplyConfiguration(new ProductionTypeConfiguration());
			modelBuilder.ApplyConfiguration(new ProductionConfiguration());
	
		}

	}

}



#endregion
