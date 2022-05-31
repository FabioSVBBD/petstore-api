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
	{ }

	[HttpGet("pets")]
	public Dictionary<String, Pet> pets(String? status)
	{
		if (status == null || status == "")
		{
			return store.Pets;
		}

		return store.Pets
			.Where(item => item.Value.Status.StartsWith(status, true, null))
			.ToDictionary(i => i.Key, i => i.Value);
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
		do
		{
			uuid = Guid.NewGuid().ToString();
		} while (store.Pets.ContainsKey(uuid));

		store.Pets.Add(uuid, pet);

		return pet.Name + " the " + pet.Species + " Added successfully.";
	}

	[HttpPatch("pets/{id}")]
	public String patchPet(String id, [FromBody] Pet pet)
	{
		if (!store.Pets.ContainsKey(id)) {
			return "There is no pet with id " + id + ".";
		}

		Pet entry = store.Pets[id];

		if (pet.Name != null) {
			entry.Name = pet.Name;
		}

		if (pet.Species != null) {
			entry.Species = pet.Species;
		}

		if (pet.Status != null) {
			entry.Status = pet.Status;
		}

		return entry.Name + " updated.";
	}

	[HttpPut("pets/{id}")]
	public String putPet(String id, [FromBody] Pet pet) {
		if (!store.Pets.ContainsKey(id)) {
			return "There is no pet with id " + id + ".";
		}

		if (!pet.isValid()) {
			return "You are missing information in your request body.";
		}

		store.Pets[id] = pet;

		return store.Pets[id].Name + " updated successfully";
	}

	[HttpGet("pets/statuses")]
	public Dictionary<String, int> statusAggregate()
	{
		Dictionary<String, int> dict = new();

		foreach(String petId in store.Pets.Keys) {
			String? key = store.Pets[petId].Status;

			if (key == null) {
				continue;
			}

			int count;
			dict.TryGetValue(key, out count);
			dict[key] = ++count;
		}

		return dict;
	}
	
	[HttpGet("pets/order")]
	public Dictionary<String, Order> getOrders()
	{
		return store.Orders;
	}

	[HttpGet("pets/order/{id}")]
	public Order getOrder(String id)
	{
		if (!store.Orders.ContainsKey(id)) {
			return null;
		}

		return store.Orders[id];
	}

	[HttpPost("pets/order")]
	public String orderPet(String userId, [FromBody] List<String> petIds)
	{
		if (!users.UserExists(userId)) {
			return "User " + userId + " does not exist.";
		}

		if (!users.isLoggedIn(userId)) {
			return "User " + userId + " is not logged in.";
		}

		foreach (String id in petIds) {
			if (!store.Pets.ContainsKey(id)) {
				return "id " + id + " does not match a pet.";
			}

			foreach (Order order in store.Orders.Values) {
				if (order.PetIds.Contains(id)) {
					return "Pet " + id + " has already been ordered.";
				}
			}
		}

		String uuid;
		do {
			uuid = Guid.NewGuid().ToString();
		} while (store.Orders.ContainsKey(uuid));

		store.Orders.Add(uuid, new Order(userId, petIds));

		return "Order " + uuid + " placed successfully.";
	}

	[HttpDelete("pets/order/{id}")]
	public String deleteOrder(String id, String userId)
	{
		if (!users.UserExists(userId)) {
			return "User " + userId + " does not exist.";
		}

		if (!users.isLoggedIn(userId)) {
			return "User " + userId + " is not logged in.";
		}

		if (!store.Orders.ContainsKey(id)) {
			return "Order " + id + " does not exist.";
		}

		if (store.Orders[id].CustomerId != userId) {
			return "You are not authorized to access this Order.";
		}

		store.Orders.Remove(id);

		return "Order " + id + " removed successfully.";
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