using System.Data;
using System.Windows.Forms;
using Core.Database.Connection;

namespace DepartmentEmployee.GUI.ControlWindows
{
    public partial class ImplementationProgress : Form
    {

        private readonly Connection connection;

        public ImplementationProgress()
        {
            InitializeComponent();
            connection = Connection.CreateConnection();
            RefreshGrid();
        }
        //Функционал вывода всего списка
        private async void RefreshGrid()
        {
            var id = AssigmentTask.IDTask;

            //Выводим общий прогресс выполнения задания
            DataTable dt = await connection.GetDataAdapterAsync("select SUM(Results.Result_Qual1) as Result_Qual1, SUM(Results.Result_Qual2) as Result_Qual2, SUM(Results.Result_Qual3) as Result_Qual3, SUM(Results.Result_Qual4) as Result_Qual4 from Results join AssignedTasks on AssignedTasks.id_Result = Results.id where AssignedTasks.id_Task = '" + id + "'");
            dataGridView1.DataSource = dt; //Присвеиваем DataTable в качестве источника данных DataGridView

            try
            {
                //dataGridView3.Columns["Result"].Visible = false;
                //Заголовки таблицы
                dataGridView1.Columns["Result_Qual1"].HeaderText = "Кол-во часов 'Инженера 3-категории'";
                dataGridView1.Columns["Result_Qual2"].HeaderText = "Кол-во часов 'Инженера 2-категории'";
                dataGridView1.Columns["Result_Qual3"].HeaderText = "Кол-во часов 'Инженера 1-категории'";
                dataGridView1.Columns["Result_Qual4"].HeaderText = "Кол-во часов 'Главного инженера'";
            }

            catch { }
        }
    }
}
