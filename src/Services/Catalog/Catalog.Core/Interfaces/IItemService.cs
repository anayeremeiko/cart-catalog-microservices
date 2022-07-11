using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
	public interface IItemService
	{
		Task<Item> GetItemAsync(int itemId);

		Task<IEnumerable<Item>> ListItemsAsync();

		Task<IEnumerable<Item>> ListItemsAsync(int pageSize, int pageNumber);

		Task<IEnumerable<Item>> ListItemsAsync(int categoryIdToFilterBy, int pageSize, int pageNumber);

		Task<Item> AddItemAsync(int id, string name, string description, string imageUrl, int categoryId, decimal price, int amount);

		Task<Item> UpdateItemAsync(int id, string name, string description, string imageUrl, int categoryId, decimal price, int amount);

		Task DeleteItemAsync(Item item);

		Task<int> CountAsync();
	}
}
