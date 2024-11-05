using PropertyChanged;
using System.Windows.Media;

namespace BomTool.Models
{
    [AddINotifyPropertyChangedInterface]
    public class BoardCheckModel
    {
        public int RowIndex { get; set; }

        public string? UsualQuantity { get; set; }

        public string? CoordNo { get; set; }

        public string? Description { get; set; } = "";

        public bool HasError { get; set; } = false;

        public Brush? BackgroundColor { get; set; }
    }
}
