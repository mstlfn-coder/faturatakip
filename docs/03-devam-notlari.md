# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.90)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.90 - Yardim Durum Satiri Kisa Yol Ipuclari`
- Bu adimda secili yardim durum satirina `Enter/Space` klavye ipucu eklendi.
- Yardim akisinin klavye yolu artik satir ustunde de gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Odeme yardimi veya PDF yardimi durum satirinin aktif oldugu anda kisa sureli bir satir ici vurgu dusunmek.
