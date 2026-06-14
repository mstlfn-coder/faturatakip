# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.142)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.142 - Yardim Kapsulu Icerik Sikilastirma`
- Bu adimda secili akis kartlarindaki `Enter ile ac` kapsullerinin ic bosluklari ve yazi boyutu biraz sikilastirildi.
- Boylece yardim kapsulleri hala rahat okunuyor ama kart icinde daha zarif ve daha hafif bir mikro ipucu gibi duruyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.141)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.141 - Yardim Metni Satir Yuksekligi`
- Bu adimda secili akis kartlarindaki `Hedef` ve `Sonraki adim` satirlarina sabit satir yuksekligi verildi.
- Boylece uzun satirlarda yardim bloklari daha temiz hizada akiyor ve alt bolum daha duzgun gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.140)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.140 - Aktif Yol Tipografi Ayrimi`
- Bu adimda secili akis kartlarindaki `Aktif yol` notu daha yardimci bir mikro katman gibi okunacak sekilde kucultuldu ve hafifletildi.
- Boylece `Hedef` satiri ana yonlendirme rolunu daha net tasirken, aktif yol notu arka planda daha sakin bir destek olarak kaliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.139)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.139 - Ton Hiyerarsisi Dengeleme`
- Bu adimda secili akis kartlarindaki yardim kapsulleri daha acik arka plan tonlarina cekildi; metin aciklamalari ile kapsuller arasindaki renk farki da yumusatildi.
- Boylece kart ici yardimci katmanlar daha ayni aileden geliyor ama yine de kendi rollerini ayri tutuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.138)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.138 - Aktif Kolon Rozeti Gorunme Gecisi`
- Bu adimda secili akis kartlarindaki `AKTIF KOLON` rozetleri gorunurken cok hafif bir opacity gecisi almaya basladi.
- Boylece aktif kolon rozeti de kisayol kapsulu ile ayni sakin geri bildirim ailesine baglanmis oldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.137)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.137 - Kisayol Kapsulu Gorunme Canlanmasi`
- Bu adimda secili akis kartlarindaki `Enter ile ac` kapsulleri gorundugunde cok hafif bir opacity canlanmasi almaya basladi.
- Boylece yardimci kisayol ipucu kart secimiyle birlikte daha yumusak bir sekilde dikkat cekiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.136)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.136 - Akis Karti Hover Gecisi`
- Bu adimda Odemeler akis kartlarina cok hafif opacity ve scale tabanli hover gecisi eklendi.
- Boylece secili kartlar fareyle taranirken biraz daha yumusak ve rafine bir canlanma hissi veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.135)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.135 - Akis Karti Dikey Ritim Ayari`
- Bu adimda secili akis kartlarindaki hedef, sonraki adim, kisayol kapsulu ve buton arasindaki dikey bosluklar biraz sikilastirildi.
- Boylece kartin alt akis bolumu daha toparli ve daha tek parca bir yonlendirme bloku gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.134)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.134 - Akis Butonu Tooltip Dili`
- Bu adimda Odemeler panelindeki akis butonlarina, ne acacagini ve `Enter` ile calisabilecegini soyleyen kisa tooltip metinleri eklendi.
- Boylece fareyle gezen kullanici da secili kartin eylemini tiklamadan once daha net okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.133)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.133 - Akis Butonu Klavye Odak Vurgusu`
- Bu adimda Odemeler panelindeki akis butonlari icin klavye odaginda daha belirgin cerceve ve ton vurgusu eklendi.
- Boylece secili karttaki `Enter ile ac` ipucu, klavye odagi alindiginda buton yuzeyinde de daha net karsilik buluyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.132)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.132 - Aktif Kolon Kisayol Kapsulu`
- Bu adimda secili akis kartlarina, butondan hemen once gorunen kucuk bir `Enter ile ac` kapsulu eklendi.
- Boylece secili kartta fare aksiyonunun yani sira klavye ile ilerleme ipucu da gorunur hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.131)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.131 - Aksiyon Butonu Fiil Dili`
- Bu adimda Odemeler panelindeki akis butonlari daha net fiil diliyle guncellendi.
- Boylece secili karttaki hedef ve sonraki adim satirlariyla buton metni daha uyumlu hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.130)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.130 - Aktif Kolon Sonraki Adim Ipuclari`
- Bu adimda secili akis kartlarina, hedef satirinin altinda gorunen kisa bir `Sonraki adim:` ipucu eklendi.
- Boylece kullanici sadece nereye gidecegini degil, oraya gidince ilk ne yapacagini da kart uzerinden okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.129)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.129 - Aktif Kolon Hedef Satiri`
- Bu adimda secili akis kartinin dugmesinden hemen once gorunen kisa bir `Hedef:` satiri eklendi.
- Boylece kullanici tiklamadan hemen once o kartin hangi sonucu acacagini daha net okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.128)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.128 - Aktif Kolon Metin Tonu`
- Bu adimda secili akis kartinin yalnizca kendi baslik ve aciklama metinleri hafif ton farki alacak sekilde guncellendi.
- Boylece aktif kolon rozeti ve kart secimine ek olarak, kartin kendi icerigi de daha rafine bir odak hissi veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.127)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.127 - Aktif Kolon Rozeti`
- Bu adimda Odemeler panelindeki secili akis kartina kucuk bir `AKTIF KOLON` rozeti eklendi.
- Boylece aktif yol artik ust rozet, secili kart, alt not, buton vurgu ve kolon rozeti ile birlikte okunuyor.
- Ayrica bozulmus devam dosyasi temiz metin olarak yeniden kuruldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

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

## Sonraki Mantikli Kucuk Adim

- Odemeler panelindeki yardim kapsulleri de inceldigi icin, sonraki mantikli kucuk adim secili akis kartlarindaki buton ust boslugunu bu yeni mikro olcekle yeniden dengelemek olabilir.
