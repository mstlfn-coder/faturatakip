# Replay Indicator UI Smoke Checklist

Son guncelleme: 2026-06-19

Son butunsel dogrulama: `v1.333 - Replay Smoke Serisi Kapanisi`

Bu belge, Faturalar ekranindaki odeme yardimi ve PDF yardimi replay indicator akislarini hizlica elle dogrulamak icin tutulur.
Amac, replay mini isareti, tooltip dili ve helper ozetlerinin birlikte dogru davrandigini kisa bir turla kontrol etmektir.

## Hazirlik

1. `UygulamayiCalistir.bat` ile uygulamayi ac.
2. `Faturalar` ekranina git.
3. Odeme kaydi olan bir fatura sec.
4. Odeme yardimi ve PDF yardimi alanlari gorunur olsun.

## Odeme Yardimi Kontrolu

1. Bir odeme yardimi aksiyonu calistir:
   - `Kalan Tutar`
   - `Son Aciklama`
   - `Secili Odeme`
2. Replay ozet satirinda ilgili prefix gorunur.
3. Mini indicator aksiyon varsa cizgi agirlikli gorunur.
4. Prefix veya mini indicator ustune gel:
   - tooltip action adini gosterir
   - `replay ayari` ve `vurgu` bilgisini gosterir
5. Ayni aksiyonu tekrar calistir:
   - son aksiyon satiri `yeniden tetiklendi` dilini alir
   - tooltip aktif durumda da bu dili tasir
6. Son hizli yardim ve secili yardim dugmelerini ayri ayri klavyeyle kontrol et:
   - `Enter` aksiyonu yeniden calistirir
   - `Space` aksiyonu yeniden calistirir
   - odak ilgili replay dugmesinde kalir

## PDF Yardimi Kontrolu

1. Bir PDF yardimi aksiyonu calistir:
   - `PDF Sec`
   - `PDF Ac`
2. Replay ozet satirinda ilgili prefix gorunur.
3. Mini indicator aksiyon varsa cizgi agirlikli gorunur.
4. Prefix veya mini indicator ustune gel:
   - tooltip action adini gosterir
   - `replay ayari` ve `vurgu` bilgisini gosterir
5. Ayni aksiyonu tekrar calistir:
   - son aksiyon satiri `yeniden tetiklendi` dilini alir
   - tooltip aktif durumda da bu dili tasir
6. Son hizli yardim ve secili yardim dugmelerini ayri ayri klavyeyle kontrol et:
   - `Enter` dosya secim/acma aksiyonunu yeniden calistirir
   - `Space` dosya secim/acma aksiyonunu yeniden calistirir
   - odak ilgili replay dugmesinde kalir

## Gecici Veri Guvenligi

PDF replay aksiyonlari odeme kaydi gerektiriyorsa:

1. Uygulama kapaliyken `database/fatura_takip.db` dosyasinin proje icinde gecici bir yedegini al.
2. Ana veritabani ve yedegin SHA-256 hash degerlerinin esit oldugunu dogrula.
3. Yalnizca smoke icin gecici odeme olustur.
4. PDF dosya secim pencerelerini `Esc` ile iptal et; gercek PDF ekleme.
5. Smoke sonunda uygulamayi kapat ve veritabanini yedekten geri yukle.
6. Geri yuklenen veritabaninin SHA-256 hash degerinin test oncesi degerle birebir eslestigini dogrula.
7. Gecici yedegi sil ve `attachments/payments` altinda test artigi kalmadigini kontrol et.

## Bos Durum Kontrolu

1. Henuz replay baglami olusmamis bir yardim alanina bak.
2. Mini indicator daha sakin gorunur.
3. Tooltip:
   - `Replay ayari hazir` dilini kullanir
   - action secilirse replay yardiminin hazirlanacagini soyler

## Cikis Kriteri

Asagidakiler birlikte saglaniyorsa replay indicator UX halkasi smoke seviyesinde temiz kabul edilir:

- Odeme yardimi replay indicator gorunuyor
- PDF yardimi replay indicator gorunuyor
- Tooltip action adini ve replay ayarini dogru anlatiyor
- Tekrar tetikleme dili satir ve tooltip arasinda tutarli
- Bos durum dili yonlendirici ve sakin
- Son aksiyon ve secili yardim yuzeyleri Enter/Space ile calisiyor
- Klavye odagi replay sonrasinda ilgili dugmede kaliyor
- Gecici smoke verisi test sonunda hash dogrulamali olarak geri aliniyor

## Son Dogrulanan Sonuclar

- `v1.329`: Kalan tutar, bos odeme ve pasif PDF durumlari dogrulandi.
- `v1.330`: Odeme son aksiyon ve secili yardim dugmeleri Enter/Space ile dogrulandi.
- `v1.331`: PDF son aksiyon replay dugmesi Enter/Space ile dogrulandi.
- `v1.332`: Secili PDF yardim replay dugmesi Enter/Space ile dogrulandi.
- Replay tercihi `2 sn, orta vurgu` olarak okundu.
- PDF secim pencereleri Esc ile iptal edildi; PDF eklenmedi.
- Gecici odemeler test sonlarinda geri alindi.
- Veritabani test oncesi SHA-256 degeri:
  `DF946F2EB74B1E6921DA1D9A03E0491C43C0125E716C2DDD3A0E5FFCED67A29E`

## Komut Destegi

- `dotnet build .\FaturaTakip.sln -c Release`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
