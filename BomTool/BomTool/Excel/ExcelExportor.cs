using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace BomTool.Excel
{
    public static class ExcelExporter
    {
        public static void ExportToExcel<T>(List<T> data, string filePath)
            where T : class, new()
        {
            using var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");

            // 获取属性信息
            var properties = typeof(T).GetProperties();

            // 创建表头
            var headerRow = sheet.CreateRow(0);
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(properties[i].Name);

                // 设置表头样式
                var headerStyle = workbook.CreateCellStyle();
                var font = workbook.CreateFont();
                font.IsBold = true;
                headerStyle.SetFont(font);
                cell.CellStyle = headerStyle;
            }

            // 填充数据
            for (int i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                for (int j = 0; j < properties.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = properties[j].GetValue(data[i]);
                    SetCellValue(cell, value);
                }
            }

            // 自动调整列宽
            for (int i = 0; i < properties.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            // 保存文件
            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            workbook.Write(fs);
        }

        private static void SetCellValue(ICell cell, object? value)
        {
            if (value == null)
            {
                cell.SetCellValue("");
                return;
            }

            switch (value)
            {
                case string s:
                    cell.SetCellValue(s);
                    break;
                case DateTime time:
                    cell.SetCellValue(time);
                    break;
                case bool b:
                    cell.SetCellValue(b);
                    break;
                case double d:
                    cell.SetCellValue(d);
                    break;
                case decimal dec:
                    cell.SetCellValue((double)dec);
                    break;
                case int i:
                    cell.SetCellValue(i);
                    break;
                case float f:
                    cell.SetCellValue(f);
                    break;
                default:
                    cell.SetCellValue(value.ToString());
                    break;
            }
        }
    }

}
