# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.66)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.66 - Replay Bos Durum Metni`
- Bu adimda replay ozet satiri son aksiyon yokken daha sakin bir bos durum metni gostermeye basladi.
- Tercih bilgisi korunurken ilk gorunum dili daha dogal hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirini secili aksiyonun durumuna gore renk tonuyla da hafif ayristirmak.
