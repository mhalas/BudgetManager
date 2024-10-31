using System.Diagnostics;
using System.IO;
using BudgetManager.Shared.BankAnalyzer;
using BudgetManager.Shared.Output;
using NLog;

namespace BudgetManager.Shared.SourceData
{
    public class SingleFileDataSource : ISourceDataExecutor
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _filePath;

        public SingleFileDataSource(string filePath)
        {
            _filePath = filePath;
        }

        public void Execute(IBankAnalyzer bankAnalyzer, IDataOutput outputLogic)
        {
            using (var reader = new StreamReader(_filePath))
            {
                Stopwatch time = new Stopwatch();

                Logger.Info($@"Start analyzing file {_filePath}.");
                time.Start();
                var result = bankAnalyzer.AnalyzeExpenseHistory(reader.ReadToEnd());
                Logger.Info("Analyze complete.");

                outputLogic.OutputData(result);

                time.Stop();
                Logger.Info($"Complete in time {time.Elapsed.Hours}:{time.Elapsed.Minutes}:{time.Elapsed.Seconds}.{time.Elapsed.Milliseconds}.");
            }
        }
    }
}