# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.61)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.61 - Mikro Kisayol Tekrar Geri Bildirimi`
- Bu adimda mikro kisayollar tekrar tetiklenince satir ici `yeniden tetiklendi` geri bildirimi eklenmeye basladi.
- Geri bildirim kisa sure sonra otomatik temizleniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Mikro kisayol geri bildirimini rozet tarafinda da kisa sureli canlandirmak.
