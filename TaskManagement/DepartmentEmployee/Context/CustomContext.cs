using Core.Model;

namespace DepartmentEmployee.Context
{
	public class CustomContext
	{
		public User CurrentUser { get; set; }
		public static CustomContext Instance;

		public static CustomContext GetInstance()
		{
			return Instance ?? (Instance = new CustomContext());
		}

		private CustomContext() { }

	}
}
