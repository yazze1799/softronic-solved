using Api.Domain;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
        {
            var items = await _itemsService.GetItems();

            return Ok(items);            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Post([FromForm]string[] items)
        {
            var allItems = await _itemsService.GetItems();
            var selectedItems = allItems.Where(i => items.Contains(i.Id));

            return Ok(selectedItems);
        }
    }
}