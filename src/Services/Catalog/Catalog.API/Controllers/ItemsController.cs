using Catalog.API.Filters;
using Catalog.API.Helpers;
using Catalog.API.Models;
using Catalog.API.Services.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/items")]
	[ApiController]
	public class ItemsController : ControllerBase
	{
		private readonly IItemService itemsService;
		private readonly IUriService uriService;

		public ItemsController(IItemService service, IUriService uriService)
		{
			this.itemsService = service;
			this.uriService = uriService;
		}

		[HttpGet()]
		public async Task<PagedResponse<List<Item>>> GetItems([FromQuery] PaginationFilter paginationFilter, [FromQuery] int? categoryId = null)
		{
			var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
			var items = await itemsService.ListItemsAsync(validFilter.PageSize, validFilter.PageNumber);
			var totalRecords = await itemsService.CountAsync();
			var pagedReponse = PaginationHelper.CreatePagedReponse<Item>(items.ToList(), validFilter, totalRecords, uriService, Request.Path.Value);

			return pagedReponse;
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> AddItem(int id, Item item)
		{
			if (item.Id != id)
			{
				return BadRequest();
			}

			try
			{
				var addedItem = await itemsService.AddItemAsync(item);

				return Created($"api/items/{addedItem.Id}", new Response<Item> {
					Data = addedItem,
					Links = new List<Link>
					{
						new Link
						{
							Action = "PUT",
							Href = $"api/items/{addedItem.Id}",
							Rel = "self"
						},
						new Link
						{
							Action = "DELETE",
							Href = $"api/items/{addedItem.Id}",
							Rel = "self"
						},
						new Link
						{
							Action = "GET",
							Href = $"api/categories/{addedItem.Category.Id}",
							Rel = "category"
						}
					}
				});
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(int id, Item item)
		{
			if (item.Id != id)
			{
				return BadRequest();
			}

			try
			{
				var updatedItem = await itemsService.UpdateItemAsync(item);

				return Ok(new Response<Item>
				{
					Data = updatedItem,
					Links = new List<Link>
					{
						new Link
						{
							Action = "DELETE",
							Href = $"api/items/{updatedItem.Id}",
							Rel = "self"
						},
						new Link
						{
							Action = "GET",
							Href = $"api/categories/{updatedItem.Category.Id}",
							Rel = "category"
						}
					}
				});
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteItem(int id)
		{
			var item = await itemsService.GetItemAsync(id);
			await itemsService.DeleteItemAsync(item);

			return NoContent();
		}
	}
}
