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

        DataTable productTypeDt = new DataTable();
        DataTable productModelDt = new DataTable();

        List<FileInfo> files = new List<FileInfo>();

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
                List < ProductModel > productModel = new List<ProductModel>()
                {
                    new ProductModel("0x01", "INV", "逆变器", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "320KW") }),
                    new ProductModel("0x02", "STORAGE", "储能", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "SKY 5-20KW") }),
                    new ProductModel("0x03", "ESS", "集储", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "ESS2000-CSU"), new ProductDetailedModel("0x0002", "ESS2000-CMU") }),
                    new ProductModel("0x04", "M-INV", "微逆", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "XXXX") }),
                    new ProductModel("0x05", "M-IOT", "物联产品", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "LEO5-GATE") }),
                    new ProductModel("0x06", "BAT", "电池", new List<ProductDetailedModel>(){ new ProductDetailedModel("0x0001", "GTX5000S"), new ProductDetailedModel("0x0002", "CBS5000") })
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
                    dgvFiles.Rows[dgvFiles.Rows.Count - 2].Cells["FileName"].Value = file.Name;
                    if (fileCrcBytes[3] == (byte)(crc >> 24) && fileCrcBytes[2] == (byte)(crc >> 16) &&
                        fileCrcBytes[1] == (byte)(crc >> 8) && fileCrcBytes[0] == (byte)(crc & 0xFF))
                    {
                        dgvFiles.Rows[dgvFiles.Rows.Count - 2].Cells["SignatureStatus"].Value = "已签名";
                        dgvFiles.Rows[dgvFiles.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Green;
                        files.Add(file);
                    }
                    else
                    {
                        dgvFiles.Rows[dgvFiles.Rows.Count - 2].Cells["SignatureStatus"].Value = "签名不正确";
                        dgvFiles.Rows[dgvFiles.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
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
                MessageBox.Show("文件夹内没有带签名信息的固件");
                return;
            }

            List<FirmwareModel> firmwares = new List<FirmwareModel>();
            List<byte> byteList = new List<byte>();
            string firmwareName = txtFirmwarePackName.Text;
            int startAddress = 0;

            //固件模块
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

            //签名信息
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

        private byte[] SignatureInformation(List<FirmwareModel> firmwares, ref string firmwareName)
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
            send[index++] = (byte)(int.Parse(cmbProductType.SelectedValue.ToString().Replace("0x","")) & 0xFF);

            //产品型号编码
            index = 18;
            int productTypeCode = int.Parse(cmbProductModel.SelectedValue.ToString().Replace("0x", ""));
            send[index++] = (byte)(productTypeCode & 0xFF);
            send[index++] = (byte)(productTypeCode >> 8);

            //固件包名称
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
            dgvFiles.ClearSelection();
        }
    }
}