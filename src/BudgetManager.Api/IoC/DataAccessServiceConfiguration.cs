using BudgetManager.DataAccess;

namespace BudgetManager.Api.IoC;

internal static class DataAccessServiceConfiguration
{
    public static void RegisterDataAccess(IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("BudgetManagerConnectionString");
        
        services.AddSingleton(new BudgetManagerContext(connectionString));
    }
}