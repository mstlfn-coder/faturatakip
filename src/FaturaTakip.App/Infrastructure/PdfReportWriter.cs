using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FaturaTakip.App.Infrastructure;

public static class PdfReportWriter
{
    public sealed record SummaryItem(string Label, string Value, string? Detail = null);
    public sealed record TableFooterCell(string Text, int ColumnSpan = 1, bool Bold = true, bool AlignRight = false);

    public sealed record ReportMeta(
        string AppTitle,
        string InstitutionName,
        string ReportTitle,
        string ReportPeriod,
        DateTime ReportDate,
        string CreatedBy,
        string FilterText);

    public static void WriteSimpleTableReport(
        string filePath,
        ReportMeta meta,
        IReadOnlyList<SummaryItem> summary,
        IReadOnlyList<string> headers,
        IReadOnlyList<IReadOnlyList<string>> rows,
        string? notes = null,
        string? secondaryTitle = null,
        (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<string>> Rows, string Title)? secondTable = null,
        bool includeSignature = false,
        IReadOnlyList<TableFooterCell>? footerCells = null,
        IReadOnlyList<float>? columnWeights = null)
    {
        // Community license is enough for internal/business apps; required by QuestPDF runtime.
        QuestPDF.Settings.License = LicenseType.Community;

        var culture = CultureInfo.GetCultureInfo("tr-TR");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(h => ComposeHeader(h, meta, secondaryTitle, culture));

                page.Content().Element(c =>
                {
                    c.Column(col =>
                    {
                        col.Spacing(10);

                        if (summary.Count > 0)
                        {
                            col.Item().Element(x => ComposeSummary(x, summary));
                        }

                        if (!string.IsNullOrWhiteSpace(notes))
                        {
                            col.Item().PaddingTop(4).Row(r =>
                            {
                                r.ConstantItem(90).Text("Açıklama").FontSize(11);
                                r.ConstantItem(20).AlignCenter().Text(":").FontSize(11);
                                r.RelativeItem().Text(notes).FontSize(11);
                            });
                        }

                        col.Item().Element(x => ComposeTable(x, headers, rows, footerCells, columnWeights));

                        if (secondTable is not null)
                        {
                            col.Item().PaddingTop(10).Text(secondTable.Value.Title).FontSize(11).SemiBold();
                            col.Item().Element(x => ComposeTable(x, secondTable.Value.Headers, secondTable.Value.Rows));
                        }

                        // Signature blocks are intentionally omitted; these PDFs are informational reports.
                    });
                });

                page.Footer().AlignCenter().Text(t =>
                {
                    t.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Darken1));
                    t.Span("Bu rapor Kurum Fatura Takip Programı tarafından oluşturulmuştur.  ");
                    t.Span("Sayfa ");
                    t.CurrentPageNumber();
                    t.Span(" / ");
                    t.TotalPages();
                });
            });
        }).GeneratePdf(filePath);
    }

    private static void ComposeHeader(IContainer container, ReportMeta meta, string? secondaryTitle, CultureInfo culture)
    {
        // Match the provided example PDFs' header layout:
        // - Centered institution block
        // - Right-aligned "Tarih : dd.MM.yyyy"
        // - Centered report title (optionally prefixed by secondary title)
        container.Column(col =>
        {
            col.Spacing(10);

            col.Item().Row(r =>
            {
                r.RelativeItem().Column(center =>
                {
                    center.Item().AlignCenter().Text(meta.AppTitle).FontSize(12);
                    foreach (var line in (meta.InstitutionName ?? string.Empty)
                                 .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                    {
                        center.Item().AlignCenter().Text(line).FontSize(12);
                    }
                });

                // Keep a fixed-width right column so the date aligns consistently.
                r.ConstantItem(200).AlignRight().PaddingTop(34).Text(t =>
                {
                    t.Span("Tarih").FontSize(11);
                    t.Span("     :     ").FontSize(11);
                    t.Span(meta.ReportDate.ToString("dd.MM.yyyy", culture)).FontSize(11);
                });
            });

            var title = string.IsNullOrWhiteSpace(secondaryTitle)
                ? meta.ReportTitle
                : $"{secondaryTitle} {meta.ReportTitle}";
            col.Item().AlignCenter().Text(title).FontSize(13).Bold();
        });
    }

    private static void ComposeSummary(IContainer container, IReadOnlyList<SummaryItem> items)
    {
        // Render up to 6 cards as 2 rows x 3 columns to better match the report plan's richer summary needs.
        var list = items.Take(6).ToList();
        container.Column(col =>
        {
            col.Spacing(8);
            for (var rowIndex = 0; rowIndex < list.Count; rowIndex += 3)
            {
                col.Item().Row(row =>
                {
                    row.Spacing(10);
                    foreach (var item in list.Skip(rowIndex).Take(3))
                    {
                        row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(c =>
                        {
                            c.Item().Text(item.Label).FontSize(9).FontColor(Colors.Grey.Darken1);
                            c.Item().Text(item.Value).FontSize(14).SemiBold();
                            if (!string.IsNullOrWhiteSpace(item.Detail))
                            {
                                c.Item().Text(item.Detail!).FontSize(9).FontColor(Colors.Grey.Darken1);
                            }
                        });
                    }
                });
            }
        });
    }

    private static void ComposeTable(IContainer container, IReadOnlyList<string> headers, IReadOnlyList<IReadOnlyList<string>> rows)
        => ComposeTable(container, headers, rows, footerCells: null, columnWeights: null);

    private static void ComposeTable(
        IContainer container,
        IReadOnlyList<string> headers,
        IReadOnlyList<IReadOnlyList<string>> rows,
        IReadOnlyList<TableFooterCell>? footerCells,
        IReadOnlyList<float>? columnWeights)
    {
        // Never render "filler" rows. If upstream accidentally produces an all-empty row,
        // skip it so the PDF doesn't look like a pre-printed form.
        var filteredRows = rows
            .Where(r => r.Any(cell => !string.IsNullOrWhiteSpace(cell)))
            .ToList();

        container.Table(table =>
        {
            table.ColumnsDefinition(cols =>
            {
                if (columnWeights is not null && columnWeights.Count == headers.Count)
                {
                    foreach (var w in columnWeights)
                        cols.RelativeColumn(w);
                }
                else
                {
                    foreach (var _ in headers)
                        cols.RelativeColumn();
                }
            });

            table.Header(header =>
            {
                for (var i = 0; i < headers.Count; i++)
                {
                    header.Cell().Element(CellStyleHeader).AlignCenter().Text(headers[i]).SemiBold();
                }
            });

            foreach (var row in filteredRows)
            {
                for (var c = 0; c < headers.Count; c++)
                {
                    var value = c < row.Count ? row[c] : string.Empty;
                    table.Cell().Element(CellStyleBody).Text(value ?? string.Empty);
                }
            }

            if (footerCells is not null && footerCells.Count > 0)
            {
                table.Footer(footer =>
                {
                    foreach (var cell in footerCells)
                    {
                        var span = Math.Max(1, cell.ColumnSpan);
                        var c = footer.Cell().ColumnSpan((uint)span).Element(x => CellStyleFooter(x, cell.AlignRight));
                        var text = c.Text(cell.Text ?? string.Empty);
                        if (cell.Bold)
                            text.SemiBold();
                        if (cell.AlignRight)
                            text.AlignRight();
                    }
                });
            }
        });
    }

    private static IContainer CellStyleHeader(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(4)
            .PaddingHorizontal(4);
    }

    private static IContainer CellStyleBody(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(3)
            .PaddingHorizontal(4);
    }

    private static IContainer CellStyleFooter(IContainer container, bool alignRight)
    {
        var styled = container
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(4)
            .PaddingHorizontal(4);

        return alignRight ? styled.AlignRight() : styled;
    }
}
