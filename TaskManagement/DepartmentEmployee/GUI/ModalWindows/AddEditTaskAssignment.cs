using System;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;

namespace DepartmentEmployee.GUI.ModalWindows
{
	public partial class AddEditTaskAssignment : Form
	{
		public AddEditTaskAssignment()
		{
			InitializeComponent();

			var connection = Connection.CreateConnection();

			AcceptButton = Button1;
			CancelButton = Button2;

			var table = connection.GetDataAdapter("Select FIO from Employees");
			var employees = table.GetColumnValuesDataTable(0, CellType.String);

			comboBox1.Items.Clear();

			foreach (var t in employees)
			{
				comboBox1.Items.Add(t);
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

	}
}
