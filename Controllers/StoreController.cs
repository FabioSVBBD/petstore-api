using Microsoft.AspNetCore.Mvc;
using pet_store.Context;
using pet_store.Entities;

namespace pet_store.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController : ControllerBase
{
	static Store store = Store.instance();
	static UserContext users = UserContext.instance();

	public StoreController()
	{}

	[HttpGet("pets")]
	public Dictionary<String, Pet> pets()
	{
		return store.Pets;
	}

	[HttpGet("pets/{id}")]
	public Pet getPet(String id)
	{
		if (!store.Pets.ContainsKey(id))
			return null;

		return store.Pets[id];
	}

	[HttpPost("pets")]
	public String addPet([FromBody] Pet pet)
	{
		String uuid;
		do {
			uuid = Guid.NewGuid().ToString();
		} while(store.Pets.ContainsKey(uuid));

		store.Pets.Add(uuid, pet);

		return pet.Name + " the " + pet.Species + " Added successfully.";
	}

	[HttpGet("transactions")]
	public Dictionary<String, List<Pet>> transactions()
	{
		Dictionary<String, List<Pet>> map = new();

		foreach (String userId in store.Transactions.Keys)
		{
			List<Pet> pets = store.Transactions[userId].Select(petId => store.Pets[petId]).ToList();
			map.Add(users.getUser(userId).ToString(), pets);
		}

		return map;
	}
}