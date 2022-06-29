using eShopServices.Services.Cart.Cart.API.Models;
using eShopServices.Services.Cart.Cart.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cart.WebAPI.Controllers
{
	[ApiController]
	[Route("api/carts")]
	[ApiVersion("1.0")]
	public class CartController : ControllerBase
	{
		private readonly ICartService cartService;

		public CartController(ICartService cartService)
		{
			this.cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
		}

		[HttpGet("{cartId}")]
		public eShopServices.Services.Cart.Cart.API.Models.Cart GetCartInfo(string cartId)
		{
			var items = cartService.GetAllCartItems(cartId).ToList();
			return new eShopServices.Services.Cart.Cart.API.Models.Cart(cartId) { Items = items };
		}

		[HttpPost("{cartId}/items")]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public IActionResult AddItemToCart(string cartId, CartItem newItem)
		{
			var cart = cartService.AddItemToCart(cartId, newItem);
			if (cart != null)
			{
				return StatusCode(200);
			} else
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("{cartId}/items/{itemId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public IActionResult DeleteItemFromCart(string cartId, int itemId)
		{
			eShopServices.Services.Cart.Cart.API.Models.Cart cart;
			try
			{
				cart = cartService.RemoveItemFromCart(cartId, itemId);
			} catch (InvalidOperationException exception)
			{
				return StatusCode(400, exception.Message);
			}

			if (cart != null)
			{
				return StatusCode(200);
			} else
			{
				return StatusCode(500);
			}
		}

		[HttpGet("{cartId}")]
		[ApiVersion("2.0")]
		public List<CartItem> GetCartInfoV2(string cartId)
		{
			return cartService.GetAllCartItems(cartId).ToList();
		}
	}
}