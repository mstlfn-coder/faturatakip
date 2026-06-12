# Guncel Durum Ozeti

Son guncelleme tarihi: 2026-06-12

## Hemen Okunacak Ozet

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son commit: `git log --oneline -1` ile teyit et; bu dosya hizli handoff ozeti olarak tutulur
- Son tamamlanan temiz fazlar:
  - `v1.08` Baglam Turu Filtresi
  - `v1.09` Baglam No Aramasi
  - `v1.10` Baglam Paneli Turkce Metin Temizligi
  - `v1.11` ReportsView Turkce Metin Temizligi
  - `v1.12` Temiz Handoff Ozet Dosyasi
  - `v1.13` Baglami Tek Tikla Daraltma
  - `v1.14` Daraltma Sonrasi Otomatik Odak
  - `v1.15` Baglamdan Inceleme Akisi
  - `v1.16` Baglam Inceleme Kisayolu
  - `v1.17` Baglam Aksiyon Tooltipleri
  - `v1.18` Baglam Aksiyon Satir Duzeni
- Son dogrulanan smoke testler:
  - `dotnet build .\FaturaTakip.sln -c Release`
  - `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Bu Dalda Son Yapilanlar

1. Inceleme baglam paneline `Bağlam Dönemi` aksiyonu eklendi.
2. Inceleme baglam paneline `Bağlam Türü` aksiyonu eklendi.
3. Inceleme baglam paneline `Bağlam No` aksiyonu eklendi.
4. `InvoiceReviewContextFormatter` icinde baglamdan donem, tur ve fatura no ayristirma yardimcilari eklendi.
5. Donem parser'i daraltildi; `INV-001` gibi numaralar artik donem sayilmiyor.
6. `InvoicesView` baglam panelindeki gorunur Turkce metinler temizlendi.
7. ReportsView ve bazi ortak uygulama metinlerindeki gorunur Turkce karakter bozulmalari temizlendi.
8. Yeni chatler icin temiz handoff ozet dosyasi eklendi.
9. Baglam paneline tek tikla daraltma aksiyonu eklendi.
10. Daraltma sonrasi tercihli baglam kaydina otomatik odak davranisi netlestirildi.
11. `Bağlamdan İncele` aksiyonu review modunu ve ikincil baglam ipuclarini tek akista kurar hale getirildi.
12. `Ctrl+Shift+I` ile baglamdan inceleme akisi klavyeden tetiklenebilir hale getirildi.
13. Baglam paneli aksiyonlarina aciklayici tooltip'ler eklendi.
14. Baglam aksiyon satiri `WrapPanel` ile dar alanlarda daha dayanikli hale getirildi.

## Son 5 Commit

- `c208e4e` `fix: clean report view turkish text`
- `4dc2384` `fix: clean review context turkish text`
- `1c88d6a` `feat: add review context invoice number action`
- `0b96ba9` `feat: add review context invoice type action`
- `d71c845` `feat: add review context period filter action (#103)`

## Sonraki Mantikli Adim

En mantikli siradaki adim:

- Eski tarihsel dokumanlardaki encoding kalintilarini parca parca temizlemek veya
- Bu temizlik yerine yeni temiz ozet dokumanlari referans alip uygulama tarafindaki sonraki is akisina gecmek

Pratik tercih:

- Dokuman tarafinda: `README`, `ROADMAP`, `REGRESYON` ve bu dosyanin en guncel bloklarini referans kabul et
- Uygulama tarafinda: baglam aksiyonlarinin ardindan operator akisini hizlandiracak yeni kucuk iyilestirmeye gec

## Yeni Chat Icin Kisa Talimat

```text
Bu projede once docs/06-guncel-durum-ozeti.md dosyasini oku. Sonra README.md, ROADMAP.md ve REGRESYON.md icindeki en guncel bolumleri kontrol et. Aktif branch uzerinde siradaki mantikli kucuk adimi uygula; sonunda build + self-test ve dokuman guncellemelerini yap.
```

## Not

`docs/03-devam-notlari.md`, `ROADMAP.md`, `REGRESYON.md` ve `README.md` icinde eski fazlardan gelen encoding kalintilari bulunabilir.
Bu dosya, yeni chatlerde hizli ve temiz handoff noktasi olarak tutulur.
