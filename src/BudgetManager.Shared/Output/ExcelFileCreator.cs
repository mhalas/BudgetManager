using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetManager.Shared.Extension;
using BudgetManager.Shared.Models;
using NLog;
using OfficeOpenXml;

namespace BudgetManager.Shared.Output
{
    public class ExcelFileCreator : IDataOutput
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();
        private const LicenseContext _licenseContext = LicenseContext.NonCommercial;

        private readonly string _originFilePath;

        public ExcelFileCreator(string originFilePath)
        {
            _originFilePath = originFilePath;
        }

        public Task OutputData(IEnumerable<TransactionRow> data)
        {
            Logger.Debug("Create Excel file.");

            ExcelPackage.LicenseContext = _licenseContext;
            using (var package = new ExcelPackage())
            {
                AddHistory(package, data);
                AddOutcome(package, data);
                AddIncome(package, data);

                var path = Path.GetDirectoryName(Path.GetFullPath(_originFilePath));
                path = Path.Combine(path, "Output");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fullFilePath = Path.Combine(path, $"AnalyzedHistory-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xlsx");

                Logger.Debug($"Save analyzed file to {fullFilePath}.");
                var file = new FileInfo(fullFilePath);
                package.SaveAs(file);
            }

            return Task.CompletedTask;
        }

        private void AddIncome(ExcelPackage package, IEnumerable<TransactionRow> data)
        {
            ExcelWorksheet income = package.Workbook.Worksheets.Add("Przychody");

            income.Cells[1, 1].Value = "Miesiąc";
            income.Cells[1, 1].AutoFilter = true;

            income.Cells[1, 2].Value = "Kategoria";
            income.Cells[1, 2].AutoFilter = true;

            income.Cells[1, 3].Value = "Suma";
            income.Cells[1, 3].AutoFilter = true;

            var incomeSummary = data.GetSummaryIncome();

            var rowNumber = 2;
            foreach (var summary in incomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                income.Cells[rowNumber, 1].Value = summary.Month;
                income.Cells[rowNumber, 2].Value = summary.Category;
                income.Cells[rowNumber, 3].Value = summary.Amount;

                rowNumber++;
            }
        }

        private void AddOutcome(ExcelPackage package, IEnumerable<TransactionRow> data)
        {
            ExcelWorksheet outcome = package.Workbook.Worksheets.Add("Wydatki");

            outcome.Cells[1, 1].Value = "Miesiąc";
            outcome.Cells[1, 1].AutoFilter = true;

            outcome.Cells[1, 2].Value = "Kategoria";
            outcome.Cells[1, 2].AutoFilter = true;

            outcome.Cells[1, 3].Value = "Suma";
            outcome.Cells[1, 3].AutoFilter = true;

            var outcomeSummary = data.GetSummaryOutcome();

            var rowNumber = 2;
            foreach (var summary in outcomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                outcome.Cells[rowNumber, 1].Value = summary.Month;
                outcome.Cells[rowNumber, 2].Value = summary.Category;
                outcome.Cells[rowNumber, 3].Value = summary.Amount;

                rowNumber++;
            }
        }

        private void AddHistory(ExcelPackage package, IEnumerable<TransactionRow> data)
        {
            ExcelWorksheet history = package.Workbook.Worksheets.Add("Historia");

            history.Cells[1, 1].Value = "Data waluty";
            history.Cells[1, 1].AutoFilter = true;

            history.Cells[1, 2].Value = "Kwota";
            history.Cells[1, 2].AutoFilter = true;

            history.Cells[1, 3].Value = "Opis";
            history.Cells[1, 3].AutoFilter = true;

            history.Cells[1, 4].Value = "Kategoria";
            history.Cells[1, 4].AutoFilter = true;

            int rowNumber = 2;
            foreach (var expense in data)
            {
                history.Cells[rowNumber, 1].Value = expense.ValueDate.ToString("yyyy-MM-dd");
                history.Cells[rowNumber, 2].Value = expense.Amount;
                history.Cells[rowNumber, 3].Value = expense.Description;
                history.Cells[rowNumber, 4].Value = expense.Category;

                rowNumber++;
            }

        }
    }
}
