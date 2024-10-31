using System.Collections.Generic;

namespace BudgetManager.Shared.Configuration
{
    public class ConfigurationDto
    {
        public ConfigurationDto(
            string defaultIncomeCategoryName,
            string defaultExpenseCategoryName,

            string sourceFilesPath,
            string outputPath,

            bool useAbsoluteValuesForAmount,
            bool generateSummary,

            IDictionary<string, List<ConfigurationColumnDto>> columnDefinitions,
            IDictionary<string, List<string>> categoryDictionary)
        {
            DefaultIncomeCategoryName = defaultIncomeCategoryName;
            DefaultExpenseCategoryName = defaultExpenseCategoryName;

            SourceFilesPath = sourceFilesPath;
            OutputPath = outputPath;

            UseAbsoluteValuesForAmount = useAbsoluteValuesForAmount;
            GenerateSummary = generateSummary;

            ColumnDefinitions = columnDefinitions;
            CategoryDictionary = categoryDictionary;
        }
        public string DefaultIncomeCategoryName { get; }
        public string DefaultExpenseCategoryName { get; }

        public string SourceFilesPath { get; }
        public string OutputPath { get; }

        public bool UseAbsoluteValuesForAmount { get; }
        public bool GenerateSummary { get; }

        public IDictionary<string, List<ConfigurationColumnDto>> ColumnDefinitions { get; }
        public IDictionary<string, List<string>> CategoryDictionary { get; }
    }
}
