# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.106)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.106 - Yardim Kisayol Ipucunda Aksiyon Varyanti`
- Bu adimda sagdaki kisayol ipucu secili aksiyona gore metin varyanti almaya basladi.
- Kisa yol artik hem tusu hem de tetikleyecegi seyi daha acik anlatıyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satirinda hover ve odak aninda prefix/orta/sag bolumlerin birlikte ama sirayla parlayan daha rafine bir mikro geri bildirim dusunebiliriz.
