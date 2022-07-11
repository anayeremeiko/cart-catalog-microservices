using AutoMapper;
using Catalog.Core.Entities;

namespace Catalog.Infrastructure.Entities
{
	public class EntitiesProfile : Profile
	{
		public EntitiesProfile()
		{
			CreateMap<Category, CategoryDTO>().ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategory.Id)).ReverseMap();

			CreateMap<Item, ItemDTO>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id)).ReverseMap();
		}
	}
}
