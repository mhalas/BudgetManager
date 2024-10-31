using BudgetManager.Shared.BankAnalyzer;
using BudgetManager.Shared.Output;

namespace BudgetManager.Shared.SourceData
{
    public interface ISourceDataExecutor
    {
        void Execute(IBankAnalyzer bankAnalyzer, IDataOutput outputLogic);
    }
}