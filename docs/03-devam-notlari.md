# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.63)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.63 - Kisayol Replay Tercihleri`
- Bu adimda mikro kisayol replay geri bildiriminin suresi ve vurgu siddeti kullanici tercihleriyle ayarlanabilir hale geldi.
- Ayarlar preference dosyasina kaydoluyor ve yeni oturumlarda korunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tercihlerini odeme yardim alani icinde ozetleyen kisa bir aciklayici satir eklemek.
