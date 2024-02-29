namespace PackagingTools
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSelectFolderPath = new System.Windows.Forms.Label();
            this.txtSelectFolderPath = new System.Windows.Forms.TextBox();
            this.btnGetFileInformation = new System.Windows.Forms.Button();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignatureStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtFirmwarePackName = new System.Windows.Forms.TextBox();
            this.lblFirmwarePackName = new System.Windows.Forms.Label();
            this.lblProductModel = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkDefaultName = new System.Windows.Forms.CheckBox();
            this.rdoTimeStampNo = new System.Windows.Forms.RadioButton();
            this.rdoTimeStampYes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtProductModel = new System.Windows.Forms.TextBox();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnUnpack = new System.Windows.Forms.Button();
            this.dgvFiles_Unpack = new System.Windows.Forms.DataGridView();
            this.Recombination = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FirmwareName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportSofarPack = new System.Windows.Forms.Button();
            this.txtSofarPackPath = new System.Windows.Forms.TextBox();
            this.lblSofarPackPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles_Unpack)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectFolderPath
            // 
            this.lblSelectFolderPath.AutoSize = true;
            this.lblSelectFolderPath.Location = new System.Drawing.Point(21, 23);
            this.lblSelectFolderPath.Name = "lblSelectFolderPath";
            this.lblSelectFolderPath.Size = new System.Drawing.Size(87, 17);
            this.lblSelectFolderPath.TabIndex = 78;
            this.lblSelectFolderPath.Text = "模块固件路径 :";
            // 
            // txtSelectFolderPath
            // 
            this.txtSelectFolderPath.Location = new System.Drawing.Point(113, 20);
            this.txtSelectFolderPath.Name = "txtSelectFolderPath";
            this.txtSelectFolderPath.Size = new System.Drawing.Size(496, 23);
            this.txtSelectFolderPath.TabIndex = 79;
            // 
            // btnGetFileInformation
            // 
            this.btnGetFileInformation.Location = new System.Drawing.Point(727, 20);
            this.btnGetFileInformation.Name = "btnGetFileInformation";
            this.btnGetFileInformation.Size = new System.Drawing.Size(95, 23);
            this.btnGetFileInformation.TabIndex = 81;
            this.btnGetFileInformation.Text = "获取文件信息";
            this.btnGetFileInformation.UseVisualStyleBackColor = true;
            this.btnGetFileInformation.Click += new System.EventHandler(this.btnGetFileInformation_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(624, 20);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(95, 23);
            this.btnSelectFolder.TabIndex = 80;
            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.SignatureStatus});
            this.dgvFiles.Location = new System.Drawing.Point(21, 137);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.RowTemplate.Height = 25;
            this.dgvFiles.Size = new System.Drawing.Size(690, 323);
            this.dgvFiles.TabIndex = 82;
            this.dgvFiles.SelectionChanged += new System.EventHandler(this.dgvFiles_SelectionChanged);
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileName.FillWeight = 80F;
            this.FileName.HeaderText = "文件名";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // SignatureStatus
            // 
            this.SignatureStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SignatureStatus.FillWeight = 20F;
            this.SignatureStatus.HeaderText = "签名状况";
            this.SignatureStatus.Name = "SignatureStatus";
            this.SignatureStatus.ReadOnly = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(727, 137);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 323);
            this.btnConfirm.TabIndex = 83;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtFirmwarePackName
            // 
            this.txtFirmwarePackName.Location = new System.Drawing.Point(113, 58);
            this.txtFirmwarePackName.Name = "txtFirmwarePackName";
            this.txtFirmwarePackName.Size = new System.Drawing.Size(130, 23);
            this.txtFirmwarePackName.TabIndex = 121;
            // 
            // lblFirmwarePackName
            // 
            this.lblFirmwarePackName.AutoSize = true;
            this.lblFirmwarePackName.Location = new System.Drawing.Point(33, 61);
            this.lblFirmwarePackName.Name = "lblFirmwarePackName";
            this.lblFirmwarePackName.Size = new System.Drawing.Size(75, 17);
            this.lblFirmwarePackName.TabIndex = 120;
            this.lblFirmwarePackName.Text = "固件包名称 :";
            // 
            // lblProductModel
            // 
            this.lblProductModel.AutoSize = true;
            this.lblProductModel.Location = new System.Drawing.Point(259, 102);
            this.lblProductModel.Name = "lblProductModel";
            this.lblProductModel.Size = new System.Drawing.Size(87, 17);
            this.lblProductModel.TabIndex = 124;
            this.lblProductModel.Text = "产品机型编码 :";
            // 
            // lblProductType
            // 
            this.lblProductType.AutoSize = true;
            this.lblProductType.Location = new System.Drawing.Point(21, 102);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(87, 17);
            this.lblProductType.TabIndex = 126;
            this.lblProductType.Text = "产品类型编码 :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(23, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(864, 520);
            this.tabControl1.TabIndex = 128;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkDefaultName);
            this.tabPage1.Controls.Add(this.rdoTimeStampNo);
            this.tabPage1.Controls.Add(this.rdoTimeStampYes);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtCompany);
            this.tabPage1.Controls.Add(this.lblCompany);
            this.tabPage1.Controls.Add(this.txtProductModel);
            this.tabPage1.Controls.Add(this.txtProductType);
            this.tabPage1.Controls.Add(this.dgvFiles);
            this.tabPage1.Controls.Add(this.btnSelectFolder);
            this.tabPage1.Controls.Add(this.lblProductType);
            this.tabPage1.Controls.Add(this.btnGetFileInformation);
            this.tabPage1.Controls.Add(this.txtSelectFolderPath);
            this.tabPage1.Controls.Add(this.lblProductModel);
            this.tabPage1.Controls.Add(this.lblSelectFolderPath);
            this.tabPage1.Controls.Add(this.txtFirmwarePackName);
            this.tabPage1.Controls.Add(this.btnConfirm);
            this.tabPage1.Controls.Add(this.lblFirmwarePackName);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(856, 490);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "打包";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkDefaultName
            // 
            this.chkDefaultName.AutoSize = true;
            this.chkDefaultName.Checked = true;
            this.chkDefaultName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDefaultName.Location = new System.Drawing.Point(727, 101);
            this.chkDefaultName.Name = "chkDefaultName";
            this.chkDefaultName.Size = new System.Drawing.Size(99, 21);
            this.chkDefaultName.TabIndex = 135;
            this.chkDefaultName.Text = "默认公司名称";
            this.chkDefaultName.UseVisualStyleBackColor = true;
            this.chkDefaultName.CheckedChanged += new System.EventHandler(this.chkDefaultName_CheckedChanged);
            // 
            // rdoTimeStampNo
            // 
            this.rdoTimeStampNo.AutoSize = true;
            this.rdoTimeStampNo.Location = new System.Drawing.Point(400, 59);
            this.rdoTimeStampNo.Name = "rdoTimeStampNo";
            this.rdoTimeStampNo.Size = new System.Drawing.Size(38, 21);
            this.rdoTimeStampNo.TabIndex = 134;
            this.rdoTimeStampNo.TabStop = true;
            this.rdoTimeStampNo.Text = "否";
            this.rdoTimeStampNo.UseVisualStyleBackColor = true;
            // 
            // rdoTimeStampYes
            // 
            this.rdoTimeStampYes.AutoSize = true;
            this.rdoTimeStampYes.Location = new System.Drawing.Point(346, 59);
            this.rdoTimeStampYes.Name = "rdoTimeStampYes";
            this.rdoTimeStampYes.Size = new System.Drawing.Size(38, 21);
            this.rdoTimeStampYes.TabIndex = 133;
            this.rdoTimeStampYes.TabStop = true;
            this.rdoTimeStampYes.Text = "是";
            this.rdoTimeStampYes.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 132;
            this.label1.Text = "带时间戳 :";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(555, 99);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(130, 23);
            this.txtCompany.TabIndex = 131;
            this.txtCompany.TextChanged += new System.EventHandler(this.txtCompany_TextChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(492, 102);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(63, 17);
            this.lblCompany.TabIndex = 130;
            this.lblCompany.Text = "公司名称 :";
            // 
            // txtProductModel
            // 
            this.txtProductModel.Location = new System.Drawing.Point(346, 99);
            this.txtProductModel.Name = "txtProductModel";
            this.txtProductModel.Size = new System.Drawing.Size(130, 23);
            this.txtProductModel.TabIndex = 129;
            // 
            // txtProductType
            // 
            this.txtProductType.Location = new System.Drawing.Point(113, 99);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(130, 23);
            this.txtProductType.TabIndex = 128;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnUnpack);
            this.tabPage2.Controls.Add(this.dgvFiles_Unpack);
            this.tabPage2.Controls.Add(this.btnImportSofarPack);
            this.tabPage2.Controls.Add(this.txtSofarPackPath);
            this.tabPage2.Controls.Add(this.lblSofarPackPath);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(856, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "重组";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnUnpack
            // 
            this.btnUnpack.Location = new System.Drawing.Point(741, 74);
            this.btnUnpack.Name = "btnUnpack";
            this.btnUnpack.Size = new System.Drawing.Size(95, 323);
            this.btnUnpack.TabIndex = 139;
            this.btnUnpack.Text = "确认";
            this.btnUnpack.UseVisualStyleBackColor = true;
            this.btnUnpack.Click += new System.EventHandler(this.btnUnpack_Click);
            // 
            // dgvFiles_Unpack
            // 
            this.dgvFiles_Unpack.AllowUserToAddRows = false;
            this.dgvFiles_Unpack.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvFiles_Unpack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles_Unpack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Recombination,
            this.FirmwareName});
            this.dgvFiles_Unpack.Location = new System.Drawing.Point(21, 74);
            this.dgvFiles_Unpack.Name = "dgvFiles_Unpack";
            this.dgvFiles_Unpack.RowHeadersVisible = false;
            this.dgvFiles_Unpack.RowTemplate.Height = 25;
            this.dgvFiles_Unpack.Size = new System.Drawing.Size(698, 323);
            this.dgvFiles_Unpack.TabIndex = 138;
            // 
            // Recombination
            // 
            this.Recombination.DataPropertyName = "Recombination";
            this.Recombination.FillWeight = 80F;
            this.Recombination.HeaderText = "重新打包";
            this.Recombination.Name = "Recombination";
            // 
            // FirmwareName
            // 
            this.FirmwareName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FirmwareName.DataPropertyName = "FirmwareName";
            this.FirmwareName.FillWeight = 80F;
            this.FirmwareName.HeaderText = "文件名";
            this.FirmwareName.Name = "FirmwareName";
            this.FirmwareName.ReadOnly = true;
            // 
            // btnImportSofarPack
            // 
            this.btnImportSofarPack.Location = new System.Drawing.Point(624, 20);
            this.btnImportSofarPack.Name = "btnImportSofarPack";
            this.btnImportSofarPack.Size = new System.Drawing.Size(95, 23);
            this.btnImportSofarPack.TabIndex = 137;
            this.btnImportSofarPack.Text = "导入";
            this.btnImportSofarPack.UseVisualStyleBackColor = true;
            this.btnImportSofarPack.Click += new System.EventHandler(this.btnImportSofarPack_Click);
            // 
            // txtSofarPackPath
            // 
            this.txtSofarPackPath.Location = new System.Drawing.Point(113, 20);
            this.txtSofarPackPath.Name = "txtSofarPackPath";
            this.txtSofarPackPath.Size = new System.Drawing.Size(496, 23);
            this.txtSofarPackPath.TabIndex = 136;
            // 
            // lblSofarPackPath
            // 
            this.lblSofarPackPath.AutoSize = true;
            this.lblSofarPackPath.Location = new System.Drawing.Point(21, 23);
            this.lblSofarPackPath.Name = "lblSofarPackPath";
            this.lblSofarPackPath.Size = new System.Drawing.Size(81, 17);
            this.lblSofarPackPath.TabIndex = 135;
            this.lblSofarPackPath.Text = "sofar包路径 :";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "打包工具T1.0.0.2";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles_Unpack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblSelectFolderPath;
        private TextBox txtSelectFolderPath;
        private Button btnGetFileInformation;
        private Button btnSelectFolder;
        private DataGridView dgvFiles;
        private Button btnConfirm;
        private TextBox txtFirmwarePackName;
        private Label lblFirmwarePackName;
        private Label lblProductModel;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn SignatureStatus;
        private Label lblProductType;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnImportSofarPack;
        private TextBox txtSofarPackPath;
        private Label lblSofarPackPath;
        private DataGridView dgvFiles_Unpack;
        private Button btnUnpack;
        private DataGridViewCheckBoxColumn Recombination;
        private DataGridViewTextBoxColumn FirmwareName;
        private TextBox txtProductType;
        private TextBox txtCompany;
        private Label lblCompany;
        private TextBox txtProductModel;
        private RadioButton rdoTimeStampNo;
        private RadioButton rdoTimeStampYes;
        private Label label1;
        private CheckBox chkDefaultName;
    }
}