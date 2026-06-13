# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.74)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.74 - Replay Tooltip Durum Mesaji`
- Bu adimda replay mini isaret tooltipine aktif / beklemede durum mesaji eklendi.
- Hover aciklamasi artik replay ayarini ve o anki canlilik durumunu birlikte anlatiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tooltip metninde action adini da gecirip hangi kisayolun baglam verdigini daha netlestirmek.
