using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BomTool;

public partial class MainWindowViewModel : ObservableRecipient
{
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
}