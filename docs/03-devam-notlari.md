# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-15 / v1.203)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.203 - Yardim Metni Yakinlastirma`
- Bu adimda aktif rozet altindaki aksiyon satirlarinda yardim metninin ust boslugu hafifce azaltildi.
- Boylece buton ile yardim metni ayni komut satiri icinde daha toplu ve daha hizli taranir hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.202)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.202 - Rozet Aksiyon Yakinlastirma`
- Bu adimda aktif filtre rozeti ile alt aksiyon satiri arasindaki ust bosluk hafifce azaltildi.
- Boylece rozet ve alt komut satiri daha bagli okunuyor; kart icindeki dikey akış biraz daha toplu hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.201)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.201 - Rozet Ust Bosluk Sikilastirma`
- Bu adimda filtre dugmeleriyle aktif filtre rozeti arasindaki ust bosluk hafifce azaltildi.
- Boylece filtre satiri ile aktif baglam satiri birbirine daha bagli ve daha tek parca okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.200)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.200 - Aktif Rozet Baglam Metni`
- Bu adimda aktif filtre rozetlerinin kisa metninde baglam daha acik hale getirildi.
- Rozetler artik dogrudan `AKTIF KUYRUK` ve `AKTIF SON ODEME` diye basliyor; iki alan yan yana okunurken anlam daha hizli oturuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.199)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.199 - Aktif Rozet Tooltip Dili`
- Bu adimda aktif filtre rozetlerinin tooltip metinleri secili gorunume ve gorunen sonuc sayisina gore dinamik hale getirildi.
- Rozet tooltipleri artik kullaniciya hem hangi filtrede oldugunu hem de bu filtrenin bes kayitlik operasyon penceresinde kac sonuc gosterdigini acikliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.198)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.198 - Aktif Filtre Rozet Tonu`
- Bu adimda aktif filtre rozetleri secili gorunume gore hafif ton farki almaya basladi.
- Kuyrukta `Acil` daha sicak, `PDF Eksik` daha uyari tonunda; son odemelerde de `PDF Eksik` rozet tonu belirginlestirildi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.197)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.197 - Donus Dugmesi Ton Ayrimi`
- Bu adimda bos filtre durumunda gorunen `Hepsini Goster` dugmeleri daha fark edilir ama sakin bir tonla ayristirildi.
- Dugmeler artik hafif mavi ton, ince cerceve ve yarim kalin yazi ile geri donus aksiyonu olarak daha kolay seciliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.196)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.196 - Bos Filtre Fiil Dili`
- Bu adimda bos filtre durumundaki ozet ve yardim metinleri daha yonlendirici fiil diliyle guncellendi.
- Kullanici artik sifir sonuc durumunda sadece bos mesajini degil, ayni satirda geri donus aksiyonunu da acik bicimde okuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.195)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.195 - Bos Filtre Donus Aksiyonu`
- Bu adimda sonuc donmeyen odeme filtre gorunumlerinde `Hepsini Goster` donus dugmeleri eklendi.
- Kullanici artik bos bir filtreye dustugunde ayni satirdan tum kayit gorunumune tek tikla geri donebiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.194)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.194 - Aktif Filtre Sonuc Ozeti`
- Bu adimda odeme kuyrugu ve son odemeler aktif filtre rozetlerine secili gorunumdeki sonuc sayisi eklendi.
- Rozetler artik uygulanan filtreyi ve gorunen kayit sayisini tek satirda ozetliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.193)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.193 - Odemeler Filtre Sonuc Sayilari`
- Bu adimda odeme kuyrugu ve son odemeler filtre dugmelerine gorunum bazli sonuc sayilari eklendi.
- Sayilar panelin en yakin veya en son bes kayitlik operasyon penceresine gore hesaplaniyor; tooltipler de bu kapsami acikliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.192)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.192 - Odemeler Aktif Filtre Rozeti`
- Bu adimda odeme kuyrugu ve son odemeler listesine secili filtreyi surekli gosteren `AKTIF` rozetleri eklendi.
- Rozet metni filtre degistiginde veri listesiyle birlikte yenileniyor; kullanici uygulanan gorunumu dugme satirindan ayrica okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.191)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.191 - Odemeler Klavye Odak Vurgusu`
- Bu adimda odeme filtreleri, hizli aksiyon dugmeleri ve satir ici `Ac` dugmeleri icin belirgin klavye odak vurgulari eklendi.
- Satir ici dugmeye odak gelince kaydin tamami mavi cerceve ve arka planla belirginlesiyor; hizli aksiyonlara da Enter kullanimini anlatan tooltipler eklendi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.190)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.190 - Odemeler Liste Filtreleri`
- Bu adimda odemeler kuyruguna `Hepsi / Acil / PDF Eksik`, son odemeler listesine de `Hepsi / PDF Eksik` gorunum filtreleri eklendi.
- Ozet metinleri ve hizli aksiyon ipuclari secili filtreye gore kendini guncelliyor; boylece panelde tarama daha hizli hale geliyor.
- Ayni dosyada encoding kaynakli bozulmaya acik birkac metin ASCII-guvenli hale getirildi ki yeni kayitlarda tekrar sorun cikmasin.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.189)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.189 - Odemeler Liste Hover Erisimi`
- Bu adimda odemeler kuyrugu ve son odemeler satirlari icin hover vurgusu eklendi; ac dugmelerine de daha net tooltip metinleri yazildi.
- Boylece kullanici hangi satir uzerinde oldugunu daha rahat algiliyor ve acis dugmesinin etkisini okumadan da anlayabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.188)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.188 - One Cikan Kayit Ozeti`
- Bu adimda Odemeler sekmesinin ust alanina oncelikli ya da son kaydi gosteren zengin bir odak karti eklendi.
- Boylece kullanici sekmeyi acar acmaz hangi kayda odaklanmasi gerektigini, durumunu ve hizli acis yolunu gorebiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.187)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.187 - Odemeler Durum Rozetleri`
- Bu adimda odemeler kuyrugu ve son odemeler listesine satir ici durum rozetleri eklendi.
- Boylece odeme durumu ve PDF durumu tek bakista ayirt edilir hale geldi; rozet renkleri veri durumuna gore hesaplaniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.186)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.186 - Odemeler Hizli Aksiyon Satiri`
- Bu adimda Odemeler sekmesine oncelikli kuyruk kaydini ve son odeme kaydini tek tikla acan hizli aksiyon satirlari eklendi.
- Boylece operasyon ozeti sadece liste gostermiyor; kullanici dogrudan en yakin islemden devam edebiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.185)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.185 - Odemeler Ozeti Hedefli Gecis`
- Bu adimda Odemeler sekmesindeki odeme kuyrugu ve son odemeler listesine dogrudan ac butonlari eklendi.
- Boylece kullanici tek tikla ilgili faturayi odeme alaninda hedefli olarak acabiliyor; panel artik sadece ozet gostermiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.184)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.184 - Odemeler Operasyon Ozeti`
- Bu adimda Odemeler sekmesine canli veriyle beslenen odeme calisma kuyrugu ve son odemeler ozeti eklendi.
- Boylece panel ilk kez sadece yonlendirme degil, gercek operasyon baglamini gosteren bir calisma yuzeyi haline geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.183)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.183 - Mikro Hiyerarsi Seri Kapanisi`
- Bu adimda odemeler gecis panelindeki mikro hiyerarsi butun olarak yeniden gozden gecirildi.
- Ek piksel ayarina ihtiyac gorulmedigi icin seri kontrollu bicimde kapatildi ve sonraki ana is akisina gecis notu hazirlandi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.182)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.182 - Kisayol Aksiyon Son Ritim`
- Bu adimda odeme akis kartlarindaki kisayol kapsulu ile aksiyon dugmesi arasindaki ust bosluk hafifce azaltildi.
- Boylece alt komut bolgesi daha toplu okunurken butonun komut agirligi korunmus oldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.181)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.181 - Hedef Kisayol Bosluk Ayari`
- Bu adimda odeme akis kartlarindaki hedef satiri ile kisayol kapsulu arasindaki ust bosluk hafifce azaltildi.
- Boylece yonlendirme metinlerinden kisayol ipucuna gecis daha toplu ve daha dengeli gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.180)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.180 - Sonraki Adim Ton Ayrimi`
- Bu adimda odeme akis kartlarindaki sonraki adim metinlerinin tonu hafifce yumusatildi.
- Boylece hedef satiri ana yonlendirme olarak daha net kalirken, sonraki adim yardimci katmana daha temiz oturdu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.179)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.179 - Aktif Yol Gecis Yumusatma`
- Bu adimda odeme akis kartlarindaki aktif yol notlarinin tonu ve ust boslugu hafifce yumusatildi.
- Boylece aciklama metninden aktif yol notuna gecis daha akici ve daha sakin gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.178)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.178 - Yardim Aksiyon Yakinlastirma`
- Bu adimda odeme akis kartlarindaki yardim kapsulu ile aksiyon dugmesi arasindaki ust bosluk hafifce azaltildi.
- Boylece kisayol ipucu ile ana komut birbirine daha bagli ve daha dengeli gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.177)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.177 - Aktif Rozet Oran Ayari`
- Bu adimda odeme akis kartlarindaki aktif kolon rozetlerinin ic boslugu hafifce azaltildi.
- Boylece aktif rozet ile yardim kapsulu arasindaki boyut orani daha yakin ve daha dengeli gorunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.176)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.176 - Hover Kenarlik Tonu Ayari`
- Bu adimda odeme akis kartlarinin hover sirasindaki kenarlik tonu hafifce yumusatildi.
- Boylece kart vurgusu korunurken arka plan ve kenarlik birlikte daha sakin bir butunluk verdi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.175)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.175 - Hover Arka Plan Tonu Ayari`
- Bu adimda odeme akis kartlarinin hover sirasindaki arka plan tonu hafifce yumusatildi.
- Boylece kart vurgusu korunurken panelin genel sakinlik seviyesi bir kademe daha dengelendi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.174)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.174 - Hover Buyume Orani Ayari`
- Bu adimda odeme akis kartlarinin hover sirasindaki mikro olcek buyumesi hafifce azaltildi.
- Boylece kart tepkisi korunurken panel icindeki genel sakinlik bir kademe daha dengelendi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.173)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.173 - Hover Donus Hiz Ayari`
- Bu adimda odeme akis kartlarinin hover cikis sureleri hafifce kisaltildi.
- Boylece kartlar sakin tonunu korurken, etkileşim bitince temel haline biraz daha cevik donuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.172)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.172 - Hover Opaklik Dengeleme`
- Bu adimda odeme akis kartlarinin dinlenim ve hover opaklik farki bir kademe azaltildi.
- Boylece hareket hissi korunurken, kartlar panel icinde daha sakin ve daha kararlı bir tonla duruyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.171)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.171 - Hover Gecisi Yumusatma`
- Bu adimda odeme akis kartlarinin hover kenarlik tonu ve mikro olcek buyumesi bir kademe sakinlestirildi.
- Boylece kart hareketi hissedilir kalirken, yeni metin ve aksiyon hiyerarsisiyle daha uyumlu bir sakinlik kazandi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-15 / v1.170)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.170 - Birincil Odak Tonu Yumusatma`
- Bu adimda odeme akis kartlarindaki birincil aksiyon dugmesinin klavye odak vurgusu bir kademe sakinlestirildi.
- Boylece odak gorunurlugu korunurken, birincil dugme gereksiz kararma olmadan panelin yeni ton dengesine daha iyi oturuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.169)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.169 - Renk Ailesi Agirlik Dengeleme`
- Bu adimda mavi ve turuncu secili rozet tonlari bir kademe yumusatilarak yesil akisla daha yakin algisal agirlikta toplandi.
- Boylece uc akis karti ayni panel dili icinde daha dengeli okunuyor; hicbiri gereksiz yere digerinin onune gecmiyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.168)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.168 - Ikincil Odak Tonu Yumusatma`
- Bu adimda odeme akis kartlarindaki ikincil aksiyon dugmelerinin klavye odak vurgusu bir kademe sakinlestirildi.
- Boylece klavye ile gezinirken odak gorunurlugu korunuyor, ancak birincil komut hiyerarsisi daha temiz sekilde sabit kaliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.167)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.167 - Ikincil Aksiyon Tonu Dengeleme`
- Bu adimda odeme akis kartlarinda kullanilan ikincil aksiyon dugmelerinin tonu bir kademe sakinlestirildi.
- Boylece birincil komut daha net onde kalirken, diger iki aksiyon butonu panelin genel yardim hiyerarsisine daha iyi oturuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.166)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.166 - Kisayol Tonu Yumusatma`
- Bu adimda secili akis kartlarindaki `Enter ile ac` kapsul metinlerinin tonu bir kademe yumusatildi.
- Boylece aksiyon dugmesi ana komut olarak onde kalirken, kisayol kapsulu daha yardimci bir operator ipucu gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.165)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.165 - Sonraki Adim Tonu Yumusatma`
- Bu adimda secili akis kartlarindaki `Sonraki adim` satirlarinin tonu bir kademe yumusatildi.
- Boylece `Hedef` satiri ve aksiyon dugmesi ana yonlendirme omurgasi olarak daha net onde kalirken, sonraki adim satiri yardimci bir devam bilgisi gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.164)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.164 - Aktif Yol Tonu Yumusatma`
- Bu adimda secili akis kartlarindaki `Aktif yol` notlarinin tonu bir kademe daha sakinlestirildi.
- Boylece hedef ve sonraki adim satirlari ana yonlendirme yukunu tasirken, aktif yol notu daha yardimci bir gecis satiri gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.163)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.163 - Baglam Cumlesi Tonu Yumusatma`
- Bu adimda secili akis kartlarindaki baglam cumlelerinin renk ve agirligi bir kademe yumusatildi.
- Boylece baslik, aktif yol ve hedef satirlari daha onde kalirken, baglam cumlesi daha yardimci bir arka plan bilgisi gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.162)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.162 - Aciklama Tonu Yumusatma`
- Bu adimda secili akis kartlarindaki ilk aciklama satirlarinin tonu bir kademe daha yardimci katmana cekildi.
- Boylece aktif durum, hedef ve sonraki adim satirlari ana odakta kalirken, aciklama satiri daha sakin bir destek bilgisi gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.161)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.161 - Baslik Ust Bosluk Dengeleme`
- Bu adimda secili akis kartlarindaki basliklarin ust boslugu bir kademe azaltildi.
- Boylece aktif kolon rozeti ile baslik iliskisi daha kompakt ve kartin ust girisi daha yekpare hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.160)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.160 - Rozet Ust Bosluk Dengeleme`
- Bu adimda secili akis kartlarindaki aktif kolon rozetlerinin ust boslugu bir kademe azaltildi.
- Boylece secili durum girisi kartin ust blogunda daha kompakt ve daha tutarli hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.159)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.159 - Aksiyon Butonu Yakinlastirma`
- Bu adimda secili akis kartlarindaki aksiyon butonlarinin ust boslugu bir kademe azaltildi.
- Boylece kisayol kapsulu ile ana eylem dugmesi daha tek bir alt aksiyon blogu gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.158)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.158 - Kisayol Kapsulu Yakinlastirma`
- Bu adimda secili akis kartlarindaki `Enter ile ac` kapsullerinin ust boslugu bir kademe azaltildi.
- Boylece `Sonraki adim` satiri ile kisayol kapsulu daha tek akis gibi okunuyor ve alt yardim zinciri daha toplu hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.157)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.157 - Sonraki Adim Yakinlastirma`
- Bu adimda secili akis kartlarindaki `Sonraki adim` satirinin ust boslugu bir kademe azaltildi.
- Boylece `Hedef` ve `Sonraki adim` satirlari daha tek blok gibi okunuyor ve alt yonlendirme zinciri daha toplu hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.156)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.156 - Hedef Satiri Yakinlastirma`
- Bu adimda secili akis kartlarindaki `Hedef` satirinin ust boslugu bir kademe azaltildi.
- Boylece aktif yol notu ile ana yonlendirme satiri daha bagli okunuyor ve secili rota bilgisi daha akici hissettiriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.155)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.155 - Aktif Yol Notu Yakinlastirma`
- Bu adimda secili akis kartlarindaki baglam cumlesi ile `Aktif yol` notu arasindaki bosluk bir kademe azaltildi.
- Boylece secili durumun yardimci notu ust bilgi blogunun daha dogal bir devam satiri gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.154)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.154 - Aciklama Baglam Bosluk Ayari`
- Bu adimda secili akis kartlarindaki aciklama satiri ile baglam cumlesi arasindaki bosluk bir kademe azaltildi.
- Boylece ust bilgi blogu daha tek parcali okunuyor ve baglam cumlesi aciklamadan kopuk durmuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.153)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.153 - Baslik Aciklama Ritim Sikilastirma`
- Bu adimda secili akis kartlarindaki baslik ile aciklama satiri arasindaki bosluk bir kademe azaltildi.
- Boylece aktif rozet, baslik ve ilk aciklama satiri daha tek parca bir ust bilgi blogu gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.152)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.152 - Rozet Baslik Yakinlastirma`
- Bu adimda secili akis kartlarinda aktif kolon rozeti ile baslik arasindaki ust bosluk bir kademe azaltildi.
- Boylece secili durum rozeti basliga daha bagli okunuyor ve kartin giris blogu daha toplu hissediliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.151)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.151 - Mikro Rozet Animasyon Ritmi`
- Bu adimda secili akis kartlarindaki `AKTIF KOLON` rozetlerinin gorunme animasyonu yardim kapsuluyle ayni fade ritmine yaklastirildi.
- Boylece kart icindeki mikro geri bildirim ogecikleri secim aninda daha tutarli ve tek parca bir tepki veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.150)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.150 - Mikro Rozet Agirlik Yumusatma`
- Bu adimda secili akis kartlarindaki `AKTIF KOLON` rozet metinlerinin agirligi bir kademe dusuruldu.
- Boylece renkli rozet vurgusu korunurken, kart icindeki diger yardim kapsulleriyle daha dengeli bir tipografik iliski kuruldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.149)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.149 - Mikro Rozet Olcek Dengeleme`
- Bu adimda secili akis kartlarindaki `AKTIF KOLON` rozetleri bir kademe kucultulup yardim kapsulleriyle daha yakin bir mikro olcege getirildi.
- Boylece aktif kolon rozetleri hala belirgin kalirken, kart icindeki yardim katmanlari ayni gorsel ailenin parcasi gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.148)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.148 - Yardim Kapsulu Agirlik Yumusatma`
- Bu adimda secili akis kartlarindaki `Enter ile ac` kapsullerinin yazi agirligi bir kademe sakinlestirildi.
- Boylece buton komutu ana aksiyon olarak onde kalirken, kapsul daha yumusak bir yardim katmani gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.147)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.147 - Hedef Sonraki Adim Aralik Ayari`
- Bu adimda secili akis kartlarindaki `Hedef` ve `Sonraki adim` satirlari arasindaki dikey aralik hafifce azaltildi.
- Boylece bu iki satir daha tek parca bir yonlendirme blogu gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.146)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.146 - Ust Bilgi Mikro Bosluk Ayari`
- Bu adimda secili akis kartlarindaki ust bilgi satiri ile `Aktif yol` notu arasindaki bosluk hafifce azaltildi.
- Boylece kartin ust bilgi blogu daha akici ve daha birlesik okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.145)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.145 - Ilk Aciklama Tonu Sakinlestirme`
- Bu adimda secili akis kartlarindaki ilk aciklama satirlarinin tonlari biraz daha sakinlestirildi.
- Boylece baslik ve `Hedef` satiri onde kalirken, ilk aciklama satiri daha yumusak bir destek metni gibi okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.144)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.144 - Ust Ritim Dengeleme`
- Bu adimda secili akis kartlarindaki aktif kolon rozeti ile kart basligi arasindaki ust bosluk hafifce sikilastirildi.
- Boylece kartin ilk bakista okunan ust blogu daha dengeli ve daha tek parca hissettirmeye basladi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.143)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.143 - Buton Ust Bosluk Dengeleme`
- Bu adimda secili akis kartlarindaki buton ust boslugu, kompakt yardim kapsulu olcegine uyacak sekilde biraz azaltildi.
- Boylece yardim kapsulu ile eylem dugmesi ayni alt yonlendirme blogu icinde daha yakin ve daha dengeli okunuyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

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

- Odemeler panelindeki yardim kapsulu agirligi da yumusadigi icin, sonraki mantikli kucuk adim secili akis kartlarinda aktif kolon rozeti ile yardim kapsulu arasindaki boyut oranini bir tik daha yaklastirmak olabilir.
