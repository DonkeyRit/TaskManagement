using Core.Model.Enums;

namespace Core.Model
{
	public class User
	{
		public string Username { get; }
		public string PasswordHash { get; }
		public Role Role { get; }

		public User(string username, string passwordHash, Role role)
		{
			Username = username;
			PasswordHash = passwordHash;
			Role = role;
		}
	}
}
