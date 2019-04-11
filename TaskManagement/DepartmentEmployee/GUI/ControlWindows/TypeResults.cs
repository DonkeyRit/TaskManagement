using System;
using System.Data;
using System.Windows.Forms;
using Core.Database.ConnectionDB;
using DepartmentEmployee.GUI.ModalWindows;

namespace DepartmentEmployee.GUI.ControlWindows
{
    public partial class TypeResults : Form
    {

        PostgreSQLConnectionParams postgresql_params = new PostgreSQLConnectionParams();
        DB db;

        public TypeResults()
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

            RefreshGrid(); // Обновляем список результов.
        }

        //Функционал вывода всех типов результатов
        private async void RefreshGrid()
        {

            DataTable dt = await db.GetDatatableFromPostgreSQLAsync("Select * from Results ORDER BY id ASC");
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

        
        //Функционал добавления нового типа результата
        private async void Button1_Click(object sender, EventArgs e)
        {
            AddEditTypeResults form = new AddEditTypeResults();

            if (form.ShowDialog() != DialogResult.OK)
            { return; }

            string ResultName = form.textBox1.Text.Replace("'", "''");

            if (string.IsNullOrEmpty(ResultName) || string.IsNullOrWhiteSpace(ResultName))
            {
                MessageBox.Show("Нужно верно заполните поле 'Название'");
            }
            if (form.textBox1.TextLength > 30)
            {
                MessageBox.Show("Название должно быть не больше 20 символов");
            }
            else
            {
                //записываем данные из текстбоксов AddEditStudent.Form в наши переменные
                // А потом экранируем кавычечку
                bool sqlresult = await db.ExecSQLAsync("INSERT into Results(Name) values('" + ResultName + "')");
            }
            RefreshGrid();
        }

        //Функционал редактирования выбранного типа результата
        private async void Button2_Click_1(object sender, EventArgs e)
        {
            string ID = "";
            string ResultName;
            
            try
            {
                ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Сначала выберите тип результата", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            AddEditTypeResults form = new AddEditTypeResults();

            //Заполняем в AddEditStudent.Form поля для того чтобы было что редактировать.
            form.textBox1.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();

            if (form.ShowDialog() != DialogResult.OK)
            { return; }

            ResultName = form.textBox1.Text.Replace("'", "''");

            if (string.IsNullOrEmpty(ResultName) || string.IsNullOrWhiteSpace(ResultName))
            {
                MessageBox.Show("Нужно верно заполните поле 'Название'");
            }
            if (form.textBox1.TextLength > 30)
            {
                MessageBox.Show("Название должно быть не больше 20 символов");
            }
            else
            {
                bool sqlresult = await db.ExecSQLAsync("UPDATE Results set Name ='" + ResultName + "' where ID = '" + ID + "'");
            }

            RefreshGrid();
        }

        //Функционал удаления выбранного типа результата
        private async void Button3_Click_1(object sender, EventArgs e)
        {
            string ID = "";

            try
            {
                ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Сначала выберите тип результата", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            //Удаляем из базы
            if ((DialogResult = MessageBox.Show("Вы действительно хотите удалить данный тип результата: " + dataGridView1.CurrentRow.Cells["id"].Value + "?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
            {
                bool sqlresult = await db.ExecSQLAsync("DELETE FROM Results where id = '" + ID + "'");
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
