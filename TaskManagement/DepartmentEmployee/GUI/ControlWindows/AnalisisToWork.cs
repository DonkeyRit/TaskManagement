using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Database.Utils;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class AnalisisToWork : Form
	{

		private readonly Connection connection;

		public AnalisisToWork()
		{
			InitializeComponent();

			connection = Connection.CreateConnection();

			AssigmentTask newform = new AssigmentTask();

			// Выводим в comboBox1 Фамилии студентов

			DataTable table = connection.GetDataAdapter("Select FIO from Students");
			List<object> questions = table.GetColumnValuesDataTable(0, CellType.String);

			//Reader reader = Workflow.connection.Select("Select FIO from Students");
			//List<object> questions = reader.GetValue(0, true);
			//reader.Close();

			comboBox1.Items.Clear();

			for (int i = 0; i < questions.Count; i++)
			{
				comboBox1.Items.Add(questions[i].ToString());
			}

			// Выводим в comboBox2 список результатов

			table = connection.GetDataAdapter("Select Task_name from Tasks");
			List<object> results = table.GetColumnValuesDataTable(0, CellType.String);

			//reader = Workflow.connection.Select("Select Task_name from Tasks");
			//List<object> results = reader.GetValue(0, true);
			//reader.Close();

			comboBox2.Items.Clear();

			for (int i = 0; i < results.Count; i++)
			{
				comboBox2.Items.Add(results[i].ToString());
			}
		}

		private async void button1_Click(object sender, EventArgs e)
		{

			DataTable dt = await connection.GetDataAdapterAsync("select id, Task_name, description, complexity from Tasks");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				// Скроем столбец ненужные столбцы
				DataGridView1.Columns["id"].Visible = false;
				DataGridView1.Columns["description"].Visible = false;

				//Заголовки таблицы
				DataGridView1.Columns["Task_name"].HeaderText = "Задание";
				DataGridView1.Columns["complexity"].HeaderText = "Срок выполнения(в днях)";

			}
			catch { }
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			DataTable dt = await connection.GetDataAdapterAsync("select id, Task_name, description, complexity from Tasks where Task_name not in((select Tasks.Task_name from Tasks join AssignedTasks on Tasks.id = AssignedTasks.id_Task));");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				// Скроем столбец ненужные столбцы
				DataGridView1.Columns["id"].Visible = false;
				DataGridView1.Columns["description"].Visible = false;

				//Заголовки таблицы
				DataGridView1.Columns["Task_name"].HeaderText = "Задание";
				DataGridView1.Columns["complexity"].HeaderText = "Срок выполнения(в днях)";

			}
			catch { }
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			DataTable dt = await connection.GetDataAdapterAsync("select Tasks.Task_name as Task, AssignedTasks.date_end AS Due_date, Results.Result AS Result from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student join Results on AssignedTasks.id_Result = Results.id and FIO  = '" + comboBox1.Text + "'");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				DataGridView1.Columns["Task"].HeaderText = "Задание";
				DataGridView1.Columns["Due_date"].HeaderText = "Дата сдачи";
				DataGridView1.Columns["Result"].HeaderText = "Статус выполнения";
			}
			catch { }
		}

		private async void button4_Click(object sender, EventArgs e)
		{

			int Interval = 7;

			DataTable dt = await connection.GetDataAdapterAsync("select Tasks.Task_name AS Task, Students.FIO as FIO,AssignedTasks.date_end AS Due_date, AssignedTasks.date_start AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student AND (SELECT (EXTRACT(epoch from age(AssignedTasks.date_end, now())) / 86400)::int) < '" + Interval + "'");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				// Скроем столбец ненужные столбцы
				DataGridView1.Columns["Date_start"].Visible = false;

				//Заголовки таблицы
				DataGridView1.Columns["Task"].HeaderText = "Задание";
				DataGridView1.Columns["FIO"].HeaderText = "Студент";
				DataGridView1.Columns["Due_date"].HeaderText = "Дата сдачи";
			}
			catch { }
		}

		private async void button6_Click(object sender, EventArgs e)
		{

			DataTable dt = await connection.GetDataAdapterAsync("WITH RECURSIVE r AS (SELECT id, Task_name, id_ParentTask FROM Tasks WHERE Task_name = '" + comboBox2.Text + "' UNION SELECT Tasks.id, Tasks.Task_name, Tasks.id_ParentTask FROM Tasks JOIN r ON Tasks.id_ParentTask = r.id) Select r.Task_name as Task_name, Students.FIO as FIO, Results.Result as Result from AssignedTasks join r on r.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student join Results on AssignedTasks.id_Result = Results.id ");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				DataGridView1.Columns["Task_name"].HeaderText = "Задание";
				DataGridView1.Columns["FIO"].HeaderText = "ФИО студента";
				DataGridView1.Columns["Result"].HeaderText = "Текущий результат";
			}
			catch { }
		}

		private async void button5_Click(object sender, EventArgs e)
		{
			DateTime currentDate = DateTime.Now;

			DataTable dt = await connection.GetDataAdapterAsync("select Tasks.Task_name AS Task, Students.FIO as FIO, AssignedTasks.date_end AS Due_date, AssignedTasks.date_start AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student AND AssignedTasks.date_end < '" + currentDate + "'");
			DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				DataGridView1.Columns["Task"].HeaderText = "Задание";
				DataGridView1.Columns["FIO"].HeaderText = "Студент";
				DataGridView1.Columns["Date_start"].HeaderText = "Дата выдачи";
				DataGridView1.Columns["Due_date"].HeaderText = "Дата сдачи";
			}
			catch { }
		}

		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mainform newForm = new Mainform();
			newForm.ToolDataToolStripMenuItem.Visible = false;
			newForm.TaskEmployeeToolStripMenuItem.Visible = false;
			newForm.Show();
			Hide();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
