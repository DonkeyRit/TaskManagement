using Core.Model;

namespace DepartmentEmployee.Context
{
	public class CustomContext
	{
		public User CurrentUser { get; set; }
		private static CustomContext _instance;

		public static CustomContext GetInstance()
		{
			return _instance ?? (_instance = new CustomContext());
		}

		private CustomContext() { }

	}
}
