using BomTool.Excel;
using BomTool.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace BomTool;

public partial class CheckResultViewModel : ObservableRecipient
{
    [ObservableProperty]
    private CheckResultModel? _checkResultModel;

    public CheckResultViewModel()
    {
    }

    internal void SetParam(CheckResultModel checkResultModel)
    {
        CheckResultModel = checkResultModel;
    }

    [RelayCommand]
    private void ExportFailed()
    {
        // 保存文件
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            FilterIndex = 1,
            DefaultExt = "xlsx"
        };

        var res = saveFileDialog.ShowDialog();
        if (res.HasValue && res.Value)
        {
            ExcelExporter.ExportToExcel(CheckResultModel!.FailedBoardCheckModels!.ToList(), saveFileDialog.FileName);
        }
    }

    [RelayCommand]
    private void OpenExcel()
    {
        try
        {
            var name = $"Open Excel File -{DateTime.Now.ToString("yyyyMMdd")}";
            System.Diagnostics.Process.Start(@"explorer.exe", CheckResultModel!.BoardFilePath!);
            System.Diagnostics.Process.Start(@"explorer.exe", CheckResultModel!.CoordFilePath!);
        }
        catch
        {
            // don't care
        }
    }
}