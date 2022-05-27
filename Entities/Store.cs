using System.Collections.Generic;

namespace pet_store;

public class Store
{
	public List<Pet> Pets { get; set; }

	Store(List<Pet> pets)
	{
		Pets = pets;
	}
}