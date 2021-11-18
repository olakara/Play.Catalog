using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Application.Items.Commands.CreateItem;
using Play.Catalog.Application.Items.Commands.DeleteItem;
using Play.Catalog.Application.Items.Commands.UpdateItem;
using Play.Catalog.Application.Items.Queries.Dtos;
using Play.Catalog.Application.Items.Queries.GetItem;
using Play.Catalog.Application.Items.Queries.GetItems;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ApiControllerBase
    {
        
        [HttpGet]
        public async Task<IList<ItemDto>>Get()
        {
            return await Mediator.Send(new GetItemsQuery());
        }

        
        [HttpGet("{id}")]
        public async Task<ItemDto> GetById(Guid id)
        {
            return await Mediator.Send(new GetItemByIdQuery { Id = id });
        }

        
         [HttpPost]
        public async Task<ActionResult<Guid>> Post(CreateItemCommand command)
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateItemCommand command)
        {           
            if (command == null)
            {
                return NotFound();
            }

            await Mediator.Send(command);          
            
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            await Mediator.Send(new DeleteItemCommand { Id = id });
            return NoContent();
        }
        
    }
    
}