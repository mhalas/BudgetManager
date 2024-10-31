using AutoMapper;
using BudgetManager.Api.Models.Controllers;
using BudgetManager.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(
        ILogger<CategoryController> logger,
        IMapper mapper,
        BudgetManagerContext context)
        : CrudBaseController<Category, DataAccess.Models.Category>(mapper, context)
    {
        [HttpPost]
        public new async Task<IActionResult> Create([FromBody]Category category, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Creating account to database.");
                return await base.Create(category, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
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

        [HttpGet("{id}")]
        public new async Task<IActionResult> Receive([FromQuery]int id, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Receiving account from database.");
                return await base.Receive(id, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPost("{id}")]
        public new async Task<IActionResult> Update([FromQuery]int id, [FromBody]Category category, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Updating account.");
                return await base.Update(id, category, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public new async Task<IActionResult> Delete([FromQuery]int id, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Updating account.");
                return await base.Delete(id, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}