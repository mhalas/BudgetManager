using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Shared.Configuration;
using BudgetManager.Shared.Extension;
using BudgetManager.Shared.Models;
using NLog;

namespace BudgetManager.Shared.Output
{
    public class CsvFileCreator : IDataOutput
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _originFilePath;
        private readonly ConfigurationDto _configuration;

        public CsvFileCreator(string originFilePath,
            ConfigurationDto configuration)
        {
            _originFilePath = originFilePath;
            _configuration = configuration;
        }

        public Task OutputData(IEnumerable<TransactionRow> data)
        {
            Logger.Debug("Create CSV file.");

            StringBuilder stringBuilder = new StringBuilder();
            FillWithData(data, stringBuilder);

            if (_configuration.GenerateSummary)
            {
                FillWithSummary(data, stringBuilder);
            }

            var result = stringBuilder.ToString();
            GenerateOutputFile(result);

            return Task.CompletedTask;
        }

        private void FillWithData(IEnumerable<TransactionRow> data, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("\"Data waluty\",\"Kwota\",\"Opis\",\"Kategoria\"");

            foreach (var row in data)
            {
                stringBuilder.AppendLine($"\"{row.ValueDate.ToString("yyyy-MM-dd")}\",\"{GetAmount(row)}\",\"{row.Description}\",\"{row.Category}\"");
            }
        }

        private string GetAmount(TransactionRow row)
        {
            if(row.Amount >= 0)
            {
                return @$"+{row.Amount}";
            }

            return row.Amount.ToString();
        }

        private void FillWithSummary(IEnumerable<TransactionRow> data, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Wydatki,,");
            stringBuilder.AppendLine("\"Miesiąc\",\"Kategoria\",\"Suma\"");

            var outcomeSummary = data.GetSummaryOutcome();

            foreach (var s in outcomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                stringBuilder.AppendLine($"\"{s.Month}\",\"{s.Category}\",\"{s.Amount}\"");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Przychód,,");
            stringBuilder.AppendLine("\"Miesiąc\",\"Kategoria\",\"Suma\"");

            var incomeSummary = data.GetSummaryIncome();

            foreach (var s in incomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                stringBuilder.AppendLine($"\"{s.Month}\",\"{s.Category}\",\"{s.Amount}\"");
            }
        }

        private Task GenerateOutputFile(string result)
        {
            if (!Directory.Exists(_configuration.OutputPath))
                Directory.CreateDirectory(_configuration.OutputPath);

            var fullFilePath = Path.Combine(_configuration.OutputPath, $"AnalyzedHistory-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.csv");

            Logger.Info($"Save analyzed file to {fullFilePath}.");
            File.WriteAllText(fullFilePath, result, Encoding.UTF8);

            return Task.CompletedTask;
        }
    }
}
