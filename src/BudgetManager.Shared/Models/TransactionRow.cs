using System;

namespace BudgetManager.Shared.Models
{
    public class TransactionRow(DateTime valueDate, decimal amount, string description, string category)
    {
        public DateTime ValueDate { get; } = valueDate;

        public decimal Amount { get; } = amount;

        public string Description { get; } = description;

        public string Category { get; } = category;
    }
}
