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
            lblSelectFolderPath = new Label();
            txtSelectFolderPath = new TextBox();
            btnGetFileInformation = new Button();
            btnSelectFolder = new Button();
            dgvFiles_Pack = new DataGridView();
            FileName = new DataGridViewTextBoxColumn();
            SignatureStatus = new DataGridViewTextBoxColumn();
            btnConfirm_Pack = new Button();
            cmbProductModel = new ComboBox();
            lblProductModel = new Label();
            cmbProductType = new ComboBox();
            lblProductType = new Label();
            tabControl_PackAndUnpack = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            btnImportSofarPack = new Button();
            txtSofarPackPath = new TextBox();
            lblSofarPackPath = new Label();
            dgvFiles_Unpack = new DataGridView();
            FileName_bin = new DataGridViewTextBoxColumn();
            FirmwareVersion_bin = new DataGridViewTextBoxColumn();
            FirmwareLength_bin = new DataGridViewTextBoxColumn();
            FirmwareFileType_bin = new DataGridViewTextBoxColumn();
            FirmwareChipRole_bin = new DataGridViewTextBoxColumn();
            FirmwareStartAddress_bin = new DataGridViewTextBoxColumn();
            btnConfirm_Unpack = new Button();
            saveFileDialog1 = new SaveFileDialog();
            txtFirmwarePackName = new TextBox();
            lblFirmwarePackName = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFiles_Pack).BeginInit();
            tabControl_PackAndUnpack.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiles_Unpack).BeginInit();
            SuspendLayout();
            // 
            // lblSelectFolderPath
            // 
            lblSelectFolderPath.AutoSize = true;
            lblSelectFolderPath.Location = new Point(5, 12);
            lblSelectFolderPath.Margin = new Padding(4, 0, 4, 0);
            lblSelectFolderPath.Name = "lblSelectFolderPath";
            lblSelectFolderPath.Size = new Size(107, 20);
            lblSelectFolderPath.TabIndex = 78;
            lblSelectFolderPath.Text = "模块固件路径 :";
            // 
            // txtSelectFolderPath
            // 
            txtSelectFolderPath.Location = new Point(124, 9);
            txtSelectFolderPath.Margin = new Padding(4);
            txtSelectFolderPath.Name = "txtSelectFolderPath";
            txtSelectFolderPath.Size = new Size(637, 27);
            txtSelectFolderPath.TabIndex = 79;
            // 
            // btnGetFileInformation
            // 
            btnGetFileInformation.Location = new Point(913, 9);
            btnGetFileInformation.Margin = new Padding(4);
            btnGetFileInformation.Name = "btnGetFileInformation";
            btnGetFileInformation.Size = new Size(122, 27);
            btnGetFileInformation.TabIndex = 81;
            btnGetFileInformation.Text = "获取文件信息";
            btnGetFileInformation.UseVisualStyleBackColor = true;
            btnGetFileInformation.Click += btnGetFileInformation_Click;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(781, 9);
            btnSelectFolder.Margin = new Padding(4);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(122, 27);
            btnSelectFolder.TabIndex = 80;
            btnSelectFolder.Text = "选择文件夹";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // dgvFiles_Pack
            // 
            dgvFiles_Pack.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFiles_Pack.Columns.AddRange(new DataGridViewColumn[] { FileName, SignatureStatus });
            dgvFiles_Pack.Location = new Point(7, 105);
            dgvFiles_Pack.Margin = new Padding(4);
            dgvFiles_Pack.Name = "dgvFiles_Pack";
            dgvFiles_Pack.RowHeadersWidth = 51;
            dgvFiles_Pack.RowTemplate.Height = 25;
            dgvFiles_Pack.Size = new Size(874, 399);
            dgvFiles_Pack.TabIndex = 82;
            dgvFiles_Pack.SelectionChanged += dgvFiles_SelectionChanged;
            // 
            // FileName
            // 
            FileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileName.FillWeight = 80F;
            FileName.HeaderText = "文件名";
            FileName.MinimumWidth = 6;
            FileName.Name = "FileName";
            FileName.ReadOnly = true;
            // 
            // SignatureStatus
            // 
            SignatureStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SignatureStatus.FillWeight = 20F;
            SignatureStatus.HeaderText = "签名状况";
            SignatureStatus.MinimumWidth = 6;
            SignatureStatus.Name = "SignatureStatus";
            SignatureStatus.ReadOnly = true;
            // 
            // btnConfirm_Pack
            // 
            btnConfirm_Pack.Location = new Point(901, 105);
            btnConfirm_Pack.Margin = new Padding(4);
            btnConfirm_Pack.Name = "btnConfirm_Pack";
            btnConfirm_Pack.Size = new Size(122, 399);
            btnConfirm_Pack.TabIndex = 83;
            btnConfirm_Pack.Text = "确认";
            btnConfirm_Pack.UseVisualStyleBackColor = true;
            btnConfirm_Pack.Click += btnConfirm_Click;
            // 
            // cmbProductModel
            // 
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Location = new Point(872, 53);
            cmbProductModel.Margin = new Padding(4);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(160, 28);
            cmbProductModel.TabIndex = 125;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Location = new Point(781, 57);
            lblProductModel.Margin = new Padding(4, 0, 4, 0);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(77, 20);
            lblProductModel.TabIndex = 124;
            lblProductModel.Text = "产品机型 :";
            // 
            // cmbProductType
            // 
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Location = new Point(581, 53);
            cmbProductType.Margin = new Padding(4);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(179, 28);
            cmbProductType.TabIndex = 127;
            cmbProductType.SelectedIndexChanged += cmbProductType_SelectedIndexChanged;
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Location = new Point(512, 79);
            lblProductType.Margin = new Padding(4, 0, 4, 0);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(77, 20);
            lblProductType.TabIndex = 126;
            lblProductType.Text = "产品类型 :";
            // 
            // tabControl_PackAndUnpack
            // 
            tabControl_PackAndUnpack.Controls.Add(tabPage1);
            tabControl_PackAndUnpack.Controls.Add(tabPage2);
            tabControl_PackAndUnpack.Location = new Point(15, 10);
            tabControl_PackAndUnpack.Name = "tabControl_PackAndUnpack";
            tabControl_PackAndUnpack.SelectedIndex = 0;
            tabControl_PackAndUnpack.Size = new Size(1058, 566);
            tabControl_PackAndUnpack.TabIndex = 128;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtFirmwarePackName);
            tabPage1.Controls.Add(lblFirmwarePackName);
            tabPage1.Controls.Add(dgvFiles_Pack);
            tabPage1.Controls.Add(btnConfirm_Pack);
            tabPage1.Controls.Add(txtSelectFolderPath);
            tabPage1.Controls.Add(cmbProductType);
            tabPage1.Controls.Add(btnSelectFolder);
            tabPage1.Controls.Add(cmbProductModel);
            tabPage1.Controls.Add(btnGetFileInformation);
            tabPage1.Controls.Add(lblProductModel);
            tabPage1.Controls.Add(lblSelectFolderPath);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1050, 533);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "打包";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnImportSofarPack);
            tabPage2.Controls.Add(txtSofarPackPath);
            tabPage2.Controls.Add(lblSofarPackPath);
            tabPage2.Controls.Add(dgvFiles_Unpack);
            tabPage2.Controls.Add(btnConfirm_Unpack);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1050, 533);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "解包";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnImportSofarPack
            // 
            btnImportSofarPack.Location = new Point(816, 20);
            btnImportSofarPack.Margin = new Padding(4);
            btnImportSofarPack.Name = "btnImportSofarPack";
            btnImportSofarPack.Size = new Size(122, 27);
            btnImportSofarPack.TabIndex = 134;
            btnImportSofarPack.Text = "导入";
            btnImportSofarPack.UseVisualStyleBackColor = true;
            btnImportSofarPack.Click += btnImportSofarPack_Click;
            // 
            // txtSofarPackPath
            // 
            txtSofarPackPath.Location = new Point(140, 20);
            txtSofarPackPath.Margin = new Padding(4);
            txtSofarPackPath.Name = "txtSofarPackPath";
            txtSofarPackPath.Size = new Size(637, 27);
            txtSofarPackPath.TabIndex = 133;
            // 
            // lblSofarPackPath
            // 
            lblSofarPackPath.AutoSize = true;
            lblSofarPackPath.Location = new Point(16, 23);
            lblSofarPackPath.Margin = new Padding(4, 0, 4, 0);
            lblSofarPackPath.Name = "lblSofarPackPath";
            lblSofarPackPath.Size = new Size(98, 20);
            lblSofarPackPath.TabIndex = 132;
            lblSofarPackPath.Text = "sofar包路径 :";
            // 
            // dgvFiles_Unpack
            // 
            dgvFiles_Unpack.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFiles_Unpack.Columns.AddRange(new DataGridViewColumn[] { FileName_bin, FirmwareVersion_bin, FirmwareLength_bin, FirmwareFileType_bin, FirmwareChipRole_bin, FirmwareStartAddress_bin });
            dgvFiles_Unpack.Location = new Point(7, 93);
            dgvFiles_Unpack.Margin = new Padding(4);
            dgvFiles_Unpack.Name = "dgvFiles_Unpack";
            dgvFiles_Unpack.RowHeadersWidth = 51;
            dgvFiles_Unpack.RowTemplate.Height = 25;
            dgvFiles_Unpack.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFiles_Unpack.Size = new Size(874, 399);
            dgvFiles_Unpack.TabIndex = 84;
            // 
            // FileName_bin
            // 
            FileName_bin.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileName_bin.DataPropertyName = "FirmwareName";
            FileName_bin.FillWeight = 80F;
            FileName_bin.HeaderText = "文件名";
            FileName_bin.MinimumWidth = 6;
            FileName_bin.Name = "FileName_bin";
            FileName_bin.ReadOnly = true;
            // 
            // FirmwareVersion_bin
            // 
            FirmwareVersion_bin.DataPropertyName = "FirmwareVersion";
            FirmwareVersion_bin.HeaderText = "版本号";
            FirmwareVersion_bin.MinimumWidth = 6;
            FirmwareVersion_bin.Name = "FirmwareVersion_bin";
            FirmwareVersion_bin.Width = 125;
            // 
            // FirmwareLength_bin
            // 
            FirmwareLength_bin.DataPropertyName = "FirmwareLength";
            FirmwareLength_bin.HeaderText = "文件长度";
            FirmwareLength_bin.MinimumWidth = 6;
            FirmwareLength_bin.Name = "FirmwareLength_bin";
            FirmwareLength_bin.Width = 125;
            // 
            // FirmwareFileType_bin
            // 
            FirmwareFileType_bin.DataPropertyName = "FirmwareFileType";
            FirmwareFileType_bin.HeaderText = "文件类型";
            FirmwareFileType_bin.MinimumWidth = 6;
            FirmwareFileType_bin.Name = "FirmwareFileType_bin";
            FirmwareFileType_bin.Width = 125;
            // 
            // FirmwareChipRole_bin
            // 
            FirmwareChipRole_bin.DataPropertyName = "FirmwareChipRole";
            FirmwareChipRole_bin.HeaderText = "芯片角色";
            FirmwareChipRole_bin.MinimumWidth = 6;
            FirmwareChipRole_bin.Name = "FirmwareChipRole_bin";
            FirmwareChipRole_bin.Width = 125;
            // 
            // FirmwareStartAddress_bin
            // 
            FirmwareStartAddress_bin.DataPropertyName = "FirmwareStartAddress";
            FirmwareStartAddress_bin.HeaderText = "起始偏移地址";
            FirmwareStartAddress_bin.MinimumWidth = 6;
            FirmwareStartAddress_bin.Name = "FirmwareStartAddress_bin";
            FirmwareStartAddress_bin.Visible = false;
            FirmwareStartAddress_bin.Width = 125;
            // 
            // btnConfirm_Unpack
            // 
            btnConfirm_Unpack.Location = new Point(906, 93);
            btnConfirm_Unpack.Margin = new Padding(4);
            btnConfirm_Unpack.Name = "btnConfirm_Unpack";
            btnConfirm_Unpack.Size = new Size(122, 399);
            btnConfirm_Unpack.TabIndex = 85;
            btnConfirm_Unpack.Text = "确认";
            btnConfirm_Unpack.UseVisualStyleBackColor = true;
            btnConfirm_Unpack.Click += btnConfirm_Unpack_Click;
            // 
            // txtFirmwarePackName
            // 
            txtFirmwarePackName.Location = new Point(129, 52);
            txtFirmwarePackName.Margin = new Padding(4);
            txtFirmwarePackName.Name = "txtFirmwarePackName";
            txtFirmwarePackName.Size = new Size(412, 27);
            txtFirmwarePackName.TabIndex = 138;
            // 
            // lblFirmwarePackName
            // 
            lblFirmwarePackName.AutoSize = true;
            lblFirmwarePackName.Location = new Point(20, 56);
            lblFirmwarePackName.Margin = new Padding(4, 0, 4, 0);
            lblFirmwarePackName.Name = "lblFirmwarePackName";
            lblFirmwarePackName.Size = new Size(92, 20);
            lblFirmwarePackName.TabIndex = 137;
            lblFirmwarePackName.Text = "固件包名称 :";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 586);
            Controls.Add(tabControl_PackAndUnpack);
            Controls.Add(lblProductType);
            Margin = new Padding(4);
            Name = "FrmMain";
            Text = "打包工具V1.0.0.0";
            Load += FrmMain_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFiles_Pack).EndInit();
            tabControl_PackAndUnpack.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiles_Unpack).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSelectFolderPath;
        private TextBox txtSelectFolderPath;
        private Button btnGetFileInformation;
        private Button btnSelectFolder;
        private DataGridView dgvFiles_Pack;
        private Button btnConfirm_Pack;
        private ComboBox cmbProductModel;
        private Label lblProductModel;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn SignatureStatus;
        private ComboBox cmbProductType;
        private Label lblProductType;
        private TabControl tabControl_PackAndUnpack;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvFiles_Unpack;
        private Button btnConfirm_Unpack;
        private SaveFileDialog saveFileDialog1;
        private Button btnImportSofarPack;
        private TextBox txtSofarPackPath;
        private Label lblSofarPackPath;
        private DataGridViewTextBoxColumn FileName_bin;
        private DataGridViewTextBoxColumn FirmwareVersion_bin;
        private DataGridViewTextBoxColumn FirmwareLength_bin;
        private DataGridViewTextBoxColumn FirmwareFileType_bin;
        private DataGridViewTextBoxColumn FirmwareChipRole_bin;
        private DataGridViewTextBoxColumn FirmwareStartAddress_bin;
        private TextBox txtFirmwarePackName;
        private Label lblFirmwarePackName;
    }
}