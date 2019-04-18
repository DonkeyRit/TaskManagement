using System;
using Core.Model;
using System.Windows.Forms;
using Core.Database.Connection;
using DepartmentEmployee.Context;

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
				// Скроем столбец ненужные столбцы
				dataGridView1.Columns["id"].Visible = false;

				//Заголовки таблицы
				dataGridView1.Columns["Name"].HeaderText = "Задание";
				dataGridView1.Columns["Name"].Width = 500;
				dataGridView1.Columns["Date_Start"].HeaderText = "Дата выдачи";
				dataGridView1.Columns["Date_Delivery"].HeaderText = "Планируемая дата сдачи";

				dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
			}
			catch
			{
				// ignored
			}
		}


		//Функционал для перехода обратно на стартовую страницу
		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newForm = new Mainform
			{
				ToolDataToolStripMenuItem = {Visible = false}, DirectorToolStripMenuItem = {Visible = false}
			};
			newForm.Show();
			Hide();
		}

		private void button7_Click(object sender, EventArgs e)
		{

			try
			{
				if (dataGridView1.CurrentRow != null)
					Id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			}
			catch
			{
				MessageBox.Show("Сначала выберите задание, по которому хотите посмотреть подробную информацию", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			new DetailedTaskAssigment();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
	}
}
