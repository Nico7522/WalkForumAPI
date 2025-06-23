using WalkForum.Domain.Entities;
using AutoMapper;
namespace WalkForum.Application.Categories.Dtos;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}
