using DepartmentEmployee.Database.ObjectReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI.ModalWindows
{
    public partial class AddEditEmployee : Form
    {
        public AddEditEmployee()
        {
            InitializeComponent();
            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
            this.CancelButton = Button2;

            // Выводим в comboBox1 квалификации
            Reader reader = Workflow.connection.Select("Select Name from Qualifications");
            List<object> questions = reader.GetValue(0, true);
            reader.Close();

            comboBox1.Items.Clear();

            for (int i = 0; i < questions.Count; i++)
            {
                comboBox1.Items.Add(questions[i].ToString());
            }

            // Выводим в comboBox2 должности
            reader = Workflow.connection.Select("Select Name from Positions");
            List<object> results = reader.GetValue(0, true);
            reader.Close();

            comboBox2.Items.Clear();

            for (int i = 0; i < results.Count; i++)
            {
                comboBox2.Items.Add(results[i].ToString());
            }

            // Выводим в comboBox3 типы учётных записей
            reader = Workflow.connection.Select("Select Name from Type");
            List<object> type = reader.GetValue(0, true);
            reader.Close();

            comboBox3.Items.Clear();

            for (int i = 0; i < type.Count; i++)
            {
                comboBox3.Items.Add(type[i].ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            {
                char l = e.KeyChar;
                if ((l < 'А' || l > 'я') && l != '\b' && l != ' ')
                {
                    e.Handled = true;
                }
            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddEditEmployee form = new AddEditEmployee();
            var password = form.CreatePassword();
            textBox3.Text = password;
        }
    

        public string CreatePassword()
        {
            int length = 10;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
