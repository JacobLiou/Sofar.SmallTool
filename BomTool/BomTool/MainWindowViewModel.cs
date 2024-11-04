using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BomTool;

public partial class MainWindowViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string _droppedText;

    [ObservableProperty]
    private bool _isDraggingOver;

    [ObservableProperty]
    private int selectedIndex;

    [ObservableProperty]
    private bool _isConnected;

    [ObservableProperty]
    private string? _connStatus;

    [ObservableProperty]
    private int _sendBytesCount;

    [ObservableProperty]
    private int _receiveBytesCount;

    [ObservableProperty]
    private int _txCount;

    [ObservableProperty]
    private int _rxCount;

    [ObservableProperty]
    private string? _lossPercent;

    public MainWindowViewModel()
    {
    }

    [RelayCommand]
    private void Loaded()
    {

    }

    [RelayCommand]
    private void OnDragEnter(DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
            e.Data.GetDataPresent(DataFormats.Text))
        {
            e.Effects = DragDropEffects.Copy;
            IsDraggingOver = true;
        }
        else
        {
            e.Effects = DragDropEffects.None;
            IsDraggingOver = false;
        }
        e.Handled = true;
    }

    [RelayCommand]
    private async void OnDrop(DragEventArgs e)
    {
        IsDraggingOver = false;

        try
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files?.Length > 0)
                {
                    // 读取第一个文件的内容
                    DroppedText = await File.ReadAllTextAsync(files[0]);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                DroppedText = (string)e.Data.GetData(DataFormats.Text);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"处理拖放数据时出错：{ex.Message}");
        }

        e.Handled = true;
    }




}