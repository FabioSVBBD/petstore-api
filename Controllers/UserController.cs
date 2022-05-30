using Microsoft.AspNetCore.Mvc;
using pet_store.Context;

namespace pet_store.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	static UserContext users = UserContext.instance();
	
	public UserController()
	{}

	[HttpGet]
	public List<User> getAll(String? firstName, String? lastName)
	{
		if (firstName == null || lastName == null) {
			return users.filterUsers();	
		}

		return users.filterUsers(firstName, lastName);
	}

	[HttpGet("{id}/login")]
	public String login(String id) 
	{
		if (!users.UserExists(id)) 
			return "User doesn't exist";

		if (users.isLoggedIn(id))
			return users.getUser(id).FirstName + " is logged in already";

		if (!users.Login(id)) 
			return "An error occurred when attempting to login";

		return users.getUser(id).FirstName + " logged in";
	}

	[HttpGet("{id}/logout")]
	public String logout(String id)
	{
		if (!users.UserExists(id))
			return "User doesn't exist";

		if (!users.isLoggedIn(id))
			return users.getUser(id).FirstName + " is logged out already";

		if (!users.Logout(id))
			return "An error occurred when attempting to login";

		return users.getUser(id).FirstName + " logged out";
	}

	[HttpPost]
	public String createUser([FromBody] User user)
	{
		if (!user.isValid())
			return "There is something wrong with your request body";

		String id = users.AddUser(user);

		if (id == "") 
			return "Something went wrong when trying to add " + user.FirstName;

		return id;
	}

	[HttpPatch("{id}")]
	public String updateUser(String id, [FromBody] User user)
	{
		if (!users.UserExists(id)) {
			return "User with id " + id + " does not exist.";
		}

		if (!users.isLoggedIn(id)) {
			return "You do not have the required privileges to perform this action. Try logging in first if this is your account";
		}

		User entry = users.getUser(id);

		if (user.FirstName != null)
		{
			entry.FirstName = user.FirstName;
		}

		if (user.LastName != null)
		{
			entry.LastName = user.LastName;
		}

		return entry.FirstName + " updated.";
	}

	[HttpDelete("{id}")]
	public String deleteUser(String id)
	{
		if (!users.UserExists(id)) {
			return "User with id " + id + " does not exist.";
		}

		if (!users.isLoggedIn(id)) {
			return "You do not have the required privileges to perform this action. Try logging in first if this is your account";
		}

		users.removeUser(id);

		return "User with id " + id + " deleted successfully.";
	}
}