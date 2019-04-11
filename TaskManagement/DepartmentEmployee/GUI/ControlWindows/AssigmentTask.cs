﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DepartmentEmployee.Database.ConnectionDB;
using DepartmentEmployee.Database.ObjectReader;
using DepartmentEmployee.GUI.ModalWindows;

namespace DepartmentEmployee.GUI.ControlWindows
{
    public partial class AssigmentTask : Form
    {
        PostgreSQLConnectionParams postgresql_params = new PostgreSQLConnectionParams();
        DB db;

        string Login = Authorization.Login;
        string Password = Authorization.Password;

        public AssigmentTask()
        {
            InitializeComponent();

            //!!!!!!!!!!УЧЕТНЫЕ ДАННЫЕ PostgreSQL!!!!!!!!!!!!!!!
            postgresql_params.hostname = "127.0.0.1";        // Hostname
            postgresql_params.port = "5432";                 // Port
            postgresql_params.database = "TaskPerformance";  // Database
            postgresql_params.user = "postgres";             // Username
            postgresql_params.password = "123456";           // Password

            //!!!!!!!!!!УЧЕТНЫЕ ДАННЫЕ PostgreSQL!!!!!!!!!!!!!!!

            db = new DB(postgresql_params);

            RefreshDeparts(); // Обновляем список заданий.
        }


        /// <summary>
        /// Работа с деревом заданий
        /// </summary>
        
        //Функционал для обновления дерева со списком заданий
        private async void RefreshDeparts()
        {

            int TaskManager = GetId(String.Format("Select id from Employees where Login = '" + Login + "' AND Password = '" + Password + "'"));

            //Очищаем treeview на случай если там что-то есть
            TreeView1.Nodes.Clear();

            //Этим этапом (всей этой функцией рефреша) мы подгружаем только корневые ноды. Тоесть те у которых ParentID = null, остальные (внутренние) мы подгружаем с помощью обработки события выделения ноды TreeView1_AfterSelect.

            //Получаем datatable из соответвующей функции - получаем только корневые папки - где парект ID = 0
            DataTable dt_tt = await db.GetDatatableFromPostgreSQLAsync("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_TaskManager = '" + TaskManager + "' AND id_ParentTask IS NULL");
            
            // для каждого элемента в datatable
            foreach (DataRow row in dt_tt.Rows)
            {
                //объявляем переменные с ячейками текущей перебираемой строки
                string current_row_id = row["id"].ToString();
                string current_row__name = row["Name"].ToString();
                string current_row__parent_id = row["id_ParentTask"].ToString();
                
                TreeNode node = new TreeNode(current_row__name);
                //Присваиваем Tag этому элементу равный текущему ID                    
                node.Tag = current_row_id;
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

            int TaskManager = GetId(String.Format("Select id from Employees where Login = '" + Login + "' AND Password = '" + Password + "'"));

            //Получаем datatable
            DataTable dt_tt = db.GetDatatableFromPostgreSQL("SELECT id, Name, id_ParentTask FROM Tasks WHERE id_TaskManager = '" + TaskManager + "' AND id_ParentTask = '" + e.Node.Tag.ToString() + "'");

            // Перебираем строки в таблице datatable
            foreach (DataRow row in dt_tt.Rows) {
                // объявляем переменные с ячейками текущей перебираемой строки

                string current_row_id = row["id"].ToString();
                string current_row__name = row["Name"].ToString();

                // Объявляем триггер который с помощью которого будем проверять не добавлены ли УЖЕ добавляемые элементы в меню при прошлом клике на него
                bool already_added = false;

                // Если parentid в текущей строке datatable = Tag текущего элемента от которого сработало событие                

                // Проверяем что уже не добавили эти элемнеты меню при прошлых кликах на пункте меню
                // Перебираем дочерние элементы текушего пункта меню от которого сработало событие
                foreach (TreeNode mynode in TreeView1.SelectedNode.Nodes)
                {
                    // Если хотя бы у одного элемента Tag = CurrentRowID значит мы уже обрабатывали этот пункт меню ранее, следовательно включаем триггер
                    if (mynode.Tag.ToString() == current_row_id)
                    {
                        already_added = true;
                    }
                }
                // Если триггер того что меню было обработано ранее всё еще равен = false
                if (already_added == false) {
                    //Создаем объект Treenode с именем из текущей строчки перебираемой datatable
                    TreeNode node = new TreeNode(current_row__name);
                    //Присваиваем ему Tag  из текущей строчки перебираемой datatable
                    node.Tag = current_row_id;
                    //Добавляем его к выбраному пункту меню от которого сработало это событие.
                    TreeView1.SelectedNode.Nodes.Add(node);
                }
            }
            // Развернем выделеную ноду
            TreeView1.SelectedNode.Expand();

            refreshGrid();
        }

        //Функционал редактирования выбранного задания
        private async void EditCurrentTask(TreeNode node)
        {

            int ID = int.Parse(node.Tag.ToString());

            //Получаем значение столбцов для выбранного задания
            DataTable table = await db.GetDatatableFromPostgreSQLAsync("select Tasks.id as id, Tasks.Name as Name, Tasks.Description as Description, Tasks.Complexity as Complexity, Tasks.Date_Delivery as Date_Delivery, Priority.Name as Priority from Tasks join Priority on Priority.id = Tasks.id_Priority where Tasks.id = '" + ID + "'");
            DataRow lastrow = table.Rows[0];

            //Задаем переменные для столбцов этой строки
            string complexity = lastrow["Complexity"].ToString();
            string description = lastrow["Description"].ToString();
            string Priority = lastrow["Priority"].ToString();
            DateTime DataOfDelivery = DateTime.Parse(lastrow["Date_Delivery"].ToString());

            AddEditTask tasks_form = new AddEditTask();

            tasks_form.TextBox1.Text = node.Text;
            tasks_form.richTextBox1.Text = description;
            tasks_form.textBox3.Text = complexity;
            tasks_form.comboBox1.Text = Priority;
            tasks_form.dateTimePicker1.Value = DataOfDelivery;

            if (tasks_form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(tasks_form.TextBox1.Text) || string.IsNullOrWhiteSpace(tasks_form.TextBox1.Text))
            {
                MessageBox.Show("Нужно верно заполните поле: " + tasks_form.Label1.Text);
                return;
            }
            if (string.IsNullOrEmpty(tasks_form.textBox3.Text) || string.IsNullOrWhiteSpace(tasks_form.textBox3.Text))
            {
                MessageBox.Show("Нужно верно заполните поле: " + tasks_form.label2.Text);
                return;
            }

            else
            {
                string newname = tasks_form.TextBox1.Text.Replace("'", "''");
                description = tasks_form.richTextBox1.Text.Replace("'", "''");
                complexity = tasks_form.textBox3.Text.Replace("'", "''");
                DataOfDelivery = tasks_form.dateTimePicker1.Value.Date;
                Priority = tasks_form.comboBox1.Text.Replace("'", "''");

                int PriorityID = GetId(String.Format("Select id from Priority where Name = '{0}'", tasks_form.comboBox1.Text));

                DataTable newdt = new DataTable();
                newdt = await db.GetDatatableFromPostgreSQLAsync("select Complexity from Tasks where id in (Select id_ParentTask from Tasks where id = '" + ID + "')");

                string temp = "";

                for (int i = 0; i < newdt.Rows.Count; i++)
                {
                    temp = newdt.Rows[i]["Complexity"].ToString();
                }

                foreach (DataRow dr in newdt.Rows)
                {
                    temp = dr["Complexity"].ToString();
                }

                string TextBox = String.Join("", temp);
                
                if(TextBox != "")
                {
                    int complexityParent = int.Parse(TextBox);

                    if (int.Parse(complexity) <= complexityParent)
                    {
                        bool sqlresult = await db.ExecSQLAsync("UPDATE Tasks set Name ='" + newname + "', Description = '" + description + "', Complexity = '" + complexity + "', Date_Delivery = '" + DataOfDelivery + "', id_Priority = '" + PriorityID + "' WHERE id = '" + ID + "'");
                        node.Text = (newname);
                    }
                    else
                    {
                        MessageBox.Show("Трудоёмкость выполнения подзадания не может быть трудоёмкости выполнения основного задания", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else
                {
                    bool sqlresult = await db.ExecSQLAsync("UPDATE Tasks set Name ='" + newname + "', Description = '" + description + "', Complexity ='" + complexity + "', Date_Delivery = '" + DataOfDelivery + "', id_Priority = '" + PriorityID + "' WHERE id = '" + ID + "'");
                    node.Text = (newname);
                }
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
            AddEditTask tasks_form = new AddEditTask();
            if (tasks_form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataTable dt_ttT = await db.GetDatatableFromPostgreSQLAsync("select id from Tasks order by id Desc limit 1");
            DataRow lastrowW = dt_ttT.Rows[0];

            // Задаем переменные для столбцов этой строчки
            int id = Int32.Parse(lastrowW["id"].ToString());
            id++;

            if (string.IsNullOrEmpty(tasks_form.TextBox1.Text) || string.IsNullOrWhiteSpace(tasks_form.TextBox1.Text))
            {
                MessageBox.Show("Нужно верно заполните поле: " + tasks_form.Label1.Text);
                return;
            }
            if (string.IsNullOrEmpty(tasks_form.textBox3.Text) || string.IsNullOrWhiteSpace(tasks_form.textBox3.Text))
            {
                MessageBox.Show("Нужно верно заполните поле: " + tasks_form.label2.Text);
                return;
            }
            else
            {
                string name = tasks_form.TextBox1.Text.Replace("'", "''");
                string description = tasks_form.richTextBox1.Text.Replace("'", "''");
                string complexity = tasks_form.textBox3.Text.Replace("'", "''");
                DateTime DataOfDelivery = tasks_form.dateTimePicker1.Value.Date;
                int TaskManager = GetId(String.Format("Select id from Employees where Login = '" + Login + "' AND Password = '" + Password + "'"));
                int Priority = GetId(String.Format("Select id from Priority where Name = '{0}'", tasks_form.comboBox1.Text));
                string tag = "";
                

                if (TreeView1.SelectedNode == null)
                {
                    tag = "";
                    bool sqlresult = await db.ExecSQLAsync("INSERT into Tasks(id, Name, Description, Complexity, Date_Delivery, id_TaskManager, id_Priority) VALUES('" + id + "', '" + name + "','" + description + "', '" + complexity + "', '" + DataOfDelivery + "', '" + TaskManager + "','" + Priority + "')");
                }

                else
                {

                    //Записываем в переменную Tag, Tag текущего выбраного элемента (это его ID в базе, которое мы будем использовать чтобы задать родителя нашего нового добавляемого элемента)
                    tag = TreeView1.SelectedNode.Tag.ToString();

                    ///
                    DataTable newdt = new DataTable();
                    newdt = await db.GetDatatableFromPostgreSQLAsync("Select Complexity from Tasks where id = '" + tag + "'");

                    string temp = "";

                    for (int i = 0; i < newdt.Rows.Count; i++)
                    {
                        temp = newdt.Rows[i]["Complexity"].ToString();
                    }

                    foreach (DataRow dr in newdt.Rows)
                    {
                        temp = dr["Complexity"].ToString();
                    }

                    string TextBox = String.Join("", temp);

                    int complexityParent = int.Parse(TextBox);
                    ///
                    if(int.Parse(complexity) <= complexityParent)
                    {
                        //Записываем в базу новую строчку, задав поля имя и parent ID
                        bool sqlresult = await db.ExecSQLAsync("INSERT into Tasks(id, Name, id_ParentTask, Description, Complexity, Date_Delivery, id_TaskManager, id_Priority) VALUES('" + id + "', '" + name + "','" + tag + "', '" + description + "', '" + complexity + "', '" + DataOfDelivery + "', '" + TaskManager + "','" + Priority + "')");
                    }
                    else
                    {
                        MessageBox.Show("Трудоёмкость выполнения подзадания не может быть больше трудоёмкости выполнения основного задания", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }  
                } 
            }

           
            //Получаем данные обратно из базы
            //Получаем datatable из функции 
            //сразу выборка из таблицы - получаем последнюю строчку - она и есть та которую мы только что добавили
            DataTable dt_tt = await db.GetDatatableFromPostgreSQLAsync("SELECT id, Name, id_ParentTask FROM Tasks WHERE id = '" + id + "'");

            //Находим последнюю запись в таблице, - она и есть та которую мы только что добавили
            //Получаем индекс последней записи в нашем массиве
            int LastRowIndex = dt_tt.Rows.Count - 1;

            // Получаем последнюю строчку по этому индексу
            DataRow lastrow = dt_tt.Rows[LastRowIndex];

            // Задаем переменные для столбцов этой строчки
            string lastrow_ID = lastrow["id"].ToString();
            string lastrow_Name = lastrow["Name"].ToString();
            string lastrow_ParentID = lastrow["id_ParentTask"].ToString();

            //Тут будем добавлять это в наш treeview
            TreeNode node = new TreeNode((lastrow_Name));  //созадем объект node с именем из базы
            node.Tag = lastrow_ID; //присваиваем ему tag, который соответвует его ID в базе

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

            string current_task_id = TreeView1.SelectedNode.Tag.ToString();

            //Если внутри выбраного элемента содержаться другие элементы выводим уведомление и выходим из процедуры
            if (TreeView1.SelectedNode.Nodes.Count != 0)
            {
                MessageBox.Show("Нельзя удалить задание, поскольку он содержит подзадания", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            //Проверим существуют ли сотрудники, привязанные к данной таблице
            DataTable dt_tt_student = await db.GetDatatableFromPostgreSQLAsync("select ID from AssignedTasks where id_Task = '" + current_task_id + "'");
            if (dt_tt_student.Rows.Count > 0)
            {
                MessageBox.Show("Нельзя удалить задание, поскольку есть студенты привязанные к данному заданию", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            //Получаем DataTable
            //сразу в запросе находим нужные строчки и добавляем в datatable посути одну нужнную строку

            DataTable dt_tt = await db.GetDatatableFromPostgreSQLAsync("SELECT id FROM Tasks WHERE ID = '" + current_task_id + "'");            
            // Перебираем все строчки в таблице

            foreach (DataRow row in dt_tt.Rows)
            {
                // объявляем переменную которая с ID шникном текущей строчки в таблице
                string row_ID = row["id"].ToString();
                
                //Если этот ID равен текущему Tag выбраного элемента, то удаляем эту строчку из базы

                if (row_ID == TreeView1.SelectedNode.Tag.ToString())
                {
                    bool sqlresult = await db.ExecSQLAsync("DELETE FROM Tasks WHERE ID = '" + row_ID + "'");
                }

            }
            // А потом удаляем эту ноду из Treeview
            TreeView1.SelectedNode.Remove();
        }

        /// <summary>
        /// Работа со списком назначений заданий
        /// </summary>

        //Функционал для обновления таблицы с назначенными задания по ID задания
        private async void refreshGrid()
        {
            try
            {
                string current_task_id = TreeView1.SelectedNode.Tag.ToString();
                DataTable dt = await db.GetDatatableFromPostgreSQLAsync("select AssignedTasks.id as ID, Tasks.Name as Task, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи,  Results.Name as Результат from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result WHERE id_Task = '" + current_task_id + "'");

               
                DataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView
            }
            catch
            {

                DataTable dt = await db.GetDatatableFromPostgreSQLAsync("select AssignedTasks.id as ID, Tasks.Name as Задание, Employees.FIO as Employee, AssignedTasks.Date_Start as Дата_выдачи, Tasks.Date_Delivery as Date_Delivery, AssignedTasks.Date_End as Дата_сдачи, Results.Name as Результат from AssignedTasks join Tasks on Tasks.id = AssignedTasks.id_Task join Employees on Employees.id = AssignedTasks.id_Employee join Results on Results.id = AssignedTasks.id_Result");
                DataGridView1.DataSource = dt;
            }

            try
            {
                // Скроем столбец ненужные столбцы
                DataGridView1.Columns["ID"].Visible = false;
                DataGridView1.Columns["Task"].Visible = false;
                
                //Заголовки таблицы
                DataGridView1.Columns["Employee"].HeaderText = "Сотрудник";
                DataGridView1.Columns["Дата_выдачи"].HeaderText = "Дата назначения";
                DataGridView1.Columns["Date_Delivery"].HeaderText = "Планируемая дата закрытия";
                DataGridView1.Columns["Дата_сдачи"].HeaderText = "Фактическая дата закрытия";
                DataGridView1.Columns["Результат"].HeaderText = "Результат";
            }
            catch { }

            // выбираем первую строчку
            try
            {
                DataGridView1.CurrentCell = DataGridView1.Rows[0].Cells[0];
            }
            catch { }

        }

        //Функционал добавления нового назначения задания студенту
        private async void Button4_Click(object sender, EventArgs e)
        {

            int TaskID = int.Parse(TreeView1.SelectedNode.Tag.ToString());

            AddEditTaskAssignment assignment_form = new AddEditTaskAssignment(TaskID);

            if (assignment_form.ShowDialog() != DialogResult.OK)
            { return; }

            if (string.IsNullOrEmpty(assignment_form.comboBox1.Text) || string.IsNullOrWhiteSpace(assignment_form.comboBox1.Text))
            {
                MessageBox.Show("Нужно выбрать студента");
                return;
            }
            if (string.IsNullOrEmpty(assignment_form.comboBox2.Text) || string.IsNullOrWhiteSpace(assignment_form.comboBox2.Text))
            {
                MessageBox.Show("Нужно выбрать текущий результат");
                return;
            }
            else
            {
                int StudentID = GetId(String.Format("Select id from Students where FIO  = '{0}'", assignment_form.comboBox1.Text));
                DateTime Data_start = assignment_form.dateTimePicker1.Value;
                int ResultID = GetId(String.Format("Select id from Results where Result = '{0}'", assignment_form.comboBox2.Text));
                string Comment = assignment_form.textBox1.Text.Replace("'", "''");

                //записываем данные из текстбоксов Form3 в наши переменные
                // А потом экранируем кавычечку
                bool sqlresult = await db.ExecSQLAsync("INSERT into AssignedTasks(id_Task, id_Student, date_start, date_end, id_ScientificAdviser, id_Result, Comment) values('" + TaskID + "', '" + StudentID + "', '" + Data_start + "', '" + ResultID + "', '" + Comment + "')");
            }

            refreshGrid();
        }

        //Функционал редактирования назначения
        private async void Button5_Click(object sender, EventArgs e)
        {
            
            int ID;
            DateTime Data_start;
            DateTime Data_end;
            string Comment;

            try
            {
                ID = int.Parse(DataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Сначала выберите назначение, которое хотите отредактировать", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            AddEditTaskAssignment assignment_form = new AddEditTaskAssignment();

            //Заполняем в assignment_form поля для того чтобы было что редактировать.
            int TaskID = int.Parse(TreeView1.SelectedNode.Tag.ToString());
            assignment_form.comboBox1.Text = DataGridView1.CurrentRow.Cells["ФИО_студента"].Value.ToString();
            assignment_form.dateTimePicker1.Value = (DateTime)DataGridView1.CurrentRow.Cells["Дата_выдачи"].Value;
            assignment_form.comboBox2.Text = DataGridView1.CurrentRow.Cells["Результат"].Value.ToString();
            assignment_form.textBox1.Text = DataGridView1.CurrentRow.Cells["Комментарий"].Value.ToString();

            if (assignment_form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(assignment_form.comboBox1.Text) || string.IsNullOrWhiteSpace(assignment_form.comboBox1.Text))
            {
                MessageBox.Show("Нужно выбрать студента");
                return;
            }
            if (string.IsNullOrEmpty(assignment_form.comboBox2.Text) || string.IsNullOrWhiteSpace(assignment_form.comboBox2.Text))
            {
                MessageBox.Show("Нужно выбрать текущий результат");
                return;
            }
            else
            {
                int StudentID = GetId(String.Format("Select id from Students where FIO = '{0}'", assignment_form.comboBox1.Text));
                int ResultID = GetId(String.Format("Select id from Results where Result = '{0}'", assignment_form.comboBox2.Text));
                Data_start = assignment_form.dateTimePicker1.Value;
                Comment = assignment_form.textBox1.Text;

                bool sqlresult = await db.ExecSQLAsync("UPDATE AssignedTasks set id_Task='" + TaskID + "', id_Student = '" + StudentID + "',date_start= '" + Data_start + "', id_Result = '" + ResultID + "', Comment= '" + Comment + "' where id = '" + ID + "'");
            }

            refreshGrid();
            
        }

        //Функционал удаления назначения
        private async void Button6_Click(object sender, EventArgs e)
        {
            int ID;

            try
            {
                ID = int.Parse(DataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Сначала выберите студента", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            //Удаляем из базы
            if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить студента: " + DataGridView1.CurrentRow.Cells["ФИО_студента"].Value  + "?", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
            {
                bool sqlresult = await db.ExecSQLAsync("DELETE FROM AssignedTasks where id = '" + ID + "'");
            }

            //Удаляем из DataGridView
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow); 

        }

        // Функционал для просмотра подробной информации по назначению
        private void button7_Click(object sender, EventArgs e)
        {
            int ID;

            try
            {
                ID = int.Parse(DataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Сначала выберите назначение, по которому хотите посмотреть подробную информацию", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            DetailedTaskAssigment assignment_form = new DetailedTaskAssigment();
            if (assignment_form.ShowDialog() != DialogResult.OK)
            { return; }
        }


        /// <summary>
        /// Вспомогательные методы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        //Функционал перехвата отрисовки Node в Treeview1
        private void TreeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            SolidBrush NodeBursh = new SolidBrush(System.Drawing.Color.White);
            SolidBrush SlectedNodeBursh = new SolidBrush(System.Drawing.Color.FromArgb(185, 209, 234));
            System.Drawing.Color textForeColor = e.Node.ForeColor;

            if (e.Node.IsSelected)
            {
                if (TreeView1.Focused)
                {
                    e.Graphics.FillRectangle(SlectedNodeBursh, e.Bounds);
                }
                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, textForeColor);
            }
            else
            {
                e.Graphics.FillRectangle(NodeBursh, e.Bounds);

                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, textForeColor);
            }

        }

        //Функционал обработки события перетаскиваниея
        private async void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            //Перетаскиваемый элемент - это ID типа string - проверим что это действительно он;
            if (e.Data.GetDataPresent("System.String", false))
            {
                TreeView treeview = (TreeView)sender;
                TreeNode DestinationNode = null;
                Point pt;
                // находим координаты того места куда сбросили перетаскиваемый объект
                pt = treeview.PointToClient(new Point(e.X, e.Y));
                // Находим ноду, которая находилась в этих координатах
                DestinationNode = treeview.GetNodeAt(pt);
                // Проверяем что там вообще есть нода в этом месте
                if (DestinationNode == null)
                {
                    return;
                }

                // Обновляем запись в таблице Empoyee - присваиваем полю DepartmentID - Tag нашей ноды
                bool sqlresult = await db.ExecSQLAsync("UPDATE AssignedTasks set id_Task='" + DestinationNode.Tag + "' where ID = '" + e.Data.GetData("System.String") + "'");

                TreeView1.SelectedNode = DestinationNode; //Выбираем ноду, в которую перетащили объект

            }
        }

        //Функионал для обработки DragAndDrop для перемещения сотрудников между отделами путем перетаскивания
        //Событие срабатывает при движении мышкой по области Datagridview. Если нажата левая кнопка мышки, то захватываем элемент для drag and drop
        private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    string dragedItemID = DataGridView1.CurrentRow.Cells["id"].Value.ToString();
                    DataGridView1.DoDragDrop(dragedItemID, DragDropEffects.Move);
                }
                catch
                {
                }
            }
        }

        //Функционал обработки событие вхождения перетаскиваемого объекта в зону ThreeView
        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; //Курсор DragAndDrop
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
            Reader reader = Workflow.connection.Select(query);
            List<object> identificator = reader.GetValue(0, false);
            reader.Close();
            return int.Parse(identificator[0].ToString());
        }

        //Функционал для перехода к главному окну
        private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mainform newForm = new Mainform();
            newForm.ToolDataToolStripMenuItem.Visible = false;
            newForm.TaskEmployeeToolStripMenuItem.Visible = false;
            newForm.Show();
            Hide();
        }

        //Функционал для выхода из программы
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Функционал для очищения Selected.Node, если нажимаем по пустому месту в TreeView1
        private void TreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.TreeView1.GetNodeAt(e.X, e.Y) == null)
            {
                this.TreeView1.SelectedNode = null;
            }
        }
    }
}