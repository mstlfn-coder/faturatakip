# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.55)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.55 - Odeme Yardiminda Son Aksiyon Satiri`
- Bu adimda odeme yardim ve odeme PDF yardim bolumlerine son kullanilan hizli aksiyonu gosteren kisa alt satir eklendi.
- Rozet secim vurgusu korunurken son aksiyon artik metinsel olarak da okunuyor.
- Yardim rozetleri yoksa alt satir otomatik gizleniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Odeme yardim ve odeme PDF yardim alt satirlarini tiklandiginda ya da tekrarlandiginda kisa sureli vurgu ile canlandirmak.
