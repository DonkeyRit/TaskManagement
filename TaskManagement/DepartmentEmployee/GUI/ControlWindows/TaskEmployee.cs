using System;
using Core.Model;
using System.Windows.Forms;
using Core.Database.Connection;
using System.Collections.Generic;
using System.Data;
using DepartmentEmployee.Context;
using DepartmentEmployee.Controllers;
using DepartmentEmployee.Model.Enums;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class TaskEmployee : Form
	{
		private readonly Connection _connection;
		private readonly User _currentUser;

		public static int Id;

		public TaskEmployee()
		{
			InitializeComponent();

			_connection = Connection.CreateConnection();
			_currentUser = CustomContext.GetInstance().CurrentUser;

			RefreshGrid();
		}

		private async void RefreshGrid()
		{
			var query = "select AssignedTasks.id_Task as id, Tasks.Name as Name, AssignedTasks.Date_Start as Date_Start, Tasks.Date_Delivery as Date_Delivery " +
				"from AssignedTasks " +
				"join Tasks on Tasks.id = AssignedTasks.id_Task join Employees " +
				$"on Employees.id = AssignedTasks.id_Employee where Employees.id  = (select id from Employees where Login = '{_currentUser.Username}' AND Password = '{_currentUser.Password}') " +
				"ORDER BY Tasks.Date_Delivery ASC";

			var dt = await _connection.GetDataAdapterAsync(query);

			AddStatusColumn(dt);

			if (dataGridView1.CurrentRow != null) Id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			dataGridView1.DataSource = dt;
			try
			{
				dataGridView1.SetVisible(false, "id");
				dataGridView1.ChangeHeader(new Dictionary<string, string>
				{
					{"Name", "Задание"},
					{"Date_Start", "Дата выдачи"},
					{"Date_Delivery", "Планируемая дата сдачи"}
				});

				if(dataGridView1.Columns["Name"] != null)
					dataGridView1.Columns["Name"].Width = 500;

				dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
			}
			catch
			{
				// ignored
			}
		}

		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newForm = new Mainform
			{
				ToolDataToolStripMenuItem = {Visible = false}, DirectorToolStripMenuItem = {Visible = false}
			};
			newForm.Show();
			Hide();
		}
		private void ShowGeneralInfo_Click(object sender, EventArgs e)
		{
			try
			{
				if (dataGridView1.CurrentRow != null)
					Id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			}
			catch
			{
				MessageBox.Show(
					@"First select the task for which you want to see detailed information.", 
					@"Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var form = new DetailedTaskAssignment();
			form.Show();
		}

		#region Helper methods

		private void AddStatusColumn(DataTable table)
		{
			var statusOfTask = new DataColumn("Статус");
			table.Columns.Add(statusOfTask);

			var uniqueIds = GetListOfTaskId(table, "id");
			var valueSection = "(" +  string.Join(",", uniqueIds) + ")";

			var query ="SELECT id_Task, id_CurrentStatus " +
						"FROM EventLog " +
						"WHERE id = (" +
							"SELECT max(id) " +
							"FROM EventLog " +
							$"WHERE id_Task IN {valueSection} " +
								$"AND id_Employee = (select id from Employees where Login = '{_currentUser.Username}' AND Password = '{_currentUser.Password}'))";

			var statusTable = _connection.GetDataAdapter(query);
			FillColumnValuesFromAnotherTable(table, statusTable);
		}

		private static void FillColumnValuesFromAnotherTable(DataTable original, DataTable values)
		{
			foreach (DataRow valuesRow in values.Rows)
			{
				var taskId = valuesRow["id_Task"].ToString();
				var status = (Status) valuesRow["id_CurrentStatus"];

				foreach (DataRow originalRow in original.Rows)
				{
					if (originalRow["id"].ToString().Equals(taskId))
						originalRow["Статус"] = status.ToString();
				}
			}
		}

		private static IEnumerable<string> GetListOfTaskId(DataTable table, string columnName)
		{
			var list = new List<string>();

			foreach (DataRow row in table.Rows)
			{
				list.Add(row[columnName].ToString());
			}

			return list;
		}


		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

		#endregion
	}
}
