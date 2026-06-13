# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.83)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.83 - Replay Indicator UI Smoke Checklist`
- Bu adimda replay indicator kullanan alanlar icin ayri bir UI smoke checklist dokumani eklendi.
- Replay yardim akislarini yeni chatte elle dogrulamak artik daha kolay.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay indicator helper icin istenirse daha sonra cok kisa bir health-check benzeri text-output kontrolu dusunmek.
