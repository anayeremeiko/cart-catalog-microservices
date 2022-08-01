using eShopServices.Services.Cart.Cart.API.Models;
using eShopServices.Services.Cart.Cart.API.Services.Interfaces;
using MassTransit;
using Shared.Models;

namespace Cart.WebAPI.Consumers
{
	public class ItemChangedConsumer : IConsumer<CatalogItem>
	{
		private readonly ICartService cartService;

		public ItemChangedConsumer(ICartService cartService)
		{
			this.cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
		}

		public Task Consume(ConsumeContext<CatalogItem> context)
		{
			CatalogItem data = context.Message;
			CartItem item = new CartItem
			{
				Id = data.Id,
				Name = data.Name,
				ImageAlt = data.ImageAlt,
				ImageUrl = data.ImageUrl,
				Price = data.Price,
				Quantity = 1
			};

			this.cartService.UpdateItemInCarts(item);

			return Task.CompletedTask;
		}
	}
}
