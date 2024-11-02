using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BomTool.Controls;

public class ExcelFileSelector : Control
{
    public static RoutedCommand Switch { get; } = new(nameof(Switch), typeof(ExcelFileSelector));

    public static readonly RoutedEvent ExcelFileSelectedEvent =
        EventManager.RegisterRoutedEvent("ExcelFileSelected", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExcelFileSelector));

    public event RoutedEventHandler ExcelFileSelected
    {
        add => AddHandler(ExcelFileSelectedEvent, value);
        remove => RemoveHandler(ExcelFileSelectedEvent, value);
    }

    public static readonly RoutedEvent ExcelFileUnselectedEvent =
        EventManager.RegisterRoutedEvent("ExcelFileUnselected", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExcelFileSelector));

    public event RoutedEventHandler ExcelFileUnselected
    {
        add => AddHandler(ExcelFileUnselectedEvent, value);
        remove => RemoveHandler(ExcelFileUnselectedEvent, value);
    }

    public ExcelFileSelector() => CommandBindings.Add(new CommandBinding(Switch, SwitchExcelFile));

    private void SwitchExcelFile(object sender, ExecutedRoutedEventArgs e)
    {
        if (!HasValue)
        {
            var dialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = Filter,
                DefaultExt = DefaultExt
            };

            if (dialog.ShowDialog() == true)
            {
                SetValue(UriPropertyKey, new Uri(dialog.FileName, UriKind.RelativeOrAbsolute));
                SetValue(PreviewBrushPropertyKey, GenerateExcelBrush(dialog.FileName));
                SetValue(HasValuePropertyKey, true);
                SetCurrentValue(ToolTipProperty, dialog.FileName);
                RaiseEvent(new RoutedEventArgs(ExcelFileSelectedEvent, this));
            }
        }
        else
        {
            SetValue(UriPropertyKey, default(Uri));
            SetValue(PreviewBrushPropertyKey, default(Brush));
            SetValue(HasValuePropertyKey, false);
            SetCurrentValue(ToolTipProperty, default);
            RaiseEvent(new RoutedEventArgs(ExcelFileUnselectedEvent, this));
        }
    }

    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
        nameof(Stretch), typeof(Stretch), typeof(ExcelFileSelector), new PropertyMetadata(default(Stretch)));

    public Stretch Stretch
    {
        get => (Stretch)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }

    public static readonly DependencyPropertyKey UriPropertyKey = DependencyProperty.RegisterReadOnly(
        "Uri", typeof(Uri), typeof(ExcelFileSelector), new PropertyMetadata(default(Uri)));

    public static readonly DependencyProperty UriProperty = UriPropertyKey.DependencyProperty;

    public Uri Uri
    {
        get => (Uri)GetValue(UriProperty);
        set => SetValue(UriProperty, value);
    }

    public static readonly DependencyPropertyKey PreviewBrushPropertyKey = DependencyProperty.RegisterReadOnly(
        "PreviewBrush", typeof(Brush), typeof(ExcelFileSelector), new PropertyMetadata(default(Brush)));

    public static readonly DependencyProperty PreviewBrushProperty = PreviewBrushPropertyKey.DependencyProperty;

    public Brush PreviewBrush
    {
        get => (Brush)GetValue(PreviewBrushProperty);
        set => SetValue(PreviewBrushProperty, value);
    }

    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        nameof(StrokeThickness), typeof(double), typeof(ExcelFileSelector), new FrameworkPropertyMetadata(1.0d, FrameworkPropertyMetadataOptions.AffectsRender));

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
        nameof(StrokeDashArray), typeof(DoubleCollection), typeof(ExcelFileSelector), new FrameworkPropertyMetadata(default(DoubleCollection), FrameworkPropertyMetadataOptions.AffectsRender));

    public DoubleCollection StrokeDashArray
    {
        get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
        set => SetValue(StrokeDashArrayProperty, value);
    }

    public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register(
        nameof(DefaultExt), typeof(string), typeof(ExcelFileSelector), new PropertyMetadata(".xls"));

    public string DefaultExt
    {
        get => (string)GetValue(DefaultExtProperty);
        set => SetValue(DefaultExtProperty, value);
    }

    public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
        nameof(Filter), typeof(string), typeof(ExcelFileSelector), new PropertyMetadata("(.xls)|*.xls|(.xlsx)|*.xlsx"));

    public string Filter
    {
        get => (string)GetValue(FilterProperty);
        set => SetValue(FilterProperty, value);
    }

    public static readonly DependencyPropertyKey HasValuePropertyKey = DependencyProperty.RegisterReadOnly(
        "HasValue", typeof(bool), typeof(ExcelFileSelector), new PropertyMetadata(false));

    public static readonly DependencyProperty HasValueProperty = HasValuePropertyKey.DependencyProperty;

    public bool HasValue
    {
        get => (bool)GetValue(HasValueProperty);
        set => SetValue(HasValueProperty, value);
    }

    public static ImageBrush GenerateExcelBrush(string excelFilePath)
    {
        var thumbnail = ShellThumbnailGenerator.GetThumbnail(excelFilePath);
        if (thumbnail != null)
        {
            return new ImageBrush(thumbnail)
            {
                Stretch = Stretch.Uniform,
                TileMode = TileMode.None
            };
        }
        return null;
    }

    public class ShellThumbnailGenerator
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint SHGFI_TYPENAME = 0x400;

        public static ImageSource GetThumbnail(string filePath, bool smallIcon = false)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            uint flags = SHGFI_ICON | (smallIcon ? SHGFI_SMALLICON : SHGFI_LARGEICON);

            IntPtr result = SHGetFileInfo(filePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            if (result != IntPtr.Zero)
            {
                try
                {
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        shfi.hIcon,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DestroyIcon(shfi.hIcon);
                }
            }

            return null;
        }
    }
}
