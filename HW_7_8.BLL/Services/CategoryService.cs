using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllByUserIdAsync(string userId)
        {
            return await _categoryRepository.GetCategoriesByUserIdAsync(userId);
        }

        public async Task<CategoryDataModel> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDataModel>(category);
        }

        public async Task<int> AddAsync(CategoryDataModel category, IdentityUser user)
        {
            return await _categoryRepository.AddAsync(new Category()
            {
                Name = category.Name,
                User = user
            });
        }

        public async Task UpdateAsync(CategoryDataModel newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}