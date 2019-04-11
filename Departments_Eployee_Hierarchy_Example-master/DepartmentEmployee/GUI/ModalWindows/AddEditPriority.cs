using System;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI.ModalWindows
{
    public partial class AddEditPriority : Form
    {
        public AddEditPriority()
        {
            InitializeComponent();
            // Это нужно для того чтобы при нажатии ентер нажималась кнопка ОК а при нажатии ESC нажималась кнопка Cancel
            this.AcceptButton = Button1;
            this.CancelButton = Button2;
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
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != ' ')
            {
                e.Handled = true;
            }
            */
        }
    }
}
