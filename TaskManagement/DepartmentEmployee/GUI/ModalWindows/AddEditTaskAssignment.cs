using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Core.Database.ObjectReader;
using DepartmentEmployee.GUI.ControlWindows;


namespace DepartmentEmployee.GUI.ModalWindows
{
    public partial class AddEditTaskAssignment : Form
    {
        public AddEditTaskAssignment(int TaskID)
        {
            InitializeComponent();
            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
            this.CancelButton = Button2;

            AssigmentTask newform = new AssigmentTask();

            int Complexity = newform.GetId(String.Format("Select Complexity from Tasks where id  = '{0}'", TaskID));

            DateTime CurrentTime = DateTime.UtcNow;
            dateTimePicker1.Value = CurrentTime; // Задаем в поле с выбором Даты выдачи задания текущую дату

            // Выводим в comboBox1 сотрудников
            Reader reader = Workflow.connection.Select("Select FIO from Employees");
            List<object> employees = reader.GetValue(0, true);
            reader.Close();

            comboBox1.Items.Clear();

            for (int i = 0; i < employees.Count; i++)
            {
                comboBox1.Items.Add(employees[i].ToString());
            }

            // Выводим в comboBox2 список результатов
            reader = Workflow.connection.Select("Select Name from Results");
            List<object> results = reader.GetValue(0, true);
            reader.Close();

            comboBox2.Items.Clear();

            for (int i = 0; i < results.Count; i++)
            {
                comboBox2.Items.Add(results[i].ToString());
            }
        }

        public AddEditTaskAssignment()
        {
            InitializeComponent();
            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
            this.CancelButton = Button2;

            // Выводим в comboBox1 сотрудников
            Reader reader = Workflow.connection.Select("Select FIO from Employees");
            List<object> employees = reader.GetValue(0, true);
            reader.Close();

            comboBox1.Items.Clear();

            for (int i = 0; i < employees.Count; i++)
            {
                comboBox1.Items.Add(employees[i].ToString());
            }

            // Выводим в comboBox2 список результатов
            reader = Workflow.connection.Select("Select Name from Results");
            List<object> results = reader.GetValue(0, true);
            reader.Close();

            comboBox2.Items.Clear();

            for (int i = 0; i < results.Count; i++)
            {
                comboBox2.Items.Add(results[i].ToString());
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

    }
}
