using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products;
using App.Services.Products.Update;
using AutoMapper;

namespace App.Services.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        }
    }
}
