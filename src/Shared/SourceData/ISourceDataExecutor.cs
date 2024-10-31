using Shared.BankAnalyzer;
using Shared.Output;

namespace Shared.SourceData
{
    public interface ISourceDataExecutor
    {
        void Execute(IBankAnalyzer bankAnalyzer, IDataOutput outputLogic);
    }
}
