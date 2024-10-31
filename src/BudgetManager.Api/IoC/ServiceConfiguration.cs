using BudgetManager.Shared.DataImports;

namespace BudgetManager.Api.IoC;

internal static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        DataAccessServiceConfiguration.RegisterDataAccess(services, configuration);

        services.AddTransient<CategoriesImport>();
    }
}