using BudgetManager.Shared.DataImports;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController(ILogger<ImportController> logger,
        PkoBpTransactionsImport bankTransactionsImport,
        CategoriesImport categoriesImport): ControllerBase
    {
        [HttpPost("transactions")]
        public async Task<IActionResult> ImportTransactionsAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("categories")]
        public async Task<IActionResult> ImportCategoriesAsync([FromBody]Dictionary<string, List<string>> dataToImport, 
            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Importing categories data.");
                await categoriesImport.ImportDataAsync(dataToImport, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while importing categories data.");
                throw;
            }
        }
    }
}