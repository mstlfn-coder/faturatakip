# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.105)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.105 - Yardim Tekrar Rozetinde Aksiyon Etiketi`
- Bu adimda tekrar rozeti secili aksiyona gore farkli kisa etiketler kullanmaya basladi.
- Satirin orta bolumu artik tekrar aksiyonunun turunu daha dogrudan anlatiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirindaki `Enter/Space` ipucunu da secili aksiyona gore ufak bir ikon ya da metin varyantiyla esleyebiliriz.
