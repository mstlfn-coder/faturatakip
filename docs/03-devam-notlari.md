# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.65)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.65 - Baglamsal Replay Ozet Satiri`
- Bu adimda replay tercih ozeti secili son aksiyona gore daha baglamsal ifade uretmeye basladi.
- Replay satiri artik ilgili hizli aksiyonun adini daha net tasiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirinda son aksiyon yokken daha sakin bir bos durum metni gostermek.
