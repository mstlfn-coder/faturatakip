# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.81)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.81 - Replay Indicator Helper`
- Bu adimda replay indicator ve tooltip metin mantigi ortak bir helper dosyasina tasindi.
- Replay yardim kararlarini tek yerden yonetmek artik daha kolay.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay indicator helper icin dar kapsamli self-test ekleyip metin kararlarini regression seviyesinde daha dogrudan korumak.
