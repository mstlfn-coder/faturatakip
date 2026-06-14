# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.126)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.126 - Akis Baslik Baglami`
- Bu adimda Odeme Is Akisi bolum basliginin altina, secili kolonu ve ilgili yonu ozetleyen dinamik bir baglam satiri eklendi.
- Boylece aktif yolun hangi akis kolonunda toplandigi panelin orta bolumunde de acikca okunabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.125)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.125 - Ozet Yonu Ipuclari`
- Bu adimda ust ozet kartlarin alt akis baglanti metinlerine asagi yon ipuclari eklendi.
- Boylece yukaridaki sayisal ozetlerden asagidaki akis kutularina gecis daha taranabilir hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.124)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.124 - Akis Ozet Geri Baglantisi`
- Bu adimda alt akis kartlarina, iliskili ust ozetleri adlandiran kisa geri baglanti metinleri eklendi.
- Boylece panel icindeki iliski dili artik sadece ust ozetlerden alta degil, alttan uste de okunabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.123)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.123 - Ozet Akis Baglanti Metni`
- Bu adimda Odemeler panelindeki ust ozet kartlara, ilgili alt akis kartiyla nasil okunacagini soyleyen kisa baglanti metinleri eklendi.
- Boylece sayisal ozetler ile asagidaki is akisi kutulari arasindaki iliski daha az yorum gerektiriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.122)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.122 - Aktif Yol Buton Vurgusu`
- Bu adimda Odemeler panelinde secili akisla iliskili eylem dugmesi de ayni renk ailesinde hafif vurgu almaya basladi.
- Boylece aktif yol artik ust rozet, secili kart, kart ici etiket ve ilgili dugme uzerinden birlikte okunabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.121)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.121 - Aktif Yol Alt Notu`
- Bu adimda Odemeler panelindeki secili kartlar, son kullanilan akis icin kart icinde kucuk bir "Aktif yol" notu gostermeye basladi.
- Boylece ust rozet, secili kart ve kart ici mikro etiket ayni yone dair tutarli bir iz birakiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.120)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.120 - Son Yol Kart Secimi`
- Bu adimda Odemeler panelindeki son yol rozeti, ilgili ozet ya da akis kartini hafif secili gorunumle desteklemeye basladi.
- Boylece kullanici son kullandigi yonu hem ust bilgi alaninda hem de kart yuzeyinde birlikte okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.119)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.119 - Son Yol Rozeti`
- Bu adimda Odemeler panelinin ust yardim alanina, son kullanilan akis yonunu gosteren renkli bir rozet eklendi.
- Dinamik yardim metnine ek olarak kullanici artik son gittigi yonu tek bakista etiket olarak da gorebiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.118)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.118 - Dinamik Odeme Ipucu`
- Bu adimda Odemeler panelinin ust yardim metni, ust butonlar ve ozet/akis kartlari uzerinde gezilirken ilgili yone gore degismeye basladi.
- Ayrica kullanici bir akis kullandiginda, ayni satir son kullanilan yonu aciklayan kisa ozet metnini gostermeye devam ediyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

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

- Odemeler panelinde akis baslik baglami da eklendigi icin, sonraki mantikli kucuk adim secili kolona daha butunlu bir vurgu vermek ya da sadece aktif kolona ozel mikro rozet eklemek olabilir.
