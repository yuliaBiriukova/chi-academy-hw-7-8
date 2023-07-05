using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace HW_7_8.BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExpenseService(IMapper mapper, IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ExpensesEnumerableModel> GetCurrentByUserIdAsync(string userId)
        {
            var model = new ExpensesEnumerableModel();
            model.Expenses = await _expenseRepository.GetExpensesAsync(model.Month, model.Year, userId);
            model.MonthNamesSelectList = GetMonthNamesSelectList();
            model.YearsSelectList = GetYearsSelectList();
            return model;
        }

        public async Task<ExpensesEnumerableModel> GetByMonthAsync(string userId, string monthName, int year)
        {
            var model = new ExpensesEnumerableModel(monthName, year);
            model.Expenses = await _expenseRepository.GetExpensesAsync(model.Month, model.Year, userId);
            model.MonthNamesSelectList = GetMonthNamesSelectList();
            model.YearsSelectList = GetYearsSelectList();
            return model;
        }

        public async Task<ExpenseDataModel> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(id);
            return _mapper.Map<ExpenseDataModel>(expense);
        }

        public async Task<int> AddAsync(ExpenseAddModel expense, IdentityUser user)
        {
            return await _expenseRepository.AddAsync(new Expense()
            {
                Cost = (int)expense.Cost,
                Comment = expense.Comment,
                DateCreated = (DateTime)expense.DateCreated,
                ExpenseCategory = await _categoryRepository.GetCategoryByIdAsync(Convert.ToInt32(expense.SelectedCategoryId)),
                User = user
            });
        }

        public async Task UpdateAsync(ExpenseAddModel updatedExpense)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(updatedExpense.Id);
            expense.Cost = updatedExpense.Cost;
            expense.Comment = updatedExpense.Comment;
            expense.DateCreated = updatedExpense.DateCreated;
            expense.ExpenseCategory = await _categoryRepository.GetCategoryByIdAsync(Convert.ToInt32(updatedExpense.SelectedCategoryId));
            await _expenseRepository.UpdateAsync(expense);
        }

        public async Task DeleteAsync(int id)
        {
            await _expenseRepository.DeleteAsync(id);
        }

        private List<SelectListItem> GetMonthNamesSelectList()
        {
            var monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.SkipLast(1).ToArray();
            var monthNamesSelectList = new List<SelectListItem>();
            foreach (var month in monthNames)
            {
                monthNamesSelectList.Add(new SelectListItem { Text = month, Value = month });
            }
            return monthNamesSelectList;
        }

        private List<SelectListItem> GetYearsSelectList()
        {
            var years = Enumerable.Range(2019, DateTime.Now.Year - 2019 + 1).OrderDescending();
            var yearsSelectList = new List<SelectListItem>();
            foreach (var year in years)
            {
                yearsSelectList.Add(new SelectListItem { Text = year.ToString(), Value = year.ToString() });
            }

            return yearsSelectList;
        }
    }
}