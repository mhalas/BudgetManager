using BudgetManager.Shared.Enum;
using BudgetManager.Shared.Output;

namespace BudgetManager.Shared.Factory
{
    public class DataOutputFactory
    {
        private readonly string _originFilePath;

        public DataOutputFactory(string originFilePath)
        {
            _originFilePath = originFilePath;
        }

        public IDataOutput GetDataOutput(OutputType type)
        {
            switch (type)
            {
                case OutputType.Excel:
                    return new ExcelFileCreator(_originFilePath);
                case OutputType.CSV:
                default:
                    return new CsvFileCreator(_originFilePath);


            }
        }
    }
}
