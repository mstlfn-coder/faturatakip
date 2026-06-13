# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.104)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.104 - Yardim Prefixinde Aksiyon Etiketi`
- Bu adimda prefix rozeti secili aksiyona gore degisen kisa etiketler kullanmaya basladi.
- Satirin en solundaki alan artik aktif yardimi daha dogrudan anlatiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinda `TEKRAR` rozetini aksiyona gore minik kelime farklariyla zenginlestirebiliriz.
