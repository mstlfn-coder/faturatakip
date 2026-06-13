# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.71)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.71 - Replay Sure Izi`
- Bu adimda replay ozet prefix isareti, secili sure bilgisini de minik cizgi yogunluguyla gosterecek sekilde genisletildi.
- Replay siddeti ve suresi artik tek prefix icinde birlikte daha hizli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirindaki minik isaret icin action varsa daha canli, yoksa daha sakin bir sekil varyasyonu dusunmek.
