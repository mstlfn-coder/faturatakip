# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.76)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.76 - Replay Tooltip Metin Sikilastirmasi`
- Bu adimda replay tooltip metni action varsa daha kisa, action yoksa daha yonlendirici hale getirildi.
- Hover aciklamasi artik daha hizli taranabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay tooltip ile replay ozet satiri arasinda dil uyumunu artirip ayni action icin benzer ifade kaliplari kullanmak.
