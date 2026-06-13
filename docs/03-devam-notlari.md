# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.73)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.73 - Replay Isaret Tooltipi`
- Bu adimda replay mini isaretine tooltip aciklamasi eklendi.
- Sembol dili artik vurgu, sure ve action bilgisini hover ile de aciklayabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay mini isareti aktifken tooltip metnini gecici replay durumunu da anlatacak sekilde canlandirmak.
