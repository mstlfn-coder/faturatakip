# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.85)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.85 - Secili Yardim Rozeti Ipuclari`
- Bu adimda secili odeme/PDF yardim rozetlerine minik `AKTIF` etiketi eklendi.
- Hangi aksiyonun secili oldugunu hizli taramak artik daha kolay.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Odeme yardimi veya PDF yardimi satirlarinda secili aksiyon degistiginde kisa sureli bir mikro gecis/vurgu dusunmek.
