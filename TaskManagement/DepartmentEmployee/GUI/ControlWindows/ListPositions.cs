using DepartmentEmployee.GUI.ModalWindows;
using System;
using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class ListPositions : Form
	{
		private readonly Connection connection;

		public ListPositions()
		{
			InitializeComponent();
			connection = Connection.CreateConnection();
			RefreshGrid(); // Обновляем список результов.
		}

		//Функционал вывода всех должностей
		private async void RefreshGrid()
		{

			DataTable dt = await connection.GetDataAdapterAsync("Select * from Positions ORDER BY id ASC");
			dataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

			try
			{
				// Скроем столбец ненужные столбцы
				dataGridView1.Columns["id"].Visible = false;

				//Заголовки таблицы
				dataGridView1.Columns["Name"].HeaderText = "Наименование";
			}
			catch { }

			// выбираем первую строчку
			try
			{
				dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
			}
			catch { }
		}

		//Функционал добавления новой должности
		private async void Button1_Click(object sender, EventArgs e)
		{
			AddEditPosition form = new AddEditPosition();

			if (form.ShowDialog() != DialogResult.OK)
			{ return; }

			string ResultName = form.textBox1.Text.Replace("'", "''");

			if (string.IsNullOrEmpty(ResultName) || string.IsNullOrWhiteSpace(ResultName))
			{
				MessageBox.Show("Нужно верно заполните поле 'Название'");
			}
			if (form.textBox1.TextLength > 20)
			{
				MessageBox.Show("Название должно быть не больше 20 символов");
			}
			else
			{
				//записываем данные из текстбоксов AddEditStudent.Form в наши переменные
				// А потом экранируем кавычечку
				bool sqlresult = await connection.ExecNonQueryAsync("INSERT into Positions(Name) values('" + ResultName + "')");
			}
			RefreshGrid();
		}

		//Функционал редактирования выбранной должности
		private async void Button2_Click(object sender, EventArgs e)
		{
			string ID = "";
			string ResultName;

			try
			{
				ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
			}
			catch
			{
				MessageBox.Show("Сначала выберите должность", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			AddEditPosition form = new AddEditPosition();

			//Заполняем в AddEditStudent.Form поля для того чтобы было что редактировать.
			form.textBox1.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();

			if (form.ShowDialog() != DialogResult.OK)
			{ return; }

			ResultName = form.textBox1.Text.Replace("'", "''");

			if (string.IsNullOrEmpty(ResultName) || string.IsNullOrWhiteSpace(ResultName))
			{
				MessageBox.Show("Нужно верно заполните поле 'Название'");
			}
			if (form.textBox1.TextLength > 20)
			{
				MessageBox.Show("Название должно быть не больше 20 символов");
			}
			else
			{
				bool sqlresult = await connection.ExecNonQueryAsync("UPDATE Positions set Name ='" + ResultName + "' where ID = '" + ID + "'");
			}

			RefreshGrid();
		}

		//Функционал удаления выбранной должности
		private async void Button3_Click(object sender, EventArgs e)
		{
			string ID = "";

			try
			{
				ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
			}
			catch
			{
				MessageBox.Show("Сначала выберите должность", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				return;
			}

			//Удаляем из базы
			if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить данную должность: " + dataGridView1.CurrentRow.Cells["id"].Value + "?", "Delete Position", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
			{
				bool sqlresult = await connection.ExecNonQueryAsync("DELETE FROM Positions where id = '" + ID + "'");
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

		//Функционал для выхода из приложения
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
