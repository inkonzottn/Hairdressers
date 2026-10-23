namespace Hairdressers
{
    partial class AdminMainForm
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
            this.tcAdmin = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tpMasters = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbMasterSpecialization = new System.Windows.Forms.ComboBox();
            this.txtMasterPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMasterLogin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMasterSearch = new System.Windows.Forms.Button();
            this.txtSearchMaster = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteMaster = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMasterFirstName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMasterLastName = new System.Windows.Forms.TextBox();
            this.btnUpdateMaster = new System.Windows.Forms.Button();
            this.btnAddMaster = new System.Windows.Forms.Button();
            this.dgvMasters = new System.Windows.Forms.DataGridView();
            this.tpClients = new System.Windows.Forms.TabPage();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtClientPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.btnSearchClient = new System.Windows.Forms.Button();
            this.txtSearchClient = new System.Windows.Forms.TextBox();
            this.lphoneNumber = new System.Windows.Forms.Label();
            this.btnDeleteClient = new System.Windows.Forms.Button();
            this.llastName = new System.Windows.Forms.Label();
            this.txtClientFirstName = new System.Windows.Forms.TextBox();
            this.lfirstName = new System.Windows.Forms.Label();
            this.txtClientLastName = new System.Windows.Forms.TextBox();
            this.btnUpdateClient = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.tpServices = new System.Windows.Forms.TabPage();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.clbMasterServices = new System.Windows.Forms.CheckedListBox();
            this.panelMainMasters = new System.Windows.Forms.Panel();
            this.tcAdmin.SuspendLayout();
            this.tpMasters.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasters)).BeginInit();
            this.tpClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            this.panel2.SuspendLayout();
            this.tpServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelMainMasters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcAdmin
            // 
            this.tcAdmin.Controls.Add(this.tabPage1);
            this.tcAdmin.Controls.Add(this.tpMasters);
            this.tcAdmin.Controls.Add(this.tpClients);
            this.tcAdmin.Controls.Add(this.tpServices);
            this.tcAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAdmin.Location = new System.Drawing.Point(0, 0);
            this.tcAdmin.Name = "tcAdmin";
            this.tcAdmin.SelectedIndex = 0;
            this.tcAdmin.Size = new System.Drawing.Size(1507, 939);
            this.tcAdmin.TabIndex = 0;
            this.tcAdmin.SelectedIndexChanged += new System.EventHandler(this.tcAdmin_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(8, 56);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1491, 875);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Розклад";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tpMasters
            // 
            this.tpMasters.Controls.Add(this.panel3);
            this.tpMasters.Controls.Add(this.dgvMasters);
            this.tpMasters.Location = new System.Drawing.Point(8, 56);
            this.tpMasters.Name = "tpMasters";
            this.tpMasters.Padding = new System.Windows.Forms.Padding(3);
            this.tpMasters.Size = new System.Drawing.Size(1491, 875);
            this.tpMasters.TabIndex = 1;
            this.tpMasters.Text = "Майстри";
            this.tpMasters.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.clbMasterServices);
            this.panel3.Controls.Add(this.panelMainMasters);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(715, 869);
            this.panel3.TabIndex = 13;
            // 
            // cmbMasterSpecialization
            // 
            this.cmbMasterSpecialization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMasterSpecialization.FormattingEnabled = true;
            this.cmbMasterSpecialization.Location = new System.Drawing.Point(337, 250);
            this.cmbMasterSpecialization.Name = "cmbMasterSpecialization";
            this.cmbMasterSpecialization.Size = new System.Drawing.Size(382, 45);
            this.cmbMasterSpecialization.TabIndex = 17;
            // 
            // txtMasterPassword
            // 
            this.txtMasterPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMasterPassword.Location = new System.Drawing.Point(337, 387);
            this.txtMasterPassword.Name = "txtMasterPassword";
            this.txtMasterPassword.Size = new System.Drawing.Size(378, 44);
            this.txtMasterPassword.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(30, 394);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 37);
            this.label8.TabIndex = 15;
            this.label8.Text = "Пароль";
            // 
            // txtMasterLogin
            // 
            this.txtMasterLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMasterLogin.Location = new System.Drawing.Point(337, 322);
            this.txtMasterLogin.Name = "txtMasterLogin";
            this.txtMasterLogin.Size = new System.Drawing.Size(378, 44);
            this.txtMasterLogin.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(30, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 37);
            this.label7.TabIndex = 13;
            this.label7.Text = "Логін";
            // 
            // btnMasterSearch
            // 
            this.btnMasterSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasterSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMasterSearch.Location = new System.Drawing.Point(470, 32);
            this.btnMasterSearch.Name = "btnMasterSearch";
            this.btnMasterSearch.Size = new System.Drawing.Size(205, 63);
            this.btnMasterSearch.TabIndex = 11;
            this.btnMasterSearch.Text = "Пошук";
            this.btnMasterSearch.UseVisualStyleBackColor = true;
            this.btnMasterSearch.Click += new System.EventHandler(this.btnSearchMaster_Click);
            // 
            // txtSearchMaster
            // 
            this.txtSearchMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearchMaster.Location = new System.Drawing.Point(36, 42);
            this.txtSearchMaster.Name = "txtSearchMaster";
            this.txtSearchMaster.Size = new System.Drawing.Size(378, 44);
            this.txtSearchMaster.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(30, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 37);
            this.label4.TabIndex = 9;
            this.label4.Text = "Спеціалізація";
            // 
            // btnDeleteMaster
            // 
            this.btnDeleteMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeleteMaster.Location = new System.Drawing.Point(491, 458);
            this.btnDeleteMaster.Name = "btnDeleteMaster";
            this.btnDeleteMaster.Size = new System.Drawing.Size(205, 63);
            this.btnDeleteMaster.TabIndex = 6;
            this.btnDeleteMaster.Text = "Видалити";
            this.btnDeleteMaster.UseVisualStyleBackColor = true;
            this.btnDeleteMaster.Click += new System.EventHandler(this.btnDeleteMaster_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(30, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 37);
            this.label5.TabIndex = 8;
            this.label5.Text = "Прізвище";
            // 
            // txtMasterFirstName
            // 
            this.txtMasterFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMasterFirstName.Location = new System.Drawing.Point(337, 119);
            this.txtMasterFirstName.Name = "txtMasterFirstName";
            this.txtMasterFirstName.Size = new System.Drawing.Size(378, 44);
            this.txtMasterFirstName.TabIndex = 1;
            this.txtMasterFirstName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyLetters_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(30, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 37);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ім\'я";
            // 
            // txtMasterLastName
            // 
            this.txtMasterLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMasterLastName.Location = new System.Drawing.Point(337, 182);
            this.txtMasterLastName.Name = "txtMasterLastName";
            this.txtMasterLastName.Size = new System.Drawing.Size(378, 44);
            this.txtMasterLastName.TabIndex = 2;
            this.txtMasterLastName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyLetters_KeyPress);
            // 
            // btnUpdateMaster
            // 
            this.btnUpdateMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdateMaster.Location = new System.Drawing.Point(261, 458);
            this.btnUpdateMaster.Name = "btnUpdateMaster";
            this.btnUpdateMaster.Size = new System.Drawing.Size(205, 63);
            this.btnUpdateMaster.TabIndex = 5;
            this.btnUpdateMaster.Text = "Редагувати";
            this.btnUpdateMaster.UseVisualStyleBackColor = true;
            this.btnUpdateMaster.Click += new System.EventHandler(this.btnUpdateMaster_Click);
            // 
            // btnAddMaster
            // 
            this.btnAddMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddMaster.Location = new System.Drawing.Point(20, 458);
            this.btnAddMaster.Name = "btnAddMaster";
            this.btnAddMaster.Size = new System.Drawing.Size(205, 63);
            this.btnAddMaster.TabIndex = 4;
            this.btnAddMaster.Text = "Додати";
            this.btnAddMaster.UseVisualStyleBackColor = true;
            this.btnAddMaster.Click += new System.EventHandler(this.btnAddMaster_Click);
            // 
            // dgvMasters
            // 
            this.dgvMasters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMasters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasters.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvMasters.Location = new System.Drawing.Point(725, 3);
            this.dgvMasters.Name = "dgvMasters";
            this.dgvMasters.RowHeadersWidth = 82;
            this.dgvMasters.RowTemplate.Height = 33;
            this.dgvMasters.Size = new System.Drawing.Size(763, 869);
            this.dgvMasters.TabIndex = 12;
            this.dgvMasters.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMasters_CellClick);
            // 
            // tpClients
            // 
            this.tpClients.Controls.Add(this.dgvClients);
            this.tpClients.Controls.Add(this.panel2);
            this.tpClients.Location = new System.Drawing.Point(8, 56);
            this.tpClients.Name = "tpClients";
            this.tpClients.Size = new System.Drawing.Size(1491, 875);
            this.tpClients.TabIndex = 2;
            this.tpClients.Text = "Клієнти";
            this.tpClients.UseVisualStyleBackColor = true;
            // 
            // dgvClients
            // 
            this.dgvClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvClients.Location = new System.Drawing.Point(728, 0);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.RowHeadersWidth = 82;
            this.dgvClients.RowTemplate.Height = 33;
            this.dgvClients.Size = new System.Drawing.Size(763, 875);
            this.dgvClients.TabIndex = 11;
            this.dgvClients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClients_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtClientPhoneNumber);
            this.panel2.Controls.Add(this.btnSearchClient);
            this.panel2.Controls.Add(this.txtSearchClient);
            this.panel2.Controls.Add(this.lphoneNumber);
            this.panel2.Controls.Add(this.btnDeleteClient);
            this.panel2.Controls.Add(this.llastName);
            this.panel2.Controls.Add(this.txtClientFirstName);
            this.panel2.Controls.Add(this.lfirstName);
            this.panel2.Controls.Add(this.txtClientLastName);
            this.panel2.Controls.Add(this.btnUpdateClient);
            this.panel2.Controls.Add(this.btnAddClient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(715, 875);
            this.panel2.TabIndex = 12;
            // 
            // txtClientPhoneNumber
            // 
            this.txtClientPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtClientPhoneNumber.Location = new System.Drawing.Point(334, 357);
            this.txtClientPhoneNumber.Mask = "+38 (000) 000-00-00";
            this.txtClientPhoneNumber.Name = "txtClientPhoneNumber";
            this.txtClientPhoneNumber.Size = new System.Drawing.Size(378, 44);
            this.txtClientPhoneNumber.TabIndex = 12;
            // 
            // btnSearchClient
            // 
            this.btnSearchClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearchClient.Location = new System.Drawing.Point(469, 42);
            this.btnSearchClient.Name = "btnSearchClient";
            this.btnSearchClient.Size = new System.Drawing.Size(205, 63);
            this.btnSearchClient.TabIndex = 11;
            this.btnSearchClient.Text = "Пошук";
            this.btnSearchClient.UseVisualStyleBackColor = true;
            this.btnSearchClient.Click += new System.EventHandler(this.btnSearchClient_Click);
            // 
            // txtSearchClient
            // 
            this.txtSearchClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearchClient.Location = new System.Drawing.Point(41, 52);
            this.txtSearchClient.Name = "txtSearchClient";
            this.txtSearchClient.Size = new System.Drawing.Size(378, 44);
            this.txtSearchClient.TabIndex = 10;
            // 
            // lphoneNumber
            // 
            this.lphoneNumber.AutoSize = true;
            this.lphoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lphoneNumber.Location = new System.Drawing.Point(33, 364);
            this.lphoneNumber.Name = "lphoneNumber";
            this.lphoneNumber.Size = new System.Drawing.Size(141, 37);
            this.lphoneNumber.TabIndex = 9;
            this.lphoneNumber.Text = "Телефон";
            // 
            // btnDeleteClient
            // 
            this.btnDeleteClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeleteClient.Location = new System.Drawing.Point(40, 653);
            this.btnDeleteClient.Name = "btnDeleteClient";
            this.btnDeleteClient.Size = new System.Drawing.Size(205, 63);
            this.btnDeleteClient.TabIndex = 6;
            this.btnDeleteClient.Text = "Видалити";
            this.btnDeleteClient.UseVisualStyleBackColor = true;
            this.btnDeleteClient.Click += new System.EventHandler(this.btnDeleteClient_Click);
            // 
            // llastName
            // 
            this.llastName.AutoSize = true;
            this.llastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.llastName.Location = new System.Drawing.Point(33, 270);
            this.llastName.Name = "llastName";
            this.llastName.Size = new System.Drawing.Size(147, 37);
            this.llastName.TabIndex = 8;
            this.llastName.Text = "Прізвище";
            // 
            // txtClientFirstName
            // 
            this.txtClientFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtClientFirstName.Location = new System.Drawing.Point(334, 173);
            this.txtClientFirstName.Name = "txtClientFirstName";
            this.txtClientFirstName.Size = new System.Drawing.Size(378, 44);
            this.txtClientFirstName.TabIndex = 1;
            this.txtClientFirstName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyLetters_KeyPress);
            // 
            // lfirstName
            // 
            this.lfirstName.AutoSize = true;
            this.lfirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lfirstName.Location = new System.Drawing.Point(33, 184);
            this.lfirstName.Name = "lfirstName";
            this.lfirstName.Size = new System.Drawing.Size(69, 37);
            this.lfirstName.TabIndex = 7;
            this.lfirstName.Text = "Ім\'я";
            // 
            // txtClientLastName
            // 
            this.txtClientLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtClientLastName.Location = new System.Drawing.Point(334, 259);
            this.txtClientLastName.Name = "txtClientLastName";
            this.txtClientLastName.Size = new System.Drawing.Size(378, 44);
            this.txtClientLastName.TabIndex = 2;
            this.txtClientLastName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyLetters_KeyPress);
            // 
            // btnUpdateClient
            // 
            this.btnUpdateClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdateClient.Location = new System.Drawing.Point(40, 557);
            this.btnUpdateClient.Name = "btnUpdateClient";
            this.btnUpdateClient.Size = new System.Drawing.Size(205, 63);
            this.btnUpdateClient.TabIndex = 5;
            this.btnUpdateClient.Text = "Редагувати";
            this.btnUpdateClient.UseVisualStyleBackColor = true;
            this.btnUpdateClient.Click += new System.EventHandler(this.btnUpdateClient_Click);
            // 
            // btnAddClient
            // 
            this.btnAddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddClient.Location = new System.Drawing.Point(40, 455);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(205, 63);
            this.btnAddClient.TabIndex = 4;
            this.btnAddClient.Text = "Додати";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
            // 
            // tpServices
            // 
            this.tpServices.Controls.Add(this.dgvServices);
            this.tpServices.Controls.Add(this.panel1);
            this.tpServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tpServices.Location = new System.Drawing.Point(8, 56);
            this.tpServices.Name = "tpServices";
            this.tpServices.Size = new System.Drawing.Size(1491, 875);
            this.tpServices.TabIndex = 3;
            this.tpServices.Text = "Послуги";
            this.tpServices.UseVisualStyleBackColor = true;
            // 
            // dgvServices
            // 
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvServices.Location = new System.Drawing.Point(728, 0);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.RowHeadersWidth = 82;
            this.dgvServices.RowTemplate.Height = 33;
            this.dgvServices.Size = new System.Drawing.Size(763, 875);
            this.dgvServices.TabIndex = 0;
            this.dgvServices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServices_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDuration);
            this.panel1.Controls.Add(this.txtPrice);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 875);
            this.panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(33, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 37);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ціна";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelete.Location = new System.Drawing.Point(25, 549);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(205, 63);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Видалити";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(33, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 37);
            this.label2.TabIndex = 8;
            this.label2.Text = "Тривалість, хв";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtName.Location = new System.Drawing.Point(334, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(378, 44);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(33, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "Назва";
            // 
            // txtDuration
            // 
            this.txtDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDuration.Location = new System.Drawing.Point(334, 119);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(378, 44);
            this.txtDuration.TabIndex = 2;
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPrice.Location = new System.Drawing.Point(334, 217);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(378, 44);
            this.txtPrice.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.Location = new System.Drawing.Point(25, 453);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(205, 63);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Редагувати";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(25, 351);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(205, 63);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Додати";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // clbMasterServices
            // 
            this.clbMasterServices.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.clbMasterServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clbMasterServices.FormattingEnabled = true;
            this.clbMasterServices.Location = new System.Drawing.Point(0, 742);
            this.clbMasterServices.Name = "clbMasterServices";
            this.clbMasterServices.Size = new System.Drawing.Size(715, 127);
            this.clbMasterServices.TabIndex = 18;
            // 
            // panelMainMasters
            // 
            this.panelMainMasters.Controls.Add(this.btnDeleteMaster);
            this.panelMainMasters.Controls.Add(this.cmbMasterSpecialization);
            this.panelMainMasters.Controls.Add(this.btnAddMaster);
            this.panelMainMasters.Controls.Add(this.txtMasterPassword);
            this.panelMainMasters.Controls.Add(this.btnUpdateMaster);
            this.panelMainMasters.Controls.Add(this.label8);
            this.panelMainMasters.Controls.Add(this.txtMasterLastName);
            this.panelMainMasters.Controls.Add(this.txtMasterLogin);
            this.panelMainMasters.Controls.Add(this.label6);
            this.panelMainMasters.Controls.Add(this.label7);
            this.panelMainMasters.Controls.Add(this.txtMasterFirstName);
            this.panelMainMasters.Controls.Add(this.btnMasterSearch);
            this.panelMainMasters.Controls.Add(this.label5);
            this.panelMainMasters.Controls.Add(this.txtSearchMaster);
            this.panelMainMasters.Controls.Add(this.label4);
            this.panelMainMasters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMainMasters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelMainMasters.Location = new System.Drawing.Point(0, 0);
            this.panelMainMasters.Name = "panelMainMasters";
            this.panelMainMasters.Size = new System.Drawing.Size(715, 591);
            this.panelMainMasters.TabIndex = 19;
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(22F, 42F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 939);
            this.Controls.Add(this.tcAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tcAdmin.ResumeLayout(false);
            this.tpMasters.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasters)).EndInit();
            this.tpClients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tpServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelMainMasters.ResumeLayout(false);
            this.panelMainMasters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcAdmin;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpMasters;
        private System.Windows.Forms.TabPage tpClients;
        private System.Windows.Forms.TabPage tpServices;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lphoneNumber;
        private System.Windows.Forms.Button btnDeleteClient;
        private System.Windows.Forms.Label llastName;
        private System.Windows.Forms.TextBox txtClientFirstName;
        private System.Windows.Forms.Label lfirstName;
        private System.Windows.Forms.TextBox txtClientLastName;
        private System.Windows.Forms.Button btnUpdateClient;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Button btnSearchClient;
        private System.Windows.Forms.TextBox txtSearchClient;
        private System.Windows.Forms.MaskedTextBox txtClientPhoneNumber;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMasterSearch;
        private System.Windows.Forms.TextBox txtSearchMaster;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDeleteMaster;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMasterFirstName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMasterLastName;
        private System.Windows.Forms.Button btnUpdateMaster;
        private System.Windows.Forms.Button btnAddMaster;
        private System.Windows.Forms.DataGridView dgvMasters;
        private System.Windows.Forms.TextBox txtMasterPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMasterLogin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbMasterSpecialization;
        private System.Windows.Forms.CheckedListBox clbMasterServices;
        private System.Windows.Forms.Panel panelMainMasters;
    }
}