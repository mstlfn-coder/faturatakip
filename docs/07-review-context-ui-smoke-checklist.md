# Review Context UI Smoke Checklist

Son guncelleme: 2026-06-13

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
3. Sag tik menusunden `Kopyala` sec:
   - alt durum cubugunda `Menü:` ile baslayan mesaj gorunur

## Klavye Akisi

1. `Tab` ile bir baglam cipine odaklan.
2. Odakli cip belirgin cerceveyle gorunur.
3. `Enter` veya `Space`:
   - ana aksiyon calisir
   - alt durum cubugunda `Klavye:` ile baslayan mesaj gorunur
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

## Cikis Kriteri

Asagidakiler birlikte saglaniyorsa review baglam cipi UX halkasi smoke seviyesinde temiz kabul edilir:

- Gorunur baglam paneli bozulmadi
- Cip aksiyonlari hem fare hem klavyede calisiyor
- Kisa durum mesajlari dogru kaynak etiketiyle geliyor
- Mikro vurgu ve secim izi beklendigi gibi davraniyor
- Tooltip ve menu yardimlari yerinde

## Komut Destegi

Elle UI smoke oncesi veya sonrasi su komutlar tavsiye edilir:

- `dotnet build .\FaturaTakip.sln -c Release`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
