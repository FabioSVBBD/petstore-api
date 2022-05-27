using Microsoft.AspNetCore.Mvc;
using pet_store.Context;

namespace pet_store.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	static UserContext users = new();
	public UserController()
	{
		Console.WriteLine(users.ToString());
	}

	[HttpPost("{id}/login")]
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

	[HttpPost("{id}/logout")]
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

	[HttpPost("new")]
	public String createUser([FromBody] User user)
	{
		if (!user.isValid())
			return "There is something wrong with your request body";

		String id = users.AddUser(user);

		if (id == "") 
			return "Something went wrong when trying to add " + user.FirstName;

		return id;
	}
}