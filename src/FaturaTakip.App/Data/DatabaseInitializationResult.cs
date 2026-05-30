namespace FaturaTakip.App.Data;

public sealed record DatabaseInitializationResult(IReadOnlyList<string> AppliedMigrations);
