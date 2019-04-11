using System;
using System.Windows.Forms;
using Core.Database;
using Core.Database.Connection;
using DepartmentEmployee.Database;
using DepartmentEmployee.Database.Connection;
using DepartmentEmployee.GUI.ControlWindows;

namespace DepartmentEmployee
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Workflow.connection = Connection.CreateConnection();
            Workflow.connection.OpenConnection();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
        }
    }
}
