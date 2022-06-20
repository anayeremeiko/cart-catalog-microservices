using Autofac;
using Catalog.Core.Interfaces;
using Catalog.Core.Services;

namespace Catalog.Core
{
	public class DefaultCoreModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<CategoryService>()
				.As<ICategoryService>().InstancePerLifetimeScope();
		}
	}
}
