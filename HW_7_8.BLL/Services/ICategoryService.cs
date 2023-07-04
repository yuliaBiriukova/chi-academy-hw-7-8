using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllByUserIdAsync(string userId);

        public Task<CategoryDataModel> GetCategoryByIdAsync(int id);

        public Task<int> AddAsync(CategoryDataModel category, IdentityUser user);

        public Task UpdateAsync(CategoryDataModel newCategory);

        public Task DeleteAsync(int id);
    }
}