# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.59)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.59 - Mikro Kisayol Prefix Rozetleri`
- Bu adimda odeme yardim ve odeme PDF yardim mikro kisayollarina kisa prefix rozetleri eklendi.
- Son kullanilan yardim artik hem rozet hem metin ile daha hizli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Mikro kisayol rozetlerini secili aksiyon rengine gore biraz daha belirginlestirmek.
