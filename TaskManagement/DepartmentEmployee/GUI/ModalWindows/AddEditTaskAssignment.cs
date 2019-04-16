using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Database.Utils;
using DepartmentEmployee.GUI.ControlWindows;


namespace DepartmentEmployee.GUI.ModalWindows
{
	public partial class AddEditTaskAssignment : Form
	{
		private Connection connection;

		public AddEditTaskAssignment(int TaskID)
		{
			connection = Connection.CreateConnection();

			InitializeComponent();
			// Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
			this.AcceptButton = Button1;
			this.CancelButton = Button2;

			AssigmentTask newform = new AssigmentTask();

			//int Complexity = newform.GetId(String.Format("Select Complexity from Tasks where id  = '{0}'", TaskID));

			// Выводим в comboBox1 сотрудников

			DataTable table = connection.GetDataAdapter("Select FIO from Employees");
			List<object> employees = table.GetColumnValuesDataTable(0, CellType.String);

			//Reader reader = Workflow.connection.Select();
			//List<object> employees = reader.GetValue(0, true);
			//reader.Close();

			comboBox1.Items.Clear();

			for (int i = 0; i < employees.Count; i++)
			{
				comboBox1.Items.Add(employees[i].ToString());
			}
		}

		public AddEditTaskAssignment()
		{
			InitializeComponent();
			// Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
			this.AcceptButton = Button1;
			this.CancelButton = Button2;

			// Выводим в comboBox1 сотрудников

			DataTable table = connection.GetDataAdapter("Select FIO from Employees");
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
