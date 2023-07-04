using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace HW_7_8.BLL.Models
{
    public class ExpensesEnumerableModel
    {
        public IEnumerable<Expense>? Expenses { get; set; }

        public string MonthName { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public List<SelectListItem>? MonthNamesSelectList { get; set; }

        public List<SelectListItem>? YearsSelectList { get; set; }

        public ExpensesEnumerableModel()
        {
            DateTime currentDate = DateTime.Now;
            Month = currentDate.Month;
            Year = currentDate.Year;
            MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month);
        }

        public ExpensesEnumerableModel(string monthName, int year)
        {
            Year = year;
            MonthName = monthName;
            Month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.InvariantCulture).Month;
        }
    }
}