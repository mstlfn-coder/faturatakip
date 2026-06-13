# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.88)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.88 - Yardim Durum Satiri Tooltip Birlesimi`
- Bu adimda secili yardim durum satiri, son aksiyon butonuyla ayni tooltip ipucunu kullanmaya basladi.
- Durum metni hover edildiginde yardim dili daha tamamli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Odeme yardimi veya PDF yardimi durum satiri icin istenirse kisa bir klavye/kisayol ipucu gorunumu dusunmek.
