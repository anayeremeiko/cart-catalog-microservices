using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
	public interface IItemService
	{
		Task<Item> GetItemAsync(int itemId);

		Task<IEnumerable<Item>> ListItemsAsync();

		Task<Item> AddItemAsync(Item item);

		Task<Item> UpdateItemAsync(Item item);

		Task DeleteItemAsync(Item item);
	}
}
