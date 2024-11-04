using NPOI.SS.UserModel;

namespace BomTool.Excel
{
    public class ExcelSheet
    {
        public SortedDictionary<int, ExcelRow> Rows { get; private set; }

        public ExcelSheet()
        {
            Rows = new SortedDictionary<int, ExcelRow>();
        }

        public static ExcelSheet Create(ISheet srcSheet)
        {
            var rows = ExcelReader.Read(srcSheet);

            return CreateSheet(rows);
        }

        public void RemoveColumn(int column)
        {
            foreach (var row in Rows)
            {
                if (row.Value.Cells.Count > column)
                    row.Value.Cells.RemoveAt(column);
            }
        }

        private static ExcelSheet CreateSheet(IEnumerable<ExcelRow> rows)
        {
            var sheet = new ExcelSheet();
            foreach (var row in rows)
            {
                sheet.Rows.Add(row.Index, row);
            }

            return sheet;
        }

        private IEnumerable<ExcelColumn> CreateColumns()
        {
            if (!Rows.Any())
                return Enumerable.Empty<ExcelColumn>();

            var columnCount = Rows.Max(r => r.Value.Cells.Count);
            var columns = new ExcelColumn[columnCount];
            foreach (var row in Rows)
            {
                var columnIndex = 0;
                foreach (var cell in row.Value.Cells)
                {
                    if (columns[columnIndex] == null)
                        columns[columnIndex] = new ExcelColumn();

                    columns[columnIndex].Cells.Add(cell);
                    columnIndex++;
                }
            }

            return columns.AsEnumerable();
        }
    }
}