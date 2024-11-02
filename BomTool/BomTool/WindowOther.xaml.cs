using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BomTool
{
    /// <summary>
    /// Interaction logic for WindowOther.xaml
    /// </summary>
    public partial class WindowOther : Window
    {
        public ObservableCollection<string> Files
        {
            get
            {
                return _files;
            }
        }

        private ObservableCollection<string> _files = new ObservableCollection<string>();

        public WindowOther()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void DropBox_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                _files.Clear();

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string filePath in files)
                {
                    _files.Add(filePath);
                }
            }

            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void ThreadSafeUpdateStatus(string status)
        {
            StatusIndicator.Dispatcher.BeginInvoke(
                 DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
                 {
                     StatusIndicator.Text = status;
                     return null;
                 }), null);
        }
    }
}
