namespace pet_store.Entities;

public class Pet
{
	public String? Name { get; set; }
	public String? Species { get; set; }

	public String? Status { get; set; }

	public Pet(String name, String species, String status)
	{
		Name = name;
		Species = species;
		Status = status;
	}

	public bool isValid() {
		return Name != null && Species != null && Status != null;
	}
}