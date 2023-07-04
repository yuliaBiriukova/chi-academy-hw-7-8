namespace HW_7_8.ViewModels
{
    public class ExpenseUpdateViewModel : ExpenseAddViewModel
    {
        public int Id { get; set; }
        public string? ReturnUrl { get; set; }
    }
}