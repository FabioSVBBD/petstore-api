namespace pet_store.Entities;

public class Store
{
	public Dictionary<String, Pet> Pets { get; set; } = new();
	public Dictionary<String, List<String>> Transactions { get; set; } = new();
	public Dictionary<String, Order> Orders { get; set; } = new();

	private static Store? store;

	public static Store instance()
	{
		if (store == null)
			store = new();

		return store;
	}

	private Store()
	{
		Pets.Add("123", new Pet("Boots", "Cat", "Single"));
		Pets.Add("987", new Pet("Shaggy", "Dog", "Married"));
		Pets.Add("456", new Pet("Thor", "Dog", "Tired"));

		Transactions.Add("abc", new List<String>() { "123", "456" });

		Orders.Add("order-1", new Order("abc", new List<string>() { "123" }));
		Orders.Add("order-2", new Order("xyz", new List<string>() { "987", "456" }));
	}
}