using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
	public class ItemConfiguraton : IEntityTypeConfiguration<ItemDTO>
	{
		public void Configure(EntityTypeBuilder<ItemDTO> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(CategoryDTO));
			navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
			builder.Property(i => i.Price).IsRequired();
			builder.Property(i => i.Amount).IsRequired();
			builder.Property(i => i.Description).IsRequired(false);
			builder.Property(i => i.ImageUrl).IsRequired(false);
		}
	}
}
