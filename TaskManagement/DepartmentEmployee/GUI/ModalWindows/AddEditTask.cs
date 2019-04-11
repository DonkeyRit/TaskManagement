﻿using DepartmentEmployee.Database.ObjectReader;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI.ModalWindows
{
    public partial class AddEditTask : Form
    {
        public AddEditTask()
        {
            InitializeComponent();
            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
            this.CancelButton = Button2;

            // Выводим в comboBox1 квалификации
            Reader reader = Workflow.connection.Select("Select Name from Priority");
            List<object> priority = reader.GetValue(0, true);
            reader.Close();

            comboBox1.Items.Clear();

            for (int i = 0; i < priority.Count; i++)
            {
                comboBox1.Items.Add(priority[i].ToString());
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}