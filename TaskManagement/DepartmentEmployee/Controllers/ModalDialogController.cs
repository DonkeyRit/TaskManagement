using System;
using System.Windows.Forms;

namespace DepartmentEmployee.Controllers
{
	internal static class ModalDialogController
	{
		public static void Display(string text) => MessageBox.Show(text);
		public static void Display(string preamble, Exception exception) =>
			MessageBox.Show(preamble + Environment.NewLine + exception.StackTrace);
	}
}
