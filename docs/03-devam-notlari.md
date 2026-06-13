# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.103)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.103 - Yardim Metninde Aksiyon Tonu`
- Bu adimda ana yardim metni secili aksiyon turune gore hafif ton farki almaya basladi.
- Metin hala sakin ama taramada yardim turlerini biraz daha hizli ayristiriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinda prefix rozetini de secili aksiyon turune gore cok hafif metin etiketi degisimiyle destekleyebiliriz.
