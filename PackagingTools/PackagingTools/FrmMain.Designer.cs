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
            this.cmbProductModel = new System.Windows.Forms.ComboBox();
            this.lblProductModel = new System.Windows.Forms.Label();
            this.cmbProductType = new System.Windows.Forms.ComboBox();
            this.lblProductType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectFolderPath
            // 
            this.lblSelectFolderPath.AutoSize = true;
            this.lblSelectFolderPath.Location = new System.Drawing.Point(19, 29);
            this.lblSelectFolderPath.Name = "lblSelectFolderPath";
            this.lblSelectFolderPath.Size = new System.Drawing.Size(87, 17);
            this.lblSelectFolderPath.TabIndex = 78;
            this.lblSelectFolderPath.Text = "模块固件路径 :";
            // 
            // txtSelectFolderPath
            // 
            this.txtSelectFolderPath.Location = new System.Drawing.Point(111, 26);
            this.txtSelectFolderPath.Name = "txtSelectFolderPath";
            this.txtSelectFolderPath.Size = new System.Drawing.Size(496, 23);
            this.txtSelectFolderPath.TabIndex = 79;
            // 
            // btnGetFileInformation
            // 
            this.btnGetFileInformation.Location = new System.Drawing.Point(725, 26);
            this.btnGetFileInformation.Name = "btnGetFileInformation";
            this.btnGetFileInformation.Size = new System.Drawing.Size(95, 23);
            this.btnGetFileInformation.TabIndex = 81;
            this.btnGetFileInformation.Text = "获取文件信息";
            this.btnGetFileInformation.UseVisualStyleBackColor = true;
            this.btnGetFileInformation.Click += new System.EventHandler(this.btnGetFileInformation_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(622, 26);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(95, 23);
            this.btnSelectFolder.TabIndex = 80;
            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.SignatureStatus});
            this.dgvFiles.Location = new System.Drawing.Point(19, 113);
            this.dgvFiles.Name = "dgvFiles";
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
            this.btnConfirm.Location = new System.Drawing.Point(725, 113);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 323);
            this.btnConfirm.TabIndex = 83;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtFirmwarePackName
            // 
            this.txtFirmwarePackName.Location = new System.Drawing.Point(111, 64);
            this.txtFirmwarePackName.Name = "txtFirmwarePackName";
            this.txtFirmwarePackName.Size = new System.Drawing.Size(225, 23);
            this.txtFirmwarePackName.TabIndex = 121;
            // 
            // lblFirmwarePackName
            // 
            this.lblFirmwarePackName.AutoSize = true;
            this.lblFirmwarePackName.Location = new System.Drawing.Point(31, 67);
            this.lblFirmwarePackName.Name = "lblFirmwarePackName";
            this.lblFirmwarePackName.Size = new System.Drawing.Size(75, 17);
            this.lblFirmwarePackName.TabIndex = 120;
            this.lblFirmwarePackName.Text = "固件包名称 :";
            // 
            // cmbProductModel
            // 
            this.cmbProductModel.FormattingEnabled = true;
            this.cmbProductModel.Location = new System.Drawing.Point(693, 64);
            this.cmbProductModel.Name = "cmbProductModel";
            this.cmbProductModel.Size = new System.Drawing.Size(125, 25);
            this.cmbProductModel.TabIndex = 125;
            // 
            // lblProductModel
            // 
            this.lblProductModel.AutoSize = true;
            this.lblProductModel.Location = new System.Drawing.Point(622, 67);
            this.lblProductModel.Name = "lblProductModel";
            this.lblProductModel.Size = new System.Drawing.Size(63, 17);
            this.lblProductModel.TabIndex = 124;
            this.lblProductModel.Text = "产品机型 :";
            // 
            // cmbProductType
            // 
            this.cmbProductType.FormattingEnabled = true;
            this.cmbProductType.Location = new System.Drawing.Point(467, 64);
            this.cmbProductType.Name = "cmbProductType";
            this.cmbProductType.Size = new System.Drawing.Size(140, 25);
            this.cmbProductType.TabIndex = 127;
            this.cmbProductType.SelectedIndexChanged += new System.EventHandler(this.cmbProductType_SelectedIndexChanged);
            // 
            // lblProductType
            // 
            this.lblProductType.AutoSize = true;
            this.lblProductType.Location = new System.Drawing.Point(398, 67);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(63, 17);
            this.lblProductType.TabIndex = 126;
            this.lblProductType.Text = "产品类型 :";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 461);
            this.Controls.Add(this.cmbProductType);
            this.Controls.Add(this.lblProductType);
            this.Controls.Add(this.cmbProductModel);
            this.Controls.Add(this.lblProductModel);
            this.Controls.Add(this.txtFirmwarePackName);
            this.Controls.Add(this.lblFirmwarePackName);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.lblSelectFolderPath);
            this.Controls.Add(this.txtSelectFolderPath);
            this.Controls.Add(this.btnGetFileInformation);
            this.Controls.Add(this.btnSelectFolder);
            this.Name = "FrmMain";
            this.Text = "打包工具V1.0.0.0";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ComboBox cmbProductModel;
        private Label lblProductModel;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn SignatureStatus;
        private ComboBox cmbProductType;
        private Label lblProductType;
    }
}