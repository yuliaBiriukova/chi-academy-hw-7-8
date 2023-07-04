using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HW_7_8.ViewModels
{
    public class ExpenseAddViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue)]
        public int? Cost { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime? DateCreated { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string? SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem>? CategoriesSelectList { get; set; }

        public string CurrentDate { get; set; }

        public string MinDate { get; set; }

        public ExpenseAddViewModel()
        {
            CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            MinDate = "2019-01-01";
        }
    }
}