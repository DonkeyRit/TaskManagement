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
            DataTable resultFio = _connection.GetDataAdapter("Select FIO from Employees");

            object[] listFio = resultFio.GetColumnValuesDataTable(0, CellType.String).ToArray();

			comboBox1.Items.Clear();

			comboBox1.Items.AddRange(listFio);
		}

		private async void button3_Click(object sender, EventArgs e)
		{
            DataTable dt = await _connection.GetDataAdapterAsync(
                "select Tasks.Name as Task, " +
                       "Tasks.Date_Delivery AS Date_Delivery, " +
                       "(Results.Result_Qual1 + Results.Result_Qual2 + Results.Result_Qual3 + Results.Result_Qual4) as Result " +
                "from AssignedTasks " +
                       "join Tasks on Tasks.id = AssignedTasks.id_Task " +
                       "join Employees on Employees.id = AssignedTasks.id_Employee " +
                       "join Results on AssignedTasks.id_Result = Results.id " +
                        $"where Results.id in (Select id_Result from AssignedTasks " +
                            "where id_Employee in (select id from Employees where FIO = '" + comboBox1.Text + "'))");
            TableContent.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

            try
			{
                //Заголовки таблицы
                TableContent.Columns["Task"].HeaderText = "Наименование";
                TableContent.Columns["Task"].Width = 500;
                TableContent.Columns["Date_Delivery"].HeaderText = "Дата сдачи";
                TableContent.Columns["Result"].HeaderText = "Прогресс выполнения";
            }
			catch { }
		}

		private async void button4_Click(object sender, EventArgs e)
		{

            int Interval = 7;

            DataTable dt = await _connection.GetDataAdapterAsync("select Tasks.Name AS Task, Employees.FIO as Employee, Tasks.Date_Delivery AS Date_Delivery from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Employees on Employees.id = AssignedTasks.id_Employee AND (SELECT (EXTRACT(epoch from age(Tasks.Date_Delivery, now())) / 86400)::int) < '" + Interval + "' AND NOT Tasks.Date_Delivery < '" + DateTime.Now + "'");
            TableContent.DataSource = dt;

            try
            {
                //Заголовки таблицы
                TableContent.Columns["Task"].HeaderText = "Наименование";
                TableContent.Columns["Employee"].HeaderText = "Сотрудник";
                TableContent.Columns["Date_Delivery"].HeaderText = "Дата сдачи";
            }
            catch { }
        }

		private async void button5_Click(object sender, EventArgs e)
		{
            DateTime currentDate = DateTime.Now;

            DataTable dt = await _connection.GetDataAdapterAsync("select Tasks.Name AS Task, Employees.FIO as FIO, AssignedTasks.Date_Start AS Date_start, Tasks.Date_Delivery AS Date_Delivery from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Employees on Employees.id = AssignedTasks.id_Employee AND Tasks.Date_Delivery < '" + currentDate + "'");
            TableContent.DataSource = dt;

            try
            {
                //Заголовки таблицы
                TableContent.Columns["Task"].HeaderText = "Задание";
                TableContent.Columns["FIO"].HeaderText = "Студент";
                TableContent.Columns["Date_Start"].HeaderText = "Дата выдачи";
                TableContent.Columns["Date_Delivery"].HeaderText = "Дата сдачи";
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
