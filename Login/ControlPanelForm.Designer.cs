namespace Login
{
    partial class ControlPanelForm
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
            this.components = new System.ComponentModel.Container();
            this.chkProfail = new System.Windows.Forms.CheckBox();
            this.chkService = new System.Windows.Forms.CheckBox();
            this.chkAbout = new System.Windows.Forms.CheckBox();
            this.chkContact = new System.Windows.Forms.CheckBox();
            this.chkPermission = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxUser = new System.Windows.Forms.ComboBox();
            this.txtScarch = new System.Windows.Forms.TextBox();
            this.dgvUserlist = new System.Windows.Forms.DataGridView();
            this.fastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sANITARIUMDataSet = new Login.SANITARIUMDataSet();
            this.userInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userInfoTableAdapter = new Login.SANITARIUMDataSetTableAdapters.UserInfoTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANITARIUMDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chkProfail
            // 
            this.chkProfail.AutoSize = true;
            this.chkProfail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProfail.Location = new System.Drawing.Point(135, 175);
            this.chkProfail.Name = "chkProfail";
            this.chkProfail.Size = new System.Drawing.Size(88, 29);
            this.chkProfail.TabIndex = 0;
            this.chkProfail.Text = "Profail";
            this.chkProfail.UseVisualStyleBackColor = true;
            // 
            // chkService
            // 
            this.chkService.AutoSize = true;
            this.chkService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkService.Location = new System.Drawing.Point(135, 233);
            this.chkService.Name = "chkService";
            this.chkService.Size = new System.Drawing.Size(100, 29);
            this.chkService.TabIndex = 1;
            this.chkService.Text = "Service";
            this.chkService.UseVisualStyleBackColor = true;
            // 
            // chkAbout
            // 
            this.chkAbout.AutoSize = true;
            this.chkAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAbout.Location = new System.Drawing.Point(135, 285);
            this.chkAbout.Name = "chkAbout";
            this.chkAbout.Size = new System.Drawing.Size(86, 29);
            this.chkAbout.TabIndex = 2;
            this.chkAbout.Text = "About";
            this.chkAbout.UseVisualStyleBackColor = true;
            // 
            // chkContact
            // 
            this.chkContact.AutoSize = true;
            this.chkContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContact.Location = new System.Drawing.Point(135, 333);
            this.chkContact.Name = "chkContact";
            this.chkContact.Size = new System.Drawing.Size(102, 29);
            this.chkContact.TabIndex = 3;
            this.chkContact.Text = "Contact";
            this.chkContact.UseVisualStyleBackColor = true;
            // 
            // chkPermission
            // 
            this.chkPermission.AutoSize = true;
            this.chkPermission.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPermission.Location = new System.Drawing.Point(135, 392);
            this.chkPermission.Name = "chkPermission";
            this.chkPermission.Size = new System.Drawing.Size(130, 29);
            this.chkPermission.TabIndex = 5;
            this.chkPermission.Text = "Permission";
            this.chkPermission.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(234, 447);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxUser
            // 
            this.cbxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUser.FormattingEnabled = true;
            this.cbxUser.Location = new System.Drawing.Point(188, 95);
            this.cbxUser.Name = "cbxUser";
            this.cbxUser.Size = new System.Drawing.Size(205, 30);
            this.cbxUser.TabIndex = 7;
            // 
            // txtScarch
            // 
            this.txtScarch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScarch.Location = new System.Drawing.Point(188, 48);
            this.txtScarch.Name = "txtScarch";
            this.txtScarch.Size = new System.Drawing.Size(205, 30);
            this.txtScarch.TabIndex = 8;
            this.txtScarch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScarch_KeyPress);
            // 
            // dgvUserlist
            // 
            this.dgvUserlist.AllowUserToAddRows = false;
            this.dgvUserlist.AllowUserToDeleteRows = false;
            this.dgvUserlist.AutoGenerateColumns = false;
            this.dgvUserlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fastNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn});
            this.dgvUserlist.DataSource = this.userInfoBindingSource1;
            this.dgvUserlist.Location = new System.Drawing.Point(537, 119);
            this.dgvUserlist.Name = "dgvUserlist";
            this.dgvUserlist.ReadOnly = true;
            this.dgvUserlist.RowTemplate.Height = 24;
            this.dgvUserlist.Size = new System.Drawing.Size(306, 289);
            this.dgvUserlist.TabIndex = 10;
            this.dgvUserlist.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserlist_CellContentDoubleClick);
            // 
            // fastNameDataGridViewTextBoxColumn
            // 
            this.fastNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fastNameDataGridViewTextBoxColumn.DataPropertyName = "FastName";
            this.fastNameDataGridViewTextBoxColumn.HeaderText = "FastName";
            this.fastNameDataGridViewTextBoxColumn.Name = "fastNameDataGridViewTextBoxColumn";
            this.fastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userInfoBindingSource1
            // 
            this.userInfoBindingSource1.DataMember = "UserInfo";
            this.userInfoBindingSource1.DataSource = this.sANITARIUMDataSet;
            // 
            // sANITARIUMDataSet
            // 
            this.sANITARIUMDataSet.DataSetName = "SANITARIUMDataSet";
            this.sANITARIUMDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // userInfoBindingSource
            // 
            this.userInfoBindingSource.DataMember = "UserInfo";
            this.userInfoBindingSource.DataSource = this.sANITARIUMDataSet;
            // 
            // userInfoTableAdapter
            // 
            this.userInfoTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(549, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // ControlPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(977, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvUserlist);
            this.Controls.Add(this.txtScarch);
            this.Controls.Add(this.cbxUser);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPermission);
            this.Controls.Add(this.chkContact);
            this.Controls.Add(this.chkAbout);
            this.Controls.Add(this.chkService);
            this.Controls.Add(this.chkProfail);
            this.Name = "ControlPanelForm";
            this.Text = "ControlPanelForm";
            this.Load += new System.EventHandler(this.ControlPanelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANITARIUMDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkProfail;
        private System.Windows.Forms.CheckBox chkService;
        private System.Windows.Forms.CheckBox chkAbout;
        private System.Windows.Forms.CheckBox chkContact;
        private System.Windows.Forms.CheckBox chkPermission;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxUser;
        private System.Windows.Forms.TextBox txtScarch;
        private System.Windows.Forms.DataGridView dgvUserlist;
        private SANITARIUMDataSet sANITARIUMDataSet;
        private System.Windows.Forms.BindingSource userInfoBindingSource;
        private SANITARIUMDataSetTableAdapters.UserInfoTableAdapter userInfoTableAdapter;
        private System.Windows.Forms.BindingSource userInfoBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
    }
}