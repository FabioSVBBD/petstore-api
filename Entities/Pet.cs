namespace pet_store;

public class Pet
{
	public String ID { get; set; }
	public String Name { get; set; }
	public String Species { get; set; }

	public String Status { get; set; }

	Pet(String id, String name, String species, String status)
	{
		ID = id;
		Name = name;
		Species = species;
		Status = status;
	}
}