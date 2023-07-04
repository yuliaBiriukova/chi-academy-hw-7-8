using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.BLL.Services;
using HW_7_8.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;

namespace HW_7_8.Controllers
{
    [Authorize]
    [Route("/expenses")]
    public class ExpensesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public ExpensesController(IMapper mapper, IExpenseService expenseService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _expenseService = expenseService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [Route("", Order = 1)]
        [Route("/", Order = 2)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetCurrentByUserIdAsync(userId);
            var model = _mapper.Map<ExpensesEnumerableViewModel>(expenses);
            return View(model);
        }

        [HttpGet("by-month")]
        public async Task<IActionResult> MonthExpenses(string monthName, int year)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetByMonthAsync(userId, monthName, year);
            var model = _mapper.Map<ExpensesEnumerableViewModel>(expenses);
            return View(model);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Add()
        {
            var model = new ExpenseAddViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCategories = await _categoryService.GetAllByUserIdAsync(userId);
            if (userCategories != null)
            {
                model.CategoriesSelectList = _mapper.Map<IEnumerable<SelectListItem>>(userCategories);
            }
            return View(model);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add(ExpenseAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                var expense = _mapper.Map<ExpenseAddModel>(model);
                var newExpenseId = await _expenseService.AddAsync(expense, user);

                var dateCreated = (DateTime)model.DateCreated;
                var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dateCreated.Month);
                
                return RedirectToAction("MonthExpenses", 
                    new { MonthName = monthName,
                    Year = dateCreated.Year}) ;
            }
            return View();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Update(int id) 
        {
            var expense = await _expenseService.GetByIdAsync(id);
            var model = _mapper.Map<ExpenseUpdateViewModel>(expense);
            model.ReturnUrl = Request.Headers["Referer"].ToString();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCategories = await _categoryService.GetAllByUserIdAsync(userId);

            if (userCategories != null)
            {
                model.CategoriesSelectList = _mapper.Map<IEnumerable<SelectListItem>>(userCategories);
            }
            return View(model);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Update(ExpenseUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var updatedExpense = _mapper.Map<ExpenseAddModel>(model);
                await _expenseService.UpdateAsync(updatedExpense);
                return Redirect(model.ReturnUrl ?? "/");
            }
            return View(model);
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseService.DeleteAsync(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}