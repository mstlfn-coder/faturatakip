# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.82)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.82 - Replay Indicator Self-Test`
- Bu adimda replay indicator helper icin dar kapsamli self-testler eklendi.
- Replay yardim metni kararlarini regression seviyesinde korumak artik daha kolay.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay indicator helper kullanan alanlar icin gerekirse cok kisa bir UI smoke checklist notu eklemek.
