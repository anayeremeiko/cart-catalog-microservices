using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
	public static class StartupSetup
	{
		public static void AddDbContext(IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
		}
	}
}
