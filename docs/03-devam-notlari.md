# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.97)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.97 - Yardim Durum Satiri Kisayol Etiketi`
- Bu adimda secili yardim durum satirlarina gorunur `Enter/Space` etiketi eklendi.
- Kisayol etiketi hover ve klavye odaginda satirla ayni vurguya katiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Prefix tarafina minik bir tekrar ikonu ya da `Tekrar` rozeti ekleyip bu satiri bir adim daha hizli taranir hale getirebiliriz.
