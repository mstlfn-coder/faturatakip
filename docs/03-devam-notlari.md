# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.98)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.98 - Yardim Durum Satiri Tekrar Rozeti`
- Bu adimda secili yardim durum satirlarina prefix yaninda `TEKRAR` rozeti eklendi.
- Rozet hover ve klavye odaginda satirla ayni vurguya katiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinin bosluklarini biraz sikilastirip daha kompakt bir satir gorunumu verebiliriz.
