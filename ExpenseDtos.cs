namespace ExpenseTrackerAPI.DTOs
{
    // Shape returned to the client
    public class ExpenseReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    // Shape accepted when creating an expense
    public class ExpenseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
    }

    // Shape accepted when updating an expense
    public class ExpenseUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
    }
}
