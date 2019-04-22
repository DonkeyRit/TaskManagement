using Core.Model;
using System.Data;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;
using DepartmentEmployee.Context;
using System.Collections.Generic;
using DepartmentEmployee.Controllers;
using DepartmentEmployee.Model;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class DetailedTaskAssignment : Form
	{
		private readonly Connection _connection;
		private readonly User _currentUser;

		public DetailedTaskAssignment()
		{
			InitializeComponent();
			_currentUser = CustomContext.GetInstance().CurrentUser;
			_connection = Connection.CreateConnection();
			CountProgressModel.CountProgress(_connection, UtilityController.GetId($"Select id_Task from AssignedTasks where id = {TaskEmployee.IdAssigment}", _connection));
			RefreshGrid();
		}

		/// <summary>
		/// Render table
		/// </summary>
		private async void RefreshGrid()
		{

			string login = _currentUser.Username,
				password = _currentUser.Password;
			var id = TaskEmployee.IdAssigment;

            var dt1 = await _connection.GetDataAdapterAsync(

                "select " +
                        "AssignedTasks.Date_Start as Дата_выдачи, " +
                        "Tasks.Date_Delivery as Дата_сдачи, " +
                        "AssignedTasks.Date_End as Дата_завершения, " +
                        "Qualifications.Name as Qualification, " +
                        $"(select Employees.FIO from Tasks join Employees on Tasks.id_TaskManager = Employees.id join AssignedTasks on Tasks.id = AssignedTasks.id_Task where AssignedTasks.id = {id}) as TaskManager, " +
                        $"(select Name from Priority where id = (select Tasks.id_Priority from Tasks join AssignedTasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = {id})) as Priority, " +
                        "(Results.Result_Qual1 + Results.Result_Qual2 + Results.Result_Qual3 + Results.Result_Qual4) as Result " +
                "from Employees " +
                        "join Qualifications on Employees.id_Qualification = Qualifications.id " +
                       "join AssignedTasks on AssignedTasks.id_Employee = Employees.id " +
                        "join Tasks on Tasks.id = AssignedTasks.id_Task " +
                        "join Results on AssignedTasks.id_Result = Results.id " +
                        $"where Results.id in (Select id_Result from AssignedTasks where AssignedTasks.id = {id})");

            DataGridView1.DataSource = dt1; 

			try
			{
				DataGridView1.ChangeHeader(new Dictionary<string, string>
				{
					{"Дата_выдачи","Дата назначения"},
					{"Дата_сдачи","Планируемая дата сдачи"},
					{"Дата_завершения","Фактическая дата сдачи"},
                    {"Qualification","Квалификация"},
                    {"TaskManager","Руководитель"},
					{"Priority","Приоритет"},
                    {"Result","Результат"}
                });

				if(DataGridView1.Columns["TaskManager"] != null)
					DataGridView1.Columns["TaskManager"].Width = 200;
			}
			catch
			{
				// ignored
			}

			var taskId = UtilityController.GetId($"Select id_Task from AssignedTasks where id = {id}", _connection);
			var table = _connection.GetDataAdapter("Select Employees.FIO as Employee " +
													"from AssignedTasks " +
													"join Tasks on Tasks.id = AssignedTasks.id_Task " +
													"join Employees on AssignedTasks.id_Employee = Employees.id " +
													$"where Employees.Login = '{login}' " +
														$"AND Employees.Password = '{password}' " +
														$"AND Tasks.id = {taskId}");

			var name = table.GetColumnValuesDataTable(0, CellType.String);
			var employeeUser = name[0].ToString();

            var dt2 = await _connection.GetDataAdapterAsync(

                "select " +
                        "Employees.FIO as Employee, " +
                        "Qualifications.Name as Qualification, " +
                        "AssignedTasks.Date_Start as Date_Start, " +
                        "(Results.Result_Qual1 + Results.Result_Qual2 + Results.Result_Qual3 + Results.Result_Qual4) as Result " +
                "from Employees " +
                        "join Qualifications on Employees.id_Qualification = Qualifications.id " +
                        "join AssignedTasks on AssignedTasks.id_Employee = Employees.id " +
                        "join Results on AssignedTasks.id_Result = Results.id " +
                        $"where Results.id in (Select id_Result from AssignedTasks where id_Task = {taskId})");

            dataGridView2.DataSource = dt2;
			dataGridView2.CurrentCell = null;

			for (var i = 0; i < dataGridView2.Rows.Count; i++)
			{
				dataGridView2.Rows[i].Visible = employeeUser != dataGridView2.Rows[i].Cells[0].Value.ToString();
			}
			
			try
			{
				dataGridView2.ChangeHeader(new Dictionary<string, string>
				{
					{"Employee","Сотрудник"},
					{"Qualification","Квалификация"},
					{"Date_Start", "Дата назначения"},
                    {"Result", "Результат"}
                });
			}
			catch
			{
				// ignored
			}

			var dt3 = await _connection.GetDataAdapterAsync("select SUM(Results.Result_Qual1) as Result_Qual1, " +
															"SUM(Results.Result_Qual2) as Result_Qual2, " +
															"SUM(Results.Result_Qual3) as Result_Qual3, " +
															"SUM(Results.Result_Qual4) as Result_Qual4 " +
															"from Results " +
															"join AssignedTasks " +
															"on AssignedTasks.id_Result = Results.id " +
															$"where AssignedTasks.id_Task = {taskId}");
			dataGridView3.DataSource = dt3; 

			try
			{

				dataGridView3.ChangeHeader(new Dictionary<string, string>
				{
					{"Result_Qual1","Кол-во часов 'Инженера 3-категории'"},
					{"Result_Qual2","Кол-во часов 'Инженера 2-категории'"},
					{"Result_Qual3","Кол-во часов 'Инженера 1-категории'"},
					{"Result_Qual4", "Кол-во часов 'Главного инженера'"}
				});
			}
			catch
			{
				// ignored
			}

			table = _connection.GetDataAdapter("Select Tasks.Name from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = '" + id + "'");
			string taskName = table.GetColumnValuesDataTable(0, CellType.String)[0].ToString(),
				textLabel = taskName;
			label1.Text = @"Описание задания: " + textLabel;


			var newDt = await _connection.GetDataAdapterAsync("Select Tasks.Description as description from AssignedTasks join Tasks on AssignedTasks.id_Task = Tasks.id where AssignedTasks.id = '" + id + "'");

			var description = "";

			for (var i = 0; i < newDt.Rows.Count; i++)
			{
				description = newDt.Rows[i]["description"].ToString();
			}

			foreach (DataRow dr in newDt.Rows)
			{
				description = dr["description"].ToString();
			}

			var textBox = string.Join("", description);
			richTextBox1.Text = textBox;
			richTextBox1.ReadOnly = true;
		}
	}
}
