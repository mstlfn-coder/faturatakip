# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.101)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.101 - Yardim Durum Satiri Ince Ayrac`
- Bu adimda prefix ile ana yardim metni arasina hafif bir nokta ayraci eklendi.
- Satirdaki bilgi bloklari daha rahat ayrisiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirindaki `TEKRAR` ve `Enter/Space` alanlarini sadece hover ya da odakta biraz daha belirginlestirip normal durumda daha sakin tutabiliriz.
