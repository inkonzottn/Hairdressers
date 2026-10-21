namespace Hairdressers
{
    partial class LoginForm
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
            this.btnModeClient = new System.Windows.Forms.Button();
            this.btnModeStaff = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelStaff = new System.Windows.Forms.Panel();
            this.lblStaffLogin = new System.Windows.Forms.Label();
            this.txtStaffLogin = new System.Windows.Forms.TextBox();
            this.txtStaffPassword = new System.Windows.Forms.TextBox();
            this.lblStaffPassword = new System.Windows.Forms.Label();
            this.panelClient = new System.Windows.Forms.Panel();
            this.txtClientLastName = new System.Windows.Forms.TextBox();
            this.lblClientLastName = new System.Windows.Forms.Label();
            this.lblClientPhone = new System.Windows.Forms.Label();
            this.lblClientFirstName = new System.Windows.Forms.Label();
            this.txtClientPhone = new System.Windows.Forms.MaskedTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtClientFirstName = new System.Windows.Forms.TextBox();
            this.panelStaff.SuspendLayout();
            this.panelClient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnModeClient
            // 
            this.btnModeClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModeClient.Location = new System.Drawing.Point(0, 0);
            this.btnModeClient.Name = "btnModeClient";
            this.btnModeClient.Size = new System.Drawing.Size(431, 60);
            this.btnModeClient.TabIndex = 0;
            this.btnModeClient.Text = "КЛІЄНТ";
            this.btnModeClient.UseVisualStyleBackColor = true;
            this.btnModeClient.Click += new System.EventHandler(this.btnModeClient_Click);
            // 
            // btnModeStaff
            // 
            this.btnModeStaff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModeStaff.Location = new System.Drawing.Point(429, 0);
            this.btnModeStaff.Name = "btnModeStaff";
            this.btnModeStaff.Size = new System.Drawing.Size(412, 60);
            this.btnModeStaff.TabIndex = 1;
            this.btnModeStaff.Text = "ПЕРСОНАЛ";
            this.btnModeStaff.UseVisualStyleBackColor = true;
            this.btnModeStaff.Click += new System.EventHandler(this.btnModeStaff_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Location = new System.Drawing.Point(0, 98);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size(841, 42);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Вхід для Персоналу";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStaff
            // 
            this.panelStaff.Controls.Add(this.txtStaffPassword);
            this.panelStaff.Controls.Add(this.lblStaffPassword);
            this.panelStaff.Controls.Add(this.txtStaffLogin);
            this.panelStaff.Controls.Add(this.lblStaffLogin);
            this.panelStaff.Location = new System.Drawing.Point(3, 220);
            this.panelStaff.Name = "panelStaff";
            this.panelStaff.Size = new System.Drawing.Size(838, 294);
            this.panelStaff.TabIndex = 4;
            // 
            // lblStaffLogin
            // 
            this.lblStaffLogin.AutoSize = true;
            this.lblStaffLogin.Location = new System.Drawing.Point(7, 23);
            this.lblStaffLogin.Name = "lblStaffLogin";
            this.lblStaffLogin.Size = new System.Drawing.Size(108, 42);
            this.lblStaffLogin.TabIndex = 0;
            this.lblStaffLogin.Text = "Логін";
            // 
            // txtStaffLogin
            // 
            this.txtStaffLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStaffLogin.Location = new System.Drawing.Point(2, 77);
            this.txtStaffLogin.Name = "txtStaffLogin";
            this.txtStaffLogin.Size = new System.Drawing.Size(833, 49);
            this.txtStaffLogin.TabIndex = 1;
            // 
            // txtStaffPassword
            // 
            this.txtStaffPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStaffPassword.Location = new System.Drawing.Point(2, 227);
            this.txtStaffPassword.Name = "txtStaffPassword";
            this.txtStaffPassword.Size = new System.Drawing.Size(833, 49);
            this.txtStaffPassword.TabIndex = 3;
            this.txtStaffPassword.UseSystemPasswordChar = true;
            // 
            // lblStaffPassword
            // 
            this.lblStaffPassword.AutoSize = true;
            this.lblStaffPassword.Location = new System.Drawing.Point(7, 175);
            this.lblStaffPassword.Name = "lblStaffPassword";
            this.lblStaffPassword.Size = new System.Drawing.Size(148, 42);
            this.lblStaffPassword.TabIndex = 2;
            this.lblStaffPassword.Text = "Пароль";
            // 
            // panelClient
            // 
            this.panelClient.Controls.Add(this.lblClientPhone);
            this.panelClient.Controls.Add(this.txtClientPhone);
            this.panelClient.Controls.Add(this.txtClientFirstName);
            this.panelClient.Controls.Add(this.lblClientFirstName);
            this.panelClient.Controls.Add(this.txtClientLastName);
            this.panelClient.Controls.Add(this.lblClientLastName);
            this.panelClient.Location = new System.Drawing.Point(3, 174);
            this.panelClient.Name = "panelClient";
            this.panelClient.Size = new System.Drawing.Size(838, 414);
            this.panelClient.TabIndex = 5;
            // 
            // txtClientLastName
            // 
            this.txtClientLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientLastName.Location = new System.Drawing.Point(0, 214);
            this.txtClientLastName.Name = "txtClientLastName";
            this.txtClientLastName.Size = new System.Drawing.Size(835, 49);
            this.txtClientLastName.TabIndex = 3;
            // 
            // lblClientLastName
            // 
            this.lblClientLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClientLastName.AutoSize = true;
            this.lblClientLastName.Location = new System.Drawing.Point(5, 157);
            this.lblClientLastName.Name = "lblClientLastName";
            this.lblClientLastName.Size = new System.Drawing.Size(185, 42);
            this.lblClientLastName.TabIndex = 2;
            this.lblClientLastName.Text = "Прізвище";
            // 
            // lblClientPhone
            // 
            this.lblClientPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClientPhone.AutoSize = true;
            this.lblClientPhone.Location = new System.Drawing.Point(5, 6);
            this.lblClientPhone.Name = "lblClientPhone";
            this.lblClientPhone.Size = new System.Drawing.Size(316, 42);
            this.lblClientPhone.TabIndex = 0;
            this.lblClientPhone.Text = "Номер телефону";
            // 
            // lblClientFirstName
            // 
            this.lblClientFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClientFirstName.AutoSize = true;
            this.lblClientFirstName.Location = new System.Drawing.Point(5, 298);
            this.lblClientFirstName.Name = "lblClientFirstName";
            this.lblClientFirstName.Size = new System.Drawing.Size(80, 42);
            this.lblClientFirstName.TabIndex = 4;
            this.lblClientFirstName.Text = "Ім\'я";
            // 
            // txtClientPhone
            // 
            this.txtClientPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientPhone.Location = new System.Drawing.Point(0, 62);
            this.txtClientPhone.Mask = "+38 (099) 999-99-99";
            this.txtClientPhone.Name = "txtClientPhone";
            this.txtClientPhone.Size = new System.Drawing.Size(835, 49);
            this.txtClientPhone.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(-1, 659);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(842, 68);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "ВХІД";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.panelClient);
            this.panel1.Controls.Add(this.panelStaff);
            this.panel1.Controls.Add(this.btnModeClient);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.btnModeStaff);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(386, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 727);
            this.panel1.TabIndex = 6;
            // 
            // txtClientFirstName
            // 
            this.txtClientFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientFirstName.Location = new System.Drawing.Point(0, 355);
            this.txtClientFirstName.Name = "txtClientFirstName";
            this.txtClientFirstName.Size = new System.Drawing.Size(835, 49);
            this.txtClientFirstName.TabIndex = 5;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 939);
            this.Controls.Add(this.panel1);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelStaff.ResumeLayout(false);
            this.panelStaff.PerformLayout();
            this.panelClient.ResumeLayout(false);
            this.panelClient.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnModeClient;
        private System.Windows.Forms.Button btnModeStaff;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelStaff;
        private System.Windows.Forms.Label lblStaffLogin;
        private System.Windows.Forms.TextBox txtStaffLogin;
        private System.Windows.Forms.TextBox txtStaffPassword;
        private System.Windows.Forms.Label lblStaffPassword;
        private System.Windows.Forms.Panel panelClient;
        private System.Windows.Forms.Label lblClientFirstName;
        private System.Windows.Forms.TextBox txtClientLastName;
        private System.Windows.Forms.Label lblClientLastName;
        private System.Windows.Forms.Label lblClientPhone;
        private System.Windows.Forms.MaskedTextBox txtClientPhone;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtClientFirstName;
    }
}

