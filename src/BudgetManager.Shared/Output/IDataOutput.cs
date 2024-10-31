using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Shared.Models;

namespace BudgetManager.Shared.Output
{
    public interface IDataOutput
    {
        Task OutputData(IEnumerable<TransactionRow> data);
    }
}
