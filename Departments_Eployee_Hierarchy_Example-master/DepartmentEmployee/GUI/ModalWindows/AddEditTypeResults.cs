using System;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI.ModalWindows
{
    public partial class AddEditTypeResults : Form
    {
        public AddEditTypeResults()
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
    }
}
