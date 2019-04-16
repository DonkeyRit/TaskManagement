using System;
using System.Collections.Generic;
using Core.Model;
using System.Data;
using System.Drawing;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;
using DepartmentEmployee.Context;
using DepartmentEmployee.Controllers;
using DepartmentEmployee.GUI.ModalWindows;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class AssignmentTask : Form
	{
		private readonly Connection _connection;
		private readonly User _user;
		public static int IdTask;

		public AssignmentTask()
		{
			InitializeComponent();

			_user = CustomContext.GetInstance().CurrentUser;
			_connection = Connection.CreateConnection();

			RefreshDeparts(); // Обновляем список заданий.
		}


		/// <summary>
		/// Работа с деревом заданий
		/// Функционал для обновления дерева со списком заданий
		/// </summary>
		private async void RefreshDeparts()
		{
			//Очищаем treeview на случай если там что-то есть
			TreeView1.Nodes.Clear();

			//Этим этапом (всей этой функцией рефреша) мы подгружаем только корневые ноды. Тоесть те у которых ParentID = null, остальные (внутренние) мы подгружаем с помощью обработки события выделения ноды TreeView1_AfterSelect.

			//Получаем datatable из соответвующей функции - получаем только корневые папки - где парект ID = 0
			var dtTt = await _connection.GetDataAdapterAsync("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_ParentTask IS NULL");

			// для каждого элемента в datatable
			foreach (DataRow row in dtTt.Rows)
			{
				//объявляем переменные с ячейками текущей перебираемой строки
				string currentRowId = row["id"].ToString(),
					currentRowName = row["Name"].ToString();

				var node = new TreeNode(currentRowName);
				//Присваиваем Tag этому элементу равный текущему ID                    
				node.Tag = currentRowId;
				//Добавляем его в treeview
				TreeView1.Nodes.Add(node);
			}

			// Пройдемся по всем корневым нодам выделением, чтобы сработало событие выделения ноды и подгрузились дочерние элементы
			foreach (TreeNode node in TreeView1.Nodes)
			{
				TreeView1.SelectedNode = node;
			}
			//Раскроем все свернутые ноды
			TreeView1.ExpandAll();
			//=================Конец Обновление корня departments=================
		}

		//Функционал, обрабатывающий событие, что при выборе элемента, в нем мы снова перебираем Datatable в поисках элементов parentID которых соответвует TagID пункта меню от которого сработало это событие
		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{

			//int TaskManager = GetId(String.Format("Select id from Employees where Login = '" + _user.Username + "' AND Password = '" + _user.Password + "'"));

			//Получаем datatable
			//DataTable dt_tt = connection.GetDataAdapter("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_TaskManager = '" + TaskManager + "' AND id_ParentTask = '" + e.Node.Tag.ToString() + "'");
			DataTable dtTt = _connection.GetDataAdapter("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_ParentTask = '" + e.Node.Tag + "'");

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

			int id = int.Parse(node.Tag.ToString());

			//Получаем значение столбцов для выбранного задания
			var table = await _connection.GetDataAdapterAsync("select Tasks.id as id, Tasks.Name as Name, Tasks.Description as Description, Tasks.id_Complexity as Complexity, Tasks.Date_Delivery as Date_Delivery, Priority.Name as Priority from Tasks join Priority on Priority.id = Tasks.id_Priority where Tasks.id = '" + id + "'");
			var lastRow = table.Rows[0];

			//Задаем переменные для столбцов этой строки
			string description = lastRow["Description"].ToString(),
				priority = lastRow["Priority"].ToString();
			DateTime dataOfDelivery = DateTime.Parse(lastRow["Date_Delivery"].ToString());


			var idOrigComplexity = int.Parse(lastRow["Complexity"].ToString());
			var table2 = await _connection.GetDataAdapterAsync("select Complexity_Qual1 as Complexity1, Complexity_Qual2 as Complexity2, Complexity_Qual3 as Complexity3, Complexity_Qual4 as Complexity4 from Complexity where id = '" + idOrigComplexity + "'");
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

				var priorityId = GetId($"Select id from Priority where Name = '{tasksForm.comboBox1.Text}'");

				var sqlResultComplexity = await _connection.ExecNonQueryAsync("UPDATE Complexity set Complexity_Qual1 = '" + complexity1 + "', Complexity_Qual2 = '" + complexity2 + "', Complexity_Qual3 = '" + complexity3 + "', Complexity_Qual4 = '" + complexity4 + "' where id = '" + idOrigComplexity + "'");
				CheckSqlResults(sqlResultComplexity);

				var sqlResult = await _connection.ExecNonQueryAsync("UPDATE Tasks set Name ='" + newName + "', Description = '" + description + "', id_Complexity = '" + idOrigComplexity + "', Date_Delivery = '" + dataOfDelivery + "', id_Priority = '" + priorityId + "' WHERE id = '" + id + "'");
				CheckSqlResults(sqlResult);

				node.Text = newName;
			}
		}

		//Кнопка редактирования задания
		private void Button2_Click(object sender, EventArgs e)
		{
			//Если не выбран элемент который мы собираемся удалять выходим
			if (TreeView1.SelectedNode == null)
			{
				return;
			}

			//Запускаем функцию редактирования папки и передаем её текущую ноду которую будем редактировать
			EditCurrentTask(TreeView1.SelectedNode);
		}

		//Функционал добавления нового задания/подзадания
		private async void Button1_Click(object sender, EventArgs e)
		{
			var tasksForm = new AddEditTask();
			if (tasksForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			var dtTtT = await _connection.GetDataAdapterAsync("select id from Tasks order by id Desc limit 1");
			var lastRowW = dtTtT.Rows[0];

			// Задаем переменные для столбцов этой строчки
			var id = int.Parse(lastRowW["id"].ToString());
			id++;

			if (string.IsNullOrEmpty(tasksForm.TextBox1.Text) || string.IsNullOrWhiteSpace(tasksForm.TextBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + tasksForm.Label1.Text);
				return;
			}
			if (string.IsNullOrEmpty(tasksForm.textBox3.Text) || string.IsNullOrWhiteSpace(tasksForm.textBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + tasksForm.label3.Text);
				return;
			}

			string name = tasksForm.TextBox1.Text.Replace("'", "''"),
				description = tasksForm.richTextBox1.Text.Replace("'", "''"),
				complexity1 = tasksForm.textBox2.Text.Replace("'", "''"),
				complexity2 = tasksForm.textBox3.Text.Replace("'", "''"),
				complexity3 = tasksForm.textBox4.Text.Replace("'", "''"),
				complexity4 = tasksForm.textBox5.Text.Replace("'", "''");
			var dataOfDelivery = tasksForm.dateTimePicker1.Value.Date;
			int taskManager = GetId("Select id from Employees where Login = '" + _user.Username + "' AND Password = '" + _user.Password + "'"),
				priority = GetId($"Select id from Priority where Name = '{tasksForm.comboBox1.Text}'");


			if (TreeView1.SelectedNode == null)
			{
				var sqlResultComplexity = await _connection.ExecNonQueryAsync("INSERT into Complexity(Complexity_Qual1, Complexity_Qual2, Complexity_Qual3, Complexity_Qual4) VALUES('" + complexity1 + "','" + complexity2 + "', '" + complexity3 + "', '" + complexity4 + "')");
				var complexityId = GetId(String.Format("SELECT id FROM Complexity ORDER BY id DESC LIMIT 1"));
				var sqlResult = await _connection.ExecNonQueryAsync("INSERT into Tasks(id, Name, Description, id_Complexity, Date_Delivery, id_TaskManager, id_Priority) VALUES('" + id + "', '" + name + "','" + description + "', '" + complexityId + "', '" + dataOfDelivery + "', '" + taskManager + "','" + priority + "')");

				CheckSqlResults(sqlResultComplexity);
				CheckSqlResults(sqlResult);
			}

			else
			{

				//Записываем в переменную Tag, Tag текущего выбраного элемента (это его ID в базе, которое мы будем использовать чтобы задать родителя нашего нового добавляемого элемента)
				var tag = TreeView1.SelectedNode.Tag.ToString();

				//Записываем в базу новую строчку, задав поля имя и parent ID
				var sqlResultComplexity = await _connection.ExecNonQueryAsync("INSERT into Complexity(Complexity_Qual1, Complexity_Qual2, Complexity_Qual3, Complexity_Qual4) VALUES('" + complexity1 + "','" + complexity2 + "', '" + complexity3 + "', '" + complexity4 + "')");
				var complexityId = GetId("SELECT id FROM Complexity ORDER BY id DESC LIMIT 1");
				//Записываем в базу новую строчку, задав поля имя и parent ID
				var sqlResult = await _connection.ExecNonQueryAsync("INSERT into Tasks(id, Name, id_ParentTask, Description, id_Complexity, Date_Delivery, id_TaskManager, id_Priority) VALUES('" + id + "', '" + name + "','" + tag + "', '" + description + "', '" + complexityId + "', '" + dataOfDelivery + "', '" + taskManager + "','" + priority + "')");

				CheckSqlResults(sqlResultComplexity);
				CheckSqlResults(sqlResult);

			}


			//Получаем данные обратно из базы
			//Получаем datatable из функции 
			//сразу выборка из таблицы - получаем последнюю строчку - она и есть та которую мы только что добавили
			var dtTt = await _connection.GetDataAdapterAsync("SELECT id, Name, id_ParentTask FROM Tasks WHERE id = '" + id + "'");

			//Находим последнюю запись в таблице, - она и есть та которую мы только что добавили
			//Получаем индекс последней записи в нашем массиве
			var lastRowIndex = dtTt.Rows.Count - 1;

			// Получаем последнюю строчку по этому индексу
			var lastRow = dtTt.Rows[lastRowIndex];

			// Задаем переменные для столбцов этой строчки
			string lastRowId = lastRow["id"].ToString(),
				lastRowName = lastRow["Name"].ToString();

			//Тут будем добавлять это в наш treeview
			var node = new TreeNode(lastRowName) {Tag = lastRowId}; //созадем объект node с именем из базы
			//присваиваем ему tag, который соответвует его ID в базе

			//Если ниче не выделено то добавляем ноду в корень treeview
			if (TreeView1.SelectedNode == null)
			{
				TreeView1.Nodes.Add(node);
			// А если выделено то добавляем в то место которое выделено 
			 }
			else
			{
				TreeView1.SelectedNode.Nodes.Add(node);
				TreeView1.SelectedNode.Expand(); // И разворачиваем 
			}
		}

		//Функционал удаления выбранного задания
		private async void Button3_Click(object sender, EventArgs e)
		{
			//Если не выбран элемент который мы собираемся удалять выходим
			if (TreeView1.SelectedNode == null)
			{
				MessageBox.Show("Сначала выберите задание.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var currentTaskId = TreeView1.SelectedNode.Tag.ToString();

			//Если внутри выбраного элемента содержаться другие элементы выводим уведомление и выходим из процедуры
			if (TreeView1.SelectedNode.Nodes.Count != 0)
			{
				MessageBox.Show("Нельзя удалить задание, поскольку он содержит подзадания", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Проверим существуют ли сотрудники, привязанные к данной таблице
			var dtTtStudent = await _connection.GetDataAdapterAsync("select ID from AssignedTasks where id_Task = '" + currentTaskId + "'");
			if (dtTtStudent.Rows.Count > 0)
			{
				MessageBox.Show("Нельзя удалить задание, поскольку есть студенты привязанные к данному заданию", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Получаем DataTable
			//сразу в запросе находим нужные строчки и добавляем в datatable посути одну нужнную строку

			var dtTt = await _connection.GetDataAdapterAsync("SELECT id FROM Tasks WHERE ID = '" + currentTaskId + "'");            
			// Перебираем все строчки в таблице

			foreach (DataRow row in dtTt.Rows)
			{
				// объявляем переменную которая с ID шникном текущей строчки в таблице
				var rowId = row["id"].ToString();
				
				//Если этот ID равен текущему Tag выбраного элемента, то удаляем эту строчку из базы

				if (rowId == TreeView1.SelectedNode.Tag.ToString())
				{
					var sqlResult = await _connection.ExecNonQueryAsync("DELETE FROM Tasks WHERE ID = '" + rowId + "'");
					CheckSqlResults(sqlResult);
				}

			}
			// А потом удаляем эту ноду из Treeview
			TreeView1.SelectedNode.Remove();
		}

		/// <summary>
		/// Работа со списком назначений заданий
		/// </summary>

		//Функционал для обновления таблицы с назначенными задания по ID задания
		private async void RefreshGrid()
		{
			try
			{
				var currentTaskId = TreeView1.SelectedNode.Tag.ToString();
				var dt = await _connection.GetDataAdapterAsync("select AssignedTasks.id as ID, Tasks.Name as Task, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи,  Results.id as Результат, AssignedTasks.Comment as Comment from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result WHERE id_Task = '" + currentTaskId + "'");

				DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView
			}
			catch
			{

				var dt = await _connection.GetDataAdapterAsync("select AssignedTasks.id as ID, Tasks.Name as Задание, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи, Results.id as Результат, AssignedTasks.Comment as Comment from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result");
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

		//Функционал добавления нового назначения задания студенту
		private async void Button4_Click(object sender, EventArgs e)
		{

			var taskId = int.Parse(TreeView1.SelectedNode.Tag.ToString());

			//AddEditTaskAssignment assignment_form = new AddEditTaskAssignment(TaskID);
			var assignmentForm = new AddEditTaskAssignment();

			if (assignmentForm.ShowDialog() != DialogResult.OK)
			{ return; }

			if (string.IsNullOrEmpty(assignmentForm.comboBox1.Text) || string.IsNullOrWhiteSpace(assignmentForm.comboBox1.Text))
			{
				MessageBox.Show("Нужно выбрать сотрудника");
				return;
			}

			var employeeId = GetId($"Select id from Employees where FIO  = '{assignmentForm.comboBox1.Text}'");
			var dataStart = DateTime.Now;
			var comment = assignmentForm.textBox1.Text.Replace("'", "''");

			//записываем данные из текстбоксов Form3 в наши переменные
			// А потом экранируем кавычечку
			var sqlRes = await _connection.ExecNonQueryAsync("INSERT into Results(Result_Qual1,Result_Qual2,Result_Qual3,Result_Qual4) values(0,0,0,0)");
			var resultId = GetId("Select id from Results ORDER BY id DESC LIMIT 1");
			var sqlResult = await _connection.ExecNonQueryAsync("INSERT into AssignedTasks(id_Task, id_Employee, Date_Start, id_Result, Comment) values('" + taskId + "', '" + employeeId + "', '" + dataStart + "', '" + resultId + "', '" + comment + "')");

			CheckSqlResults(sqlResult);
			CheckSqlResults(sqlRes);

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
				MessageBox.Show("Сначала выберите назначение, которое хотите отредактировать", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

			var employeeId = GetId($"Select id from Employees where FIO = '{assignmentForm.comboBox1.Text}'");
			var comment = assignmentForm.textBox1.Text;

			bool sqlResult = await _connection.ExecNonQueryAsync("UPDATE AssignedTasks set id_Task ='" + taskId + "', id_Employee = '" + employeeId + "', Comment = '" + comment + "' where id = '" + id + "'");
			CheckSqlResults(sqlResult);

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
				MessageBox.Show("Сначала выберите студента", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Удаляем из базы
			if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить студента: " + DataGridView1.CurrentRow.Cells["ФИО_студента"].Value  + "?", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
			{
				var sqlResult = await _connection.ExecNonQueryAsync("DELETE FROM AssignedTasks where id = '" + id + "'");
				CheckSqlResults(sqlResult);
			}

			//Удаляем из DataGridView
			DataGridView1.Rows.Remove(DataGridView1.CurrentRow); 

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
				MessageBox.Show("Сначала выберите задание по которому хотите посмотреть прогресс выполнения", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			var form = new ImplementationProgress();
		}


		/// <summary>
		/// Вспомогательные методы
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		//Функционал перехвата отрисовки Node в Treeview1
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

		//Функционал обработки события перетаскиваниея
		private async void TreeView1_DragDrop(object sender, DragEventArgs e)
		{
			//Перетаскиваемый элемент - это ID типа string - проверим что это действительно он;
			if (!e.Data.GetDataPresent("System.String", false)) return;
			var treeView = (TreeView)sender;
			// находим координаты того места куда сбросили перетаскиваемый объект
			var pt = treeView.PointToClient(new Point(e.X, e.Y));
			// Находим ноду, которая находилась в этих координатах
			var destinationNode = treeView.GetNodeAt(pt);
			// Проверяем что там вообще есть нода в этом месте
			if (destinationNode == null)
			{
				return;
			}

			// Обновляем запись в таблице Empoyee - присваиваем полю DepartmentID - Tag нашей ноды
			bool sqlResult = await _connection.ExecNonQueryAsync("UPDATE AssignedTasks set id_Task='" + destinationNode.Tag + "' where ID = '" + e.Data.GetData("System.String") + "'");
			CheckSqlResults(sqlResult);

			TreeView1.SelectedNode = destinationNode; //Выбираем ноду, в которую перетащили объект
		}

		//Функионал для обработки DragAndDrop для перемещения сотрудников между отделами путем перетаскивания
		//Событие срабатывает при движении мышкой по области Datagridview. Если нажата левая кнопка мышки, то захватываем элемент для drag and drop
		private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
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
		}

		//Функционал обработки событие вхождения перетаскиваемого объекта в зону ThreeView
		private void TreeView1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		//Функционал обработки события даблклика по ячейке DatagridView
		private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			Button5_Click(sender, e);
		}

		//Функционал обработки нажатия клавиши Enter на Datagridview1 - Это отключение перехода на следующую строку при нажатии Enter.
		private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
			}

		}

		//Функционал обработки события даблклика по Threeview Node
		private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			EditCurrentTask(e.Node);
		}

		//Функционал обработки нажатия клавиши Enter на Datagridview1
		private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
			{
				Button5_Click(sender, e);
			}
		}

		//Функционал получения ID
		public int GetId(string query)
		{
			var table = _connection.GetDataAdapter(query);
			var id = table.GetColumnValuesDataTable(0, CellType.Integer);
			return int.Parse(id[0].ToString());
		}

		//Функционал для перехода к главному окну
		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newForm = new Mainform
			{
				ToolDataToolStripMenuItem = {Visible = false}, TaskEmployeeToolStripMenuItem = {Visible = false}
			};
			newForm.Show();
			Hide();
		}

		//Функционал для выхода из программы
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

		private void CheckSqlResults(bool value)
		{
			if(!value)
				ModalDialogController.Display("Exception in query.");
		}

		//Функционал для очищения Selected.Node, если нажимаем по пустому месту в TreeView1
		private void TreeView1_MouseUp(object sender, MouseEventArgs e)
		{
			if (TreeView1.GetNodeAt(e.X, e.Y) == null)
			{
				TreeView1.SelectedNode = null;
			}
		}

	}
}
