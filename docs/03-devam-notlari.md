# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.56)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.56 - Son Aksiyon Satirinda Gecici Vurgu`
- Bu adimda odeme yardim ve odeme PDF yardim bolumlerindeki son aksiyon satirlari guncellendiginde kisa sureli vurgu almaya basladi.
- Vurgu suresi tamamlaninca satir normal gorunume otomatik donuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Son aksiyon satirlarini tiklanabilir mikro kisayollara donusturup ayni yardimi tekrar calistirabilmek.
