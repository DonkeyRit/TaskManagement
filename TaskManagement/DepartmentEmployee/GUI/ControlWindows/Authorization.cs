using System;
using System.Data;
using System.Linq;
using Core.Exceptions;
using Core.Database.Utils;
using System.Windows.Forms;
using Core.Database.Connection;
using System.Collections.Generic;
using Core.Model;
using Core.Model.Enums;
using DepartmentEmployee.Context;
using DepartmentEmployee.Controllers;

namespace DepartmentEmployee.GUI.ControlWindows
{
	public partial class Authorization : Form
	{
		public static string Login;
		public static string Password;
		private readonly Connection _connection;


		public Authorization()
		{
			InitializeComponent();
			PasswordField.UseSystemPasswordChar = true;
			_connection = Connection.CreateConnection();
		}

		/// <summary>
		/// Функционал для авторизации в программе
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoginButton_Click(object sender, EventArgs e)
		{
			try
			{
				FieldController.CheckFieldValue(LoginField.Text, "Введите логин");
				FieldController.CheckFieldValue(PasswordField.Text, "Введите пароль");

				//Запрашиваем тип входящего пользователя из таблицы Users 
				DataTable table = _connection.GetDataAdapter(
					$"Select Type.Name as Name from Employees join Type on Type.id = id_Type where Login = '{LoginField.Text}' AND Password = '{PasswordField.Text}'");
				List<object> typeName = table.GetColumnValuesDataTable(0, CellType.String);

				if (typeName.Any() && typeName.Count == 1)
				{
					string type = typeName.First().ToString();
					Mainform mainForm;
					Role role;

					switch (type)
					{
						case "Admin":
							mainForm = new Mainform
							{
								TaskEmployeeToolStripMenuItem = { Visible = false },
								DirectorToolStripMenuItem = { Visible = false }
							};
							role = Role.Admin;
							break;
						case "Director":
							mainForm = new Mainform
							{
								ToolDataToolStripMenuItem = { Visible = false },
								TaskEmployeeToolStripMenuItem = { Visible = false }
							};
							role = Role.Director;
							break;
						case "User":
							mainForm = new Mainform
							{
								ToolDataToolStripMenuItem = { Visible = false },
								DirectorToolStripMenuItem = { Visible = false }
							};
							role = Role.User;
							break;
						default:
							throw new TaskManagementProjectException("Database have invalid value");
					}

					CustomContext context = CustomContext.GetInstance();
					User currentUser = new User(LoginField.Text, PasswordField.Text.GetHashCode().ToString(), role);
					context.CurrentUser = currentUser;

					mainForm.Show();
					Hide();
				}
				else
				{
					throw new TaskManagementProjectException("Введенный не верный логин или пароль");
				}
			}
			catch (TaskManagementProjectException ex)
			{
				ModalDialogController.Display(ex.Message, ex);
			}
			
		}

		private void InputField_KeyDown(object sender, KeyEventArgs e) => FieldController.CheckClickOnEnterButton(sender, e, LoginButton_Click);
		private void PasswordField_KeyDown(object sender, KeyEventArgs e) => FieldController.CheckClickOnEnterButton(sender, e, LoginButton_Click);
		private void ShowPasswordOption_CheckedChanged(object sender, EventArgs e) => PasswordField.UseSystemPasswordChar = !ShowPasswordOption.Checked;
		private void ExitButton_Click(object sender, EventArgs e) => Application.Exit();
	}
}
