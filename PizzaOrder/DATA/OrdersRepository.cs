using Microsoft.EntityFrameworkCore;

namespace PizzaOrder.DATA
{
	internal static class OrdersRepository
	{
		internal async static Task<List<Order>> GetOrdersAsync()
		{
			using (var db = new AppDBContext())
			{
				return await db.Orders.ToListAsync();
			}
		}
		internal async static Task<List<Pizza>> GetPizzasAsync()
		{
			using (var db = new AppDBContext())
			{
				return await db.Pizza.ToListAsync();
			}
		}
		internal async static Task<List<Topping>> GetToppingsAsync()
		{
			using (var db = new AppDBContext())
			{
				return await db.Topping.ToListAsync();
			}
		}

		internal async static Task<Order> GetOrderByIdAsync(int orderId)
		{
			using (var db = new AppDBContext())
			{
				return await db.Orders
					.FirstOrDefaultAsync(order => order.OrderId == orderId);
			}
		}

		internal async static Task<bool> CreateOrderAsync(Order orderToCreate)
		{
			using (var db = new AppDBContext())
			{
				try
				{
					await db.Orders.AddAsync(orderToCreate);

					return await db.SaveChangesAsync() >= 1;
				}
				catch (Exception e)
				{
					return false;
				}
			}
		}

		internal async static Task<bool> UpdateOrderAsync(Order orderToUpdate)
		{
			using (var db = new AppDBContext())
			{
				try
				{
					db.Orders.Update(orderToUpdate);

					return await db.SaveChangesAsync() >= 1;
				}
				catch (Exception e)
				{
					return false;
				}
			}
		}

		internal async static Task<bool> DeleteOrderAsync(int orderId)
		{
			using (var db = new AppDBContext())
			{
				try
				{
					Order orderToDelete = await GetOrderByIdAsync(orderId);

					db.Remove(orderToDelete);

					return await db.SaveChangesAsync() >= 1;
				}
				catch (Exception e)
				{
					return false;
				}
			}
		}
	}
}
