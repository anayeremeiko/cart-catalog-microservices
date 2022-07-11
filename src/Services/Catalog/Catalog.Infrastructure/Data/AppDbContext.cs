using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<CategoryDTO> Categories { get; set; }

		public DbSet<ItemDTO> Items { get; set; }

		//public DbSet<CategoryDTO> NewCategories { get; set; }

		//public DbSet<ItemDTO> NewItems { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
