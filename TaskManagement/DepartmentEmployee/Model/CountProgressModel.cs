using System;
using System.Data;
using Core.Database.Utils;
using Core.Database.Connection;
using DepartmentEmployee.Model.Enums;

namespace DepartmentEmployee.Model
{
	public static class CountProgressModel
	{
		public static void CountProgress(Connection _connection, int idTask)
		{
			var query = "SELECT id_Employee FROM EventLog " +
				$"WHERE id_Task = {idTask} " +
				"GROUP BY id_Employee";

			var dt = _connection.GetDataAdapter(query);

			foreach (DataRow row in dt.Rows)
			{
				GetCountWorkHours(_connection, (int)row["id_Employee"], idTask);
			}
		}

		private static void GetCountWorkHours(Connection _connection, int idEmployee, int idTask)
		{
			var query = $"SELECT * FROM EventLog WHERE id_Employee = {idEmployee} ORDER BY id";
			var dt = _connection.GetDataAdapter(query);
			var rows = dt.Rows;

			query = $"SELECT id_Qualification FROM Employees WHERE id = {idEmployee};";
			var result = _connection.GetDataAdapter(query);
			var qualification = (int) result.GetColumnValuesDataTable(0, CellType.Integer)[0];

			var workHours = 0;

			for (int i = 0, n = dt.Rows.Count; i < n - 1; i++)
			{
				var firstDate = Convert.ToDateTime(rows[i]["Date"]);
				var secondDate = Convert.ToDateTime(rows[i + 1]["Date"]);

				Status lastStatus = (Status)(int)rows[i + 1]["id_LastStatus"],
					currentStatus = (Status)(int)rows[i + 1]["id_CurrentStatus"];

				if (lastStatus == Status.OnExecution)
				{
					workHours += (int)secondDate.Subtract(firstDate).TotalHours;
				}
			}

			InsertResult(_connection, workHours, qualification, idEmployee, idTask);
		}
		public static void InsertResult(Connection _connection, int progress, int qualification, int idEmployee, int idTask)
		{
			int q1 = 0, q2 = 0, q3 = 0, q4 = 0;

			switch (qualification)
			{
				case 1:
					q1 = progress;
					break;
				case 2:
					q2 = progress;
					break;
				case 3:
					q3 = progress;
					break;
				case 4:
					q4 = progress;
					break;
			}

			var query = "UPDATE Results " +
				$"SET Result_Qual1 = {q1}, Result_Qual2 = {q2}, Result_Qual3 = {q3}, Result_Qual4 = {q4} " +
				"WHERE id = (" +
					$"SELECT id_Result FROM AssignedTasks WHERE id_Task = {idTask} " +
					$"AND id_Employee = {idEmployee}" +
				");";
			_connection.ExecNonQuery(query);
		}
	}
}
