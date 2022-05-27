namespace pet_store.Context;

public class UserContext
{
	private static Dictionary<String, User> Users = new();
	public UserContext()
	{
		Users.Add("abc", new User("Fabio", "Sousa"));
		Users.Add("xyz", new User("Diana", "Prince"));
	}

	public String AddUser(User user)
	{
		if (Users.ContainsValue(user))
			return "";

		String uuid;
		do
		{
			uuid = Guid.NewGuid().ToString();
		} while(Users.ContainsKey(uuid));

		Users.Add(Guid.NewGuid().ToString(), user);
		return uuid;
	}

	public bool removeUser(String id)
	{
		if (!Users.ContainsKey(id)) return true;

		return Users.Remove(id);
	}

	public User getUser(String id) {
		if (UserExists(id)) return Users[id];

		return null;
	}

	public bool UserExists(String id)
	{
		return Users.ContainsKey(id);
	}

	public bool UserExists(User user)
	{
		return Users.ContainsValue(user);
	}

	public bool isLoggedIn(String id)
	{
		if (!UserExists(id)) return false;

		return Users[id].isLoggedIn();
	}

	public bool Login(String id)
	{
		if (!UserExists(id)) return false;

		Users[id].login();
		return true;
	}

	public bool Logout(String id)
	{
		if (!UserExists(id)) return false;

		Users[id].logout();
		return true;
	}
}