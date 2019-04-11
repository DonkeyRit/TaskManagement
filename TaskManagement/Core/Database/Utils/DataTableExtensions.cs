using System.Data;
using System.Collections.Generic;

namespace Core.Database.Utils
{
	public static class DataTableExtensions
	{
		public static List<object> ParseDataTable(this DataTable table, int columnIndex, CellType type)
		{
			List<object> list = new List<object>();


			//if (reader.HasRows)
			//{

			//	while (reader.Read())
			//	{
			//		if (type)
			//		{
			//			list.Add(reader.GetString(index));
			//		}
			//		else
			//		{
			//			list.Add(reader.GetInt32(index));
			//		}
			//	}
			//}
			//else
			//{
			//	return null;
			//}

			return list;
		} 

	}

	public enum CellType
	{
		Integer, String
	}
}
