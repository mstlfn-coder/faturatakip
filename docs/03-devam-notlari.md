# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.109)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.109 - Replay Rozeti Gecis Uyumu`
- Bu adimda secili yardim durum satirinin tekrar rozeti replay aktifken ayni renk ve kenarlik diliyle canlanmaya basladi.
- Boylece satirin kisa geri donus isigi ile replay geri bildirimi birbirine daha yakin bir gecis hissi veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.108)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.108 - Yardim Durum Satiri Geri Donus Isigi`
- Bu adimda secili yardim aksiyonu tetiklenince durum satirinin tamami cok kisa sureli bir flash ile geri bildirim vermeye basladi.
- Odeme yardimi tarafinda yesil, PDF yardimi tarafinda mavi tonlu satir isigi kullanildi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.107)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.107 - Yardim Mikro Vurgu Kademesi`
- Bu adimda prefix, tekrar rozeti ve kisayol ipucu icin daha rafine bir belirginlik kademesi kuruldu.
- Hover ve odakta uc bolum ayni anda ama farkli siddetlerde canlaniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Replay aktifken secili durum satirinin kisayol ipucuna da daha hafif bir uyum tonu verip tum satiri tek dil gibi hissettirebiliriz.
