using HW_7_8.DAL.Entities;

namespace HW_7_8.ViewModels
{
    public class ExpenseFilter
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string? UserId { get; set; }

        public Category? Category { get; set; }

    }
}