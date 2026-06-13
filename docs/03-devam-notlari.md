# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.77)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.77 - Replay Dili Uyumu`
- Bu adimda replay tooltip dili, replay ozet satiriyla uyumlu hale getirildi.
- Replay ayari ve vurgu dili farkli yuzeylerde daha tutarli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tooltip aktif durum cümlesini ozet satirindaki yeniden tetiklenme diliyle daha yakin hale getirmek.
