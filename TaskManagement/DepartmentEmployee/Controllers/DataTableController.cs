using System.Windows.Forms;
using System.Collections.Generic;

namespace DepartmentEmployee.Controllers
{
	public static class DataTableController
	{
		public static void SetVisible(this DataGridView view, bool value, params string[] columns)
		{
			var elements = view.Columns;

			foreach (var column in columns)
			{
				var element = elements[column];

				if (element != null)
					element.Visible  = true;
			}
		}

		public static void ChangeHeader(this DataGridView view, Dictionary<string, string> pairs)
		{
			var elements = view.Columns;

			foreach (var pair in pairs)
			{
				var element = elements[pair.Key];

				if (element != null)
					element.HeaderText = pair.Value;

			}
		}
	}
}
