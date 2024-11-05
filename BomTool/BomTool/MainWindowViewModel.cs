using BomTool.Behaviors;
using BomTool.Const;
using BomTool.Excel;
using BomTool.Models;
using BomTool.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;


namespace BomTool;

public partial class MainWindowViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private string? _boardFilePath;

    [ObservableProperty]
    private string? _coordFilePath;

    [ObservableProperty]
    private List<string>? _boardSheetNames;

    [ObservableProperty]
    private List<string>? _coordSheetNames;

    [ObservableProperty]
    private int _selectedBoardSheetIndex;

    [ObservableProperty]
    private int selectedCoordSheetIndex;

    [ObservableProperty]
    private DragAcceptDescription _description;

    [ObservableProperty]
    private bool _executable;

    [ObservableProperty]
    private ObservableCollection<BoardCheckModel>? _boardTables;

    [ObservableProperty]
    private ObservableCollection<CoordinateCheckModel>? _coordTables;

    public MainWindowViewModel()
    {
        _description = new DragAcceptDescription();
        _description.DragDrop += DragDrop;
        _description.DragDrop += DragOver;
    }

    [RelayCommand]
    private void Loaded()
    {
    }

    private void DragDrop(DragEventArgs e)
    {
        var paths = e.Data.GetData(DataFormats.FileDrop) as string[];
        if (paths == null || !paths.Any())
            return;

        var target = e.Source as FrameworkElement;
        if (target == null)
            return;

        if (paths.ToList().Any(path => Path.GetExtension(path) != ".xls" && Path.GetExtension(path) != ".xlsx"))
        {
            MessageTips.ShowDialog("请拖拽Excel文件");
            return;
        }

        OnDragDrop(paths, target);
    }

    protected virtual void OnDragDrop(string[] filePath, FrameworkElement target)
    {
        if (filePath.Length > 1)
        {
            CoordFilePath = filePath[1];
            BoardFilePath = filePath[0];

            return;
        }

        var tag = Convert.ToInt32(target.Tag);
        if (tag == 0)
            BoardFilePath = filePath[0];
        else if (tag == 1)
            CoordFilePath = filePath[0];

        UpdateExecutableFlag();
    }

    private void DragOver(DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            e.Effects = DragDropEffects.Copy;
        else
            e.Effects = DragDropEffects.None;

        e.Handled = true;
    }

    [RelayCommand]
    private void OpenBoardFile()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Title = "选择Excel文件 (板文件)",
            Filter = "Excel文件|*.xls;*.xlsx"
        };

        var res = dialog.ShowDialog();
        if (res.HasValue && res.Value)
            BoardFilePath = dialog.FileName;

        UpdateExecutableFlag();
    }

    [RelayCommand]
    private void OpenCoordFile()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Title = "选择Excel文件 (坐标文件)",
            Filter = "Excel文件|*.xls;*.xlsx"
        };

        var res = dialog.ShowDialog();
        if (res.HasValue && res.Value)
            CoordFilePath = dialog.FileName;

        UpdateExecutableFlag();
    }

    private void UpdateExecutableFlag()
    {
        var existsBoard = File.Exists(BoardFilePath);
        var existsCoord = File.Exists(CoordFilePath);

        if (existsBoard)
        {
            BoardSheetNames = ExcelWorkbook.GetSheetNames(BoardFilePath!).ToList();
            SelectedBoardSheetIndex = 0;
        }
        else
        {
            BoardSheetNames = new List<string>();
            SelectedBoardSheetIndex = -1;
        }

        if (existsCoord)
        {
            CoordSheetNames = ExcelWorkbook.GetSheetNames(CoordFilePath!).ToList();
            SelectedCoordSheetIndex = 0;
        }
        else
        {
            CoordSheetNames = new List<string>();
            SelectedCoordSheetIndex = -1;
        }

        Executable = existsBoard && existsCoord;
    }

    [RelayCommand]
    private async Task StartCheck()
    {
        if (!File.Exists(BoardFilePath) || !File.Exists(CoordFilePath))
        {
            MessageTips.ShowDialog("文件不存在");
            return;
        }

        try
        {
            IsRunning = true;
            BoardTables?.Clear();
            CoordTables?.Clear();

            await Task.Delay(10);
            NLogManager.Info(BoardFilePath);
            NLogManager.Info(CoordFilePath);

            if (!ProcessBoardSheet())
                return;

            if (!ProcessCoordSheet())
                return;

            CheckBoardSheet();

            CheckResultModel checkResultModel = new CheckResultModel()
            {
                BoardFilePath = BoardFilePath,
                CoordFilePath = CoordFilePath,
            };

            foreach (var board in BoardTables!)
            {
                if (board.HasError)
                    checkResultModel.FailedBoardCheckModels!.Add(board);
            }

            checkResultModel.IsSuccess = !checkResultModel.FailedBoardCheckModels!.Any();
            if (checkResultModel.IsSuccess)
                _ = Task.Run(() => PlayResultSound(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "Audio", "success.mp3")));
            else
                _ = Task.Run(() => PlayResultSound(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "Audio", "fail.mp3")));

            CheckResult resultView = new CheckResult();
            var vm = resultView.DataContext as CheckResultViewModel;
            vm?.SetParam(checkResultModel);
            resultView.ShowDialog();
        }
        catch (Exception ex)
        {
            NLogManager.Error(ex.StackTrace!);
            MessageTips.ShowDialog(ex.Message);
        }
        finally
        {
            IsRunning = false;
        }
    }

    private bool ProcessBoardSheet()
    {
        var boardWorkbook = ExcelWorkbook.Create(BoardFilePath!);

        var boardSheet = ExcelWorkbook.GetSheetByName(boardWorkbook, BoardSheetNames![SelectedBoardSheetIndex]);
        if (boardSheet == null || boardSheet.Rows == null || boardSheet.Rows.Count == 0)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(BoardFilePath)}{BoardSheetNames![SelectedBoardSheetIndex]}为空");
            return false;
        }

        var validIndex = -1;
        var uqColumnIndex = -1;
        var cnColumnIndex = -1;
        for (int i = 0; i < boardSheet.Rows.Count; i++)
        {
            ////查找特征值
            var uqCell = boardSheet.Rows[i].Cells.Find(item => item.Value == ColumnConst.UsualQuantity
                 || item.Value == ColumnConst.UsualQuantity1);

            var cnCell = boardSheet.Rows[i].Cells.Find(item => item.Value == ColumnConst.CoordNo);
            if (uqCell != null && cnCell != null)
            {
                validIndex = i;
                uqColumnIndex = uqCell.OriginalColumnIndex;
                cnColumnIndex = cnCell.OriginalColumnIndex;
                break;
            }
        }

        if (validIndex == -1 || uqColumnIndex == -1 || cnColumnIndex == -1)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(BoardFilePath)}{BoardSheetNames![SelectedBoardSheetIndex]}未找到特征值{ColumnConst.UsualQuantity}或者{ColumnConst.CoordNo}");
            return false;
        }

        BoardTables = new();
        for (int i = validIndex + 1; i < boardSheet.Rows.Count; i++)
        {
            var uqCell = boardSheet.Rows[i].Cells[uqColumnIndex];
            var cnCell = boardSheet.Rows[i].Cells[cnColumnIndex];
            BoardTables.Add(new BoardCheckModel
            {
                RowIndex = i + 1,
                UsualQuantity = uqCell.Value,
                CoordNo = cnCell.Value,
            });
        }

        LogUtil.LogObservableJson(BoardTables);
        if (BoardTables.Count == 0)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(BoardFilePath)} {BoardSheetNames![SelectedBoardSheetIndex]}没有数据行");
            return false;
        }

        return true;
    }

    private bool ProcessCoordSheet()
    {
        var coordWorkbook = ExcelWorkbook.Create(CoordFilePath!);
        var coordSheet = ExcelWorkbook.GetSheetByName(coordWorkbook, CoordSheetNames![SelectedCoordSheetIndex]);
        if (coordSheet == null || coordSheet.Rows == null || coordSheet.Rows.Count == 0)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(CoordFilePath)}{CoordSheetNames![SelectedCoordSheetIndex]}为空");
            return false;
        }

        var cwIndex = -1;
        var rdColumnIndex = -1;
        for (int i = 0; i < coordSheet.Rows.Count; i++)
        {
            ////查找特征值
            var rdCell = coordSheet.Rows[i].Cells.Find(item => item.Value == ColumnConst.RefDes);
            if (rdCell != null)
            {
                cwIndex = i;
                rdColumnIndex = rdCell.OriginalColumnIndex;
                break;
            }
        }

        if (cwIndex == -1 || rdColumnIndex == -1)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(CoordFilePath)}{CoordSheetNames![SelectedCoordSheetIndex]}未找到特征值{ColumnConst.RefDes}");
            return false;
        }

        CoordTables = new();
        for (int i = cwIndex + 1; i < coordSheet.Rows.Count; i++)
        {
            var uqCell = coordSheet.Rows[i].Cells[rdColumnIndex];
            CoordTables.Add(new CoordinateCheckModel
            {
                RowIndex = i + 1,
                RefDes = uqCell.Value,
            });
        }

        LogUtil.LogObservableJson(CoordTables);
        if (CoordTables.Count == 0)
        {
            MessageTips.ShowDialog($"{Path.GetFileNameWithoutExtension(CoordFilePath)}{CoordSheetNames![SelectedCoordSheetIndex]}没有数据行");
            return false;
        }

        return true;
    }

    private void CheckBoardSheet()
    {
        var coordSet = new HashSet<string>();
        CoordTables?.ToList().ForEach(item =>
        {
            if (!string.IsNullOrEmpty(item.RefDes))
                coordSet.Add(item.RefDes.Trim());
        });

        var boardSetDic = new Dictionary<int, HashSet<string>>();
        foreach (var board in BoardTables!)
        {
            if (!ParseDecimalString(board.UsualQuantity!, out var uq))
            {
                board.HasError = true;
                board.Description += $"{ColumnConst.UsualQuantity}{board.UsualQuantity}无法转换为整数\n";
            }

            var sArray = board.CoordNo?.Split('，', ',', ';', '；', ' ', ' ').ToList();
            if (sArray == null)
            {
                board.HasError = true;
                board.Description += $"{ColumnConst.CoordNo}个数为空\n";
                continue;
            }

            sArray = sArray.Select(s => s.Trim()).ToList();
            sArray.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            if (sArray.Count != uq)
            {
                board.HasError = true;
                board.Description += $"{ColumnConst.UsualQuantity}-{uq}和{ColumnConst.CoordNo}-{sArray.Count} 个数不匹配\n";
            }

            var tHashSet = new HashSet<string>();
            foreach (var s in sArray)
            {
                if (tHashSet.Contains(s))
                {
                    board.Description += $"本身存在重复坐标 {s}\n";
                    board.HasError = true;
                }
                else
                    tHashSet.Add(s);
            }

            foreach (var pair in boardSetDic)
            {
                if (pair.Value.Overlaps(tHashSet))
                {
                    var repeated = HashSetUtil.GetIntersection(pair.Value, tHashSet);
                    board.Description += $"{board.RowIndex} {pair.Key} 存在重复坐标 {HashSetUtil.HashSetToString(repeated)}\n";
                    board.HasError = true;
                }
            }

            if (tHashSet.Count != 0)
                boardSetDic.Add(board.RowIndex, tHashSet);

            if (sArray.Count > 0 && sArray.Any(item => !coordSet.Contains(item)))
            {
                var notInclude = sArray.Except(coordSet);
                board.HasError = true;
                board.Description += $"坐标文件{ColumnConst.RefDes}不包含{ColumnConst.CoordNo}{HashSetUtil.EnumerableToString(notInclude)}\n";
            }
        }
    }

    private static void PlayResultSound(string path)
    {
        using (var ms = File.OpenRead(path))
        using (var rdr = new Mp3FileReader(ms))
        using (var wavStream = WaveFormatConversionStream.CreatePcmStream(rdr))
        using (var baStream = new BlockAlignReductionStream(wavStream))
        using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
        {
            waveOut.Init(baStream);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100);
            }
        }
    }

    private static bool ParseDecimalString(string value, out int intValue)
    {
        intValue = 0;
        try
        {
            if (string.IsNullOrEmpty(value))
                return false;

            value = value.Trim();

            if (decimal.TryParse(value, out decimal decimalValue))
            {
                intValue = Convert.ToInt32(decimalValue);
                return true;
            }
        }
        catch
        {
            return false;
        }

        return true;
    }
}