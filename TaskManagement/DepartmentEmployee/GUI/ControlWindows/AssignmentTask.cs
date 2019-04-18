using System;
using Core.Model;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DepartmentEmployee.Model;
using Core.Database.Connection;
using System.Collections.Generic;
using DepartmentEmployee.Context;
using DepartmentEmployee.Controllers;
using DepartmentEmployee.GUI.ModalWindows;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class AssignmentTask : Form
	{
		private readonly AssignmentTaskModel _model;

		public Connection Connection { get; set; }
		public User User { get; set; }

		public static int IdTask;

		public AssignmentTask()
		{
			InitializeComponent();

			Connection = Connection.CreateConnection();
			User = CustomContext.GetInstance().CurrentUser;
			_model = new AssignmentTaskModel(this);

			_model.RefreshTaskTree();
		}

		//Функционал, обрабатывающий событие, что при выборе элемента, в нем мы снова перебираем Datatable в поисках элементов parentID которых соответвует TagID пункта меню от которого сработало это событие
		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{

			//int TaskManager = UtilityController.GetId(String.Format("Select id from Employees where Login = '" + _user.Username + "' AND Password = '" + _user.Password + "'"));

			//Получаем datatable
			//DataTable dt_tt = connection.GetDataAdapter("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_TaskManager = '" + TaskManager + "' AND id_ParentTask = '" + e.Node.Tag.ToString() + "'");
			DataTable dtTt = Connection.GetDataAdapter("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_ParentTask = '" + e.Node.Tag + "'");

			// Перебираем строки в таблице datatable
			foreach (DataRow row in dtTt.Rows) {
				// объявляем переменные с ячейками текущей перебираемой строки

				string currentRowId = row["id"].ToString(),
					currentRowName = row["Name"].ToString();

				// Объявляем триггер который с помощью которого будем проверять не добавлены ли УЖЕ добавляемые элементы в меню при прошлом клике на него
				bool alreadyAdded = false;

				// Если parentid в текущей строке datatable = Tag текущего элемента от которого сработало событие                

				// Проверяем что уже не добавили эти элемнеты меню при прошлых кликах на пункте меню
				// Перебираем дочерние элементы текушего пункта меню от которого сработало событие
				foreach (TreeNode node in TreeView1.SelectedNode.Nodes)
				{
					// Если хотя бы у одного элемента Tag = CurrentRowID значит мы уже обрабатывали этот пункт меню ранее, следовательно включаем триггер
					if (node.Tag.ToString() == currentRowId)
					{
						alreadyAdded = true;
					}
				}
				// Если триггер того что меню было обработано ранее всё еще равен = false
				if (alreadyAdded == false) {
					//Создаем объект Treenode с именем из текущей строчки перебираемой datatable
					var node = new TreeNode(currentRowName);
					//Присваиваем ему Tag  из текущей строчки перебираемой datatable
					node.Tag = currentRowId;
					//Добавляем его к выбраному пункту меню от которого сработало это событие.
					TreeView1.SelectedNode.Nodes.Add(node);
				}
			}
			// Развернем выделеную ноду
			TreeView1.SelectedNode.Expand();

			RefreshGrid();
		}

		//Функционал редактирования выбранного задания
		private async void EditCurrentTask(TreeNode node)
		{
			var id = int.Parse(node.Tag.ToString());

			//Получаем значение столбцов для выбранного задания
			var table = await Connection.GetDataAdapterAsync(
				"select Tasks.id as id, Tasks.Name as Name, Tasks.Description as Description, " +
				"Tasks.id_Complexity as Complexity, Tasks.Date_Delivery as Date_Delivery, Priority.Name as Priority " +
				$"from Tasks join Priority on Priority.id = Tasks.id_Priority where Tasks.id = {id}");
			var lastRow = table.Rows[0];

			//Задаем переменные для столбцов этой строки
			string description = lastRow["Description"].ToString(),
				priority = lastRow["Priority"].ToString();
			var dataOfDelivery = DateTime.Parse(lastRow["Date_Delivery"].ToString());


			var idOrigComplexity = int.Parse(lastRow["Complexity"].ToString());
			var table2 = await Connection.GetDataAdapterAsync("select Complexity_Qual1 as Complexity1, Complexity_Qual2 as Complexity2, Complexity_Qual3 as Complexity3, Complexity_Qual4 as Complexity4 from Complexity where id = '" + idOrigComplexity + "'");
			var lastRow2 = table2.Rows[0];

			string complexity1 = lastRow2["Complexity1"].ToString(),
				complexity2 = lastRow2["Complexity2"].ToString(),
				complexity3 = lastRow2["Complexity3"].ToString(),
				complexity4 = lastRow2["Complexity4"].ToString();

			var tasksForm = new AddEditTask
			{
				TextBox1 = {Text = node.Text},
				richTextBox1 = {Text = description},
				textBox2 = {Text = complexity1},
				textBox3 = {Text = complexity2},
				textBox4 = {Text = complexity3},
				textBox5 = {Text = complexity4},
				comboBox1 = {Text = priority},
				dateTimePicker1 = {Value = dataOfDelivery}
			};


			if (tasksForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			if (string.IsNullOrEmpty(tasksForm.TextBox1.Text) || string.IsNullOrWhiteSpace(tasksForm.TextBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + tasksForm.Label1.Text);
				return;
			}

			if (string.IsNullOrEmpty(tasksForm.textBox3.Text) || string.IsNullOrWhiteSpace(tasksForm.textBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + tasksForm.label3.Text);
			}
			else
			{
				var newName = tasksForm.TextBox1.Text.Replace("'", "''");
				description = tasksForm.richTextBox1.Text.Replace("'", "''");
				complexity1 = tasksForm.textBox2.Text.Replace("'", "''");
				complexity2 = tasksForm.textBox3.Text.Replace("'", "''");
				complexity3 = tasksForm.textBox4.Text.Replace("'", "''");
				complexity4 = tasksForm.textBox5.Text.Replace("'", "''");
				dataOfDelivery = tasksForm.dateTimePicker1.Value.Date;

				var priorityId = UtilityController.GetId($"Select id from Priority where Name = '{tasksForm.comboBox1.Text}'", Connection);

				await Connection.ExecNonQueryAsync("UPDATE Complexity set Complexity_Qual1 = '" + complexity1 + "', Complexity_Qual2 = '" + complexity2 + "', Complexity_Qual3 = '" + complexity3 + "', Complexity_Qual4 = '" + complexity4 + "' where id = '" + idOrigComplexity + "'");
				await Connection.ExecNonQueryAsync("UPDATE Tasks set Name ='" + newName + "', Description = '" + description + "', id_Complexity = '" + idOrigComplexity + "', Date_Delivery = '" + dataOfDelivery + "', id_Priority = '" + priorityId + "' WHERE id = '" + id + "'");
				

				node.Text = newName;
			}
		}

		//Кнопка редактирования задания
		private void EditTaskButton_Click(object sender, EventArgs e)
		{
			//Если не выбран элемент который мы собираемся удалять выходим
			if (TreeView1.SelectedNode == null)
			{
				return;
			}

			//Запускаем функцию редактирования папки и передаем её текущую ноду которую будем редактировать
			EditCurrentTask(TreeView1.SelectedNode);
		}

		/// <summary>
		/// Add new task or subTask
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddNewTaskButton_Click(object sender, EventArgs e) => _model.AddNewTaskButton_Click();
		

		//Функционал удаления выбранного задания
		private async void RemoveTaskButton_Click(object sender, EventArgs e)
		{
			//Если не выбран элемент который мы собираемся удалять выходим
			if (TreeView1.SelectedNode == null)
			{
				MessageBox.Show("Сначала выберите задание.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var currentTaskId = TreeView1.SelectedNode.Tag.ToString();

			//Если внутри выбраного элемента содержаться другие элементы выводим уведомление и выходим из процедуры
			if (TreeView1.SelectedNode.Nodes.Count != 0)
			{
				MessageBox.Show("Нельзя удалить задание, поскольку он содержит подзадания", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Проверим существуют ли сотрудники, привязанные к данной таблице
			var dtTtStudent = await Connection.GetDataAdapterAsync("select ID from AssignedTasks where id_Task = '" + currentTaskId + "'");
			if (dtTtStudent.Rows.Count > 0)
			{
				MessageBox.Show("Нельзя удалить задание, поскольку есть студенты привязанные к данному заданию", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Получаем DataTable
			//сразу в запросе находим нужные строчки и добавляем в datatable посути одну нужнную строку

			var dtTt = await Connection.GetDataAdapterAsync("SELECT id FROM Tasks WHERE ID = '" + currentTaskId + "'");            
			// Перебираем все строчки в таблице

			foreach (DataRow row in dtTt.Rows)
			{
				// объявляем переменную которая с ID шникном текущей строчки в таблице
				var rowId = row["id"].ToString();
				
				//Если этот ID равен текущему Tag выбраного элемента, то удаляем эту строчку из базы

				if (rowId == TreeView1.SelectedNode.Tag.ToString())
				{
					await Connection.ExecNonQueryAsync("DELETE FROM Tasks WHERE ID = '" + rowId + "'");
				}

			}
			// А потом удаляем эту ноду из Treeview
			TreeView1.SelectedNode.Remove();
		}

		//Функционал для обновления таблицы с назначенными задания по ID задания
		private async void RefreshGrid()
		{
			try
			{
				var currentTaskId = TreeView1.SelectedNode.Tag.ToString();
				var dt = await Connection.GetDataAdapterAsync("select AssignedTasks.id as ID, Tasks.Name as Task, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи,  Results.id as Результат, AssignedTasks.Comment as Comment from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result WHERE id_Task = '" + currentTaskId + "'");

				DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView
			}
			catch
			{

				var dt = await Connection.GetDataAdapterAsync("select AssignedTasks.id as ID, Tasks.Name as Задание, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи, Results.id as Результат, AssignedTasks.Comment as Comment from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result");
				DataGridView1.DataSource = dt;
			}

			try
			{
				DataGridView1.SetVisible(false, "ID", "Task", "Comment");
				DataGridView1.ChangeHeader(new Dictionary<string, string>()
				{
					{"Employee", "Сотрудник"},
					{"Дата_выдачи", "Дата назначения"},
					{"Date_Delivery", "Планируемая дата закрытия"},
					{"Дата_сдачи", "Фактическая дата закрытия"},
					{"Результат", "Результат"}
				});

				if(DataGridView1.Columns["Employee"] != null)
					DataGridView1.Columns["Employee"].Width = 200;

				DataGridView1.CurrentCell = DataGridView1.Rows[0].Cells[0];
			}
			catch
			{
				// ignored
			}
		}

		/// <summary>
		/// Add new Assignment for Task
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AssignTask_Click(object sender, EventArgs e)
		{
			_model.AssignTask_Click();
			RefreshGrid();
		}

		//Функционал редактирования назначения
		private async void Button5_Click(object sender, EventArgs e)
		{
			
			var id = 0;

			try
			{
				if (DataGridView1.CurrentRow != null)
					id = int.Parse(DataGridView1.CurrentRow.Cells["ID"].Value.ToString());
			}
			catch
			{
				MessageBox.Show("Сначала выберите назначение, которое хотите отредактировать", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var assignmentForm = new AddEditTaskAssignment();

			//Заполняем в assignment_form поля для того чтобы было что редактировать.
			var taskId = int.Parse(TreeView1.SelectedNode.Tag.ToString());
			assignmentForm.comboBox1.Text = DataGridView1.CurrentRow?.Cells["Employee"].Value.ToString();
			assignmentForm.textBox1.Text = DataGridView1.CurrentRow?.Cells["Comment"].Value.ToString();

			if (assignmentForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			if (string.IsNullOrEmpty(assignmentForm.comboBox1.Text) || string.IsNullOrWhiteSpace(assignmentForm.comboBox1.Text))
			{
				MessageBox.Show("Нужно выбрать сотрудника");
				return;
			}

			var employeeId = UtilityController.GetId($"Select id from Employees where FIO = '{assignmentForm.comboBox1.Text}'", Connection);
			var comment = assignmentForm.textBox1.Text;

			await Connection.ExecNonQueryAsync("UPDATE AssignedTasks set id_Task ='" + taskId + "', id_Employee = '" + employeeId + "', Comment = '" + comment + "' where id = '" + id + "'");
			

			RefreshGrid();
			
		}

		//Функционал удаления назначения
		private async void Button6_Click(object sender, EventArgs e)
		{
			var id = 0;

			try
			{
				if (DataGridView1.CurrentRow != null)
					id = int.Parse(DataGridView1.CurrentRow.Cells["ID"].Value.ToString());
			}
			catch
			{
				MessageBox.Show("Сначала выберите студента", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Удаляем из базы
			if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить студента: " + DataGridView1.CurrentRow?.Cells["ФИО_студента"].Value  + "?", @"Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
			{
				await Connection.ExecNonQueryAsync("DELETE FROM AssignedTasks where id = '" + id + "'");
			}

			//Удаляем из DataGridView
			if (DataGridView1.CurrentRow != null) DataGridView1.Rows.Remove(DataGridView1.CurrentRow);
		}

		// Функционал для просмотра общего прогресса выполнения задания
		private void button7_Click(object sender, EventArgs e)
		{

			//Если не выбран элемент который мы собираемся удалять выходим
			if (TreeView1.SelectedNode == null)
			{
				return;
			}

			//Запускаем функцию редактирования папки и передаем её текущую ноду которую будем редактировать
			DetailedResults(TreeView1.SelectedNode);
		}

		private void DetailedResults(TreeNode node)
		{

			try
			{
				IdTask = int.Parse(node.Tag.ToString());
			}
			   
			catch
			{
				MessageBox.Show("Сначала выберите задание по которому хотите посмотреть прогресс выполнения", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var form = new ImplementationProgress();
		}


		#region Helper Methods

		/// <summary>
		/// Custom render TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			SolidBrush nodeBrush = new SolidBrush(Color.White),
				selectedNodeBrush = new SolidBrush(Color.FromArgb(185, 209, 234));
			var textForeColor = e.Node.ForeColor;

			if (e.Node.IsSelected)
			{
				if (TreeView1.Focused)
				{
					e.Graphics.FillRectangle(selectedNodeBrush, e.Bounds);
				}
				TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, textForeColor);
			}
			else
			{
				e.Graphics.FillRectangle(nodeBrush, e.Bounds);

				TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, textForeColor);
			}

		}
		
		/// <summary>
		/// Drag_Drop mechanism for TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void TreeView1_DragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("System.String", false)) return;

			var treeView = (TreeView)sender;
			var pt = treeView.PointToClient(new Point(e.X, e.Y));
			var destinationNode = treeView.GetNodeAt(pt);

			if (destinationNode == null)
			{
				return;
			}

			await Connection.ExecNonQueryAsync("UPDATE AssignedTasks set id_Task='" + destinationNode.Tag + "' where ID = '" + e.Data.GetData("System.String") + "'");

			TreeView1.SelectedNode = destinationNode;
		}

		/// <summary>
		/// Drag_Drop mechanism for DataGridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;

			try
			{
				if (DataGridView1.CurrentRow == null) return;
				var draggedItemId = DataGridView1.CurrentRow.Cells["id"].Value.ToString();
				DataGridView1.DoDragDrop(draggedItemId, DragDropEffects.Move);
			}
			catch
			{
				// ignored
			}
		}

		private void TreeView1_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;
		private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Button5_Click(sender, e);
		private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
			}

		}
		private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) => EditCurrentTask(e.Node);
		private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
			{
				Button5_Click(sender, e);
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
		private void TreeView1_MouseUp(object sender, MouseEventArgs e)
		{
			if (TreeView1.GetNodeAt(e.X, e.Y) == null)
			{
				TreeView1.SelectedNode = null;
			}
		}

		#endregion
	}
}
