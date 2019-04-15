using System;
using System.Collections.Generic;
using System.Data;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;
using DepartmentEmployee.Controllers;


namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class AnalysisToWork : Form
	{
		private readonly Connection _connection;

		public AnalysisToWork()
		{
			_connection = Connection.CreateConnection();

			InitializeComponent();
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

		private void button1_Click(object sender, EventArgs e)
		{

			var dt = _connection.GetDataAdapter("select id, Name, description, complexity from Tasks");
			TableContent.DataSource = dt;

			try
			{
				TableContent.SetVisible(false, "id","description");
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task_name","Задание"},{"complexity", "Срок выполнения(в днях)"}
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			var dt = await _connection.GetDataAdapterAsync(
				"select id, Task_name, description, complexity from Tasks where Task_name not in" +
				"((select Tasks.Task_name from Tasks join AssignedTasks on Tasks.id = AssignedTasks.id_Task));");
			TableContent.DataSource = dt;

			try
			{
				TableContent.SetVisible(false, "id", "description");
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task_name","Задание"},{"complexity", "Срок выполнения(в днях)"}
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			var dt = await _connection.GetDataAdapterAsync(
				"select Tasks.Task_name as Task, AssignedTasks.date_end AS Due_date, Results.Result AS Result from AssignedTasks " +
				"join Tasks on Tasks.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student join Results " +
				"on AssignedTasks.id_Result = Results.id and FIO  = '" + comboBox1.Text + "'");
			TableContent.DataSource = dt;

			try
			{
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task","Задание"},{"Due_date", "Дата сдачи"},{"Result", "Статус выполнения" }
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private async void button4_Click(object sender, EventArgs e)
		{
			var dt = await _connection.GetDataAdapterAsync(
				"select Tasks.Task_name AS Task, Students.FIO as FIO,AssignedTasks.date_end AS Due_date, AssignedTasks.date_start " +
				"AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student " +
				$"AND (SELECT (EXTRACT(epoch from age(AssignedTasks.date_end, now())) / 86400)::int) < '7'");
			TableContent.DataSource = dt;

			try
			{
				TableContent.SetVisible(false, "Date_start");
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task","Задание"},{"FIO", "Студент"},{"Due_date", "Дата сдачи" }
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private async void button6_Click(object sender, EventArgs e)
		{

			var dt = await _connection.GetDataAdapterAsync(
				$"WITH RECURSIVE r AS (SELECT id, Task_name, id_ParentTask FROM Tasks WHERE Task_name = '{ComboBoxListWorkers.Text}' " +
				"UNION SELECT Tasks.id, Tasks.Task_name, Tasks.id_ParentTask FROM Tasks JOIN r ON Tasks.id_ParentTask = r.id) " +
				"Select r.Task_name as Task_name, Students.FIO as FIO, Results.Result as Result from AssignedTasks " +
				"join r on r.id = AssignedTasks.id_Task join Students on Students.id = AssignedTasks.id_Student " +
				"join Results on AssignedTasks.id_Result = Results.id ");
			TableContent.DataSource = dt;

			try
			{
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task_name","Задание"}, {"FIO", "ФИО студента"}, {"Result", "Текущий результат" }
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private async void button5_Click(object sender, EventArgs e)
		{
			var currentDate = DateTime.Now;

			DataTable dt = await _connection.GetDataAdapterAsync(
				"select Tasks.Task_name AS Task, Students.FIO as FIO, AssignedTasks.date_end AS Due_date, AssignedTasks.date_start " +
				"AS Date_start from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id join Students on Students.id = AssignedTasks.id_Student " +
				$"AND AssignedTasks.date_end < '{currentDate}'");
			TableContent.DataSource = dt;

			try
			{
				TableContent.ChangeHeader(new Dictionary<string, string>
				{
					{"Task","Задание"}, {"FIO", "Студент"}, {"Date_start", "Дата выдачи" }, {"Due_date", "Дата сдачи"}
				});
			}
			catch (Exception ex)
			{
				ModalDialogController.Display("Exception: ", ex);
			}
		}

		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newForm = new Mainform
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
