namespace pet_store.Entities;

public class Store
{
	public Dictionary<String, Pet> Pets { get; set; } = new();
	public Dictionary<String, List<String>> Transactions { get; set; } = new();

	public Store()
	{
		Pets.Add("123", new Pet("Boots", "Cat", "Single"));
		Pets.Add("987", new Pet("Shaggy", "Dog", "Married"));
		Pets.Add("456", new Pet("Thor", "Dog", "Tired"));

		Transactions.Add("abc", new List<String>() { "123", "456" });
	}
}