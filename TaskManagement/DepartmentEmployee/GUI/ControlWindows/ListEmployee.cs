using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;
using Core.Database.Utils;
using DepartmentEmployee.GUI.ModalWindows;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class ListEmployee : Form
	{

		private readonly Connection connection;

		public ListEmployee()
		{
			InitializeComponent();
			connection = Connection.CreateConnection();
			RefreshGrid(); // Обновляем список заданий.  
		}

		//Функционал вывода всего списка сотрудников
		private async void RefreshGrid()
		{
 
			DataTable dt = await connection.GetDataAdapterAsync("Select Employees.id as id, Employees.FIO as FIO, Employees.DateOfBirth as DateOfBirth, Qualifications.Name as Qualification, Positions.Name as Position, Employees.Login as Login, Employees.Password as Password, Type.Name as Type from Employees join Positions on Positions.id = Employees.id_Position join Qualifications on Qualifications.id = Employees.id_Qualification join Type on Employees.id_Type = Type.id ORDER BY Employees.id ASC");
			dataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView
			
			try
			{
				// Скроем столбец ненужные столбцы
				dataGridView1.Columns["id"].Visible = false;

				//Заголовки таблицы
				dataGridView1.Columns["FIO"].HeaderText = "ФИО сотрудника";
				dataGridView1.Columns["FIO"].Width = 200;
				dataGridView1.Columns["DateOfBirth"].HeaderText = "Дата рождения";
				dataGridView1.Columns["Qualification"].HeaderText = "Квалификация";
				dataGridView1.Columns["Qualification"].Width = 200;
				dataGridView1.Columns["Position"].HeaderText = "Должность";
				dataGridView1.Columns["Login"].HeaderText = "Логин";
				dataGridView1.Columns["Password"].HeaderText = "Пароль";
				dataGridView1.Columns["Type"].HeaderText = "Роль";
			}
			catch { }

			// выбираем первую строчку
			try
			{
				dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
			}
			catch { }
		}

		//Функционал добавления нового студента
		private async void Button1_Click_1(object sender, EventArgs e)
		{
			AddEditEmployee form = new AddEditEmployee();

			if (form.ShowDialog() != DialogResult.OK)
			{ return; }

			if (string.IsNullOrEmpty(form.textBox1.Text) || string.IsNullOrWhiteSpace(form.textBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label1.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox1.Text) || string.IsNullOrWhiteSpace(form.comboBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox2.Text) || string.IsNullOrWhiteSpace(form.comboBox2.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.textBox2.Text) || string.IsNullOrWhiteSpace(form.textBox2.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.textBox3.Text) || string.IsNullOrWhiteSpace(form.textBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox3.Text) || string.IsNullOrWhiteSpace(form.comboBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}

			if (form.textBox1.TextLength > 50)
			{
				MessageBox.Show("ФИО должно быть не больше 50 символов");
			}
			
			else
			{
				string EmployeeFIO = form.textBox1.Text.Replace("'", "''");
				DateTime DataOfBirth = form.dateTimePicker1.Value.Date;
				int QualificationID = GetId(String.Format("Select id from Qualifications where Name = '{0}'", form.comboBox1.Text));
				int PositionID = GetId(String.Format("Select id from Positions where Name = '{0}'", form.comboBox2.Text));
				string Login = form.textBox2.Text.Replace("'", "''");
				string Password = form.textBox3.Text.Replace("'", "''");
				int TypeID = GetId(String.Format("Select id from Type where Name = '{0}'", form.comboBox3.Text));

				//записываем данные из текстбоксов AddEditStudent.Form в наши переменные
				// А потом экранируем кавычечку
				bool sqlresult = await connection.ExecNonQueryAsync("INSERT into Employees(FIO, DateOfBirth, id_Qualification, id_Position, Login, Password, id_Type) values('" + EmployeeFIO + "', '" + DataOfBirth + "', '" + QualificationID + "', '" + PositionID + "', '" + Login + "', '" + Password + "', '" + TypeID + "')");
			}
			RefreshGrid();
		}

		//Функционал редактирования выбранного сотрудника
		private async void Button2_Click(object sender, EventArgs e)
		{

			string ID = "";
			
			try
			{
				ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
			}
			catch
			{
				MessageBox.Show("Сначала выберите сотрудника", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			AddEditEmployee form = new AddEditEmployee();

			//Заполняем в AddEditStudent.Form поля для того чтобы было что редактировать.
			form.textBox1.Text = dataGridView1.CurrentRow.Cells["FIO"].Value.ToString();
			form.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells["DateOfBirth"].Value;
			form.comboBox1.Text = dataGridView1.CurrentRow.Cells["Qualification"].Value.ToString();
			form.comboBox2.Text = dataGridView1.CurrentRow.Cells["Position"].Value.ToString();
			form.textBox2.Text = dataGridView1.CurrentRow.Cells["Login"].Value.ToString();
			form.textBox3.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
			form.comboBox3.Text = dataGridView1.CurrentRow.Cells["Type"].Value.ToString();

			if (form.ShowDialog() != DialogResult.OK)
			{ return; }

			if (string.IsNullOrEmpty(form.textBox1.Text) || string.IsNullOrWhiteSpace(form.textBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label1.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox1.Text) || string.IsNullOrWhiteSpace(form.comboBox1.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox2.Text) || string.IsNullOrWhiteSpace(form.comboBox2.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.textBox2.Text) || string.IsNullOrWhiteSpace(form.textBox2.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.textBox3.Text) || string.IsNullOrWhiteSpace(form.textBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (string.IsNullOrEmpty(form.comboBox3.Text) || string.IsNullOrWhiteSpace(form.comboBox3.Text))
			{
				MessageBox.Show("Нужно верно заполните поле: " + form.label5.Text);
				return;
			}
			if (form.textBox1.TextLength > 50)
			{
				MessageBox.Show("ФИО должно быть не больше 50 символов");
			}
			else
			{
				string EmployeeFIO = form.textBox1.Text.Replace("'", "''");
				DateTime DataOfBirth = form.dateTimePicker1.Value.Date;
				int QualificationID = GetId(String.Format("Select id from Qualifications where Name = '{0}'", form.comboBox1.Text));
				int PositionID = GetId(String.Format("Select id from Positions where Name = '{0}'", form.comboBox2.Text));
				string Login = form.textBox2.Text.Replace("'", "''");
				string Password = form.textBox3.Text.Replace("'", "''");
				int TypeID = GetId(String.Format("Select id from Type where Name = '{0}'", form.comboBox3.Text));

				bool sqlresult = await connection.ExecNonQueryAsync("UPDATE Employees set FIO ='" + EmployeeFIO + "', DateOfBirth = '" + DataOfBirth + "' ,id_Qualification = '" + QualificationID + "', id_Position = '" + PositionID + "', Login = '" + Login + "', Password = '" + Password + "', id_Type = '" + TypeID + "' where ID = '" + ID + "'");
			}
			RefreshGrid();
		}

		//Функционал удаления выбранного сотрудника
		private async void Button3_Click(object sender, EventArgs e)
		{
			string ID = "";

			try
			{
				ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
			}
			catch
			{
				MessageBox.Show("Сначала выберите сотрудника", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Удаляем из базы
			if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить сотрудника: " + dataGridView1.CurrentRow.Cells["id"].Value + "?", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
			{
				bool sqlresult = await connection.ExecNonQueryAsync("DELETE FROM Employees where id = '" + ID + "'");
			}

			//Удаляем из DataGridView
			dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
		}

		//Функционал для перехода обратно на стартовую страницу
		private void BackwardToMainformToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mainform newForm = new Mainform();
			newForm.TaskEmployeeToolStripMenuItem.Visible = false;
			newForm.DirectorToolStripMenuItem.Visible = false;
			newForm.Show();
			Hide();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		//Функционал получения ID
		public int GetId(string query)
		{

            DataTable table = connection.GetDataAdapter(query);
            List<object> identificator = table.GetColumnValuesDataTable(0, CellType.String);

            //Reader reader = Workflow.connection.Select(query);
            //List<object> identificator = reader.GetValue(0, false);
            //reader.Close();
            return int.Parse(identificator[0].ToString());
		}
	}
}
