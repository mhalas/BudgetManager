using BudgetManager.Shared.BankAnalyzer;
using BudgetManager.Shared.Configuration;
using BudgetManager.Shared.Enum;

namespace BudgetManager.Shared.Factory
{
    public class BankFactory(ConfigurationDto configuration)
    {
        public IBankAnalyzer GetBankAnalyzer(BankType type)
        {
            switch (type)
            {
                case BankType.PkoBP:
                default:
                    return new PkoBpDataAnalyzer(configuration);
            }

        }
    }
}
