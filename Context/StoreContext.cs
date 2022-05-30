namespace pet_store.Context;

public class StoreContext
{
	private static StoreContext? context;

	public static StoreContext instance()
	{
		if (context == null)
			context = new StoreContext();

		return context;
	}

	private StoreContext() {}
}