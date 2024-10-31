namespace BudgetManager.Shared.Models
{
    public class MonthlyTransactionSummary(int month, string category, decimal amount)
    {
        public int Month { get; } = month;
        public string Category { get; } = category;
        public decimal Amount { get; } = amount;
    }
}
