namespace DepartmentEmployee.GUI.ModalWindows
{
    partial class AddEditTask
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
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(347, 511);
            this.Button2.Margin = new System.Windows.Forms.Padding(2);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(174, 26);
            this.Button2.TabIndex = 11;
            this.Button2.Text = "Отмена";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(124, 511);
            this.Button1.Margin = new System.Windows.Forms.Padding(2);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(178, 26);
            this.Button1.TabIndex = 10;
            this.Button1.Text = "ОК";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(194, 41);
            this.TextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox1.MaxLength = 50;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(429, 20);
            this.TextBox1.TabIndex = 9;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 41);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(145, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Введите название задания";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 40);
            this.label2.TabIndex = 12;
            this.label2.Text = "Введите трудоёмкость выполнения задания";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 190);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Введите описание";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(194, 103);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.MaxLength = 50;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(429, 20);
            this.textBox3.TabIndex = 15;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(194, 187);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(429, 96);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 344);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 30);
            this.label4.TabIndex = 17;
            this.label4.Text = "Введите планируемую дату закрытия";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(194, 344);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(429, 20);
            this.dateTimePicker1.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 424);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 30);
            this.label5.TabIndex = 19;
            this.label5.Text = "Выберите приоритет выполнения задания";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(194, 424);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(429, 21);
            this.comboBox1.TabIndex = 20;
            // 
            // AddEditTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 580);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddEditTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление и редактирование задания";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox textBox3;
        internal System.Windows.Forms.RichTextBox richTextBox1;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.ComboBox comboBox1;
    }
}