namespace ExpenseTrackerAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property: one category has many expenses
        public List<Expense> Expenses { get; set; } = new();
    }
}
