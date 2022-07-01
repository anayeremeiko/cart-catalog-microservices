using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Queries;
using Catalog.Core.Validators;
using FluentValidation;
using MediatR;

namespace Catalog.Core.Services
{
	public class ItemService : IItemService
	{
		private readonly IMediator mediator;

		public ItemService(IMediator mediator)
		{
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		/// <summary>
		/// Adds item.
		/// </summary>
		/// <param name="item">New item.</param>
		/// <returns>Added item.</returns>
		public async Task<Item> AddItemAsync(Item item)
		{
			var validator = new ItemValidator();
			await validator.ValidateAndThrowAsync(item);

			CreateItemCommand request = new CreateItemCommand()
			{
				NewItem = item
			};
			var addedItem = await mediator.Send(request);

			return addedItem;
		}

		public async Task<int> CountAsync()
		{
			CountItemsQuery request = new CountItemsQuery();

			var count = await mediator.Send(request);

			return count;
		}

		/// <summary>
		/// Deletes the item.
		/// </summary>
		/// <param name="item">Item to delete.</param>
		public async Task DeleteItemAsync(Item item)
		{
			DeleteItemCommand request = new DeleteItemCommand()
			{
				Item = item
			};

			await mediator.Send(request);
		}

		/// <summary>
		/// Gets item by item identifier.
		/// </summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>Item with the provided identifier.</returns>
		public async Task<Item> GetItemAsync(int itemId)
		{
			GetItemQuery request = new GetItemQuery()
			{
				Id = itemId
			};
			var item = await mediator.Send(request);

			return item;
		}

		/// <summary>
		/// Lists items.
		/// </summary>
		/// <returns>All items.</returns>
		public async Task<IEnumerable<Item>> ListItemsAsync()
		{
			GetItemsQuery request = new GetItemsQuery();
			var items = await mediator.Send(request);

			return items;
		}

		/// <summary>
		/// Paginated items.
		/// </summary>
		/// <returns>Paginated items.</returns>
		public async Task<IEnumerable<Item>> ListItemsAsync(int pageSize, int pageNumber)
		{
			GetItemsQuery request = new GetItemsQuery
			{
				PageSize = pageSize,
				PageNumber = pageNumber
			};
			var items = await mediator.Send(request);

			return items;
		}

		public async Task<IEnumerable<Item>> ListItemsAsync(int categoryIdToFilterBy, int pageSize, int pageNumber)
		{
			GetItemsQuery request = new GetItemsQuery
			{
				PageSize = pageSize,
				PageNumber = pageNumber,
				CategoryIdToFilterBy = categoryIdToFilterBy,
			};
			var items = await mediator.Send(request);

			return items;
		}

		/// <summary>
		/// Updates item.
		/// </summary>
		/// <param name="item">Updated item.</param>
		/// <returns>Updated item.</returns>
		public async Task<Item> UpdateItemAsync(Item item)
		{
			var validator = new ItemValidator();
			await validator.ValidateAndThrowAsync(item);

			UpdateItemCommand request = new UpdateItemCommand()
			{
				UpdatedItem = item
			};
			var updatedItem = await mediator.Send(request);

			return updatedItem;
		}
	}
}
