using System;
using Core.Exceptions;
using System.Windows.Forms;


namespace DepartmentEmployee.Controllers
{
	internal static class FieldController
	{

		public static void CheckClickOnEnterButton(object sender, KeyEventArgs e, Action<object, KeyEventArgs> method)
		{
			if (e.KeyCode == Keys.Enter)
			{
				method(sender, e);
			}
		}

		public static void CheckFieldValue(string value, string exceptionText)
		{
			if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			{
				throw new TaskManagementProjectException(exceptionText);
			}
		}

	}
}
