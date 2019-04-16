using DepartmentEmployee.GUI.ControlWindows;
using System;
using System.Windows.Forms;

namespace DepartmentEmployee.GUI
{
    public partial class Mainform : Form
    {

        public Mainform()
        {
            InitializeComponent();
        }

        private void ViewListStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListEmployee newForm = new ListEmployee();
            newForm.Show();
            Hide();
        }


        private void AnalisisToWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisToWork newForm = new AnalysisToWork();
            newForm.Show();
            Hide();
        }


        private void PriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListPriority newForm = new ListPriority();
            newForm.Show();
            Hide();
        }

        private void AssigmentTaskToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AssignmentTask newForm = new AssignmentTask();
            newForm.Show();
            Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void QualificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListQialifications newForm = new ListQialifications();
            newForm.Show();
            Hide();
        }


        private void ExitUserAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization newForm = new Authorization();
            newForm.Show();
            Hide();
        }

        private void ViewYouListTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskEmployee newForm = new TaskEmployee();
            newForm.Show();
            Hide();
        }
    }
}
