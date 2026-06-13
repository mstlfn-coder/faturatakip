# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.94)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.94 - Yardim Durum Satiri Hover Focus Ipuclari`
- Bu adimda secili yardim durum satiri hover ve klavye odaginda daha belirgin hale getirildi.
- Yardim metni ve prefix rozeti mevcut yardim butonlariyla ayni etkilesim dilini kullaniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirina kucuk bir klavye odak cercevesi ya da durum cubugu metnine `yeniden calistir` gibi daha kisa bir yardim ipucu eklenebilir.
