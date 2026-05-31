using System.Globalization;
using ClosedXML.Excel;
using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Infrastructure;

public static class ExcelExportWriter
{
    public sealed record ReportMeta(
        string AppTitle,
        string InstitutionName,
        string ReportTitle,
        string ReportPeriod,
        DateTime ReportDate,
        string CreatedBy,
        string FilterText);

    public sealed record SummaryItem(string Label, string Value, string? Detail = null);

    public static void WriteTable(
        string filePath,
        string sheetName,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName);

        var headerRow = 1;
        for (var i = 0; i < headers.Count; i++)
            sheet.Cell(headerRow, i + 1).Value = headers[i];

        var rowIndex = headerRow + 1;
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

        var headerRange = sheet.Range(headerRow, 1, headerRow, lastDataCol);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        sheet.Range(1, 1, lastDataRow, lastDataCol).SetAutoFilter();
        sheet.Columns(1, lastDataCol).AdjustToContents();

        workbook.SaveAs(filePath);
    }

    public static void WriteReportWithHeader(
        string filePath,
        string sheetName,
        ReportMeta meta,
        IReadOnlyList<SummaryItem> summary,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows,
        string? totalsLabel = "GENEL TOPLAM")
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(sheetName) ? "Rapor" : sheetName);

        // Row 1-5: title + meta
        sheet.Cell(1, 1).Value = meta.AppTitle;
        sheet.Cell(2, 1).Value = meta.ReportTitle;

        sheet.Cell(3, 1).Value = "Kurum";
        sheet.Cell(3, 2).Value = ":";
        sheet.Cell(3, 3).Value = meta.InstitutionName;

        sheet.Cell(4, 1).Value = "Dönem";
        sheet.Cell(4, 2).Value = ":";
        sheet.Cell(4, 3).Value = meta.ReportPeriod;

        sheet.Cell(5, 1).Value = "Rapor Tarihi";
        sheet.Cell(5, 2).Value = ":";
        sheet.Cell(5, 3).Value = meta.ReportDate.ToString("dd.MM.yyyy");

        sheet.Cell(3, 5).Value = "Oluşturan";
        sheet.Cell(3, 6).Value = ":";
        sheet.Cell(3, 7).Value = meta.CreatedBy;

        sheet.Cell(4, 5).Value = "Filtre";
        sheet.Cell(4, 6).Value = ":";
        sheet.Cell(4, 7).Value = meta.FilterText;

        sheet.Range(1, 1, 1, Math.Max(7, headers.Count)).Merge();
        sheet.Range(2, 1, 2, Math.Max(7, headers.Count)).Merge();
        sheet.Cell(1, 1).Style.Font.Bold = true;
        sheet.Cell(1, 1).Style.Font.FontSize = 14;
        sheet.Cell(2, 1).Style.Font.Bold = true;
        sheet.Cell(2, 1).Style.Font.FontSize = 16;

        // Row 7: table headers
        var headerRow = 7;
        for (var i = 0; i < headers.Count; i++)
            sheet.Cell(headerRow, i + 1).Value = headers[i];

        var headerRange = sheet.Range(headerRow, 1, headerRow, headers.Count);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        // Row 8..: data
        var rowIndex = headerRow + 1;
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

        // Totals row (optional)
        if (!string.IsNullOrWhiteSpace(totalsLabel))
        {
            sheet.Cell(rowIndex + 1, 1).Value = totalsLabel;
            sheet.Cell(rowIndex + 1, 1).Style.Font.Bold = true;
        }

        // Summary block (top-right)
        if (summary.Count > 0)
        {
            var sr = 3;
            var sc = Math.Max(9, headers.Count + 2);
            sheet.Cell(sr, sc).Value = "Özet";
            sheet.Cell(sr, sc).Style.Font.Bold = true;

            var idx = 0;
            foreach (var item in summary.Take(10))
            {
                sheet.Cell(sr + 1 + idx, sc).Value = item.Label;
                sheet.Cell(sr + 1 + idx, sc + 1).Value = item.Value;
                idx++;
            }
        }

        sheet.Range(headerRow, 1, Math.Max(headerRow, rowIndex - 1), headers.Count).SetAutoFilter();
        sheet.Columns(1, Math.Max(headers.Count, 12)).AdjustToContents();

        workbook.SaveAs(filePath);
    }

    public static void WriteReportWithTwoSheets(
        string filePath,
        string mainSheetName,
        ReportMeta meta,
        IReadOnlyList<SummaryItem> summary,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows,
        string secondSheetName,
        IReadOnlyList<string> secondHeaders,
        IEnumerable<IReadOnlyList<object?>> secondRows)
    {
        using var workbook = new XLWorkbook();
        var main = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(mainSheetName) ? "Rapor" : mainSheetName);
        var second = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(secondSheetName) ? "Ek" : secondSheetName);

        // Build main via the same logic (inline, to avoid refactoring the sheet param through the public API).
        WriteReportWithHeaderIntoSheet(main, meta, summary, headers, rows);
        WriteSimpleTableIntoSheet(second, secondHeaders, secondRows);

        workbook.SaveAs(filePath);
    }

    private static void WriteReportWithHeaderIntoSheet(
        IXLWorksheet sheet,
        ReportMeta meta,
        IReadOnlyList<SummaryItem> summary,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows,
        string? totalsLabel = "GENEL TOPLAM")
    {
        sheet.Cell(1, 1).Value = meta.AppTitle;
        sheet.Cell(2, 1).Value = meta.ReportTitle;

        sheet.Cell(3, 1).Value = "Kurum";
        sheet.Cell(3, 2).Value = ":";
        sheet.Cell(3, 3).Value = meta.InstitutionName;

        sheet.Cell(4, 1).Value = "Dönem";
        sheet.Cell(4, 2).Value = ":";
        sheet.Cell(4, 3).Value = meta.ReportPeriod;

        sheet.Cell(5, 1).Value = "Rapor Tarihi";
        sheet.Cell(5, 2).Value = ":";
        sheet.Cell(5, 3).Value = meta.ReportDate.ToString("dd.MM.yyyy");

        sheet.Cell(3, 5).Value = "Oluşturan";
        sheet.Cell(3, 6).Value = ":";
        sheet.Cell(3, 7).Value = meta.CreatedBy;

        sheet.Cell(4, 5).Value = "Filtre";
        sheet.Cell(4, 6).Value = ":";
        sheet.Cell(4, 7).Value = meta.FilterText;

        sheet.Range(1, 1, 1, Math.Max(7, headers.Count)).Merge();
        sheet.Range(2, 1, 2, Math.Max(7, headers.Count)).Merge();
        sheet.Cell(1, 1).Style.Font.Bold = true;
        sheet.Cell(1, 1).Style.Font.FontSize = 14;
        sheet.Cell(2, 1).Style.Font.Bold = true;
        sheet.Cell(2, 1).Style.Font.FontSize = 16;

        var headerRow = 7;
        for (var i = 0; i < headers.Count; i++)
            sheet.Cell(headerRow, i + 1).Value = headers[i];

        var headerRange = sheet.Range(headerRow, 1, headerRow, headers.Count);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        var rowIndex = headerRow + 1;
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

        if (!string.IsNullOrWhiteSpace(totalsLabel))
        {
            sheet.Cell(rowIndex + 1, 1).Value = totalsLabel;
            sheet.Cell(rowIndex + 1, 1).Style.Font.Bold = true;
        }

        if (summary.Count > 0)
        {
            var sr = 3;
            var sc = Math.Max(9, headers.Count + 2);
            sheet.Cell(sr, sc).Value = "Özet";
            sheet.Cell(sr, sc).Style.Font.Bold = true;

            var idx = 0;
            foreach (var item in summary.Take(10))
            {
                sheet.Cell(sr + 1 + idx, sc).Value = item.Label;
                sheet.Cell(sr + 1 + idx, sc + 1).Value = item.Value;
                idx++;
            }
        }

        sheet.Range(headerRow, 1, Math.Max(headerRow, rowIndex - 1), headers.Count).SetAutoFilter();
        sheet.Columns(1, Math.Max(headers.Count, 12)).AdjustToContents();
    }

    private static void WriteSimpleTableIntoSheet(
        IXLWorksheet sheet,
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<object?>> rows)
    {
        var headerRow = 1;
        for (var i = 0; i < headers.Count; i++)
            sheet.Cell(headerRow, i + 1).Value = headers[i];

        var rowIndex = headerRow + 1;
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

        var headerRange = sheet.Range(headerRow, 1, headerRow, headers.Count);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E7EEF6");

        sheet.Range(headerRow, 1, Math.Max(headerRow, rowIndex - 1), headers.Count).SetAutoFilter();
        sheet.Columns(1, Math.Max(headers.Count, 12)).AdjustToContents();
    }

    public static void WriteInvoices(string filePath, IEnumerable<Invoice> invoices, Func<Invoice, bool> isPdfMissing)
    {
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
        var list = invoices.ToList();
        var rows = list.Select(invoice =>
        {
            var pdfState = !invoice.HasPdf
                ? "PDF Yok"
                : isPdfMissing(invoice) ? "PDF Kayıp" : "PDF Var";

            return (IReadOnlyList<object?>)new object?[]
            {
                invoice.Period,
                invoice.InvoiceTypeName,
                invoice.SubscriptionName,
                invoice.InstitutionName,
                invoice.State,
                invoice.InvoiceNo,
                invoice.InvoiceDate,
                invoice.DueDate,
                invoice.Amount,
                invoice.PaidAmount,
                invoice.RemainingAmount,
                pdfState,
                invoice.PdfFilePath,
                invoice.PdfSha256Hash,
            };
        }).ToList();

        var summary = new List<SummaryItem>
        {
            new("Toplam Fatura", list.Count.ToString(CultureInfo.InvariantCulture)),
            new("Toplam Tutar", list.Sum(i => i.Amount).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))),
            new("Ödenen", list.Sum(i => i.PaidAmount).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))),
            new("Kalan", list.Sum(i => i.RemainingAmount).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))),
        };

        WriteReportWithHeader(
            filePath,
            sheetName: "Faturalar",
            meta: new ReportMeta(
                AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                InstitutionName: string.Empty,
                ReportTitle: "FATURA LİSTESİ",
                ReportPeriod: string.Empty,
                ReportDate: DateTime.Today,
                CreatedBy: Environment.UserName,
                FilterText: string.Empty),
            summary: summary,
            headers: headers,
            rows: rows);
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
