using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Repositories
{
    public interface IExpenseRepository
    {
        public Task<Expense?> GetExpenseByIdAsync(int id);

        public Task<Expense?> GetExpenseWithoutCategoryAsync(int id);

        public Task<IEnumerable<Expense>> GetExpensesAsync(int month, int year, string userId);

        public Task<int> AddAsync(Expense expense);

        public Task DeleteAsync(int id);

        public Task UpdateAsync(Expense expense);
    }
}