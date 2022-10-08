using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebShop.Data.Entities;
using WebShop.Models;
using WebShop.Models.Helpers;

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

            CreateMap<CategoryEntity, SelectItemViewModel>();

            CreateMap<SelectItemViewModel, SelectListItem>()
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Id));

            CreateMap<ProductEntity, ProductItemViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));

            CreateMap<ProductCreateViewModel, ProductEntity>();

            CreateMap<ProductEntity, ProductEditViewModel>()
                .ReverseMap();

            CreateMap<ProductEntity, ProductDetailsViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages
                .Select(i => $@"/images/{i.Name}")));

        }
    }
}
