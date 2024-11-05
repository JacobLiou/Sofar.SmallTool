using System.IO;
using System.Reflection;

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

            if (!LicenseIntegration.LicenseManager.VerifyDialog(out var licStr))
                Environment.Exit(0);
            Title = Title + licStr;
        }
    }
}