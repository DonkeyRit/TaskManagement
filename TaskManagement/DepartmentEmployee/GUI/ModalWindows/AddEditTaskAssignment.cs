using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Database.Utils;


namespace DepartmentEmployee.GUI.ModalWindows
{
	public partial class AddEditTaskAssignment : Form
	{
        private readonly Connection _connection;

		public AddEditTaskAssignment()
		{
			InitializeComponent();

            _connection = Connection.CreateConnection();

            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
			this.CancelButton = Button2;

			// Выводим в comboBox1 сотрудников
			DataTable table = _connection.GetDataAdapter("Select FIO from Employees");
			List<object> employees = table.GetColumnValuesDataTable(0, CellType.String);

			//Reader reader = Workflow.connection.Select("Select FIO from Employees");
			//List<object> employees = reader.GetValue(0, true);
			//reader.Close();

			comboBox1.Items.Clear();

			for (int i = 0; i < employees.Count; i++)
			{
				comboBox1.Items.Add(employees[i].ToString());
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

	}
}
