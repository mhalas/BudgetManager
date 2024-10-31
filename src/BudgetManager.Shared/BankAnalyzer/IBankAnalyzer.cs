using System.Collections.Generic;
using BudgetManager.Shared.Models;

namespace BudgetManager.Shared.BankAnalyzer
{
    public interface IBankAnalyzer
    {
        bool CanExecute();
        IEnumerable<TransactionRow> AnalyzeExpenseHistory(string historyData);
    }
}
