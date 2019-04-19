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

			if (dataGridView1.CurrentRow != null) Id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			dataGridView1.DataSource = dt;

			AddStatusColumn(dt);

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
			var uniqueIds = GetListOfTaskId(table, "id");
			var valueSection = "(" +  string.Join(",", uniqueIds) + ")";

			var query ="SELECT id_Task, id_CurrentStatus " +
						"FROM EventLog " +
						"WHERE id IN (" +
							"SELECT max(id) " +
							"FROM EventLog " +
							$"WHERE id_Task IN {valueSection} " +
								$"AND id_Employee = (select id from Employees where Login = '{_currentUser.Username}' AND Password = '{_currentUser.Password}')" +
								"group by id_Task, id_Employee)";

			var statusTable = _connection.GetDataAdapter(query);
			var statuses = GetOrderListOfStatuses(table, statusTable);

			AddNewColumn("Статус", table, statusTable);
			int i = 0;

			foreach(DataGridViewRow row in dataGridView1.Rows)
			{
				row.Cells["Status"].Value = statuses[i];
				i++;
			}
		}

		private List<string> GetOrderListOfStatuses(DataTable original, DataTable values)
		{
			var statuses = new List<string>();

			foreach (DataRow originalRow in original.Rows)
			{
				var id = (int)originalRow["id"];
				foreach (DataRow valueRow in values.Rows)
				{
					var taskId = (int)valueRow["id_Task"];
					var status = (Status)valueRow["id_CurrentStatus"];

					if (id == taskId)
						statuses.Add(status.ToString());
				}
			}

			return statuses;
		}

		private void AddNewColumn(string name, DataTable original, DataTable values)
		{
			var cmb = new DataGridViewComboBoxColumn
			{
				HeaderText = "Статус",
				Name = "Status",
				MaxDropDownItems = 4,
				
			};

			cmb.Items.AddRange(
				Status.Assigned.ToString(), 
				Status.Completed.ToString(), 
				Status.OnExecution.ToString(), 
				Status.Suspended.ToString());

			dataGridView1.Columns.Add(cmb);
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
