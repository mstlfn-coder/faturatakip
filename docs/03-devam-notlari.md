# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.117)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.117 - Odeme Kart Hover Vurgusu`
- Bu adimda Odemeler panelindeki ust ozet kartlari ve alt akis kartlari hover aninda hafif arka plan ve kenarlik vurgusu almaya basladi.
- Boylece panel fare ile taranirken kullanicinin gozu aktif oldugu bolumu daha rahat izliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.116)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.116 - Ust Yonlendirme Netligi`
- Bu adimda Odemeler panelinin ust sagindaki genel gecis butonlari daha hedefli isimlerle guncellendi ve altlarina kisa bir yardimci durum metni eklendi.
- Boylece listeye gitme ile rapor merkezine gitme ayrimi panel basliginin yaninda da daha kolay anlasiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.115)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.115 - Odeme Ozet Baglami`
- Bu adimda Odemeler panelinin ustteki uc ozet kartina baglam rozeti ve kisa hedef aciklamasi eklendi.
- Boylece kartlarin hangi rapora ya da hangi calisma akisine baglandigi butona basmadan once okunabilir hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.114)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.114 - Odeme Akisi Rozetleri`
- Bu adimda Odemeler panelindeki uc ana yonlendirme, rozetli ve aciklamali kartlara ayrildi.
- Kullanici artik Faturalar icindeki odeme alani, eksik evrak kontrolu ve odenmemisler raporu arasindaki farki ilk bakista okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.113)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.113 - Odemeye Hedefli Gecis`
- Bu adimda Odemeler panelindeki odeme odakli butonlar, Faturalar ekranini genel acmak yerine dogrudan odeme calisma modunu baslatacak hale getirildi.
- Gecis sirasinda odenmemis filtresi uygulanip ilk uygun kayit seciliyor; secili kayit varsa odeme tarihi alanina odak veriliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.112)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.112 - Odemeler Gecis Paneli`
- Bu adimda sol menudeki Odemeler artik tiklanabilir hale getirildi ve ayri modul olmadigi acik bir gecis paneli ile gorunur kilindi.
- Panel uzerinden Faturalar ve Raporlar akisina tek tikla gecis veriliyor; mevcut odeme yonetiminin Faturalar icinde oldugu netlestirildi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.111)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.111 - Replay Ayirac Uyumu`
- Bu adimda secili yardim durum satirindaki ayirac nokta replay aktifken cok hafif ton uyumu almaya basladi.
- Boylece satirin icindeki tum mikro parcalar ayni geri bildirim ailesine baglanmis oldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.110)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.110 - Replay Kisa Yol Ipucu Uyumu`
- Bu adimda secili yardim durum satirinin sagdaki kisayol kapsulu replay aktifken hafif ton ve kenarlik uyumu almaya basladi.
- Boylece satir icindeki flash, tekrar rozeti ve kisayol ipucu daha tek parca bir geri bildirim dili gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

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

- Odemeler panelinde hover dili oturdugu icin, sonraki mantikli kucuk adim bu kartlarda secilen yone gore ustteki genel yonlendirme metnini dinamik hale getirmek ya da son tiklanan akis icin hafif bir durum ozeti gostermek olabilir.
