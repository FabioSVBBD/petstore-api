namespace pet_store.Entities;

public class User
{
	public String? FirstName { get; set; }
	public String? LastName { get; set; }

	private bool Loggedin { get; set; }

	public User(String firstName, String lastName)
	{
		FirstName = firstName;
		LastName = lastName;
		Loggedin = false;
	}

	public override string ToString()
	{
		return "[" + FirstName + " " + LastName + "]";
	}

	public bool isValid()
	{
		return true;
	}

	public bool isLoggedIn() {
		return Loggedin;
	}

	public void login() {
		Loggedin = true;
	}

	public void logout() {
		Loggedin = false;
	}
}