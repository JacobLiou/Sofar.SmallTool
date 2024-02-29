using System;
using PackagingTools.Common;
using System.Data;
using System.Text;
using UpgradePackTool.Common;
using UpgradePackTool.Model;
using Newtonsoft.Json;

namespace PackagingTools
{
    public partial class FrmMain : Form
    {
        const string COMPANY_NAME = "sofar";
        const int BLOCK_SIZE = 256;

        DataTable productTypeDt = new DataTable();
        DataTable productModelDt = new DataTable();

        List<FileInfo> files = new List<FileInfo>();

        public FrmMain()
        {
            InitializeComponent();
            dgvFiles_Unpack.AutoGenerateColumns = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtSelectFolderPath.Text = IniConfig.ReadIniData("IniConfig", "SelectFolderPath", "");

            //ProductModelList productModelList = JsonConvert.DeserializeObject<ProductModelList>(File.ReadAllText("config.json"));

            //productModelDt.Columns.Add("ProductLineCode");
            //productModelDt.Columns.Add("ProductModelCode");
            //productModelDt.Columns.Add("ProductModel");

            //productTypeDt.Columns.Add("ProductLineCode");
            //productTypeDt.Columns.Add("ProductType");
            //productTypeDt.Columns.Add("ProductLine");

            //if (productModelList == null)
            //{
            //    List < ProductModel > productModel = new List<ProductModel>()
            //    {
            //        new ProductModel("0x01", "INV", "逆变器", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "320KW") }),
            //        new ProductModel("0x02", "STORAGE", "储能", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "SKY 5-20KW") }),
            //        new ProductModel("0x03", "ESS", "集储", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "ESS2000-CSU"), new ProductDetailedModel("0x0002", "ESS2000-CMU") }),
            //        new ProductModel("0x04", "M-INV", "微逆", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "XXXX") }),
            //        new ProductModel("0x05", "M-IOT", "物联产品", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "LEO5-GATE") }),
            //        new ProductModel("0x06", "BAT", "电池", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "GTX5000S"), new ProductDetailedModel("0x0002", "CBS5000") })
            //    };
            //    productModelList = new ProductModelList(productModel);
            //}

            //for (int i = 0; i < productModelList.ProductModel.Count; i++)
            //{
            //    for (int ii = 0; ii < productModelList.ProductModel[i].ProductDetailedModel.Count; ii++)
            //    {
            //        productModelDt.Rows.Add(productModelList.ProductModel[i].ProductLineCode, 
            //            productModelList.ProductModel[i].ProductDetailedModel[ii].ProductModelCode, 
            //            productModelList.ProductModel[i].ProductDetailedModel[ii].ProductModel);
            //    }
            //    productTypeDt.Rows.Add(productModelList.ProductModel[i].ProductLineCode, 
            //        productModelList.ProductModel[i].ProductType, 
            //        productModelList.ProductModel[i].ProductLine);
            //}

            //cmbProductModel.DataSource = productModelDt;
            //cmbProductModel.ValueMember = "ProductModelCode";
            //cmbProductModel.DisplayMember = "ProductModel";

            //cmbProductType.DataSource = productTypeDt;
            //cmbProductType.ValueMember = "ProductLineCode";
            //cmbProductType.DisplayMember = "ProductLine";

            //int.TryParse(IniConfig.ReadIniData("IniConfig", "ProductType", ""), out int productTypeSelectedIndex);
            //cmbProductType.SelectedIndex = productTypeSelectedIndex;
            //int.TryParse(IniConfig.ReadIniData("IniConfig", "ProductModel", ""), out int productModelSelectedIndex);
            //cmbProductModel.SelectedIndex = productModelSelectedIndex;
            txtFirmwarePackName.Text = IniConfig.ReadIniData("IniConfig", "FirmwarePackName", "");
            int.TryParse(IniConfig.ReadIniData("IniConfig", "TimeStamp", ""), out int timeStamp);
            if (timeStamp == 1)
            {
                rdoTimeStampYes.Checked = true;
            }
            else
            {
                rdoTimeStampNo.Checked = true;
            }
            txtProductType.Text = IniConfig.ReadIniData("IniConfig", "ProductType", "");
            txtProductModel.Text = IniConfig.ReadIniData("IniConfig", "ProductModel", "");
            txtCompany.Text = IniConfig.ReadIniData("IniConfig", "Company", "");
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.SelectedPath == string.Empty)
            {
                MessageBox.Show("未选择文件夹");
                return;
            }
            txtSelectFolderPath.Text = folderBrowserDialog.SelectedPath;
            IniConfig.WriteIniData("IniConfig", "SelectFolderPath", txtSelectFolderPath.Text);
        }

        private void btnGetFileInformation_Click(object sender, EventArgs e)
        {
            if (txtSelectFolderPath.Text == string.Empty)
            {
                MessageBox.Show("未选择文件夹");
                return;
            }

            dgvFiles.Rows.Clear();
            files.Clear();

            DirectoryInfo folder = new DirectoryInfo(txtSelectFolderPath.Text);
            foreach (FileInfo file in folder.GetFiles())
            {
                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(stream);
                    byte[] binchar = binReader.ReadBytes((int)stream.Length);
                    byte[] fileLengthBytes = new byte[4];
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

                    dgvFiles.Rows.Add();
                    dgvFiles.Rows[dgvFiles.Rows.Count - 1].Cells["FileName"].Value = file.Name;
                    if (fileCrcBytes[3] == (byte)(crc >> 24) && fileCrcBytes[2] == (byte)(crc >> 16) &&
                        fileCrcBytes[1] == (byte)(crc >> 8) && fileCrcBytes[0] == (byte)(crc & 0xFF))
                    {
                        dgvFiles.Rows[dgvFiles.Rows.Count - 1].Cells["SignatureStatus"].Value = "已签名";
                        dgvFiles.Rows[dgvFiles.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
                        files.Add(file);
                    }
                    else
                    {
                        dgvFiles.Rows[dgvFiles.Rows.Count - 1].Cells["SignatureStatus"].Value = "签名不正确";
                        dgvFiles.Rows[dgvFiles.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!InputCheck()) return;

            IniConfig.WriteIniData("IniConfig", "FirmwarePackName", txtFirmwarePackName.Text);
            IniConfig.WriteIniData("IniConfig", "TimeStamp", rdoTimeStampYes.Checked ? "1" : "0");
            IniConfig.WriteIniData("IniConfig", "ProductType", txtProductType.Text);
            IniConfig.WriteIniData("IniConfig", "ProductModel", txtProductModel.Text);
            IniConfig.WriteIniData("IniConfig", "Company", txtCompany.Text);

            if (files.Count == 0)
            {
                MessageBox.Show("文件夹内没有带签名信息的固件");
                return;
            }

            List<FirmwareModel> firmwares = new List<FirmwareModel>();
            List<byte> byteList = new List<byte>();
            int startAddress = 0;

            //固件模块
            foreach (FileInfo file in files)
            {
                int index = 0;
                //bool flg = false;
                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(stream);
                    byte[] binchar = binReader.ReadBytes((int)stream.Length);
                    for (int i = binchar.Length - 1024 - 256; i > 0; i--)
                    {
                        byte b = binchar[i - 1];
                        //if (!flg && b == 0xff) flg = true;
                        //if (flg && b != 0xff)
                        //{
                        //    index = i;
                        //    break;
                        //}
                        if (b != 0xff)
                        {
                            if (i % 256 == 0)
                            {
                                index = i;
                            }
                            else
                            {
                                index = i + 256 - i % 256;
                            }
                            break;
                        }
                    }
                    byteList.AddRange(binchar.Skip(0).Take(index).ToList());
                    byteList.AddRange(binchar.Skip(binchar.Length - 1024 - 256).Take(1024 + 256).ToList());
                    //byteList.AddRange(binchar.ToList());

                    FirmwareModel firmwareModel = new FirmwareModel();
                    firmwareModel.FirmwareName = file.Name;
                    firmwareModel.FirmwareChipRole = binchar.Skip(binchar.Length - 1024 + 125).Take(1).ToArray()[0]; 
                    firmwareModel.FirmwareFileType = binchar.Skip(binchar.Length - 1024 + 128).Take(1).ToArray()[0];
                    firmwareModel.FirmwareStartAddress = startAddress;
                    firmwareModel.FirmwareLength = 1024 + 256 + index;
                    //firmwareModel.FirmwareLength = (int)stream.Length;
                    firmwareModel.FirmwareVersion = Encoding.ASCII.GetString(binchar.Skip(binchar.Length - 1024 + 1 + 4 + 4 + 30).Take(20).ToArray());
                    firmwares.Add(firmwareModel);

                    startAddress += 1024 + 256 + index;
                }
            }

            //签名信息
            string firmwareName = "";
            DateTime dtnow = DateTime.Now;
            if (rdoTimeStampYes.Checked)
            {
                firmwareName = txtFirmwarePackName.Text + "_" + dtnow.ToString("yyMMddHHmmss") + "." + txtCompany.Text;
            }
            else
            {
                firmwareName = txtFirmwarePackName.Text + "." + txtCompany.Text;
            }
            byte[] signatureInformationByte = SignatureInformation(firmwares, dtnow, firmwareName);
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
            if (path == "")//判断路径是否为空
            {
                MessageBox.Show("导出失败");
                return;
            }

            using (var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryWriter binWriter = new BinaryWriter(stream);
                binWriter.Write(byteList.ToArray());
            }
        }

        private byte[] SignatureInformation(List<FirmwareModel> firmwares, DateTime dtnow, string firmwareName)
        {
            int index = 0;
            byte[] send = new byte[2044];
            //协议版本号
            send[index++] = 0x02;
            //公司名称
            index = 1;
            foreach (var item in Encoding.ASCII.GetBytes(COMPANY_NAME))
            {
                send[index++] = (byte)item;
            }

            //产品线编码
            index = 17;
            send[index++] = (byte)(int.Parse(txtProductType.Text.Replace("0x","")) & 0xFF);

            //产品型号编码
            index = 18;
            int productTypeCode = int.Parse(txtProductModel.Text.Replace("0x", ""));
            send[index++] = (byte)(productTypeCode & 0xFF);
            send[index++] = (byte)(productTypeCode >> 8);

            //固件包名称
            index = 20;
            foreach (var item in Encoding.ASCII.GetBytes(firmwareName))
            {
                send[index++] = (byte)item;
            }

            //固件包打包时间日期
            index = 72;
            send[index++] = Convert.ToByte(dtnow.Year.ToString().Substring(2, 2), 10);
            send[index++] = Convert.ToByte(dtnow.Month.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Day.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Hour.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Minute.ToString(), 10);
            send[index++] = Convert.ToByte(dtnow.Second.ToString(), 10);

            //固件模块数量
            index = 78;
            send[index++] = (byte)firmwares.Count;

            long totalLength = 0;
            //固件信息
            index = 137;
            foreach (FirmwareModel firmwareModel in firmwares)
            {
                //文件类型
                int tempIndex = index + 2;
                send[index++] = firmwareModel.FirmwareFileType;

                //芯片角色
                send[index++] = firmwareModel.FirmwareChipRole;

                //名称
                index = tempIndex;
                tempIndex = index + 56;
                foreach (byte item in Encoding.ASCII.GetBytes(firmwareModel.FirmwareName))
                {
                    send[index++] = item;
                }

                //起始偏移地址
                index = tempIndex;
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress & 0xFF);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 8);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 16);
                send[index++] = (byte)(firmwareModel.FirmwareStartAddress >> 24);

                //长度
                send[index++] = (byte)(firmwareModel.FirmwareLength & 0xFF);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 8);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 16);
                send[index++] = (byte)(firmwareModel.FirmwareLength >> 24);

                //版本号
                tempIndex = index + 20;
                var firmwareVersionBytes = Encoding.ASCII.GetBytes(firmwareModel.FirmwareVersion);
                foreach (byte item in firmwareVersionBytes)
                {
                    send[index++] = item;
                }
                index = tempIndex + 18;
                totalLength += firmwareModel.FirmwareLength;
            }

            //固件包总长
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

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbProductType.SelectedIndex > -1)
            //{
            //    DataRowView drv = (DataRowView)cmbProductType.SelectedItem;
            //    DataRow[] rows = productModelDt.Select("ProductLineCode = '" + drv.Row[0].ToString() + "'");
            //    if (rows.Length > 0)
            //    {
            //        DataTable dt = rows.CopyToDataTable();
            //        cmbProductModel.DataSource = dt;
            //    }
            //}
        }

        private void dgvFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgvFiles.ClearSelection();
        }

        List<FirmwareModel> firmwareModels = new List<FirmwareModel>();
        string safeFileNameStr = "";
        private void btnImportSofarPack_Click(object sender, EventArgs e)
        {
            safeFileNameStr = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.sofar;*.tar)|*.sofar;*.tar||";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dgvFiles_Unpack.DataSource = null;
                string file_name = openFileDialog.FileName;
                safeFileNameStr = openFileDialog.SafeFileName;
                using (var stream = new FileStream(file_name, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(stream);
                    byte[] binchar = binReader.ReadBytes((int)stream.Length);
                    AnalysisData(binchar);
                }
                txtSofarPackPath.Text = file_name;
                dgvFiles_Unpack.DataSource = firmwareModels;
                dgvFiles_Unpack.Refresh();
            }
        }

        private void AnalysisData(byte[] binchar)
        {
            //清空旧数据
            firmwareModels.Clear();
            //固件模块数量
            int count = binchar[binchar.Length - 2048 + 78];
            byte[] firmwareBytes = binchar.Skip(binchar.Length - 2048 + 137).Take(104 * count).ToArray();
            //AddMsg($"固件模块数量：" + count);

            //固件状态
            int index = 0;
            for (int i = 0; i < firmwareBytes.Length; i += 104)
            {
                FirmwareModel firmwareModel = new FirmwareModel();
                //文件类型
                firmwareModel.FirmwareFileType = firmwareBytes[i];
                //芯片角色
                firmwareModel.FirmwareChipRole = firmwareBytes[i + 1];
                //名称
                firmwareModel.FirmwareName = Encoding.ASCII.GetString(firmwareBytes.Skip(i + 2).Take(56).ToArray()).Replace("\0", "");
                //起始偏移地址
                long startAddressByte1 = firmwareBytes[i + 58] & 0xFF;
                long startAddressByte2 = firmwareBytes[i + 59] << 8;
                long startAddressByte3 = firmwareBytes[i + 60] << 16;
                long startAddressByte4 = firmwareBytes[i + 61] << 24;
                firmwareModel.FirmwareStartAddress = startAddressByte1 + startAddressByte2 + startAddressByte3 + startAddressByte4;
                //长度
                long lengthByte1 = firmwareBytes[i + 62] & 0xFF;
                long lengthByte2 = firmwareBytes[i + 63] << 8;
                long lengthByte3 = firmwareBytes[i + 64] << 16;
                long lengthByte4 = firmwareBytes[i + 65] << 24;
                firmwareModel.FirmwareLength = lengthByte1 + lengthByte2 + lengthByte3 + lengthByte4;
                //版本号
                firmwareModel.FirmwareVersion = Encoding.ASCII.GetString(firmwareBytes.Skip(i + 66).Take(20).ToArray());
                //数据
                firmwareModel.FirmwareData = binchar.Skip((int)firmwareModel.FirmwareStartAddress).Take((int)firmwareModel.FirmwareLength).ToArray();
                firmwareModels.Add(firmwareModel);
                index++;
            }
        }

        private void btnUnpack_Click(object sender, EventArgs e)
        {
            List<FirmwareModel> firmwares = new List<FirmwareModel>();
            List<byte> byteList = new List<byte>();

            int startAddress = 0;
            int i = 0;
            foreach (FirmwareModel firmwareModel in firmwareModels)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvFiles_Unpack.Rows[i].Cells["Recombination"];
                if (Convert.ToBoolean(checkCell.Value))
                {
                    firmwareModel.FirmwareStartAddress = startAddress;
                    startAddress += (int)firmwareModel.FirmwareLength;
                    firmwares.Add(firmwareModel);

                    byteList.AddRange(firmwareModel.FirmwareData.ToList());
                }
                i++;
            }

            if (firmwares.Count == 0)
            {
                MessageBox.Show("至少选择一个固件进行重组");
                return;
            }

            //签名信息
            string tempStr = safeFileNameStr.Substring(safeFileNameStr.LastIndexOf("_") + 1,
                    safeFileNameStr.LastIndexOf(".") - safeFileNameStr.LastIndexOf("_") - 1);
            DateTime dtnow = DateTime.Now;
            string firmwareName = safeFileNameStr;
            if (DateTime.TryParseExact(tempStr, "yyMMddHHmmss", 
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                firmwareName = firmwareName.Replace(tempStr, dtnow.ToString("yyMMddHHmmss"));
            }
            byte[] signatureInformationByte = SignatureInformation(firmwares, dtnow, firmwareName);
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
            if (path == "")//判断路径是否为空
            {
                MessageBox.Show("导出失败");
                return;
            }

            using (var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryWriter binWriter = new BinaryWriter(stream);
                binWriter.Write(byteList.ToArray());
            }
        }

        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(txtFirmwarePackName.Text))
            {
                MessageBox.Show("输入固件包名称不正确");
                return false;
            }

            if (string.IsNullOrEmpty(txtProductType.Text) || !IsIllegalHexadecimal(txtProductType.Text))
            {
                MessageBox.Show("输入产品类型编码不正确");
                return false;
            }

            if (string.IsNullOrEmpty(txtProductModel.Text) || !IsIllegalHexadecimal(txtProductModel.Text))
            {
                MessageBox.Show("输入产品机型编码不正确");
                return false;
            }

            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                MessageBox.Show("输入公司名称不正确");
                return false;
            }

            return true;
        }

        public bool IsIllegalHexadecimal(string hex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hex, @"([^A-Fa-f0-9]|\s+?)+");
        }

        private void chkDefaultName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefaultName.Checked)
            {
                txtCompany.Text = COMPANY_NAME;
            }
        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {
            if (txtCompany.Text != COMPANY_NAME)
            {
                chkDefaultName.Checked = false;
            }
        }
    }
}