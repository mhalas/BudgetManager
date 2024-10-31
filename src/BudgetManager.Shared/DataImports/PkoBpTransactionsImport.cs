using System.Threading.Tasks;
using BudgetManager.DataAccess;

namespace BudgetManager.Shared.DataImports
{
    public class PkoBpTransactionsImport(BudgetManagerContext context)
    {
        public Task ImportDataAsync(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}