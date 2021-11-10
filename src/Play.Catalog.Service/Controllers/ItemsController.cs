using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        public static readonly List<ItemDto> items = new() 
        {
            new ItemDto(Guid.NewGuid(),"Item 1", "Description update!!", 5,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Item 2", "Description 2", 7,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Item 3", "Description 3", 10,DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {            
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
           var item = new ItemDto(Guid.NewGuid(),createItemDto.Name,createItemDto.Description,createItemDto.Price,DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var item = items.Find(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = new ItemDto(item.Id,updateItemDto.Name,updateItemDto.Description,updateItemDto.Price,item.CreatedDate);
            items.Remove(item);
            items.Add(updatedItem);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = items.Find(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            items.Remove(item);
            return NoContent();
        }
    }
    
}