using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomTool.Models
{
    [AddINotifyPropertyChangedInterface]
    public class CoordinateCheckModel
    {
        public int RowIndex { get; set; }

        public string? RefDes { get; set; }
    }
}
