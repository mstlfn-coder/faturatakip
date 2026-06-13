# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.75)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.75 - Replay Tooltip Action Adi`
- Bu adimda replay mini isaret tooltipine kaynak action adi eklendi.
- Hover aciklamasi artik replay ayari, durum ve action kaynagini birlikte anlatiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tooltip metnini action varsa daha kisa, action yoksa daha yonlendirici hale getirip okunurlugu sikilastirmak.
