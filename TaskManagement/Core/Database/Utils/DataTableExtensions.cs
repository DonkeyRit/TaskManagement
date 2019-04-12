using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Core.Database.Utils
{
	public static class DataTableExtensions
	{
		public static List<object> GetColumnValuesDataTable(this DataTable table, int columnIndex, CellType type)
		{

			List<object> list = new List<object>();

			switch (type)
			{
				case CellType.Integer:
					list = table.AsEnumerable().Select(row => row.Field<int>(columnIndex)).Cast<object>().ToList();
					break;
				case CellType.String:
					list = table.AsEnumerable().Select(row => row.Field<string>(columnIndex)).Cast<object>().ToList();
					break;
			}
			return list;
		} 

	}

	public enum CellType
	{
		Integer, String
	}
}
