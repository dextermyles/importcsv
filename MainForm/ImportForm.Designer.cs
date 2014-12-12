namespace SharepointListImport
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.lblSiteUrl = new System.Windows.Forms.Label();
            this.gbValidate = new System.Windows.Forms.GroupBox();
            this.lblNumRecords = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnValidateFile = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtImportFilename = new System.Windows.Forms.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbImport = new System.Windows.Forms.GroupBox();
            this.dgvMappings = new System.Windows.Forms.DataGridView();
            this.Include = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Mapping = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblMappings = new System.Windows.Forms.Label();
            this.cbListname = new System.Windows.Forms.ComboBox();
            this.lblListname = new System.Windows.Forms.Label();
            this.gbLogin.SuspendLayout();
            this.gbValidate.SuspendLayout();
            this.gbImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.txtDomain);
            this.gbLogin.Controls.Add(this.txtPassword);
            this.gbLogin.Controls.Add(this.txtUsername);
            this.gbLogin.Controls.Add(this.label5);
            this.gbLogin.Controls.Add(this.lblPassword);
            this.gbLogin.Controls.Add(this.lblUsername);
            this.gbLogin.Controls.Add(this.btnConnect);
            this.gbLogin.Controls.Add(this.txtSiteUrl);
            this.gbLogin.Controls.Add(this.lblSiteUrl);
            this.gbLogin.Location = new System.Drawing.Point(12, 7);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(398, 165);
            this.gbLogin.TabIndex = 0;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Sharepoint Details";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(95, 106);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(140, 23);
            this.txtDomain.TabIndex = 4;
            this.txtDomain.Text = "opsba0";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(95, 77);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 23);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(96, 48);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(140, 23);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Text = "elvis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Domain:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 16);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 51);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(71, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(317, 136);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Location = new System.Drawing.Point(95, 19);
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(297, 23);
            this.txtSiteUrl.TabIndex = 1;
            this.txtSiteUrl.Text = "http://empire.opsba.org:50300";
            // 
            // lblSiteUrl
            // 
            this.lblSiteUrl.AutoSize = true;
            this.lblSiteUrl.Location = new System.Drawing.Point(6, 22);
            this.lblSiteUrl.Name = "lblSiteUrl";
            this.lblSiteUrl.Size = new System.Drawing.Size(61, 16);
            this.lblSiteUrl.TabIndex = 0;
            this.lblSiteUrl.Text = "Site URL:";
            // 
            // gbValidate
            // 
            this.gbValidate.Controls.Add(this.lblNumRecords);
            this.gbValidate.Controls.Add(this.lblRecords);
            this.gbValidate.Controls.Add(this.btnValidateFile);
            this.gbValidate.Controls.Add(this.btnBrowse);
            this.gbValidate.Controls.Add(this.txtImportFilename);
            this.gbValidate.Controls.Add(this.lblFilename);
            this.gbValidate.Location = new System.Drawing.Point(12, 178);
            this.gbValidate.Name = "gbValidate";
            this.gbValidate.Size = new System.Drawing.Size(398, 111);
            this.gbValidate.TabIndex = 0;
            this.gbValidate.TabStop = false;
            this.gbValidate.Text = "Prepare CSV for import";
            // 
            // lblNumRecords
            // 
            this.lblNumRecords.AutoSize = true;
            this.lblNumRecords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumRecords.Location = new System.Drawing.Point(96, 51);
            this.lblNumRecords.Name = "lblNumRecords";
            this.lblNumRecords.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblNumRecords.Size = new System.Drawing.Size(64, 22);
            this.lblNumRecords.TabIndex = 0;
            this.lblNumRecords.Text = "0 records";
            this.lblNumRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(6, 53);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(59, 16);
            this.lblRecords.TabIndex = 0;
            this.lblRecords.Text = "Records:";
            // 
            // btnValidateFile
            // 
            this.btnValidateFile.Location = new System.Drawing.Point(290, 82);
            this.btnValidateFile.Name = "btnValidateFile";
            this.btnValidateFile.Size = new System.Drawing.Size(102, 23);
            this.btnValidateFile.TabIndex = 8;
            this.btnValidateFile.Text = "Validate CSV";
            this.btnValidateFile.UseVisualStyleBackColor = true;
            this.btnValidateFile.Click += new System.EventHandler(this.btnValidateFile_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(315, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtImportFilename
            // 
            this.txtImportFilename.Location = new System.Drawing.Point(95, 20);
            this.txtImportFilename.Name = "txtImportFilename";
            this.txtImportFilename.Size = new System.Drawing.Size(214, 23);
            this.txtImportFilename.TabIndex = 6;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(6, 23);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(69, 16);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "File name:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV Files|*.csv";
            this.openFileDialog.Title = "Import CSV file into Sharepoint";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // gbImport
            // 
            this.gbImport.Controls.Add(this.dgvMappings);
            this.gbImport.Controls.Add(this.cbSelectAll);
            this.gbImport.Controls.Add(this.btnImport);
            this.gbImport.Controls.Add(this.lblMappings);
            this.gbImport.Controls.Add(this.cbListname);
            this.gbImport.Controls.Add(this.lblListname);
            this.gbImport.Location = new System.Drawing.Point(12, 295);
            this.gbImport.Name = "gbImport";
            this.gbImport.Size = new System.Drawing.Size(398, 225);
            this.gbImport.TabIndex = 0;
            this.gbImport.TabStop = false;
            this.gbImport.Text = "Import into Sharepoint";
            // 
            // dgvMappings
            // 
            this.dgvMappings.AllowUserToAddRows = false;
            this.dgvMappings.AllowUserToDeleteRows = false;
            this.dgvMappings.AllowUserToResizeRows = false;
            this.dgvMappings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvMappings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMappings.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMappings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Include,
            this.Column,
            this.Mapping,
            this.Type});
            this.dgvMappings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMappings.Location = new System.Drawing.Point(95, 46);
            this.dgvMappings.MultiSelect = false;
            this.dgvMappings.Name = "dgvMappings";
            this.dgvMappings.RowHeadersVisible = false;
            this.dgvMappings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvMappings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMappings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMappings.ShowCellToolTips = false;
            this.dgvMappings.ShowEditingIcon = false;
            this.dgvMappings.ShowRowErrors = false;
            this.dgvMappings.Size = new System.Drawing.Size(297, 123);
            this.dgvMappings.TabIndex = 10;
            this.dgvMappings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMappings_CellValueChanged);
            // 
            // Include
            // 
            this.Include.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Include.HeaderText = "*";
            this.Include.Name = "Include";
            this.Include.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Include.ToolTipText = "Import this column";
            this.Include.Width = 22;
            // 
            // Column
            // 
            this.Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column.HeaderText = "Column";
            this.Column.Name = "Column";
            this.Column.ReadOnly = true;
            // 
            // Mapping
            // 
            this.Mapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Mapping.HeaderText = "Mapping";
            this.Mapping.Name = "Mapping";
            this.Mapping.Width = 62;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.Width = 42;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(311, 170);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(79, 20);
            this.cbSelectAll.TabIndex = 11;
            this.cbSelectAll.Text = "Select all";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(306, 196);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(86, 23);
            this.btnImport.TabIndex = 12;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblMappings
            // 
            this.lblMappings.AutoSize = true;
            this.lblMappings.Location = new System.Drawing.Point(6, 46);
            this.lblMappings.Name = "lblMappings";
            this.lblMappings.Size = new System.Drawing.Size(61, 16);
            this.lblMappings.TabIndex = 0;
            this.lblMappings.Text = "Mapping:";
            // 
            // cbListname
            // 
            this.cbListname.FormattingEnabled = true;
            this.cbListname.Location = new System.Drawing.Point(95, 16);
            this.cbListname.Name = "cbListname";
            this.cbListname.Size = new System.Drawing.Size(297, 24);
            this.cbListname.TabIndex = 0;
            this.cbListname.SelectedIndexChanged += new System.EventHandler(this.cbListname_SelectedIndexChanged);
            this.cbListname.TextUpdate += new System.EventHandler(this.cbListname_TextUpdate);
            this.cbListname.SelectedValueChanged += new System.EventHandler(this.cbListname_SelectedValueChanged);
            // 
            // lblListname
            // 
            this.lblListname.AutoSize = true;
            this.lblListname.Location = new System.Drawing.Point(6, 19);
            this.lblListname.Name = "lblListname";
            this.lblListname.Size = new System.Drawing.Size(32, 16);
            this.lblListname.TabIndex = 0;
            this.lblListname.Text = "List:";
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 532);
            this.Controls.Add(this.gbImport);
            this.Controls.Add(this.gbValidate);
            this.Controls.Add(this.gbLogin);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import CSV into SharePoint";
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.gbValidate.ResumeLayout(false);
            this.gbValidate.PerformLayout();
            this.gbImport.ResumeLayout(false);
            this.gbImport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtSiteUrl;
        private System.Windows.Forms.Label lblSiteUrl;
        private System.Windows.Forms.GroupBox gbValidate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtImportFilename;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnValidateFile;
        private System.Windows.Forms.GroupBox gbImport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblMappings;
        private System.Windows.Forms.ComboBox cbListname;
        private System.Windows.Forms.Label lblListname;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label lblNumRecords;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.DataGridView dgvMappings;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Include;
        private System.Windows.Forms.DataGridViewLinkColumn Column;
        private System.Windows.Forms.DataGridViewComboBoxColumn Mapping;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
    }
}