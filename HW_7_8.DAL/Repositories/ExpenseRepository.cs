using HW_7_8.DAL.Database;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.DAL.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly HomeAccountingDbContext _dbContext;

        public ExpenseRepository(HomeAccountingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return await _dbContext.Expenses.Include(e => e.ExpenseCategory).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expense?> GetExpenseWithoutCategoryAsync(int id)
        {
            return await _dbContext.Expenses.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(int month, int year)
        {
            return await _dbContext.Expenses
                .Include(e => e.ExpenseCategory)
                .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year)
                .OrderByDescending(e => e.DateCreated)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(int month, int year, string userId)
        {
            return await _dbContext.Expenses
                .Include(e => e.ExpenseCategory)
                .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year && e.User.Id == userId)
                .OrderByDescending(e => e.DateCreated)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(int month, int year, Category category)
        {
            return await _dbContext.Expenses
                .Include(e => e.ExpenseCategory)
                .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year && e.ExpenseCategory == category)
                .ToListAsync();
        }

        public async Task<int> AddAsync(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return expense.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await GetExpenseWithoutCategoryAsync(id);
            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
            await _dbContext.SaveChangesAsync();
        }
    }
}