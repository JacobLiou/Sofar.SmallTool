using NPOI.SS.UserModel;

namespace BomTool.Excel
{
    public class ExcelWorkbook
    {
        public Dictionary<string, ExcelSheet> Sheets { get; private set; }

        public ExcelWorkbook()
        {
            Sheets = new Dictionary<string, ExcelSheet>();
        }

        public static ExcelWorkbook Create(string path)
        {
            var srcWb = WorkbookFactory.Create(path);
            var wb = new ExcelWorkbook();
            for (int i = 0; i < srcWb.NumberOfSheets; i++)
            {
                var srcSheet = srcWb.GetSheetAt(i);
                wb.Sheets.Add(srcSheet.SheetName, ExcelSheet.Create(srcSheet));
            }

            return wb;
        }

        public static IEnumerable<string> GetSheetNames(string path)
        {
            var wb = WorkbookFactory.Create(path);
            for (int i = 0; i < wb.NumberOfSheets; i++)
                yield return wb.GetSheetAt(i).SheetName;
        }

        public static ExcelSheet? GetSheetByName(ExcelWorkbook workbook, string sheetName)
        {
            if (workbook.Sheets.ContainsKey(sheetName))
                return workbook.Sheets[sheetName];
            return default;
        }
    }
}