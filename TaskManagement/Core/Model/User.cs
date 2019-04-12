using Core.Model.Enums;

namespace Core.Model
{
	public class User
	{
		public string Username { get; }
		public string Password { get; }
		public Role Role { get; }

		public User(string username, string password, Role role)
		{
			Username = username;
			Password = password;
			Role = role;
		}
	}
}
