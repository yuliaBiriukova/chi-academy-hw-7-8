using HW_7_8.DAL.Database;
using HW_7_8.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeAccountingDbContext _dbContext;

        public CategoryRepository(HomeAccountingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId)
        {
            return await _dbContext.Categories.Where(c => c.User.Id == userId || c.User.Id == null).ToListAsync();
        }

        public async Task<int> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}