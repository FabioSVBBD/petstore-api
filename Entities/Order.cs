namespace pet_store.Entities;

public class Order
{
	public DateTime OrderDate { get; }
	public DateTime DeliveryDate { get; }

	public String CustomerId { get; set; }

	public List<String> PetIds { get; set; }

	public String status { get; private set; }

	public Order(String customerId, List<String> petIds)
	{
		CustomerId = customerId;
		PetIds = petIds;

		OrderDate = DateTime.Now;
		DeliveryDate = DateTime.Now.AddDays(5);

		status = "Processing";
	}

	public void updateStatus()
	{
		if (DeliveryDate <= DateTime.Now) {
			status = "Delivered";
		}
	}

}