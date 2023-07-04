using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.ViewModels;

namespace HW_7_8.Infrastructure
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpensesEnumerableModel, ExpensesEnumerableViewModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Expenses.Sum(e => e.Cost)));
            CreateMap<Expense, ExpenseDataModel>().ReverseMap();
            CreateMap<ExpenseAddModel, ExpenseAddViewModel>().ReverseMap();
            CreateMap<ExpenseAddModel, ExpenseUpdateViewModel>().ReverseMap();
            CreateMap<ExpenseDataModel, ExpenseUpdateViewModel>()
                .ForMember(dest => dest.SelectedCategoryId, opt => opt.MapFrom(src => src.ExpenseCategory.Id.ToString()));
        }
    }
}