using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<CategoryDTO>
	{
		public void Configure(EntityTypeBuilder<CategoryDTO> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(CategoryDTO));
			navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
			builder.Property(x => x.ImageUrl).IsRequired(false);
		}
	}
}
