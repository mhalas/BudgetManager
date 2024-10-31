using BudgetManager.Shared.Configuration;
using BudgetManager.Shared.Enum;
using BudgetManager.Shared.Output;

namespace BudgetManager.Shared.Factory
{
    public class DataOutputFactory(string originFilePath, ConfigurationDto configuration)
    {
        public IDataOutput GetDataOutput(OutputType type)
        {
            switch (type)
            {
                case OutputType.Excel:
                    return new ExcelFileCreator(originFilePath);
                case OutputType.CSV:
                default:
                    return new CsvFileCreator(originFilePath, configuration);


            }
        }
    }
}
