using NPOI.SS.Formula.Functions;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BomTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            var fileInfo = new FileInfo(assembly.Location);
            Title = $"BOM 检查工具 V{fvi.FileVersion} 编译：{fileInfo.LastWriteTime.ToString("yyyy/MM/dd-HH:mm:ss-UTCzz")}";

            //if (!LicenseIntegration.LicenseManager.VerifyDialog(out var licStr))
            //    Environment.Exit(0);
            //Title = Title + licStr;
        }

        //private void DropBox_Drop(object sender, DragEventArgs e)
        //{

        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        _files.Clear();

        //        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

        //        foreach (string filePath in files)
        //        {
        //            _files.Add(filePath);
        //        }
        //    }

        //    var listbox = sender as ListBox;
        //    listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        //}

        //private void DropBox_DragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effects = DragDropEffects.Copy;
        //        var listbox = sender as ListBox;
        //        listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void DropBox_DragLeave(object sender, DragEventArgs e)
        //{
        //    var listbox = sender as ListBox;
        //    listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        //}
    }
}