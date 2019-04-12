using System;
using System.Data;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;


namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class AnalysisToWork : Form
	{
		private readonly Connection _connection;

		public AnalysisToWork()
		{
			InitializeComponent();
			_connection = Connection.CreateConnection();
			SetUp();
		}

		private void SetUp()
		{
			DataTable resultFio = _connection.GetDataAdapter("Select FIO from Students"),
				resultTaskName = _connection.GetDataAdapter("Select Task_name from Tasks");

			object[] listFio = resultFio.GetColumnValuesDataTable(0, CellType.String).ToArray(),
				listTaskNames = resultTaskName.GetColumnValuesDataTable(0, CellType.String).ToArray();

			comboBox1.Items.Clear();
			ComboBoxListWorkers.Items.Clear();

			comboBox1.Items.AddRange(listFio);
			ComboBoxListWorkers.Items.AddRange(listTaskNames);
		}

		private async void button1_Click(object sender, EventArgs e)
		{

			DataTable dt = await _connection.GetDataAdapterAsync("select id, Task_name, description, complexity from Tasks");
			TableContent.DataSource = dt;

			try
			{
				// Скроем столбец ненужные столбцы
				TableContent.Columns["id"].Visible = false;
				TableContent.Columns["description"].Visible = false;

				//Заголовки таблицы
				TableContent.Columns["Task_name"].HeaderText = "Задание";
				TableContent.Columns["complexity"].HeaderText = "Срок выполнения(в днях)";

			}
			catch { }
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			DataTable dt = await _connection.GetDataAdapterAsync("select id, Task_name, description, complexity from Tasks where Task_name not in((select Tasks.Task_name from Tasks join AssignedTasks on Tasks.id = AssignedTasks.id_Task));");
			TableContent.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				// Скроем столбец ненужные столбцы
				TableContent.Columns["id"].Visible = false;
				TableContent.Columns["description"].Visible = false;

				//Заголовки таблицы
				TableContent.Columns["Task_name"].HeaderText = "Задание";
				TableContent.Columns["complexity"].HeaderText = "Срок выполнения(в днях)";

			}
			catch { }
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			DataTable dt = await _connection.GetDataAdapterAsync("select Tasks.Task_name as Task, AssignedTasks.date_end AS Due_date, Results.Result AS Result from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student join Results on AssignedTasks.id_Result = Results.id and FIO  = '" + comboBox1.Text + "'");
			TableContent.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				TableContent.Columns["Task"].HeaderText = "Задание";
				TableContent.Columns["Due_date"].HeaderText = "Дата сдачи";
				TableContent.Columns["Result"].HeaderText = "Статус выполнения";
			}
			catch { }
		}

		private async void button4_Click(object sender, EventArgs e)
		{

			int Interval = 7;

			DataTable dt = await _connection.GetDataAdapterAsync("select Tasks.Task_name AS Task, Students.FIO as FIO,AssignedTasks.date_end AS Due_date, AssignedTasks.date_start AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student AND (SELECT (EXTRACT(epoch from age(AssignedTasks.date_end, now())) / 86400)::int) < '" + Interval + "'");
			TableContent.DataSource = dt;

			try
			{
				// Скроем столбец ненужные столбцы
				TableContent.Columns["Date_start"].Visible = false;

				//Заголовки таблицы
				TableContent.Columns["Task"].HeaderText = "Задание";
				TableContent.Columns["FIO"].HeaderText = "Студент";
				TableContent.Columns["Due_date"].HeaderText = "Дата сдачи";
			}
			catch { }
		}

		private async void button6_Click(object sender, EventArgs e)
		{

			DataTable dt = await _connection.GetDataAdapterAsync("WITH RECURSIVE r AS (SELECT id, Task_name, id_ParentTask FROM Tasks WHERE Task_name = '" + ComboBoxListWorkers.Text + "' UNION SELECT Tasks.id, Tasks.Task_name, Tasks.id_ParentTask FROM Tasks JOIN r ON Tasks.id_ParentTask = r.id) Select r.Task_name as Task_name, Students.FIO as FIO, Results.Result as Result from AssignedTasks join r on r.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student join Results on AssignedTasks.id_Result = Results.id ");
			TableContent.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				TableContent.Columns["Task_name"].HeaderText = "Задание";
				TableContent.Columns["FIO"].HeaderText = "ФИО студента";
				TableContent.Columns["Result"].HeaderText = "Текущий результат";
			}
			catch { }
		}

		private async void button5_Click(object sender, EventArgs e)
		{
			DateTime currentDate = DateTime.Now;

			DataTable dt = await _connection.GetDataAdapterAsync("select Tasks.Task_name AS Task, Students.FIO as FIO, AssignedTasks.date_end AS Due_date, AssignedTasks.date_start AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student AND AssignedTasks.date_end < '" + currentDate + "'");
			TableContent.DataSource = dt;

			try
			{
				//Заголовки таблицы
				TableContent.Columns["Task"].HeaderText = "Задание";
				TableContent.Columns["FIO"].HeaderText = "Студент";
				TableContent.Columns["Date_start"].HeaderText = "Дата выдачи";
				TableContent.Columns["Due_date"].HeaderText = "Дата сдачи";
			}
			catch { }
		}

		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mainform newForm = new Mainform
			{
				ToolDataToolStripMenuItem = { Visible = false },
				TaskEmployeeToolStripMenuItem = { Visible = false }
			};

			newForm.Show();
			Hide();
		}
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
	}
}
