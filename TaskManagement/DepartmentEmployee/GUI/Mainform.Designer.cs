namespace DepartmentEmployee.GUI
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ToolDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewListStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QualificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalisisToWorkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AssigmentTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitUserAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TaskEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewYouListTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DepartmentEmployee.Properties.Resources.kontrol_vypolneniya_programm;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(687, 286);
            this.pictureBox1.TabIndex = 120;
            this.pictureBox1.TabStop = false;
            // 
            // ToolDataToolStripMenuItem
            // 
            this.ToolDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewListStudentsToolStripMenuItem,
            this.PriorityToolStripMenuItem,
            this.QualificationsToolStripMenuItem});
            this.ToolDataToolStripMenuItem.Name = "ToolDataToolStripMenuItem";
            this.ToolDataToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.ToolDataToolStripMenuItem.Text = "Настройки данных";
            // 
            // ViewListStudentsToolStripMenuItem
            // 
            this.ViewListStudentsToolStripMenuItem.Name = "ViewListStudentsToolStripMenuItem";
            this.ViewListStudentsToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.ViewListStudentsToolStripMenuItem.Text = "Настройка списка сотрудников";
            this.ViewListStudentsToolStripMenuItem.Click += new System.EventHandler(this.ViewListStudentsToolStripMenuItem_Click);
            // 
            // PriorityToolStripMenuItem
            // 
            this.PriorityToolStripMenuItem.Name = "PriorityToolStripMenuItem";
            this.PriorityToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.PriorityToolStripMenuItem.Text = "Настройка выбираемых приоритетов";
            this.PriorityToolStripMenuItem.Click += new System.EventHandler(this.PriorityToolStripMenuItem_Click);
            // 
            // QualificationsToolStripMenuItem
            // 
            this.QualificationsToolStripMenuItem.Name = "QualificationsToolStripMenuItem";
            this.QualificationsToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.QualificationsToolStripMenuItem.Text = "Настройка списка квалификаций";
            this.QualificationsToolStripMenuItem.Click += new System.EventHandler(this.QualificationsToolStripMenuItem_Click);
            // 
            // DirectorToolStripMenuItem
            // 
            this.DirectorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AnalisisToWorkToolStripMenuItem,
            this.AssigmentTaskToolStripMenuItem});
            this.DirectorToolStripMenuItem.Name = "DirectorToolStripMenuItem";
            this.DirectorToolStripMenuItem.Size = new System.Drawing.Size(155, 20);
            this.DirectorToolStripMenuItem.Text = "Возможности директора";
            // 
            // AnalisisToWorkToolStripMenuItem
            // 
            this.AnalisisToWorkToolStripMenuItem.Name = "AnalisisToWorkToolStripMenuItem";
            this.AnalisisToWorkToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.AnalisisToWorkToolStripMenuItem.Text = "Анализ работы";
            this.AnalisisToWorkToolStripMenuItem.Click += new System.EventHandler(this.AnalisisToWorkToolStripMenuItem_Click);
            // 
            // AssigmentTaskToolStripMenuItem
            // 
            this.AssigmentTaskToolStripMenuItem.Name = "AssigmentTaskToolStripMenuItem";
            this.AssigmentTaskToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.AssigmentTaskToolStripMenuItem.Text = "Назначение заданий";
            this.AssigmentTaskToolStripMenuItem.Click += new System.EventHandler(this.AssigmentTaskToolStripMenuItem_Click_1);
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem,
            this.ExitUserAccountToolStripMenuItem});
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ExitToolStripMenuItem.Text = "Выход из приложения";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ExitUserAccountToolStripMenuItem
            // 
            this.ExitUserAccountToolStripMenuItem.Name = "ExitUserAccountToolStripMenuItem";
            this.ExitUserAccountToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ExitUserAccountToolStripMenuItem.Text = "Выход из учетной записи";
            this.ExitUserAccountToolStripMenuItem.Click += new System.EventHandler(this.ExitUserAccountToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolDataToolStripMenuItem,
            this.DirectorToolStripMenuItem,
            this.TaskEmployeeToolStripMenuItem,
            this.инструментыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(687, 24);
            this.menuStrip1.TabIndex = 119;
            this.menuStrip1.Text = "awdawd";
            // 
            // TaskEmployeeToolStripMenuItem
            // 
            this.TaskEmployeeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewYouListTaskToolStripMenuItem});
            this.TaskEmployeeToolStripMenuItem.Name = "TaskEmployeeToolStripMenuItem";
            this.TaskEmployeeToolStripMenuItem.Size = new System.Drawing.Size(161, 20);
            this.TaskEmployeeToolStripMenuItem.Text = "Возможности сотрудника";
            // 
            // ViewYouListTaskToolStripMenuItem
            // 
            this.ViewYouListTaskToolStripMenuItem.Name = "ViewYouListTaskToolStripMenuItem";
            this.ViewYouListTaskToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.ViewYouListTaskToolStripMenuItem.Text = "Просмотреть список задач";
            this.ViewYouListTaskToolStripMenuItem.Click += new System.EventHandler(this.ViewYouListTaskToolStripMenuItem_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 311);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное окно программы";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.ToolStripMenuItem ToolDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewListStudentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QualificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalisisToWorkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AssigmentTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitUserAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewYouListTaskToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem TaskEmployeeToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DirectorToolStripMenuItem;
    }
}