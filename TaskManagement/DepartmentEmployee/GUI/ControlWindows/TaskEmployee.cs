using System;
using Core.Model;
using System.Windows.Forms;
using Core.Database.Connection;
using System.Collections.Generic;
using DepartmentEmployee.Context;
using DepartmentEmployee.Controllers;

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

			string login = _currentUser.Username,
				password = _currentUser.Password;

			var dt = await _connection.GetDataAdapterAsync(
				"select AssignedTasks.id as id, Tasks.Name as Name, AssignedTasks.Date_Start as Date_Start, Tasks.Date_Delivery as Date_Delivery " +
				"from AssignedTasks " +
				"join Tasks on Tasks.id = AssignedTasks.id_Task join Employees " +
				$"on Employees.id = AssignedTasks.id_Employee where Employees.id  = (select id from Employees where Login = '{login}' AND Password = '{password}') " +
				"ORDER BY Tasks.Date_Delivery ASC");

			dataGridView1.DataSource = dt;
			if (dataGridView1.CurrentRow != null) Id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
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

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
	}
}
