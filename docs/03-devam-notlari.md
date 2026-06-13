# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.78)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.78 - Replay Aktiflik Dili Uyumu`
- Bu adimda replay tooltip aktiflik cumlesi, `yeniden tetiklendi` diliyle daha uyumlu hale getirildi.
- Replay geri bildirimi farkli yuzeylerde daha ayni ses tonuyla okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tooltip bekleme dilini de ozet satirindaki yardim tonu ile daha uyumlu hale getirmek.
