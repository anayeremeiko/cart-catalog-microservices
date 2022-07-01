using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
	public interface IItemService
	{
		Task<Item> GetItemAsync(int itemId);

		Task<IEnumerable<Item>> ListItemsAsync();

		Task<IEnumerable<Item>> ListItemsAsync(int pageSize, int pageNumber);

		Task<IEnumerable<Item>> ListItemsAsync(int categoryIdToFilterBy, int pageSize, int pageNumber);

		Task<Item> AddItemAsync(Item item);

		Task<Item> UpdateItemAsync(Item item);

		Task DeleteItemAsync(Item item);

		Task<int> CountAsync();
	}
}
