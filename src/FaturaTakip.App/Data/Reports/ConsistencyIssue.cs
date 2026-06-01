namespace FaturaTakip.App.Data.Reports;

public sealed record ConsistencyIssue(
    string Severity,
    string Code,
    string Entity,
    long? EntityId,
    string Message);

