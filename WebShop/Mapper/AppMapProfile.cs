using AutoMapper;
using WebShop.Data.Entities;
using WebShop.Models;

namespace WebShop.Mapper
{
    public class AppMapProfile:Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryCreateViewModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => @"/images/" +
                (string.IsNullOrEmpty(x.Image) ? "default.jpg" : x.Image)));

        }
    }
}
