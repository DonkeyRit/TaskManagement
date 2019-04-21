using System.Data;
using System.Windows.Forms;
using DepartmentEmployee.Model;
using Core.Database.Connection;
using System.Collections.Generic;
using DepartmentEmployee.Controllers;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class ImplementationProgress : Form
	{
		private readonly Connection _connection;

		public ImplementationProgress()
		{
			InitializeComponent();
			_connection = Connection.CreateConnection();
			CountProgressModel.CountProgress(_connection, AssignmentTask.IdTask);
			RefreshGrid();
		}

		private async void RefreshGrid()
		{
			var id = AssignmentTask.IdTask;

			DataTable dt = await _connection.GetDataAdapterAsync(
				"select SUM(Results.Result_Qual1) as Result_Qual1, " +
					"SUM(Results.Result_Qual2) as Result_Qual2, " +
					"SUM(Results.Result_Qual3) as Result_Qual3, " +
					"SUM(Results.Result_Qual4) as Result_Qual4 " +
				"from Results " +
				"join AssignedTasks " +
					"on AssignedTasks.id_Result = Results.id " +
						$"AND AssignedTasks.id_Task = {id}");
			dataGridView1.DataSource = dt; 

			try
			{
				dataGridView1.ChangeHeader(new Dictionary<string, string>
				{
					{"Result_Qual1", "Кол-во часов 'Инженера 3-категории'"},
					{"Result_Qual2", "Кол-во часов 'Инженера 2-категории'"},
					{"Result_Qual3", "Кол-во часов 'Инженера 1-категории'"},
					{"Result_Qual4", "Кол-во часов 'Главного инженера'"},
				});
			}
			catch { }
		}
	}
}
