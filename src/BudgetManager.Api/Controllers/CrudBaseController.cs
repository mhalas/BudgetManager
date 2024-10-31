using AutoMapper;
using BudgetManager.DataAccess;
using BudgetManager.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    public class CrudBaseController<TModel, TDatabaseModel>(IMapper mapper, 
        BudgetManagerContext context)
        : ControllerBase
        where TDatabaseModel : class, IIdEntity, new()
    {
        public async Task<IActionResult> Create(TModel model, CancellationToken cancellationToken)
        {
            var id = await context.AddAsync(mapper.Map<TDatabaseModel>(model), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            
            return StatusCode(201, id);
        }
        
        public async Task<IActionResult> ReceiveAll(CancellationToken cancellationToken)
        {
            var result = await context.FindAsync<TDatabaseModel>(cancellationToken)
                .ConfigureAwait(false);
            
            return Ok(mapper.Map<IEnumerable<TModel>>(result));
        }

        public async Task<IActionResult> Receive(int id, CancellationToken cancellationToken)
        {
            var result = await context.FindAsync<TDatabaseModel>(new TDatabaseModel { Id = id }, cancellationToken);
            
            return Ok(mapper.Map<TModel>(result));
        }
        
        public async Task<IActionResult> Update(int id, TModel model, CancellationToken cancellationToken)
        {
            var updatingModel = mapper.Map<TDatabaseModel>(model);
            updatingModel.Id = id;
            
            context.Update(updatingModel);
            await context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }
        
        public async Task<IActionResult> Delete([FromQuery]int id, CancellationToken cancellationToken)
        {
            context.Remove(id);
            await context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }
    }
}