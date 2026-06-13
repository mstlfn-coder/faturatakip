# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.69)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.69 - Replay Ozet Prefix Canlanmasi`
- Bu adimda replay ozet satirindaki prefix isareti replay aktif oldugunda kisa sureli canlanma almaya basladi.
- Replay geri bildirimi yardim alaninin farkli katmanlarinda daha tutarli gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirina secili vurgu seviyesini minik bir gorsel isaretle de ayirmak.
