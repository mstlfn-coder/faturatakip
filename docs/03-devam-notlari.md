# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.102)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.102 - Yardim Etiketlerinde Etkilesim Vurgusu`
- Bu adimda tekrar ve kisayol etiketleri normal durumda biraz sakinlestirildi.
- Hover ve klavye odaginda yardimci etiketler tekrar tam belirginlige cikiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinda ana metni secili aksiyonun turune gore cok hafif ikonimsi bir prefix tonu ile destekleyebiliriz.
