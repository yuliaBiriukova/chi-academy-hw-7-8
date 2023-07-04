using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> GetCategoryByIdAsync(int id);

        public Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId);

        public Task<int> AddAsync(Category category);

        public Task DeleteAsync(int id);

        public Task UpdateAsync(Category category);
    }
}