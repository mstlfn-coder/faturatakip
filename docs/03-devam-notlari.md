# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.93)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.93 - Tiklanabilir Yardim Durum Satiri`
- Bu adimda secili yardim durum satiri tiklanabilir hale getirildi.
- Yardim metni artik dogrudan tekrar eylemine de bagli.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Tiklanabilir yardim durum satiri icin istersek bundan sonra hover/focus alt cizgi gibi minik bir etkilesim ipucu dusunmek.
