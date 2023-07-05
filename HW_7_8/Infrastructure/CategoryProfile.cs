using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HW_7_8.Infrastructure
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDataModel>().ReverseMap();
            CreateMap<CategoryDataModel, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<CategoryAddViewModel, CategoryDataModel >();
            CreateMap<Category, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}