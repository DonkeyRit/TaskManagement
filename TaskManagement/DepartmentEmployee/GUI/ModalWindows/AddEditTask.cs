using System;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;

namespace DepartmentEmployee.GUI.ModalWindows
{
	public partial class AddEditTask : Form
	{
		public AddEditTask()
		{
			var connection = Connection.CreateConnection();

			InitializeComponent();

			AcceptButton = Button1;
			CancelButton = Button2;
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
