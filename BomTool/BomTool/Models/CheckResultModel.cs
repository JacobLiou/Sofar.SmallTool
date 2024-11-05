using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomTool.Models
{
    [AddINotifyPropertyChangedInterface]
    public class CheckResultModel
    {
        public bool IsSuccess { get; set; } = false;

        public string? BoardFilePath { get; set; }

        public string? CoordFilePath { get; set; }

        public ObservableCollection<BoardCheckModel>? FailedBoardCheckModels { get; set; } = new ObservableCollection<BoardCheckModel>();
    }
}
