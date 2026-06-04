# Kurum Fatura Takip ProgramÃ„Â±

Bu depo, kurum aboneliklerine ait elektrik, su, doÃ„Å¸algaz, telefon, internet ve benzeri faturalarÃ„Â±n takip edilmesi iÃƒÂ§in geliÃ…Å¸tirilecek masaÃƒÂ¼stÃƒÂ¼ uygulamanÃ„Â±n ÃƒÂ§alÃ„Â±Ã…Å¸ma alanÃ„Â±dÃ„Â±r.

## GÃƒÂ¼ncel Durum

- Proje klasÃƒÂ¶rÃƒÂ¼: `C:\Users\Asus\Documents\FATURA TAKÃ„Â°P PROGRAMI`
- Git deposu hazÃ„Â±r.
- `v0.1` - `v0.18` arasÃ„Â± temel uygulama, raporlar ve Excel/PDF export altyapÃ„Â±sÃ„Â± tamamlandÃ„Â±.
- `v0.19` manuel gÃƒÂ¼venli yedekleme eklendi (`--create-backup` + UI).
- `v0.20` tutarlÃ„Â±lÃ„Â±k denetimi eklendi (`Raporlar > TutarlÃ„Â±lÃ„Â±k` + `--consistency-check`).
- `v0.21` gÃƒÂ¼venli yedek geri yÃƒÂ¼kleme eklendi (`--restore-backup` + `--restore-target`).
- `v0.22` backup ekranÃ„Â±na UI restore akÃ„Â±Ã…Å¸Ã„Â± eklendi.
- `v0.23` rapor export Ã…Å¸ablon hizalama ve `YÃ„Â±llÃ„Â±k Liste` sekmesi tamamlandÃ„Â±.
- `v0.24` derleme uyarÃ„Â±larÃ„Â± temizlendi.
- `v0.25` - `v0.35` arasÃ„Â± PDF rapor yerleÃ…Å¸imi ÃƒÂ¶rnek matbu dÃƒÂ¼zene yaklaÃ…Å¸tÃ„Â±rÃ„Â±ldÃ„Â± ve gÃƒÂ¶rsel QA yapÃ„Â±ldÃ„Â±.
- `v0.36` restore iÃƒÂ§in "boÃ…Å¸ olmayan hedef reddedilir" negatif smoke testi self-test kapsamÃ„Â±na alÃ„Â±ndÃ„Â±.
- `v0.37` audit log temeli eklendi; kritik veri iÃ…Å¸lemleri artÃ„Â±k `audit_logs` tablosuna kaydediliyor.
- `v0.38` `Raporlar > Islem Gecmisi` sekmesi eklendi; audit log kayitlari artik UI ve export uzerinden gorulebiliyor.
- `v0.39` `Islem Gecmisi` sekmesine islem turu ve tarih filtreleri eklendi.
- `v0.40` `Islem Gecmisi` icin varlik, kullanici ve metin arama filtreleri eklendi.
- `v0.41` secili audit log satiri icin eski/yeni deger detay paneli eklendi.
- `v0.42` audit log detayinda alan bazli fark tablosu eklendi.
- `v0.43` audit log diff tablosuna "sadece degisen alanlar" filtresi eklendi.
- `v0.44` audit log diff `Durum` alani renkli rozetlerle gosterilmeye baslandi.
- `v0.45` audit log detay paneline `Kopyala` aksiyonlari eklendi.
- `v0.46` audit log detayinda gorunen diff tablosunu topluca kopyalama aksiyonu eklendi.
- `v0.47` audit log detayina `TXT` ve `JSON` disa aktarma aksiyonlari eklendi.
- `v0.48` audit log araclarina `exports` klasorunu tek tikla acma aksiyonu eklendi.
- Solution dosyasÃ„Â±: `FaturaTakip.sln`
- Uygulama projesi: `src/FaturaTakip.App/FaturaTakip.App.csproj`
- GeliÃ…Å¸tirme planÃ„Â± proje iÃƒÂ§ine alÃ„Â±ndÃ„Â±: `docs/01-gelistirme-plani.md`
- Roadmap oluÃ…Å¸turuldu: `ROADMAP.md`
- Regresyon kontrol dosyasÃ„Â± oluÃ…Å¸turuldu: `REGRESYON.md`
- Yeni Codex chatlerinde devam edebilmek iÃƒÂ§in kÃ„Â±lavuz ve devam notlarÃ„Â± eklendi.

## Yeni Codex Chatinde Ã„Â°lk Okunacak Dosyalar

Yeni bir Codex sohbetinde bu projeye devam ederken ÃƒÂ¶nce Ã…Å¸u dosyalar okunmalÃ„Â±:

1. `README.md`
2. `docs/00-codex-devam-kilavuzu.md`
3. `docs/03-devam-notlari.md`
4. `ROADMAP.md`
5. `REGRESYON.md`
6. `docs/01-gelistirme-plani.md`

ArdÃ„Â±ndan terminalde durum kontrolÃƒÂ¼ yapÃ„Â±lmalÃ„Â±:

```powershell
git status --short --branch
```

## Yol ve Dosya AdÃ„Â± Ã„Â°lkesi

Proje klasÃƒÂ¶rÃƒÂ¼nÃƒÂ¼n adÃ„Â± TÃƒÂ¼rkÃƒÂ§e karakter iÃƒÂ§erdiÃ„Å¸i iÃƒÂ§in PowerShell komutlarÃ„Â±nda gerekirse `-LiteralPath` kullanÃ„Â±lmalÃ„Â±. Proje iÃƒÂ§indeki yeni dosya ve klasÃƒÂ¶r adlarÃ„Â± mÃƒÂ¼mkÃƒÂ¼n olduÃ„Å¸unca ASCII tutulacak.

Ãƒâ€“rnek:

```powershell
Get-ChildItem -LiteralPath 'C:\Users\Asus\Documents\FATURA TAKÃ„Â°P PROGRAMI'
```

## Son Tamamlanan Faz

Son tamamlanan geliştirme fazı `v0.49 - Islem Gecmisi Filtre Tercihleri`.

Bu fazda audit log filtreleri (islem, varlik, kullanici, arama, tarih araligi ve sadece degisen alanlar secimi) config dosyasina yazilarak yeni acilista geri yuklenir hale getirildi.

## SÃ„Â±radaki Ã„Â°Ã…Å¸

Sıradaki mantıklı iş: audit log ekranina filtreleri tek tikla sifirlama veya secili kayda hizli odaklanma gibi kullanim rahatligi iyilestirmeleri eklemek.

Bu noktadan sonra yeni chatlerde ÃƒÂ¶nce `docs/03-devam-notlari.md` ve `REGRESYON.md` okunmalÃ„Â±; en gÃƒÂ¼ncel durum artÃ„Â±k bu dosyalarda tutuluyor.

## Ãƒâ€¡alÃ„Â±Ã…Å¸ma KuralÃ„Â±

Her faz sonunda Ã…Å¸u dosyalar gÃƒÂ¼ncellenmeli:

- `docs/03-devam-notlari.md`
- `ROADMAP.md`
- `REGRESYON.md`

BÃƒÂ¶ylece yeni Codex chatine geÃƒÂ§ildiÃ„Å¸inde nerede kalÃ„Â±ndÃ„Â±Ã„Å¸Ã„Â± dosyalardan anlaÃ…Å¸Ã„Â±lÃ„Â±r.
