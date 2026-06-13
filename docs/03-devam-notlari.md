# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.80)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.80 - Replay Tooltip Ton Sadeleştirmesi`
- Bu adimda replay tooltip tonu, komsu kisa yardim metinleriyle daha kompakt hale getirildi.
- Replay yardim metni daha hizli taranabilir hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay mini isaret metinlerini istenirse artik ortak bir helper uzerine alip tekrarli ton kararlarini tek yerde toplamak.
