using HW_7_8.BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW_7_8.BLL.Services
{
    public interface IExpenseService
    {
        public Task<ExpensesEnumerableModel> GetCurrentByUserIdAsync(string userId);

        public Task<ExpensesEnumerableModel> GetByMonthAsync(string userId, string monthName, int year);

        public Task<ExpenseDataModel> GetByIdAsync(int id);

        public Task<int> AddAsync(ExpenseAddModel expense, IdentityUser user);

        public Task UpdateAsync(ExpenseAddModel updatedExpense);

        public Task DeleteAsync(int id);
    }
}