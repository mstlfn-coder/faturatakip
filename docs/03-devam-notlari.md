# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.57)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.57 - Son Aksiyon Mikro Kisayollari`
- Bu adimda odeme yardim ve odeme PDF yardim bolumlerindeki son aksiyon satirlari tiklanabilir mikro kisayollara donusturuldu.
- Son kullanilan yardim ayni satirdan tekrar tetiklenebiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Mikro kisayol satirlarina klavye odagi ve hover gorunurlugunu daha belirgin yapmak.
