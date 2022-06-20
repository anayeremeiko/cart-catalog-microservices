using eShopServices.Services.Cart.Cart.API.Models;

namespace Cart.UnitTests.Models
{
	[TestClass]
	public class CartItemTests
	{
		[TestMethod]
		public void AddQuantity_PassedPositiveNumber_IncreasesQuantity()
		{
			CartItem item = new CartItem()
			{
				Id = 1,
				Name = "testItem",
				Price = 11,
				Quantity = 4
			};

			item.AddQuantity(5);

			Assert.AreEqual(9, item.Quantity);
		}

		[TestMethod]
		public void AddQuantity_PassedNegativeNumber_QuantityDoesNotChange()
		{
			CartItem item = new CartItem()
			{
				Id = 1,
				Name = "testItem",
				Price = 11,
				Quantity = 4
			};

			item.AddQuantity(-2);

			Assert.AreEqual(4, item.Quantity);
		}

		[TestMethod]
		public void RemoveQuantity_PassedPositiveNumber_DecreasesQuantity()
		{
			CartItem item = new CartItem()
			{
				Id = 1,
				Name = "testItem",
				Price = 11,
				Quantity = 4
			};

			item.RemoveQuantity(1);

			Assert.AreEqual(3, item.Quantity);
		}

		[TestMethod]
		public void RemoveQuantity_PassedPositiveNumberGreaterThanCurrentQuantity_QuantitySetToZero()
		{
			CartItem item = new CartItem()
			{
				Id = 1,
				Name = "testItem",
				Price = 11,
				Quantity = 4
			};

			item.RemoveQuantity(30);

			Assert.AreEqual(0, item.Quantity);
		}

		[TestMethod]
		public void RemoveQuantity_PassedNegativeNumber_QuantityDoesNotChange()
		{
			CartItem item = new CartItem()
			{
				Id = 1,
				Name = "testItem",
				Price = 11,
				Quantity = 4
			};

			item.RemoveQuantity(-2);

			Assert.AreEqual(4, item.Quantity);
		}
	}
}
