# Guncel Durum Ozeti

Son guncelleme tarihi: 2026-06-13

## Hemen Okunacak Ozet

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son commit: `git log --oneline -1` ile teyit et; bu dosya hizli handoff ozeti olarak tutulur
- Son tamamlanan temiz fazlar:
  - `v1.85` Secili Yardim Rozeti Ipuclari
  - `v1.84` Replay Ozet Tooltip Genisleme
  - `v1.83` Replay Indicator UI Smoke Checklist
  - `v1.82` Replay Indicator Self-Test
  - `v1.81` Replay Indicator Helper
  - `v1.80` Replay Tooltip Ton Sadeleştirmesi
  - `v1.79` Replay Bekleme Dili Uyumu
  - `v1.78` Replay Aktiflik Dili Uyumu
  - `v1.77` Replay Dili Uyumu
  - `v1.76` Replay Tooltip Metin Sikilastirmasi
  - `v1.75` Replay Tooltip Action Adi
  - `v1.74` Replay Tooltip Durum Mesaji
  - `v1.73` Replay Isaret Tooltipi
  - `v1.72` Replay Sekil Ayrimi
  - `v1.71` Replay Sure Izi
  - `v1.70` Replay Vurgu Seviye Isareti
  - `v1.69` Replay Ozet Prefix Canlanmasi
  - `v1.68` Replay Ozet Prefix Isareti
  - `v1.67` Replay Ozet Ton Ayrimi
  - `v1.66` Replay Bos Durum Metni
  - `v1.65` Baglamsal Replay Ozet Satiri
  - `v1.64` Replay Tercih Ozeti
  - `v1.63` Kisayol Replay Tercihleri
  - `v1.62` Prefix Rozette Tekrar Canlanmasi
  - `v1.61` Mikro Kisayol Tekrar Geri Bildirimi
  - `v1.60` Aktif Prefix Rozet Vurgusu
  - `v1.59` Mikro Kisayol Prefix Rozetleri
  - `v1.58` Mikro Kisayol Odak Gorunurlugu
  - `v1.57` Son Aksiyon Mikro Kisayollari
  - `v1.56` Son Aksiyon Satirinda Gecici Vurgu
  - `v1.55` Odeme Yardiminda Son Aksiyon Satiri
  - `v1.54` Baglamsal Odeme Durum Mesajlari
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
  - `v1.31` Filtre Ozetinde Gecici Baglam Vurgusu
  - `v1.32` Son Baglam Aksiyonu Gosterimi
  - `v1.33` Son Aksiyon Dugme Vurgusu
  - `v1.34` Pasif Baglam Aksiyon Nedenleri
  - `v1.35` Baglam Ozet/Detay Gorunumu
  - `v1.36` Baglam Cipi Hizli Kopyalama
  - `v1.37` Aksiyonlu Baglam Cipleri
  - `v1.38` Cip Davranis Isareti
  - `v1.39` Baglam Cipi Sag Tik Menusu
  - `v1.40` Baglam Cipi Klavye Erisimi
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
28. Filtre ozet satiri, baglam aksiyonu sonrasi kisa sureli `Bağlam: ...` vurgusu gosterecek sekilde zenginlestirildi.
29. Review baglam paneline son calistirilan aksiyonu gosteren mikro metin satiri eklendi.
30. Son kullanilan ana baglam aksiyon dugmesine hafif bir gorsel vurgu eklendi.
31. Pasif baglam aksiyon dugmeleri, neden kullanilamadigini tooltip uzerinden aciklayacak hale getirildi.
32. Review baglam metni icin ozet ve detay gorunumu arasinda gecis eklendi.
33. Review baglam cipleri tiklanabilir hizli kopyalama aracina donusturuldu.
34. Uygun review baglam cipleri ilgili filtre aksiyonlarini dogrudan tetikler hale getirildi.
35. Review baglam ciplerine davranis tipini gosteren `UYG` / `KPY` mikro isaretleri eklendi.
36. Review baglam ciplerine `Uygula` / `Kopyala` secenekleri sunan sag tik menusu eklendi.
37. Review baglam ciplerine klavye odagi ve klavyeden menu acma destegi eklendi.
38. Review baglam panelinde son kullanilan cip hafif bir secim vurgusuyla izlenir hale getirildi.
39. Review baglam ciplerinde `Enter` / `Space` ile ana aksiyon, `Ctrl+C` ile hizli kopyalama desteklendi.
40. Review baglam cipi tooltip'lerine `Enter/Space`, `Ctrl+C`, `Shift+F10` kisayol ipuclari eklendi.
41. Review baglam ciplerinde `Esc` ile panel icinde notr odaga donus eklendi.
42. Review baglam cipi aksiyonlarinda durum cubugu mesaji `Çip`, `Klavye`, `Menü` etiketiyle kisaltildi.
43. Review baglam cipi tooltip ve kisa durum mesajlari self-test icinde dogrudan dogrulanmaya baslandi.
44. Review baglam cipi kaynakli durum mesajlari alt durum cubugunda kisa sureli renk vurgusu almaya basladi.
45. Review baglam cipi akislarini hizli dogrulamak icin ayri UI smoke checklist dokumani eklendi.
46. Odemeler ana akisina gecis icin odeme formuna hazir yardim ozet rozetleri eklendi.
47. Odeme yardim rozetleri tiklanabilir hizli yol ve secim geri bildirimi kazandi.
48. Odeme yardim rozetleri klavyede daha belirgin odak ve `Enter/Space` tooltip ipucu kazandi.
49. Odeme PDF bolumune secili odeme ve evrak durumunu gosteren mini yardim ozeti eklendi.
50. Odeme PDF yardim rozetleri de tiklanabilir hizli yol ve secim geri bildirimi kazandi.
51. Odeme ve odeme PDF yardim akislari alt durum cubugunda baglamsal mesaj etiketleri kazandi.

## Son 5 Commit

- `git log --oneline -1` ile teyit et: `feat: highlight last used review context chip`
- `34a4f0c` `feat: add shortcut for clearing review context`
- `ca28b1b` `feat: add clear action for review context`
- `2b405ea` `fix: reset badge highlight when review context changes`
- `a55cc11` `feat: highlight last used review context badge`

## Sonraki Mantikli Adim

En mantikli siradaki adim:

- Eski tarihsel dokumanlardaki encoding kalintilarini parca parca temizlemek veya
- Bu temizlik yerine yeni temiz ozet dokumanlari referans alip uygulama tarafindaki sonraki is akisina gecmek

Pratik tercih:

- Dokuman tarafinda: `README`, `ROADMAP`, `REGRESYON` ve bu dosyanin en guncel bloklarini referans kabul et
- Uygulama tarafinda: baglam aksiyonlarinin ardindan operator akisini hizlandiracak yeni kucuk iyilestirmeye gec

Guncel pratik tercih:

- Odeme yardim mesajlari da baglamsal hale geldigi icin bir sonraki mantikli kucuk adim odeme/PDF akisina klavye kisayol ipuclari eklemek veya bu rozetler icin hafif UI smoke dogrulamasi tanimlamak olabilir.

## Yeni Chat Icin Kisa Talimat

```text
Bu projede once docs/06-guncel-durum-ozeti.md dosyasini oku. Sonra README.md, ROADMAP.md ve REGRESYON.md icindeki en guncel bolumleri kontrol et. Aktif branch uzerinde siradaki mantikli kucuk adimi uygula; sonunda build + self-test ve dokuman guncellemelerini yap.
```

## Not

`docs/03-devam-notlari.md`, `ROADMAP.md`, `REGRESYON.md` ve `README.md` icinde eski fazlardan gelen encoding kalintilari bulunabilir.
Bu dosya, yeni chatlerde hizli ve temiz handoff noktasi olarak tutulur.
