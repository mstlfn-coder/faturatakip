# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.96)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.96 - Yardim Tooltip Dili Sadelestirme`
- Bu adimda secili yardim durum satirlarinin tooltip dili kisaltildi.
- Secili durum satiri artik kisa `Yeniden calistir: ...` ifadesini kullaniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirina minik bir `Enter/Space` kisayol etiketi ya da prefix yaninda ufak bir tekrar ikonu ekleyebiliriz.
