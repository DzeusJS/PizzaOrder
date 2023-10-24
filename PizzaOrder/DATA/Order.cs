using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.DATA
{

	//internal-only available in this project
	//sealed-cant be inherited
	//UZSAKYMAS- 
	// picos dydis - (small 8,medium 10,large 12) - pasirinkti viena
	// priedai - pievagrybiai, pipirai, agurkai, alyvuoges, svogunui - multiple choice 1
	//reikia apskaiciuoti uzsakymo kaina
	//jeigu pasirenka daugiau uz 3 topingu 10% akcija orderiui
	//order-pizza-topping relations
	//order-pizza many many orderis gali tureti daug picu, ta pati pica gali tureti daug uzsakymu
	//pizza-topping many many pica gali tureti daug toppingu, tas pats topingas gali tureti daug picu

	internal sealed class Order
	{
		[Key]
		public int OrderId { get; set; }

		[Required]
		public double TotalOrderCost { get; set; }

		[Required]
		public List<Pizza> Pizzas { get; } = new();

		[Required]
		public List<Pizza_Order> Pizzas_Orders { get; } = new();

	}
	internal sealed class Pizza
	{
		[Key]
		public int PizzaId { get; set; }

		[Required]
		public string Size { get; set; } = string.Empty;

		[Required]
		public double TotalPizzaCost { get; set; }

		[Required]
		public List<Order> Orders { get; } = new();

		[Required]
		public List<Topping> Toppings { get; } = new();

		[Required]
		public List<Pizza_Order> Pizzas_Orders { get; } = new();

		[Required]
		public List<Pizza_Topping> Pizzas_Toppings { get; } = new();


	}
	internal sealed class Topping
	{
		[Key]
		public int ToppingId { get; set; }

		[Required]
		public string ToppingName { get; set; } = string.Empty;

		[Required]
		public double ToppingPrice { get; set; }

		[Required]
		public List<Pizza> Pizzas { get; } = new();

		[Required]
		public List<Pizza_Topping> Pizzas_Toppings { get; } = new();

	}

	internal sealed class Pizza_Order
	{
		public int PizzaId { get; set; }
		public int OrderId { get; set; }
	}
	internal sealed class Pizza_Topping
	{
		public int PizzaId { get; set; }
		public int ToppingId { get; set; }
	}




}
