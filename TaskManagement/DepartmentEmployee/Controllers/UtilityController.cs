using Core.Database.Utils;
using Core.Database.Connection;

namespace DepartmentEmployee.Controllers
{
	public static class UtilityController
	{
		public static int GetId(string query, Connection connection)
		{
			var table = connection.GetDataAdapter(query);
			var id = table.GetColumnValuesDataTable(0, CellType.Integer);
			return int.Parse(id[0].ToString());
		}
	}
}
