using ClosedXML.Excel;
using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Infrastructure;

public static class ExcelExportWriter
{
    public static void WriteTable(
        string filePath,
        string sheetName,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName);

        for (var i = 0; i < headers.Count; i++)
        {
            sheet.Cell(1, i + 1).Value = headers[i];
        }

        var rowIndex = 2;
        foreach (var row in rows)
        {
            for (var col = 0; col < headers.Count; col++)
            {
                var cell = sheet.Cell(rowIndex, col + 1);
                var value = col < row.Count ? row[col] : null;
                SetCellValue(cell, value);
            }

            rowIndex++;
        }

        var lastDataRow = Math.Max(1, rowIndex - 1);
        var lastDataCol = Math.Max(1, headers.Count);

        var headerRange = sheet.Range(1, 1, 1, lastDataCol);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        sheet.Range(1, 1, lastDataRow, lastDataCol).SetAutoFilter();
        sheet.Columns(1, lastDataCol).AdjustToContents();

        workbook.SaveAs(filePath);
    }

    public static void WriteInvoices(string filePath, IEnumerable<Invoice> invoices, Func<Invoice, bool> isPdfMissing)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Faturalar");

        var headers = new[]
        {
            "Dönem",
            "Tür",
            "Abonelik",
            "Kurum",
            "Durum",
            "Fatura No",
            "Fatura Tarihi",
            "Son Ödeme",
            "Tutar",
            "Ödenen",
            "Kalan",
            "PDF",
            "PDF Yol",
            "PDF Hash",
        };

        for (var i = 0; i < headers.Length; i++)
        {
            sheet.Cell(1, i + 1).Value = headers[i];
        }

        var rowIndex = 2;
        foreach (var invoice in invoices)
        {
            var pdfState = !invoice.HasPdf
                ? "PDF Yok"
                : isPdfMissing(invoice) ? "PDF Kayıp" : "PDF Var";

            sheet.Cell(rowIndex, 1).Value = invoice.Period;
            sheet.Cell(rowIndex, 2).Value = invoice.InvoiceTypeName;
            sheet.Cell(rowIndex, 3).Value = invoice.SubscriptionName;
            sheet.Cell(rowIndex, 4).Value = invoice.InstitutionName;
            sheet.Cell(rowIndex, 5).Value = invoice.State;
            sheet.Cell(rowIndex, 6).Value = invoice.InvoiceNo;
            sheet.Cell(rowIndex, 7).Value = invoice.InvoiceDate;
            sheet.Cell(rowIndex, 8).Value = invoice.DueDate;
            sheet.Cell(rowIndex, 9).Value = invoice.Amount;
            sheet.Cell(rowIndex, 10).Value = invoice.PaidAmount;
            sheet.Cell(rowIndex, 11).Value = invoice.RemainingAmount;
            sheet.Cell(rowIndex, 12).Value = pdfState;
            sheet.Cell(rowIndex, 13).Value = invoice.PdfFilePath;
            sheet.Cell(rowIndex, 14).Value = invoice.PdfSha256Hash;

            rowIndex++;
        }

        var lastDataRow = Math.Max(1, rowIndex - 1);
        var lastDataCol = headers.Length;

        var headerRange = sheet.Range(1, 1, 1, lastDataCol);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        sheet.Range(1, 1, lastDataRow, lastDataCol).SetAutoFilter();

        sheet.Column(7).Style.DateFormat.Format = "dd.MM.yyyy";
        sheet.Column(8).Style.DateFormat.Format = "dd.MM.yyyy";
        sheet.Column(9).Style.NumberFormat.Format = "#,##0.00";
        sheet.Column(10).Style.NumberFormat.Format = "#,##0.00";
        sheet.Column(11).Style.NumberFormat.Format = "#,##0.00";

        sheet.Columns(1, lastDataCol).AdjustToContents();

        workbook.SaveAs(filePath);
    }

    private static void SetCellValue(IXLCell cell, object? value)
    {
        if (value is null)
        {
            cell.Clear();
            return;
        }

        switch (value)
        {
            case string s:
                cell.Value = s;
                return;
            case int i:
                cell.Value = i;
                return;
            case long l:
                cell.Value = l;
                return;
            case decimal d:
                cell.Value = d;
                return;
            case double dbl:
                cell.Value = dbl;
                return;
            case DateTime dt:
                cell.Value = dt;
                return;
            case DateTimeOffset dto:
                cell.Value = dto.DateTime;
                return;
            case bool b:
                cell.Value = b;
                return;
            default:
                cell.Value = value.ToString() ?? string.Empty;
                return;
        }
    }
}
