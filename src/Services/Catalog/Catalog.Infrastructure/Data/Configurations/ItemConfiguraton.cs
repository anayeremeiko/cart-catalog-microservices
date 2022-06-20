using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
	public class ItemConfiguraton : IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(Category));
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
			builder.Property(i => i.Price).IsRequired();
			builder.Property(i => i.Amount).IsRequired();
			builder.Property<int>("CategoryForeignKey");
			builder.HasOne(i => i.Category).WithOne().HasForeignKey("CategoryForeignKey");
		}
	}
}
