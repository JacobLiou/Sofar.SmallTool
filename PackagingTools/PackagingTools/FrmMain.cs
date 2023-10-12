using System;
using PackagingTools.Common;
using System.Data;
using System.Text;
using UpgradePackTool.Common;
using UpgradePackTool.Model;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace PackagingTools
{
    public partial class FrmMain : Form
    {
        const string COMPANY_NAME = "sofar";

        DataTable productTypeDt = new DataTable();
        DataTable productModelDt = new DataTable();

        List<FileInfo> files = new List<FileInfo>();
        List<FirmwareModel> firmwares = new List<FirmwareModel>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtSelectFolderPath.Text = IniConfig.ReadIniData("IniConfig", "SelectFolderPath", "");

            ProductModelList productModelList = JsonConvert.DeserializeObject<ProductModelList>(File.ReadAllText("config.json"));

            productModelDt.Columns.Add("ProductLineCode");
            productModelDt.Columns.Add("ProductModelCode");
            productModelDt.Columns.Add("ProductModel");

            productTypeDt.Columns.Add("ProductLineCode");
            productTypeDt.Columns.Add("ProductType");
            productTypeDt.Columns.Add("ProductLine");

            if (productModelList == null)
            {
                List<ProductModel> productModel = new List<ProductModel>()
                {
                    new ProductModel("0x01", "INV", "�����", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "320KW") }),
                    new ProductModel("0x02", "STORAGE", "����", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "SKY 5-20KW") }),
                    new ProductModel("0x03", "ESS", "����", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "ESS2000-CSU"), new ProductDetailedModel("0x0002", "ESS2000-CMU") }),
                    new ProductModel("0x04", "M-INV", "΢��", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "XXXX") }),
                    new ProductModel("0x05", "M-IOT", "������Ʒ", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "LEO5-GATE") }),
                    new ProductModel("0x06", "BAT", "���", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "GTX5000S"), new ProductDetailedModel("0x0002", "CBS5000") })
                };
                productModelList = new ProductModelList(productModel);
            }

            for (int i = 0; i < productModelList.ProductModel.Count; i++)
            {
                for (int ii = 0; ii < productModelList.ProductModel[i].ProductDetailedModel.Count; ii++)
                {
                    productModelDt.Rows.Add(productModelList.ProductModel[i].ProductLineCode,
                        productModelList.ProductModel[i].ProductDetailedModel[ii].ProductModelCode,
                        productModelList.ProductModel[i].ProductDetailedModel[ii].ProductModel);
                }
                productTypeDt.Rows.Add(productModelList.ProductModel[i].ProductLineCode,
                    productModelList.ProductModel[i].ProductType,
                    productModelList.ProductModel[i].ProductLine);
            }

            cmbProductModel.DataSource = productModelDt;
            cmbProductModel.ValueMember = "ProductModelCode";
            cmbProductModel.DisplayMember = "ProductModel";

            cmbProductType.DataSource = productTypeDt;
            cmbProductType.ValueMember = "ProductLineCode";
            cmbProductType.DisplayMember = "ProductLine";

            int.TryParse(IniConfig.ReadIniData("IniConfig", "cmbProductType", ""), out int productTypeSelectedIndex);
            cmbProductType.SelectedIndex = productTypeSelectedIndex;
            int.TryParse(IniConfig.ReadIniData("IniConfig", "cmbProductModel", ""), out int productModelSelectedIndex);
            cmbProductModel.SelectedIndex = productModelSelectedIndex;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.SelectedPath == string.Empty)
            {
                MessageBox.Show("δѡ���ļ���");
                return;
            }
            txtSelectFolderPath.Text = folderBrowserDialog.SelectedPath;
            IniConfig.WriteIniData("IniConfig", "SelectFolderPath", txtSelectFolderPath.Text);
        }

        private void btnGetFileInformation_Click(object sender, EventArgs e)
        {
            if (txtSelectFolderPath.Text == string.Empty)
            {
                MessageBox.Show("δѡ���ļ���");
                return;
            }

            dgvFiles_Pack.Rows.Clear();
            files.Clear();
            DirectoryInfo folder = new DirectoryInfo(txtSelectFolderPath.Text);
            foreach (FileInfo file in folder.GetFiles())
            {
                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(stream);
                    byte[] binchar = binReader.ReadBytes((int)stream.Length);
                    byte[] fileLengthBytes = new byte[4];
                    //ǩ����Ϣtest
                    byte[] t = new byte[1024];
                    Buffer.BlockCopy(binchar, binchar.Length - 1024, t, 0, t.Length);
                    if (binchar[binchar.Length - 1024] == 0x01)
                    {
                        fileLengthBytes = binchar.Skip(binchar.Length - 1024 - 8).Take(4).ToArray();
                    }
                    else
                    {
                        fileLengthBytes = binchar.Skip(binchar.Length - 1024 + 1).Take(4).ToArray();
                    }
                    //byte[] fileLengthBytes = binchar.Skip(binchar.Length - 1024 + 1).Take(4).ToArray();
                    //byte[] fileLengthBytes = binchar.Skip(binchar.Length - 1024 - 8).Take(4).ToArray();
                    byte[] fileCrcBytes = binchar.Skip(binchar.Length - 1024 + 5).Take(4).ToArray();
                    int fileLength = Convert.ToInt32((fileLengthBytes[0] & 0xff) + (fileLengthBytes[1] << 8) + (fileLengthBytes[2] << 16) + (fileLengthBytes[3] << 24));
                    uint crc = 0;
                    if (fileLength <= (int)binchar.Length)
                        crc = ~CrcHelper.ComputeCrc32(binchar, fileLength);

                    dgvFiles_Pack.Rows.Add();
                    dgvFiles_Pack.Rows[dgvFiles_Pack.Rows.Count - 2].Cells["FileName"].Value = file.Name;
                    if (fileCrcBytes[3] == (byte)(crc >> 24) && fileCrcBytes[2] == (byte)(crc >> 16) &&
                        fileCrcBytes[1] == (byte)(crc >> 8) && fileCrcBytes[0] == (byte)(crc & 0xFF))
                    {
                        dgvFiles_Pack.Rows[dgvFiles_Pack.Rows.Count - 2].Cells["SignatureStatus"].Value = "��ǩ��";
                        dgvFiles_Pack.Rows[dgvFiles_Pack.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Green;
                        files.Add(file);
                    }
                    else
                    {
                        dgvFiles_Pack.Rows[dgvFiles_Pack.Rows.Count - 2].Cells["SignatureStatus"].Value = "ǩ������ȷ";
                        dgvFiles_Pack.Rows[dgvFiles_Pack.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IniConfig.WriteIniData("IniConfig", "cmbProductType", cmbProductType.SelectedIndex.ToString());
            IniConfig.WriteIniData("IniConfig", "cmbProductModel", cmbProductModel.SelectedIndex.ToString());

            if (files.Count == 0)
            {
                MessageBox.Show("�ļ�����û�д�ǩ����Ϣ�Ĺ̼�");
                return;
            }

            List<FirmwareModel> firmwares = new List<FirmwareModel>();
            List<byte> byteList = new List<byte>();
            string firmwareName = txtFirmwarePackName.Text;
            int startAddress = 0;

            //�̼�ģ��
            foreach (FileInfo file in files)
            {
                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(stream);
                    byte[] binchar = binReader.ReadBytes((int)stream.Length);
                    byteList.AddRange(binchar.ToList());

                    FirmwareModel firmwareModel = new FirmwareModel();
                    firmwareModel.FirmwareName = file.Name;
                    firmwareModel.FirmwareChipRole = binchar.Skip(binchar.Length - 1024 + 125).Take(1).ToArray()[0];
                    firmwareModel.FirmwareFileType = binchar.Skip(binchar.Length - 1024 + 128).Take(1).ToArray()[0];
                    firmwareModel.FirmwareStartAddress = startAddress;
                    firmwareModel.FirmwareLength = (int)stream.Length;
                    firmwareModel.FirmwareVersion = Encoding.ASCII.GetString(binchar.Skip(binchar.Length - 1024 + 1 + 4 + 4 + 30).Take(20).ToArray());
                    firmwares.Add(firmwareModel);

                    startAddress += (int)stream.Length;
                }
            }
            //ǩ����Ϣ
            byte[] signatureInformationByte = SignatureInformation(firmwares, ref firmwareName);
            byteList.AddRange(signatureInformationByte.ToList());
            //CRC32
            uint crc = CrcHelper.ComputeCrc32(byteList.ToArray(), byteList.Count);
            byteList.Add((byte)(crc & 0xFF));
            byteList.Add((byte)(crc >> 8));
            byteList.Add((byte)(crc >> 16));
            byteList.Add((byte)(crc >> 24));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = firmwareName;
            sfd.ShowDialog();
            String path = sfd.FileName;
            if (path == "")//�ж�·���Ƿ�Ϊ��
            {
                MessageBox.Show("����ʧ��");
                return;
            }

            using (var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryWriter binWriter = new BinaryWriter(stream);
                binWriter.Write(byteList.ToArray());
            }
        }

        private byte[] SignatureInformation(List<FirmwareModel> firmwares, ref string firmwareName)
        {
            int index = 0;
            byte[] send = new byte[2044];
            //Э��汾��
            send[index++] = 0x02;
            //��˾����
            index = 1;
            foreach (var item in Encoding.ASCII.GetBytes(COMPANY_NAME))
            {
                send[index++] = (byte)item;
            }

            //��Ʒ�߱���
            index = 17;
            send[index++] = (byte)(int.Parse(cmbProductType.SelectedValue.ToString().Replace("0x", "")) & 0xFF);

            //��Ʒ�ͺű���
            index = 18;
            int productTypeCode = int.Parse(cmbProductModel.SelectedValue.ToString().Replace("0x", ""));
            send[index++] = (byte)(productTypeCode & 0xFF);
            send[index++] = (byte)(productTypeCode >> 8);

            //�̼�������
            index = 20;
            DateTime dtnow = DateTime.Now;
            if (firmwareName == "")
            {
                DataRowView drv = (DataRowView)cmbProductType.SelectedItem;
                firmwareName = drv.Row.ItemArray[1].ToString() + "_" + cmbProductModel.Text + "_" + dtnow.ToString("yyMMddHHmmss") + "." + COMPANY_NAME;
            }
            else
            {
                firmwareName = firmwareName + dtnow.ToString("yyMMddHHmmss") + "." + COMPANY_NAME;
            }
            foreach (var item in Encoding.ASCII.GetBytes(firmwareName))
            {
                send[index++] = (byte)item;
            }

            //�̼������ʱ������
            index = 72;
            send[index++] = Convert.ToByte(dtnow.Year.ToString().Substring(2, 2), 10);
            send[index++] = Convert.ToByte(dtnow.Month.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Day.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Hour.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Minute.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Second.ToString(), 10);

            //�̼�ģ������
            index = 78;
            send[index++] = (byte)firmwares.Count;

            long totalLength = 0;
            //�̼���Ϣ
            index = 137;
            foreach (FirmwareModel firmwareModel in firmwares)
            {
                //�ļ�����
                int tempIndex = index + 2;
                send[index++] = firmwareModel.FirmwareFileType;

                //оƬ��ɫ
                send[index++] = firmwareModel.FirmwareChipRole;

                //����
                index = tempIndex;
                tempIndex = index + 56;
                foreach (byte item in Encoding.ASCII.GetBytes(firmwareModel.FirmwareName))
                {
                    send[index++] = item;
                }

                //��ʼƫ�Ƶ�ַ
                index = tempIndex;
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress & 0xFF);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 8);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 16);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 24);

                //����
                send[index++] = (byte)(firmwareModel.FirmwareLength & 0xFF);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 8);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 16);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 24);

                //�汾��
                tempIndex = index + 20;
                var firmwareVersionBytes = Encoding.ASCII.GetBytes(firmwareModel.FirmwareVersion);
                foreach (byte item in firmwareVersionBytes)
                {
                    send[index++] = item;
                }
                index = tempIndex + 18;
                totalLength += firmwareModel.FirmwareLength;
            }

            //�̼����ܳ�
            index = 2040;
            send[index++] = (byte)(totalLength & 0xFF);
            send[index++] = (byte)(totalLength >> 8);
            send[index++] = (byte)(totalLength >> 16);
            send[index++] = (byte)(totalLength >> 24);

            //CRC32
            //index = 2044;
            //uint crc = CrcHelper.ComputeCrc32(send, (int)send.Length - 4);
            //send[index++] = (byte)(crc & 0xFF);
            //send[index++] = (byte)(crc >> 8);
            //send[index++] = (byte)(crc >> 16);
            //send[index++] = (byte)(crc >> 24);

            return send;
        }
        List<FirmwareModel> firmwareModels = null;
        private void AnalysisData(byte[] binchar)
        {
            //��վ�����
            firmwareModels = new List<FirmwareModel>();
            //�̼�ģ������
            int count = binchar[binchar.Length - 2048 + 78];
            byte[] firmwareBytes = binchar.Skip(binchar.Length - 2048 + 137).Take(104 * count).ToArray();
            //AddMsg($"�̼�ģ��������" + count);

            //�̼�״̬
            int index = 0;
            for (int i = 0; i < firmwareBytes.Length; i += 104)
            {
                FirmwareModel firmwareModel = new FirmwareModel();
                //�ļ�����
                firmwareModel.FirmwareFileType = firmwareBytes[i];
                //оƬ��ɫ
                firmwareModel.FirmwareChipRole = firmwareBytes[i + 1];
                //����
                firmwareModel.FirmwareName = Encoding.ASCII.GetString(firmwareBytes.Skip(i + 2).Take(56).ToArray()).Replace("\0", "");
                //��ʼƫ�Ƶ�ַ
                long startAddressByte1 = firmwareBytes[i + 58] & 0xFF;
                long startAddressByte2 = firmwareBytes[i + 59] << 8;
                long startAddressByte3 = firmwareBytes[i + 60] << 16;
                long startAddressByte4 = firmwareBytes[i + 61] << 24;
                firmwareModel.FirmwareStartAddress = startAddressByte1 + startAddressByte2 + startAddressByte3 + startAddressByte4;
                //����
                long lengthByte1 = firmwareBytes[i + 62] & 0xFF;
                long lengthByte2 = firmwareBytes[i + 63] << 8;
                long lengthByte3 = firmwareBytes[i + 64] << 16;
                long lengthByte4 = firmwareBytes[i + 65] << 24;
                firmwareModel.FirmwareLength = lengthByte1 + lengthByte2 + lengthByte3 + lengthByte4;
                //�汾��
                firmwareModel.FirmwareVersion = Encoding.ASCII.GetString(firmwareBytes.Skip(i + 66).Take(20).ToArray());
                firmwareModels.Add(firmwareModel);
                index++;
            }
        }

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductType.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbProductType.SelectedItem;
                DataRow[] rows = productModelDt.Select("ProductLineCode = '" + drv.Row[0].ToString() + "'");
                if (rows.Length > 0)
                {
                    DataTable dt = rows.CopyToDataTable();
                    cmbProductModel.DataSource = dt;
                }
            }
        }

        private void dgvFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgvFiles_Pack.ClearSelection();
        }

        //����������sofar��
        private void btnImportSofarPack_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "ѡ���ļ�";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "SOFAR���ļ�|*.sofar";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSofarPackPath.Text = openFileDialog.FileName;
                //�����ļ����ݲ�����չʾ
                CheckFile();
            }
        }
        /// <summary>
        /// ���sofar�ļ�������ѡ���bin�ļ��������Զ����ļ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Unpack_Click(object sender, EventArgs e)
        {
            List<byte> byteList = new List<byte>();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            dgvFiles_Unpack.CellValueChanged += (sender, e) =>
            {
                if (e.ColumnIndex == dgvFiles_Unpack.Columns["IsSelectBinFile_bin"].Index && e.RowIndex >= 0)
                {
                    try
                    {
                        bool isSelected = (bool)dgvFiles_Unpack.Rows[e.RowIndex].Cells["IsSelectBinFile_bin"].Value;
                    }
                    catch (Exception ex)
                    {
                        // �����쳣�����Լ�¼��־������ʾ������Ϣ
                        Console.WriteLine($"����ѡ��״̬�仯ʱ��������: {ex.Message}");
                    }
                }
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string targetFolder = folderBrowserDialog.SelectedPath;
                try
                {
                    // ����Ŀ���ļ���
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // �����̼�ģ���б���������ѡ�е��ļ�
                    foreach (var firmwareModel in firmwareModels.Where(model => model.IsSelected))
                    {
                        string destinationPath = Path.Combine(targetFolder, firmwareModel.FirmwareName);

                        // ��Դ�������ļ��ж�ȡ���������
                        using (var stream = new FileStream(txtSofarPackPath.Text, FileMode.Open, FileAccess.Read))
                        {
                            BinaryReader binReader = new BinaryReader(stream);
                            byte[] binchar = binReader.ReadBytes((int)stream.Length);

                            // ����Ŀ���ļ���д�����������
                            using (var destinationStream = File.Create(destinationPath))
                            {
                                BinaryWriter binWriter = new BinaryWriter(destinationStream);

                                // �����������ݴ��������
                                binWriter.Write(binchar, (int)firmwareModel.FirmwareStartAddress, (int)firmwareModel.FirmwareLength);
                            }
                        }
                    }
                    MessageBox.Show($"��ѡ���ļ��ѱ��浽Ŀ���ļ��С�", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"�����ļ�ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckFile()
        {
            //ÿ�ε������sofar����ť������б����
            List<byte> byteList = new List<byte>();
            int startAddress = 0;
            FileInfo fileInfo = new FileInfo(txtSofarPackPath.Text);
            List<FileInfo> Files = new List<FileInfo>();
            using (var stream = new FileStream(txtSofarPackPath.Text, FileMode.Open, FileAccess.Read))
            {
                //txtFirmwarePackName.Text = Path.GetFileNameWithoutExtension(txtSofarPackPath.Text);               
                BinaryReader binReader = new BinaryReader(stream);
                byte[] binchar = binReader.ReadBytes((int)stream.Length);
                byteList.AddRange(binchar.ToList());
                AnalysisData(binchar);
                startAddress += (int)stream.Length;
                byte[] fileLengthBytes = new byte[4];
                if (binchar[binchar.Length - 1024] == 0x01)
                {
                    fileLengthBytes = binchar.Skip(binchar.Length - 1024 - 8).Take(4).ToArray();
                }
                else
                {
                    fileLengthBytes = binchar.Skip(binchar.Length - 1024 + 1).Take(4).ToArray();
                }

                byte[] fileCrcBytes = binchar.Skip(binchar.Length - 1024 + 5).Take(4).ToArray();
                int fileLength = Convert.ToInt32((fileLengthBytes[0] & 0xff) + (fileLengthBytes[1] << 8) + (fileLengthBytes[2] << 16) + (fileLengthBytes[3] << 24));
                uint crc = 0;
                if (fileLength <= (int)binchar.Length)
                    crc = ~CrcHelper.ComputeCrc32(binchar, fileLength);

                dgvFiles_Unpack.DataSource = firmwareModels;
                //for (int i = 0; i < firmwareModels.Count; i++)
                //{
                //    dgvFiles_Unpack.Rows.Add();
                //    int rowIndex = dgvFiles_Unpack.Rows.Count - 2;
                //    dgvFiles_Unpack.Rows[rowIndex].Cells["FileName_bin"].Value = firmwareModels[i].FirmwareName;
                //    if (fileCrcBytes[3] == (byte)(crc >> 24) &&
                //        fileCrcBytes[2] == (byte)(crc >> 16) &&
                //        fileCrcBytes[1] == (byte)(crc >> 8) &&
                //        fileCrcBytes[0] == (byte)(crc & 0xFF))
                //    {
                //        dgvFiles_Unpack.Rows[rowIndex].Cells["SignatureStatus_bin"].Value = "��ǩ��";
                //        dgvFiles_Unpack.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Green;
                //        Files.Add(fileInfo);
                //    }
                //    else
                //    {
                //        dgvFiles_Unpack.Rows[rowIndex].Cells["SignatureStatus_bin"].Value = "ǩ������ȷ";
                //        dgvFiles_Unpack.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                //    }
                //}
            }
        }

        private void txtSofarPackPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}