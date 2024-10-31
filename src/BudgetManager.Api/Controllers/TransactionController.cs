using AutoMapper;
using BudgetManager.Api.Models.Controllers;
using BudgetManager.DataAccess;
using BudgetManager.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(
        ILogger<CategoryController> logger,
        IMapper mapper,
        BudgetManagerContext context)
        : CrudBaseController<Transaction, BankTransaction>(mapper, context)
    {
        [HttpGet]
        public new async Task<IActionResult> ReceiveAll(CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Receiving all accounts from database.");
                return await base.ReceiveAll(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}