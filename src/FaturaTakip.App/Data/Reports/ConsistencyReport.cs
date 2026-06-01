namespace FaturaTakip.App.Data.Reports;

public sealed record ConsistencyReport(
    IReadOnlyList<ConsistencyIssue> Issues)
{
    public static readonly ConsistencyReport Empty = new(Array.Empty<ConsistencyIssue>());

    public int ErrorCount => Issues.Count(item => item.Severity == "ERROR");

    public int WarningCount => Issues.Count(item => item.Severity == "WARN");

    public int TotalCount => Issues.Count;
}

