using AutoMapper;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Categories.Dtos;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}
