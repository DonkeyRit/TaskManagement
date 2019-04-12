namespace DepartmentEmployee.GUI.ControlWindows
{
    partial class Authorization
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
			this.LoginButton = new System.Windows.Forms.Button();
			this.LoginLabel = new System.Windows.Forms.Label();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.LoginField = new System.Windows.Forms.TextBox();
			this.PasswordField = new System.Windows.Forms.TextBox();
			this.ShowPasswordOption = new System.Windows.Forms.CheckBox();
			this.ExitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LoginButton
			// 
			this.LoginButton.Location = new System.Drawing.Point(36, 95);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(157, 23);
			this.LoginButton.TabIndex = 0;
			this.LoginButton.Text = "Войти в систему";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
			// 
			// LoginLabel
			// 
			this.LoginLabel.AutoSize = true;
			this.LoginLabel.Location = new System.Drawing.Point(19, 25);
			this.LoginLabel.Name = "LoginLabel";
			this.LoginLabel.Size = new System.Drawing.Size(38, 13);
			this.LoginLabel.TabIndex = 2;
			this.LoginLabel.Text = "Логин";
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.AutoSize = true;
			this.PasswordLabel.Location = new System.Drawing.Point(19, 55);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(45, 13);
			this.PasswordLabel.TabIndex = 3;
			this.PasswordLabel.Text = "Пароль";
			// 
			// LoginField
			// 
			this.LoginField.Location = new System.Drawing.Point(101, 22);
			this.LoginField.Name = "LoginField";
			this.LoginField.Size = new System.Drawing.Size(283, 20);
			this.LoginField.TabIndex = 4;
			this.LoginField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputField_KeyDown);
			// 
			// PasswordField
			// 
			this.PasswordField.Location = new System.Drawing.Point(101, 48);
			this.PasswordField.Name = "PasswordField";
			this.PasswordField.Size = new System.Drawing.Size(283, 20);
			this.PasswordField.TabIndex = 5;
			this.PasswordField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordField_KeyDown);
			// 
			// ShowPasswordOption
			// 
			this.ShowPasswordOption.AutoSize = true;
			this.ShowPasswordOption.Location = new System.Drawing.Point(270, 74);
			this.ShowPasswordOption.Name = "ShowPasswordOption";
			this.ShowPasswordOption.Size = new System.Drawing.Size(114, 17);
			this.ShowPasswordOption.TabIndex = 6;
			this.ShowPasswordOption.Text = "Показать пароль";
			this.ShowPasswordOption.UseVisualStyleBackColor = true;
			this.ShowPasswordOption.CheckedChanged += new System.EventHandler(this.ShowPasswordOption_CheckedChanged);
			// 
			// ExitButton
			// 
			this.ExitButton.Location = new System.Drawing.Point(199, 97);
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.Size = new System.Drawing.Size(157, 23);
			this.ExitButton.TabIndex = 7;
			this.ExitButton.Text = "Выйти из программы";
			this.ExitButton.UseVisualStyleBackColor = true;
			this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
			// 
			// Authorization
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(398, 133);
			this.Controls.Add(this.ExitButton);
			this.Controls.Add(this.ShowPasswordOption);
			this.Controls.Add(this.PasswordField);
			this.Controls.Add(this.LoginField);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.LoginLabel);
			this.Controls.Add(this.LoginButton);
			this.Name = "Authorization";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Окно авторизации";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.CheckBox ShowPasswordOption;
        private System.Windows.Forms.Button ExitButton;
        public System.Windows.Forms.TextBox LoginField;
        public System.Windows.Forms.TextBox PasswordField;
    }
}