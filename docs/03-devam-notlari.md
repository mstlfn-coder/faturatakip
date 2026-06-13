# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.64)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.64 - Replay Tercih Ozeti`
- Bu adimda secili replay tercihleri odeme yardim alanlarinda kisa ozet satiri olarak gorunur hale getirildi.
- Tercih paneli ve yardim alani ayni replay ayarini birlikte gostermeye basladi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirini secili aksiyona gore daha baglamsal ifade etmek.
