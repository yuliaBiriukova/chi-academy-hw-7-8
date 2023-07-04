using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.BLL.Services;
using HW_7_8.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW_7_8.Controllers
{
    [Route("/categories")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetAllByUserIdAsync(userId);
            var model = new CategoriesEnumerableViewModel()
            {
                Categories = categories,
            };
            return View(model);
        }

        [HttpGet("new")]
        public IActionResult Add()
        {
            var model = new CategoryAddViewModel()
            {
                ReturnUrl = Request.Headers["Referer"].ToString()
            };
            return View(model);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                var category = _mapper.Map<CategoryDataModel>(model);
                var newCategoryId = await _categoryService.AddAsync(category, user);

                return Redirect(model.ReturnUrl ?? "/");
            }
            return View(model); 
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            var model = _mapper.Map<CategoryUpdateViewModel>(category);
            return View(model);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<CategoryDataModel>(model);
                await _categoryService.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}