using AutoMapper;
using Identity.Core.Entities;

namespace Identity.Infrastructure.Entities
{
	public class EntitiesProfile : Profile
	{
		public EntitiesProfile()
		{
			CreateMap<Role, UserRole>().ReverseMap();

			CreateMap<User, UserInformation>().ForMember(dest => dest.UserRoleId, opt => opt.MapFrom(src => src.UserRole.Id)).ReverseMap();
		}
	}
}
