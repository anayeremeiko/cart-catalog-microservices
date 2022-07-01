using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(Category));
			navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
			builder.Property(x => x.ImageUrl).IsRequired(false);
			builder.Property<int?>("ParentCategoryForeignKey").IsRequired(false);
			builder.HasOne(x => x.ParentCategory).WithMany().HasForeignKey("ParentCategoryForeignKey");
		}
	}
}
