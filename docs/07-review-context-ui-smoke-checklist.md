# Review Context UI Smoke Checklist

Son guncelleme: 2026-06-19

Son etkilesimli dogrulama: `v1.334 - Review Cipi Klavye Kaynak Ayrimi`
Son butunsel kapanis: `v1.341 - Zengin Rapor Baglami Serisi Kapanisi`

Bu belge, Faturalar ekranindaki review baglam cipi akislarini yeni chatlerde veya release oncesi hizlica gozden gecirmek icin tutulur.
Amac, son eklenen mikro UX rahatliklarini elle ama hizli sekilde dogrulamaktir.

## Hazirlik

1. `UygulamayiCalistir.bat` ile uygulamayi ac.
2. Raporlar ekranindan review baglami uretecek bir rapor sec:
   - `İncelenmedi`
   - `Gecikmiş`
   - `Evrak Kontrol`
3. Rapor satirindan Faturalar ekranina gec ve `Rapor Gecis Baglami` panelinin gorundugunu dogrula.

## Temel Gorunum

1. Baglam paneli gorunuyor.
2. Baglam cipleri sirali ve okunur.
3. Aksiyonlu ciplere `UYG`, kopya odakli ciplere `KPY` isareti geliyor.
4. Son kullanilan cip tiklandiktan sonra hafif secim vurgusu aliyor.
5. Baglam degistiginde eski cip vurgusu temizleniyor.

## Fare Akisi

1. Aksiyonlu bir cipe sol tikla:
   - ilgili filtre/aksiyon uygulanir
   - alt durum cubugunda `Çip:` ile baslayan kisa mesaj gorunur
   - mesaj kisa sureli renkli vurgu alir ve sonra normale doner
2. Bir cipe sag tikla:
   - menu acilir
   - uygun ciplere `Uygula`, tum ciplere `Kopyala` secenegi vardir
   - klavye/context-menu acilisi ile acik sag tik olayi ayni menu kurucusunu kullanir
3. Sag tik menusunden `Kopyala` sec:
   - alt durum cubugunda `Menü:` ile baslayan mesaj gorunur

## Klavye Akisi

1. `Tab` ile bir baglam cipine odaklan.
2. Odakli cip belirgin cerceveyle gorunur.
3. `Enter` veya `Space`:
   - ana aksiyon calisir
   - alt durum cubugunda `Klavye:` ile baslayan mesaj gorunur
   - cip olay baglantisi `PreviewKeyDown` uzerinden calisir; varsayilan Button tiklamasi mesaji `Cip:` olarak ezmez
4. `Ctrl+C`:
   - cip metni panoya kopyalanir
   - `Klavye:` mesajı gorunur
5. `Shift+F10` veya menu tusu:
   - cip baglam menusu acilir
6. `Esc`:
   - odak cipten cikar
   - odak once `Bağlamı Göster` alanina, o uygun degilse inceleme notu alanina gider

## Tooltip Kontrolu

1. Aksiyonlu bir cipte tooltip:
   - tik davranisini aciklar
   - `Enter/Space`, `Ctrl+C`, `Shift+F10` ipuclarini gosterir
2. Kopya odakli bir cipte tooltip:
   - kopyalama davranisini aciklar
   - ayni klavye ipuclarini gosterir

## Inceleme Turu Kisayollari

Review modu acikken:

1. `Ctrl+Shift+Sag`: sonraki kayda gider; son kayitta sinir mesaji verir.
2. `Ctrl+Shift+Sol`: onceki kayda gider; ilk kayitta sinir mesaji verir.
3. `Ctrl+Shift+O`: secili fatura PDF dosyasini acar; eksikse aciklayici hata verir.
4. `Ctrl+Shift+K`: secili faturanin PDF klasorunu acar ve tur akisini ilerletir.
5. `Ctrl+Shift+B`: review baglam gorunurlugunu degistirir.
6. `Ctrl+Shift+C`: tam review baglamini panoya kopyalar.
7. `Ctrl+Shift+I`: review baglamindan hedef faturayi inceleme akisi kurar.
8. `Ctrl+Shift+X`: review baglamini temizleyip normal akisa doner.

Tercih dosyasini degistiren smoke turlarinda test oncesi yedek ve test sonrasi hash dogrulamali geri yukleme kullan.

## Cikis Kriteri

Asagidakiler birlikte saglaniyorsa review baglam cipi UX halkasi smoke seviyesinde temiz kabul edilir:

- Gorunur baglam paneli bozulmadi
- Cip aksiyonlari hem fare hem klavyede calisiyor
- Kisa durum mesajlari dogru kaynak etiketiyle geliyor
- Mikro vurgu ve secim izi beklendigi gibi davraniyor
- Tooltip ve menu yardimlari yerinde

## Son Dogrulanan Sonuclar

- `Rapor: İncelenmedi` cipi fare ile `Cip:` kaynak etiketi uretir.
- Ayni cip Enter ve Space ile `Klavye:` kaynak etiketi uretir.
- Ctrl+C pano metnini kopyalar ve `Klavye:` kaynak etiketi uretir.
- Shift+F10 ve Menu tusu Uygula/Kopyala seceneklerini acar.
- Menu aksiyonlari `Menu:` kaynak etiketi uretir.
- Fare sag tik ayni Uygula/Kopyala seceneklerini acar.
- Sag tik Uygula ve Kopyala aksiyonlari `Menu:` kaynak etiketi uretir.
- Esc odagi `Baglami Goster` alanina geri tasir.
- `Baglam Filtresi`, `Baglami Daralt` ve `Baglamdan Incele` aksiyonlari calisir.
- Baglamda donem, tur veya fatura no bilgisi yoksa ilgili detay dugmeleri pasif kalir.
- Sekiz Ctrl+Shift inceleme turu kisayolu beklenen durum mesajlarini uretir.
- Gecikmis secili satir baglami tur ve fatura no aksiyonlarini etkinlestirir.
- Evrak Kontrol `YYYY/MM` donemini `YYYY-MM` donem cipine normalize eder.
- PDF Eksik baglaminda Donem cipi ve Donem aksiyonu etkinlesir.
- Tur, fatura no ve donem cipleri Enter/Space ile `Klavye:` mesaji uretir.
- Tur, arama no, yil ve ay filtreleri gercek degerleriyle uygulanir.
- Tur, fatura no ve donem cipleri Ctrl+C ile dogru metni panoya kopyalar.
- Detay ciplerinin Shift+F10 menuleri cipe ozel Uygula ve Kopyala seceneklerini gosterir.
- Menu Kopyala `Menu:` mesaji, Menu Uygula ise gercek filtre sonucunu uretir.

## Zengin Baglam Aksiyon Matrisi

| Baglam | Ana aksiyonlar | Donem | Tur | Fatura No |
| --- | --- | --- | --- | --- |
| Gecikmis - genel | Aktif | Pasif | Pasif | Pasif |
| PDF Eksik - genel | Aktif | Pasif | Pasif | Pasif |
| Gecikmis - secili satir | Aktif | Pasif | Aktif | Aktif |
| PDF Eksik - secili satir | Aktif | Aktif | Pasif | Pasif |

Secili detay cipleri icin:

- Tur: `Telefon`, gercek filtre `InvoiceTypeId=4`.
- Fatura No: `1241231231123123`, gercek arama metni ayni deger.
- Donem: `2026-06`, gercek filtre yil `2026`, ay `Haziran (6)`.
- Enter/Space `Klavye:`, menu aksiyonlari `Menu:` kaynak etiketini uretir.
- Ctrl+C ve Menu Kopyala cipe ait metni panoya alir.

Review baglami cipi ve zengin rapor baglami UX halkasi v1.342 checkpoint denetimi itibariyla fare ve klavye smoke seviyesinde temiz kabul edilir.

## Komut Destegi

Elle UI smoke oncesi veya sonrasi su komutlar tavsiye edilir:

- `dotnet build .\FaturaTakip.sln -c Release`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
