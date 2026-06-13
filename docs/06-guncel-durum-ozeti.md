# Guncel Durum Ozeti

Son guncelleme tarihi: 2026-06-13

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
  - `v1.19` Baglam Aksiyon Hiyerarsisi
  - `v1.19.1` Acilis NullReference Hotfix
  - `v1.20` Baglam Aksiyon Durum Vurgulari
  - `v1.21` Hazir Aksiyon Ozeti
  - `v1.22` Rozetli Hazir Aksiyon Ozeti
  - `v1.23` Tiklanabilir Aksiyon Rozetleri
  - `v1.24` Rozet Secim Geri Bildirimi
  - `v1.25` Rozet Secim Temizleme
  - `v1.26` Baglami Temizle Aksiyonu
  - `v1.27` Baglami Temizle Kisayolu
  - `v1.28` Baglam Durum Mesaji Temizligi
  - `v1.29` Baglam Odak Rozeti
  - `v1.30` Form Basliginda Baglam Odagi
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
15. Baglam aksiyonlari ana ve detay gruplarina ayrilarak birincil akis daha belirgin hale getirildi.
16. Acilista `Bağlamı Göster` eventi erken geldiginde olusan `NullReferenceException` hotfix ile kapatildi.
17. Baglam panelindeki uygulanabilir aksiyonlara renkli durum vurgulari eklendi.
18. Baglam paneline hazir aksiyon sayisini ve aktif aksiyon adlarini gosteren ozet satiri eklendi.
19. Hazir aksiyon ozeti rozetli mini gostergelere donusturuldu.
20. Hazir aksiyon rozetleri dogrudan tiklanabilir hizli yol haline getirildi.
21. Son tiklanan hazir aksiyon rozetine secim geri bildirimi eklendi.
22. Baglam degistiginde onceki secili rozet vurgusu otomatik temizlenir hale getirildi.
23. `Bağlamı Temizle` aksiyonu ile review baglamindan normal moda tek tikla donus eklendi.
24. `Ctrl+Shift+X` ile baglam temizleme akisi klavyeden de tetiklenebilir hale getirildi.
25. Inceleme baglami aksiyonlarinin durum mesajlari daha kisa ve ortak formatli hale getirildi.
26. Baglam aksiyonuyla secilen satira tablo icinde aksiyon tipini de gosteren `ODAK` rozeti eklendi.
27. Secili kayit form basligina baglam odagini anlatan yardimci satir eklendi.

## Son 5 Commit

- `34a4f0c` `feat: add shortcut for clearing review context`
- `ca28b1b` `feat: add clear action for review context`
- `2b405ea` `fix: reset badge highlight when review context changes`
- `a55cc11` `feat: highlight last used review context badge`
- `861d450` `feat: make review context badges actionable`

## Sonraki Mantikli Adim

En mantikli siradaki adim:

- Eski tarihsel dokumanlardaki encoding kalintilarini parca parca temizlemek veya
- Bu temizlik yerine yeni temiz ozet dokumanlari referans alip uygulama tarafindaki sonraki is akisina gecmek

Pratik tercih:

- Dokuman tarafinda: `README`, `ROADMAP`, `REGRESYON` ve bu dosyanin en guncel bloklarini referans kabul et
- Uygulama tarafinda: baglam aksiyonlarinin ardindan operator akisini hizlandiracak yeni kucuk iyilestirmeye gec

Guncel pratik tercih:

- Baglam odagi artik hem listede hem form basliginda gorundugu icin bir sonraki mantikli adim, ayni baglam aksiyonlarinin sonucunda filtre ozet satirinda da minik bir gecici vurgu vermek olabilir.

## Yeni Chat Icin Kisa Talimat

```text
Bu projede once docs/06-guncel-durum-ozeti.md dosyasini oku. Sonra README.md, ROADMAP.md ve REGRESYON.md icindeki en guncel bolumleri kontrol et. Aktif branch uzerinde siradaki mantikli kucuk adimi uygula; sonunda build + self-test ve dokuman guncellemelerini yap.
```

## Not

`docs/03-devam-notlari.md`, `ROADMAP.md`, `REGRESYON.md` ve `README.md` icinde eski fazlardan gelen encoding kalintilari bulunabilir.
Bu dosya, yeni chatlerde hizli ve temiz handoff noktasi olarak tutulur.
