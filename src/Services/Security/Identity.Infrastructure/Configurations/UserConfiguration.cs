using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<UserInformation>
	{
		public void Configure(EntityTypeBuilder<UserInformation> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(UserRole));
			navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasKey(i => i.UserName);
			builder.Property(i => i.UserRoleId).HasDefaultValue(1);
		}
	}
}
