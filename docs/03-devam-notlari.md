# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.100)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.100 - Yardim Rozet Ton Dengeleme`
- Bu adimda tekrar rozeti ve kisayol etiketi daha yumusak tonlara cekildi.
- Ana yardim metni gorsel olarak biraz daha one cikiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinda prefix ve metin arasina cok ince bir ayrac ya da hafif opak nokta ekleyip okunurlugu biraz daha keskinlestirebiliriz.
