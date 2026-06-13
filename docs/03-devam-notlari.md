# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.70)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.70 - Replay Vurgu Seviye Isareti`
- Bu adimda replay ozet prefix isaretine, secili vurgu seviyesini anlatan minik bir gorsel seviye isareti eklendi.
- Replay siddeti artik metin ve prefix gorunumuyle birlikte daha hizli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay seviye isaretini sadece vurgu seviyesinde degil sure farkinda da gorsel olarak zenginlestirmek.
