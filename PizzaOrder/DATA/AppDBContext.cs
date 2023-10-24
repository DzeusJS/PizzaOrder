using Microsoft.EntityFrameworkCore;

namespace PizzaOrder.DATA
{
	internal sealed class AppDBContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<Pizza> Pizza { get; set; }
		public DbSet<Topping> Topping { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.HasMany(e => e.Pizzas)
				.WithMany(e => e.Orders)
				.UsingEntity<Pizza_Order>();
			modelBuilder.Entity<Pizza>()
				.HasMany(e => e.Orders)
				.WithMany(e => e.Pizzas)
				.UsingEntity<Pizza_Order>();

			modelBuilder.Entity<Pizza>()
				.HasMany(e => e.Toppings)
				.WithMany(e => e.Pizzas)
				.UsingEntity<Pizza_Topping>();
			modelBuilder.Entity<Topping>()
				.HasMany(e => e.Pizzas)
				.WithMany(e => e.Toppings)
				.UsingEntity<Pizza_Topping>();

			Pizza_Topping[] picosToppingai = new Pizza_Topping[9]
			{
				new Pizza_Topping {PizzaId = 1, ToppingId = 1},
				new Pizza_Topping { PizzaId = 1, ToppingId = 2 },
				new Pizza_Topping { PizzaId = 1, ToppingId = 3 },
				new Pizza_Topping {PizzaId = 1, ToppingId = 4},
				new Pizza_Topping { PizzaId = 2, ToppingId = 3 },
				new Pizza_Topping { PizzaId = 2, ToppingId = 4 },
				new Pizza_Topping {PizzaId = 3, ToppingId = 3},
				new Pizza_Topping { PizzaId = 3, ToppingId = 4 },
				new Pizza_Topping { PizzaId = 3, ToppingId = 5 },

			};
			modelBuilder.Entity<Pizza_Topping>().HasData(picosToppingai);

			Pizza_Order[] picosOrderiai = new Pizza_Order[6]
			{
				new Pizza_Order {PizzaId = 1, OrderId = 1},
				new Pizza_Order { PizzaId = 3, OrderId = 1 },
				new Pizza_Order { PizzaId = 2, OrderId = 2 },
				new Pizza_Order {PizzaId = 3, OrderId = 2 },
				new Pizza_Order { PizzaId = 1, OrderId = 3 },
				new Pizza_Order { PizzaId = 2, OrderId = 3 },

			};
			modelBuilder.Entity<Pizza_Order>().HasData(picosOrderiai);




			Topping[] toppingsToSeed = new Topping[5]
			{
				new Topping { ToppingId = 1, ToppingName = "Mushrooms", ToppingPrice = 1 },
				new Topping { ToppingId = 2, ToppingName = "Peppers", ToppingPrice = 1 },
				new Topping { ToppingId = 3, ToppingName = "Cucumbers", ToppingPrice = 1 },
				new Topping { ToppingId = 4, ToppingName = "Olives", ToppingPrice = 1 },
				new Topping { ToppingId = 5, ToppingName = "Onions", ToppingPrice = 1 },
			};
			modelBuilder.Entity<Topping>().HasData(toppingsToSeed);

			Pizza[] pizzasToSeed = new Pizza[3]
			{
				new Pizza { PizzaId = 1, Size = "Small", TotalPizzaCost = 8},
				new Pizza { PizzaId = 2, Size = "Medium", TotalPizzaCost = 10 },
				new Pizza { PizzaId = 3, Size = "Large", TotalPizzaCost = 12 }
			};

			modelBuilder.Entity<Pizza>().HasData(pizzasToSeed);

			Order[] ordersToSeed = new Order[3];
			//seedina orderius
			for (int i = 0; i < 3; i++)
			{
				//priskiria prie orderio piza pagal ju ids
				var selectedOrderPizzas = picosOrderiai.Where(po => po.OrderId == i + 1)
										  .Select(po => po.PizzaId)
										  .ToList();
				//var selectedPizzaTopping = picosToppingai.Where(pt => pt.PizzaId == )
				double totalOrderCost = 0;

				//isskiria po viena pica is visu picu susietu su orderiu
				foreach (var pizzaId in selectedOrderPizzas)
				{

					var selectedPizza = pizzasToSeed.Single(pizza => pizza.PizzaId == pizzaId);
					// Add the pizza cost and topping cost to the total order cost
					totalOrderCost += selectedPizza.TotalPizzaCost;


				}
				ordersToSeed[i] = new Order
				{
					OrderId = i + 1,
					TotalOrderCost = totalOrderCost
				};
			}
			modelBuilder.Entity<Order>().HasData(ordersToSeed);
		}
	}
}
