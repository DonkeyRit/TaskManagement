using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Database.Utils;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class DetailedTaskAssigment : Form
	{
		private readonly Connection connection;

		public DetailedTaskAssigment()
		{
			InitializeComponent();
			connection = Connection.CreateConnection();
			RefreshGrid();
		}

		//Функционал вывода всего списка
		private async void RefreshGrid()
		{

			string Login = Authorization.Login;
			string Password = Authorization.Password;
			int ID = TaskEmployee.ID;

			//Выводим подробную информацию по назначенному заданию по конкретному сотруднику
			DataTable dt1 = await connection.GetDataAdapterAsync("select AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Дата_сдачи, AssignedTasks.Date_End as Дата_завершения, Employees.FIO AS TaskManager, Results.Name as Результат, Priority.Name as Priority from Tasks join Employees on Employees.id = Tasks.id_TaskManager join Priority on Priority.id = Tasks.id_Priority join AssignedTasks on Tasks.id = AssignedTasks.id_Task join Results on Results.id = AssignedTasks.id_Result where AssignedTasks.id = '" + ID + "'");
			DataGridView1.DataSource = dt1; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				//Заголовки таблицы
				DataGridView1.Columns["Дата_выдачи"].HeaderText = "Дата назначения";
				DataGridView1.Columns["Дата_сдачи"].HeaderText = "Планируемая дата сдачи";
				DataGridView1.Columns["Дата_завершения"].HeaderText = "Фактическая дата сдачи";
				DataGridView1.Columns["TaskManager"].HeaderText = "Руководитель";
				DataGridView1.Columns["TaskManager"].Width = 200;
				DataGridView1.Columns["Результат"].HeaderText = "Прогресс выполнения";
				DataGridView1.Columns["Priority"].HeaderText = "Приоритет";
			}
			catch { }

			//Получаем ID задания, которое выполняет сотрудник
			int TaskID = GetId(String.Format("Select id_Task from AssignedTasks where id = '" + ID + "'"));

			//Получаем исполнителя задания под которым авторизовались

			DataTable table = connection.GetDataAdapter("Select Employees.FIO as Employee from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on AssignedTasks.id_Employee = Employees.id where Employees.Login = '" + Login + "' AND Employees.Password = '" + Password + "' AND Tasks.id = '" + TaskID + "'");
			List<object> name = table.ParseDataTable(0, CellType.String);

			//Reader reader = Workflow.connection.Select();
			//List<object> name = reader.GetValue(0, true);
			//reader.Close();

			//Записываем исполнителя в строку для дальнейшего сравнения
			string EmployeeUser = String.Join("", name);
			
			//Выводим всех сотрудников, выполняющих совместно данное задание
			DataTable dt2 = await connection.GetDataAdapterAsync("Select Employees.FIO as Employee, Qualifications.Name as Qualification, AssignedTasks.Date_Start as Data_Start, (select Name from Results where id = (select id_Result from Tasks where id = '" + TaskID + "')) As Result from Employees join Qualifications on Qualifications.id = Employees.id_Qualification join AssignedTasks on AssignedTasks.id_Employee = Employees.id where AssignedTasks.id_Task = '" + TaskID + "'");
			dataGridView2.DataSource = dt2; //Присвеиваем DataTable в качестве источника данных DataGridView

			dataGridView2.CurrentCell = null;
			//Делаем проверку, чтобы во второй таблице не показывался сотрудник, под которым мы зашли в систему
			for (int i = 0; i < dataGridView2.Rows.Count; i++)
			{
				if (EmployeeUser == dataGridView2.Rows[i].Cells[0].Value.ToString())
				{
					dataGridView2.Rows[i].Visible = false;
				}
				else dataGridView2.Rows[i].Visible = true;
			}
			
			try
			{
				//Заголовки таблицы
				dataGridView2.Columns["Employee"].HeaderText = "Сотрудник";

				dataGridView2.Columns["Qualification"].HeaderText = "Квалификация";
				dataGridView2.Columns["Data_Start"].HeaderText = "Дата назначения";
				dataGridView2.Columns["Result"].HeaderText = "Прогресс выполнения";
			}
			catch { }

			//Получаем количество "человеко-часов" необходимых для выполнения задания
			int Complexity= GetId(String.Format("Select complexity from Tasks where id = '" + TaskID + "'"));

			//Получаем и выводим название задания

			table = connection.GetDataAdapter("Select Tasks.Name from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = '" + ID + "'");
			List<object> task_name = table.ParseDataTable(0, CellType.String);

			//reader = Workflow.connection.Select("Select Tasks.Name from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = '" + ID + "'");
			//List<object> task_name = reader.GetValue(0, true);
			//reader.Close();

			string TextLabel = String.Join("", task_name);
			label1.Text = "Описание задания: " + TextLabel + " (Трудоёмкость: " + Complexity + " часов)";

			//Получаем и выводим описание к заданию
			DataTable newdt = await connection.GetDataAdapterAsync("Select Tasks.Description as description from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = '" + ID + "'");

			string description = "";

			for (int i = 0; i < newdt.Rows.Count; i++)
			{
				description = newdt.Rows[i]["description"].ToString();
			}

			foreach (DataRow dr in newdt.Rows)
			{
				description = dr["description"].ToString();
			}

			string TextBox = String.Join("", description);
			richTextBox1.Text = TextBox;
			richTextBox1.ReadOnly = true;
		}

		//Функционал получения ID
		public int GetId(string query)
		{
			DataTable table = connection.GetDataAdapter(query);
			List<object> identificator = table.ParseDataTable(0, CellType.Integer);

			//Reader reader = Workflow.connection.Select(query);
			//List<object> identificator = reader.GetValue(0, false);
			//reader.Close();
			return int.Parse(identificator[0].ToString());
		}
	}
}
