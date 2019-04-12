using System;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Model;
using DepartmentEmployee.Context;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class TaskEmployee : Form
	{
		private readonly Connection _connection;
		private readonly User _currentUser;

		public static int ID;

		public TaskEmployee()
		{
			InitializeComponent();

			_connection = Connection.CreateConnection();
			_currentUser = CustomContext.GetInstance().CurrentUser;

			RefreshGrid();
		}

		private async void RefreshGrid()
		{

			string login = _currentUser.Username;
			string password = _currentUser.Password;


			DataTable dt = await _connection.GetDataAdapterAsync("select AssignedTasks.id as id, Tasks.Name as Name, AssignedTasks.Date_Start as Date_Start, Tasks.Date_Delivery as Date_Delivery from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee where Employees.id  = (select id from Employees where Login = '" + login + "' AND Password = '" + password + "') ORDER BY Tasks.Date_Delivery ASC");
			dataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView
			ID = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			try
			{
				// Скроем столбец ненужные столбцы
				dataGridView1.Columns["id"].Visible = false;

				//Заголовки таблицы
				dataGridView1.Columns["Name"].HeaderText = "Задание";
				dataGridView1.Columns["Name"].Width = 500;
				dataGridView1.Columns["Date_Start"].HeaderText = "Дата выдачи";
				dataGridView1.Columns["Date_Delivery"].HeaderText = "Планируемая дата сдачи";
			}
			catch { }

			// выбираем первую строчку
			try
			{
				dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
			}
			catch { }
		}


		//Функционал для перехода обратно на стартовую страницу
		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mainform newForm = new Mainform();
			newForm.ToolDataToolStripMenuItem.Visible = false;
			newForm.DirectorToolStripMenuItem.Visible = false;
			newForm.Show();
			Hide();
		}

		//Функционал для выхода из приложения
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			//int ID;

			try
			{
				ID = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			}
			catch
			{
				MessageBox.Show("Сначала выберите задание, по которому хотите посмотреть подробную информацию", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			DetailedTaskAssigment form = new DetailedTaskAssigment();
			if (form.ShowDialog() != DialogResult.OK)
			{ return; }
		}
	}
}
