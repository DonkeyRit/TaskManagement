namespace DepartmentEmployee.GUI.ControlWindows
{
    partial class AssignmentTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignmentTask));
            this.Button1 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.редактированиеБазыЗнаниToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackwardToMainformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button1.Location = new System.Drawing.Point(5, 28);
            this.Button1.Margin = new System.Windows.Forms.Padding(2);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(302, 23);
            this.Button1.TabIndex = 111;
            this.Button1.Text = "Добавить новое задание";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.Location = new System.Drawing.Point(334, 29);
            this.Button6.Margin = new System.Windows.Forms.Padding(2);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(146, 34);
            this.Button6.TabIndex = 116;
            this.Button6.Text = "Удалить назначение";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button3.Location = new System.Drawing.Point(158, 54);
            this.Button3.Margin = new System.Windows.Forms.Padding(2);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(149, 23);
            this.Button3.TabIndex = 112;
            this.Button3.Text = "Удалить задание";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.Location = new System.Drawing.Point(15, 29);
            this.Button4.Margin = new System.Windows.Forms.Padding(2);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(146, 34);
            this.Button4.TabIndex = 114;
            this.Button4.Text = "Назначить задание сотруднику";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button2.Location = new System.Drawing.Point(5, 54);
            this.Button2.Margin = new System.Windows.Forms.Padding(2);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(149, 23);
            this.Button2.TabIndex = 113;
            this.Button2.Text = "Редактировать задание";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.Location = new System.Drawing.Point(2, 16);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.RowTemplate.Height = 24;
            this.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(681, 400);
            this.DataGridView1.TabIndex = 9;
            this.DataGridView1.Tag = "1";
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
            this.DataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
            this.DataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridView1_KeyPress);
            this.DataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseMove);
            // 
            // TreeView1
            // 
            this.TreeView1.AllowDrop = true;
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.TreeView1.FullRowSelect = true;
            this.TreeView1.Location = new System.Drawing.Point(2, 16);
            this.TreeView1.Margin = new System.Windows.Forms.Padding(2);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(329, 401);
            this.TreeView1.TabIndex = 108;
            this.TreeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TreeView1_DrawNode);
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.TreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
            this.TreeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragDrop);
            this.TreeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragEnter);
            this.TreeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Location = new System.Drawing.Point(9, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.TreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.DataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 507);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 117;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Button3);
            this.groupBox2.Controls.Add(this.Button1);
            this.groupBox2.Controls.Add(this.Button2);
            this.groupBox2.Location = new System.Drawing.Point(9, 422);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 82);
            this.groupBox2.TabIndex = 118;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Операции со списком заданий";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.Button6);
            this.groupBox1.Controls.Add(this.Button4);
            this.groupBox1.Controls.Add(this.Button5);
            this.groupBox1.Location = new System.Drawing.Point(6, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 83);
            this.groupBox1.TabIndex = 117;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Операции со списком назначений";
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.Location = new System.Drawing.Point(175, 29);
            this.Button5.Margin = new System.Windows.Forms.Padding(2);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(146, 34);
            this.Button5.TabIndex = 115;
            this.Button5.Text = "Редактировать назначение";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // редактированиеБазыЗнаниToolStripMenuItem
            // 
            this.редактированиеБазыЗнаниToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackwardToMainformToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.редактированиеБазыЗнаниToolStripMenuItem.Name = "редактированиеБазыЗнаниToolStripMenuItem";
            this.редактированиеБазыЗнаниToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.редактированиеБазыЗнаниToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.редактированиеБазыЗнаниToolStripMenuItem.Text = "Инструменты";
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
            this.ExitToolStripMenuItem.Text = "Выйти из программы";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактированиеБазыЗнаниToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 118;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(494, 29);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(146, 34);
            this.button7.TabIndex = 117;
            this.button7.Text = "Узнать прогресс выполнения";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // AssigmentTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1020, 526);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AssigmentTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма назначения заданий сотрудникам";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TreeView TreeView1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Button Button5;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button Button6;
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem редактированиеБазыЗнаниToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackwardToMainformToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.Button Button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.Button button7;
    }
}

