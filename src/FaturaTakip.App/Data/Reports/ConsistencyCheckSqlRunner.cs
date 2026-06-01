using System.Globalization;
using FaturaTakip.App.Data;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Reports;

public static class ConsistencyCheckSqlRunner
{
    public static ConsistencyReport Run(string databasePath, int maxIssues = 200)
    {
        using var connection = SqliteConnectionFactory.Create(databasePath);
        connection.Open();

        var issues = new List<ConsistencyIssue>();

        // Invalid invoice status
        issues.AddRange(QueryIssues(
            connection,
            """
            SELECT id, status
            FROM invoices
            WHERE status NOT IN ('unpaid','paid','canceled')
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "ERROR",
                "INVOICE_STATUS_INVALID",
                "invoice",
                reader.GetInt64(0),
                $"Fatura durumu geÃ§ersiz: {reader.GetString(1)}")));

        // Paid status but remaining
        issues.AddRange(QueryIssues(
            connection,
            """
            WITH paid AS (
                SELECT invoice_id, SUM(CAST(amount AS REAL)) AS paid_amount
                FROM payments
                GROUP BY invoice_id
            )
            SELECT i.id, i.amount,
                   COALESCE(p.paid_amount, 0) AS paid_amount
            FROM invoices i
            LEFT JOIN paid p ON p.invoice_id = i.id
            WHERE i.status = 'paid' AND CAST(i.amount AS REAL) > 0
              AND paid_amount + 0.01 < CAST(i.amount AS REAL)
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "ERROR",
                "INVOICE_STATUS_PAID_BUT_REMAINING",
                "invoice",
                reader.GetInt64(0),
                $"Fatura 'Ã¶dendi' ama kalan var. Ã–denen={reader.GetDecimal(2).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}, Tutar={reader.GetDecimal(1).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}")));

        // Unpaid status but fully paid
        issues.AddRange(QueryIssues(
            connection,
            """
            WITH paid AS (
                SELECT invoice_id, SUM(CAST(amount AS REAL)) AS paid_amount
                FROM payments
                GROUP BY invoice_id
            )
            SELECT i.id, i.amount,
                   COALESCE(p.paid_amount, 0) AS paid_amount
            FROM invoices i
            LEFT JOIN paid p ON p.invoice_id = i.id
            WHERE i.status = 'unpaid' AND CAST(i.amount AS REAL) > 0
              AND paid_amount + 0.01 >= CAST(i.amount AS REAL)
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "WARN",
                "INVOICE_STATUS_UNPAID_BUT_FULLY_PAID",
                "invoice",
                reader.GetInt64(0),
                $"Fatura 'Ã¶denmedi' ama Ã¶deme tamamlanmÄ±ÅŸ gÃ¶rÃ¼nÃ¼yor. Ã–denen={reader.GetDecimal(2).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}, Tutar={reader.GetDecimal(1).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}")));

        // Paid exceeds amount
        issues.AddRange(QueryIssues(
            connection,
            """
            WITH paid AS (
                SELECT invoice_id, SUM(CAST(amount AS REAL)) AS paid_amount
                FROM payments
                GROUP BY invoice_id
            )
            SELECT i.id, i.amount,
                   COALESCE(p.paid_amount, 0) AS paid_amount
            FROM invoices i
            LEFT JOIN paid p ON p.invoice_id = i.id
            WHERE CAST(i.amount AS REAL) > 0
              AND paid_amount > CAST(i.amount AS REAL) + 0.01
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "ERROR",
                "INVOICE_PAID_EXCEEDS_AMOUNT",
                "invoice",
                reader.GetInt64(0),
                $"Ã–denen tutar fatura tutarÄ±nÄ± aÅŸÄ±yor. Ã–denen={reader.GetDecimal(2).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}, Tutar={reader.GetDecimal(1).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}")));

        // Orphan payments
        issues.AddRange(QueryIssues(
            connection,
            """
            SELECT p.id, p.invoice_id
            FROM payments p
            LEFT JOIN invoices i ON i.id = p.invoice_id
            WHERE i.id IS NULL
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "ERROR",
                "ORPHAN_PAYMENT",
                "payment",
                reader.GetInt64(0),
                $"Ã–deme kaydÄ± faturaya baÄŸlÄ± deÄŸil. invoice_id={reader.GetInt64(1)}")));

        // Non-positive payments
        issues.AddRange(QueryIssues(
            connection,
            """
            SELECT id, amount
            FROM payments
            WHERE CAST(amount AS REAL) <= 0
            LIMIT $limit;
            """,
            maxIssues - issues.Count,
            reader => new ConsistencyIssue(
                "ERROR",
                "PAYMENT_AMOUNT_NON_POSITIVE",
                "payment",
                reader.GetInt64(0),
                $"Ã–deme tutarÄ± geÃ§ersiz: {reader.GetDecimal(1).ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))}")));

        return new ConsistencyReport(issues);
    }

    private static IReadOnlyList<ConsistencyIssue> QueryIssues(
        SqliteConnection connection,
        string sql,
        int limit,
        Func<SqliteDataReader, ConsistencyIssue> map)
    {
        if (limit <= 0)
        {
            return Array.Empty<ConsistencyIssue>();
        }

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$limit", limit);

        using var reader = command.ExecuteReader();
        var list = new List<ConsistencyIssue>();
        while (reader.Read())
        {
            list.Add(map(reader));
        }

        return list;
    }
}
