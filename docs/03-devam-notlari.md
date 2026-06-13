# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.72)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.72 - Replay Sekil Ayrimi`
- Bu adimda replay mini isaretine action baglamina gore sekil ayrimi eklendi.
- Action varsa daha net, action yoksa daha sakin sembol dili kullanilmaya baslandi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay mini isaretini tooltip ile kisa bir aciklamaya baglayip sembol dilini ilk bakista daha kolay kesfetmek.
