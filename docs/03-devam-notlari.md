# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.62)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.62 - Prefix Rozette Tekrar Canlanmasi`
- Bu adimda mikro kisayol tekrar geri bildirimi prefix rozet tarafinda da kisa sureli canlanma almaya basladi.
- Tekrar aksiyonu artik metin ve rozet uzerinde birlikte belirginlesiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Mikro kisayol satirinda tekrar geri bildiriminin suresini veya siddetini kullanici tercihiyle ayarlanabilir yapmak.
