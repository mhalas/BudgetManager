using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BudgetManager.Shared.BankAnalyzer;
using BudgetManager.Shared.Models;
using BudgetManager.Shared.Output;
using NLog;

namespace BudgetManager.Shared.SourceData
{
    public class FolderDataSource : ISourceDataExecutor
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _filesSourcePath;

        public FolderDataSource(string filesSourcePath)
        {
            _filesSourcePath = filesSourcePath;
        }

        public void Execute(IBankAnalyzer bankAnalyzer, IDataOutput outputLogic)
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            var expenseHistory = new List<TransactionRow>();

            var files = Directory.GetFiles(_filesSourcePath);
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    Logger.Info($@"Start analyzing file {file}.");
                    var result = bankAnalyzer.AnalyzeExpenseHistory(reader.ReadToEnd());
                    expenseHistory.AddRange(result);
                    Logger.Info("Analyze complete.");
                }
            }

            outputLogic.OutputData(expenseHistory);
            time.Stop();

            Logger.Info($"Complete in time {time.Elapsed.Hours}:{time.Elapsed.Minutes}:{time.Elapsed.Seconds}.{time.Elapsed.Milliseconds}.");
        }
    }
}