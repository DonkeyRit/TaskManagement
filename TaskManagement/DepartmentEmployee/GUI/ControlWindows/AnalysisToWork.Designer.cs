namespace DepartmentEmployee.GUI.ControlWindows
{
    partial class AnalysisToWork
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisToWork));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowExpiredTasks = new System.Windows.Forms.Button();
            this.ShowYoungTasks = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ShowTaskResults = new System.Windows.Forms.Button();
            this.TableContent = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TOOLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackwardToMainformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableContent)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShowExpiredTasks);
            this.groupBox1.Controls.Add(this.ShowYoungTasks);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 463);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Операции";
            // 
            // ShowExpiredTasks
            // 
            this.ShowExpiredTasks.Location = new System.Drawing.Point(6, 96);
            this.ShowExpiredTasks.Name = "ShowExpiredTasks";
            this.ShowExpiredTasks.Size = new System.Drawing.Size(187, 35);
            this.ShowExpiredTasks.TabIndex = 12;
            this.ShowExpiredTasks.Text = "Просроченные задания";
            this.ShowExpiredTasks.UseVisualStyleBackColor = true;
            this.ShowExpiredTasks.Click += new System.EventHandler(this.button5_Click);
            // 
            // ShowYoungTasks
            // 
            this.ShowYoungTasks.Location = new System.Drawing.Point(6, 37);
            this.ShowYoungTasks.Name = "ShowYoungTasks";
            this.ShowYoungTasks.Size = new System.Drawing.Size(187, 34);
            this.ShowYoungTasks.TabIndex = 11;
            this.ShowYoungTasks.Text = "Выданные задания у которых срок исполнения менее недели";
            this.ShowYoungTasks.UseVisualStyleBackColor = true;
            this.ShowYoungTasks.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(437, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(546, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // ShowTaskResults
            // 
            this.ShowTaskResults.Location = new System.Drawing.Point(226, 31);
            this.ShowTaskResults.Name = "ShowTaskResults";
            this.ShowTaskResults.Size = new System.Drawing.Size(175, 50);
            this.ShowTaskResults.TabIndex = 11;
            this.ShowTaskResults.Text = "Показать задания и результаты работы сотрудников";
            this.ShowTaskResults.UseVisualStyleBackColor = true;
            this.ShowTaskResults.Click += new System.EventHandler(this.button3_Click);
            // 
            // TableContent
            // 
            this.TableContent.AllowUserToAddRows = false;
            this.TableContent.AllowUserToDeleteRows = false;
            this.TableContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TableContent.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TableContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TableContent.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TableContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TableContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TableContent.DefaultCellStyle = dataGridViewCellStyle2;
            this.TableContent.Location = new System.Drawing.Point(216, 103);
            this.TableContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TableContent.Name = "TableContent";
            this.TableContent.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TableContent.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.TableContent.RowHeadersVisible = false;
            this.TableContent.RowTemplate.Height = 24;
            this.TableContent.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TableContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TableContent.Size = new System.Drawing.Size(767, 392);
            this.TableContent.TabIndex = 10;
            this.TableContent.Tag = "1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TOOLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TOOLToolStripMenuItem
            // 
            this.TOOLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackwardToMainformToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.TOOLToolStripMenuItem.Name = "TOOLToolStripMenuItem";
            this.TOOLToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.TOOLToolStripMenuItem.Text = "Инструменты";
            // 
            // BackwardToMainformToolStripMenuItem
            // 
            this.BackwardToMainformToolStripMenuItem.Name = "BackwardToMainformToolStripMenuItem";
            this.BackwardToMainformToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.BackwardToMainformToolStripMenuItem.Text = "Вернуться на главную страницу";
            this.BackwardToMainformToolStripMenuItem.Click += new System.EventHandler(this.BackwardToMainformToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.ExitToolStripMenuItem.Text = "Выйти из рограммы";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // AnalysisToWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 518);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ShowTaskResults);
            this.Controls.Add(this.TableContent);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnalysisToWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Анализ работы программы";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableContent)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button ShowTaskResults;
        internal System.Windows.Forms.DataGridView TableContent;
        private System.Windows.Forms.Button ShowYoungTasks;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TOOLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackwardToMainformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Button ShowExpiredTasks;
    }
}