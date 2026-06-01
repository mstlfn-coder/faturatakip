using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FaturaTakip.App.Infrastructure;

public static class PdfReportWriter
{
    public sealed record SummaryItem(string Label, string Value, string? Detail = null);

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
        bool includeSignature = true)
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
                            col.Item().Text(notes).FontSize(9).FontColor(Colors.Grey.Darken1);
                        }

                        col.Item().Element(x => ComposeTable(x, headers, rows));

                        if (secondTable is not null)
                        {
                            col.Item().PaddingTop(10).Text(secondTable.Value.Title).FontSize(11).SemiBold();
                            col.Item().Element(x => ComposeTable(x, secondTable.Value.Headers, secondTable.Value.Rows));
                        }

                        if (includeSignature)
                        {
                            col.Item().PaddingTop(12).Row(r =>
                            {
                                r.RelativeItem().Column(left =>
                                {
                                    left.Item().Text("Hazırlayan").SemiBold();
                                    left.Item().Text("Ad Soyad / İmza");
                                    left.Item().PaddingTop(18).Height(1).Background(Colors.Grey.Lighten1);
                                });
                                r.ConstantItem(40);
                                r.RelativeItem().Column(right =>
                                {
                                    right.Item().Text("Kontrol Eden").SemiBold();
                                    right.Item().Text("Ad Soyad / İmza");
                                    right.Item().PaddingTop(18).Height(1).Background(Colors.Grey.Lighten1);
                                });
                            });
                        }
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
        container.Column(col =>
        {
            col.Spacing(6);

            col.Item().Text(meta.AppTitle).FontSize(13).SemiBold();
            col.Item().Text(meta.ReportTitle).FontSize(16).Bold();
            if (!string.IsNullOrWhiteSpace(secondaryTitle))
            {
                col.Item().Text(secondaryTitle).FontSize(11).FontColor(Colors.Grey.Darken1);
            }

            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

            col.Item().Row(r =>
            {
                r.RelativeItem().Column(left =>
                {
                    var lines = (meta.InstitutionName ?? string.Empty)
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    left.Item().Text("Kurum       :").FontSize(10);
                    if (lines.Length == 0)
                    {
                        left.Item().Text(string.Empty).FontSize(10);
                    }
                    else
                    {
                        foreach (var line in lines)
                            left.Item().Text(line).FontSize(10);
                    }

                    left.Item().Text($"Dönem       : {meta.ReportPeriod}").FontSize(10);
                });
                r.RelativeItem().Column(right =>
                {
                    right.Item().Text($"Rapor Tarihi: {meta.ReportDate:dd.MM.yyyy}").FontSize(10);
                    right.Item().Text($"Oluşturan   : {meta.CreatedBy}").FontSize(10);
                });
            });

            if (!string.IsNullOrWhiteSpace(meta.FilterText))
            {
                col.Item().Text($"Filtre      : {meta.FilterText}").FontSize(10);
            }
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
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(cols =>
            {
                foreach (var _ in headers)
                    cols.RelativeColumn();
            });

            table.Header(header =>
            {
                for (var i = 0; i < headers.Count; i++)
                {
                    header.Cell().Element(CellStyleHeader).Text(headers[i]).SemiBold();
                }
            });

            foreach (var row in rows)
            {
                for (var c = 0; c < headers.Count; c++)
                {
                    var value = c < row.Count ? row[c] : string.Empty;
                    table.Cell().Element(CellStyleBody).Text(value ?? string.Empty);
                }
            }
        });
    }

    private static IContainer CellStyleHeader(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.Grey.Lighten4)
            .PaddingVertical(4)
            .PaddingHorizontal(5);
    }

    private static IContainer CellStyleBody(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Grey.Lighten3)
            .PaddingVertical(3)
            .PaddingHorizontal(5);
    }
}
