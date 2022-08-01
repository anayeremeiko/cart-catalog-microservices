using Catalog.API.Filters;
using Catalog.API.Helpers;
using Catalog.API.Models;
using Catalog.API.Services.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Catalog.API.Controllers
{
	[Route("api/items")]
	[ApiController]
	public class ItemsController : ControllerBase
	{
		private readonly IItemService itemsService;
		private readonly IUriService uriService;
		private readonly IPublishEndpoint publishEndpoint;

		public ItemsController(IItemService service, IUriService uriService, IPublishEndpoint publishEndpoint)
		{
			this.itemsService = service;
			this.uriService = uriService;
			this.publishEndpoint = publishEndpoint;
		}

		/// <summary>
		/// Get a paginated list of items. Can be filtered by category identifier.
		/// </summary>
		/// <param name="paginationFilter">Parameters for pagination: page size,
		/// page number. By default page size is 10, page number is 1.</param>
		/// <param name="categoryId">Category identifier to filter items by.</param>
		/// <returns>A paginated list of items.</returns>
		[HttpGet()]
		public async Task<PagedResponse<List<Item>>> GetItems([FromQuery] PaginationFilter paginationFilter, [FromQuery] int? categoryId = null)
		{
			var validFilter = new PaginationFilter(paginationFilter.PageNumber.Value, paginationFilter.PageSize.Value);
			var items = categoryId.HasValue
				? await itemsService.ListItemsAsync(categoryId.Value, validFilter.PageSize.Value, validFilter.PageNumber.Value) 
				: await itemsService.ListItemsAsync(validFilter.PageSize.Value, validFilter.PageNumber.Value);
			var totalRecords = await itemsService.CountAsync();
			var pagedReponse = PaginationHelper.CreatePagedReponse<Item>(items.ToList(), validFilter, totalRecords, uriService, Request.Path.Value);

			return pagedReponse;
		}

		/// <summary>
		/// Add new item
		/// </summary>
		/// <param name="id">New item identifier</param>
		/// <param name="item">New item parameters</param>
		/// <returns>Created item</returns>
		[HttpPost("{id}")]
		public async Task<IActionResult> AddItem(int id, UpdatedItem item)
		{
			var addedItem = await itemsService.AddItemAsync(id, item.Name, item.Description, item.ImageUrl, item.CategoryId, item.Price, item.Amount);

			return Created($"api/items/{addedItem.Id}", new Models.Response<Item>
			{
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

		/// <summary>
		/// Update existing item
		/// </summary>
		/// <param name="id">Existing item identifier</param>
		/// <param name="item">Updated item parameters</param>
		/// <returns>Updated item</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(int id, UpdatedItem item)
		{
			var updatedItem = await itemsService.UpdateItemAsync(id, item.Name, item.Description, item.ImageUrl, item.CategoryId, item.Price, item.Amount);

			await publishEndpoint.Publish<CatalogItem>(new CatalogItem
			{
				Id = id,
				Name = item.Name,
				ImageAlt = item.Description,
				ImageUrl = item.ImageUrl,
				Price = item.Price
			});

			return Ok(new Models.Response<Item>
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

		/// <summary>
		/// Delete existing item
		/// </summary>
		/// <param name="id">Item identifier</param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteItem(int id)
		{
			var item = await itemsService.GetItemAsync(id);
			await itemsService.DeleteItemAsync(item);

			return NoContent();
		}
	}
}
