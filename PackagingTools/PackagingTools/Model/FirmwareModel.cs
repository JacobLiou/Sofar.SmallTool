using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradePackTool.Model
{
    public class FirmwareModel
    {
        public FirmwareModel()
        {

        }
        public FirmwareModel(string firmwareName, byte firmwareFileType, byte firmwareChipRole, int firmwareStartAddress, string firmwareVersion, int firmwareLength)
        {
            this.FirmwareName = firmwareName;
            this.FirmwareFileType = firmwareFileType;
            this.FirmwareChipRole = firmwareChipRole;
            this.FirmwareStartAddress = firmwareStartAddress;
            this.FirmwareVersion = firmwareVersion;
            this.FirmwareLength = firmwareLength;
        }

        //名称
        public string FirmwareName { get; set; }

        //文件类型
        public byte FirmwareFileType { get; set; }

        //芯片角色
        public byte FirmwareChipRole { get; set; }

        //起始偏移地址
        public long FirmwareStartAddress { get; set; }

        //版本号
        public string FirmwareVersion { get; set; }

        //长度
        public long FirmwareLength { get; set; }

        //文件类型
        public byte[] FirmwareData { get; set; }

    }
}
