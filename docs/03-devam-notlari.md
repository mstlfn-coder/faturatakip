# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.68)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.68 - Replay Ozet Prefix Isareti`
- Bu adimda replay ozet satirina minik bir prefix isareti eklendi.
- Satir artik ton farkina ek olarak kisa bir rozet ile de baglamsal ipucu veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet prefix isaretini replay aktif oldugunda da kisa sureli canlandirmak.
