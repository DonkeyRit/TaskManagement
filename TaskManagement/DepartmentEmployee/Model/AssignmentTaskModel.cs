using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using DepartmentEmployee.Controllers;
using DepartmentEmployee.Model.Enums;
using DepartmentEmployee.GUI.ModalWindows;
using DepartmentEmployee.GUI.ControlWindows;

namespace DepartmentEmployee.Model
{
	public class AssignmentTaskModel
	{
		private readonly AssignmentTask _form;

		public AssignmentTaskModel(AssignmentTask form)
		{
			_form = form;
		}

		#region ButtonControllers

		public void AddNewTaskButton_Click()
		{
			var tasksForm = new AddEditTask();

			if (tasksForm.ShowDialog() != DialogResult.OK)
				return;

			if (string.IsNullOrEmpty(tasksForm.TextBox1.Text) || string.IsNullOrWhiteSpace(tasksForm.TextBox1.Text))
			{
				MessageBox.Show(@"It is necessary to fill the field correctly: " + tasksForm.Label1.Text);
				return;
			}
			if (string.IsNullOrEmpty(tasksForm.textBox3.Text) || string.IsNullOrWhiteSpace(tasksForm.textBox3.Text))
			{
				MessageBox.Show(@"It is necessary to fill the field correctly: " + tasksForm.label3.Text);
				return;
			}


			var fields = new Dictionary<string, string>
			{
				{ "name", tasksForm.TextBox1.Text},
				{ "description", tasksForm.richTextBox1.Text},
				{ "complexity1", tasksForm.textBox2.Text},
				{ "complexity2", tasksForm.textBox3.Text},
				{ "complexity3", tasksForm.textBox4.Text},
				{ "complexity4", tasksForm.textBox5.Text},
				{ "dataOfDelivery", tasksForm.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")},
			};

			int taskManager = UtilityController.GetId(
					$"Select id from Employees where Login = '{_form.User.Username}' AND Password = '{_form.User.Password}'", 
					_form.Connection),
				priority = UtilityController.GetId($"Select id from Priority where Name = '{tasksForm.comboBox1.Text}'", _form.Connection);

			AddNewTask(fields, taskManager, priority);
			RefreshTaskTree();
		}

		public void AssignTask_Click()
		{
			var taskId = int.Parse(_form.TreeView1.SelectedNode.Tag.ToString());
			var assignmentForm = new AddEditTaskAssignment();

			if (assignmentForm.ShowDialog() != DialogResult.OK)
			{ return; }

			if (string.IsNullOrEmpty(assignmentForm.comboBox1.Text) || string.IsNullOrWhiteSpace(assignmentForm.comboBox1.Text))
			{
				MessageBox.Show(@"Need to choose an employee");
				return;
			}

			var employeeId = UtilityController.GetId($"SELECT id FROM Employees WHERE FIO  = '{assignmentForm.comboBox1.Text}'", _form.Connection);
			string dataStart = DateTime.Now.ToString("yyyy-MM-dd"),
				dataTimeStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				comment = assignmentForm.textBox1.Text;

			var query = "INSERT into Results " +
								"(Result_Qual1, Result_Qual2, Result_Qual3, Result_Qual4) " +
								"VALUES (0, 0, 0, 0); " +
							"INSERT into AssignedTasks " +
								"(id_Task, id_Employee, Date_Start, id_Result, Comment) " +
								$"SELECT {taskId}, {employeeId}, '{dataStart}', id, '{comment}' " +
									"FROM Results " +
									"WHERE id = (SELECT max(id) FROM Results);" +
							"INSERT into EventLog" +
								"(Date, id_LastStatus, id_CurrentStatus, id_Employee, id_Task) " +
								$"VALUES ('{dataTimeStart}', {(int) Status.Created}, {(int)Status.Assigned}, {employeeId}, {taskId} )";

			_form.Connection.ExecNonQuery(query);
		}

		public void ShowSummaryProgress_Click()
		{
			if (_form.TreeView1.SelectedNode == null)
				return;

			try
			{
				AssignmentTask.IdTask = int.Parse(_form.TreeView1.SelectedNode.Tag.ToString());
			}
			catch
			{
				MessageBox.Show(
					"Сначала выберите задание по которому хотите посмотреть прогресс выполнения",
					@"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1);

				return;
			}

			var form = new ImplementationProgress();
			form.Show();
		}

		#endregion

		/// <summary>
		/// Refresh Task Tree
		/// </summary>
		public void RefreshTaskTree()
		{
			_form.TreeView1.Nodes.Clear();
			var dtTt = _form.Connection.GetDataAdapter("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_ParentTask IS NULL");

			foreach (DataRow row in dtTt.Rows)
			{
				string currentRowId = row["id"].ToString(),
					currentRowName = row["Name"].ToString();

				var node = new TreeNode(currentRowName) { Tag = currentRowId };

				_form.TreeView1.Nodes.Add(node);
			}

			foreach (TreeNode node in _form.TreeView1.Nodes)
			{
				_form.TreeView1.SelectedNode = node;
			}

			_form.TreeView1.ExpandAll();
		}

		private void AddNewTask(IReadOnlyDictionary<string, string> fields, int taskManager, int priority)
		{
			string complexity1 = fields["complexity1"],
				complexity2 = fields["complexity2"],
				complexity3 = fields["complexity3"],
				complexity4 = fields["complexity4"],
				name = fields["name"],
				description = fields["description"],
				dataOfDelivery = fields["dataOfDelivery"];

			var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var userId = UtilityController.GetId(
				$"SELECT id FROM Employees WHERE Login = '{_form.User.Username}' AND Password = '{_form.User.Password}'",
				_form.Connection);

			var tag = _form.TreeView1.SelectedNode?.Tag.ToString();
			var isSelectTask = _form.TreeView1.SelectedNode != null;

			string parentIdField = isSelectTask ? "id_ParentTask," : "",
				parentIdValue = isSelectTask ? $"{tag}," : "";

			var query = "INSERT into Complexity" +
								"(Complexity_Qual1, Complexity_Qual2, Complexity_Qual3, Complexity_Qual4) " +
								$"VALUES({complexity1},{complexity2},{complexity3},{complexity4}); " +
							"INSERT into Tasks " +
								$"({parentIdField}Name, Description, id_Complexity, Date_Delivery, id_TaskManager, id_Priority) " +
								$"SELECT {parentIdValue}'{name}','{description}', id, '{dataOfDelivery}', {taskManager}, {priority} " +
									"FROM Complexity WHERE id = (SELECT max(id) FROM Complexity); " +
							"INSERT into EventLog" +
								"(Date, id_LastStatus, id_Employee, id_Task)" +
								$"SELECT '{currentDate}', {(int) Status.Created}, {userId}, id " +
									$"FROM Tasks WHERE Name = '{name}'";

			_form.Connection.ExecNonQuery(query);
		}
	}
}
