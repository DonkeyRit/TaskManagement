using DepartmentEmployee.Database.ConnectionDB;
using DepartmentEmployee.Database.ObjectReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI.ControlWindows
{
    public partial class Authorization : Form
    {

        PostgreSQLConnectionParams postgresql_params = new PostgreSQLConnectionParams();
        DB db;
        public static string Login;
        public static string Password;


        public Authorization()
        {
            InitializeComponent();

            textBox2.UseSystemPasswordChar = true;

            //!!!!!!!!!!УЧЕТНЫЕ ДАННЫЕ PostgreSQL!!!!!!!!!!!!!!!
            postgresql_params.hostname = "127.0.0.1";        // Hostname
            postgresql_params.port = "5432";                 // Port
            postgresql_params.database = "TaskPerformance";  // Database
            postgresql_params.user = "postgres";             // Username
            postgresql_params.password = "123456";           // Password

            //!!!!!!!!!!УЧЕТНЫЕ ДАННЫЕ PostgreSQL!!!!!!!!!!!!!!!

            db = new DB(postgresql_params);
        }

        //Функционал для авторизации в программе
        private void button1_Click(object sender, EventArgs e)
        {

            //Запрашиваем тип входящего пользователя из таблицы Users 
            Reader reader = Workflow.connection.Select("Select Type.Name as Name from Employees join Type on Type.id = Employees.id_Type where Employees.Login = '" + textBox1.Text + "' AND Employees.Password = '" + textBox2.Text + "'");
            List<object> task_name = reader.GetValue(0, true);
            reader.Close();

            string Type;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите логин");
            }
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите пароль");
            }

            Login = textBox1.Text;
            Password = textBox2.Text;

            if (task_name != null)
            {
                Type = String.Join("", task_name);
                //Если есть совпадения, то открываем главное окно программы
                if (Type == "Admin")
                {
                    Mainform newForm = new Mainform();
                    newForm.TaskEmployeeToolStripMenuItem.Visible = false;
                    newForm.DirectorToolStripMenuItem.Visible = false;
                    newForm.Show();
                    Hide();
                }
                else if (Type == "Director")
                {
                    Mainform newForm = new Mainform();
                    newForm.ToolDataToolStripMenuItem.Visible = false;
                    newForm.TaskEmployeeToolStripMenuItem.Visible = false;
                    newForm.Show();
                    Hide();
                }
                else if (Type == "User")
                {
                    Mainform newForm = new Mainform();
                    newForm.ToolDataToolStripMenuItem.Visible = false;
                    newForm.DirectorToolStripMenuItem.Visible = false;
                    newForm.Show();
                    Hide();
                }

            }
            //Если совпадений нет, сообщаем об ошибке
            else if (task_name == null)
            {
                MessageBox.Show("Введенный логин или пароль не верный");
            }  
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button1_Click(sender, e);
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button1_Click(sender, e);
                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else textBox2.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
