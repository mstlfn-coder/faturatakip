# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.67)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.67 - Replay Ozet Ton Ayrimi`
- Bu adimda replay ozet satiri secili aksiyon varken hafif renk tonuyla ayristirilmaya baslandi.
- Satir artik aktif baglami sadece metinle degil ton farkiyla da tasiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay ozet satirina istenirse minik ikon/prefix gibi ikinci bir baglamsal isaret eklemek.
