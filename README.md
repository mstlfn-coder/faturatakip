# Kurum Fatura Takip ProgramÃƒâ€Ã‚Â±

Bu depo, kurum aboneliklerine ait elektrik, su, doÃƒâ€Ã…Â¸algaz, telefon, internet ve benzeri faturalarÃƒâ€Ã‚Â±n takip edilmesi iÃƒÆ’Ã‚Â§in geliÃƒâ€¦Ã…Â¸tirilecek masaÃƒÆ’Ã‚Â¼stÃƒÆ’Ã‚Â¼ uygulamanÃƒâ€Ã‚Â±n ÃƒÆ’Ã‚Â§alÃƒâ€Ã‚Â±Ãƒâ€¦Ã…Â¸ma alanÃƒâ€Ã‚Â±dÃƒâ€Ã‚Â±r.

## GÃƒÆ’Ã‚Â¼ncel Durum

- Proje klasÃƒÆ’Ã‚Â¶rÃƒÆ’Ã‚Â¼: `C:\Users\Asus\Documents\FATURA TAKÃƒâ€Ã‚Â°P PROGRAMI`
- Git deposu hazÃƒâ€Ã‚Â±r.
- `v0.1` - `v0.18` arasÃƒâ€Ã‚Â± temel uygulama, raporlar ve Excel/PDF export altyapÃƒâ€Ã‚Â±sÃƒâ€Ã‚Â± tamamlandÃƒâ€Ã‚Â±.
- `v0.19` manuel gÃƒÆ’Ã‚Â¼venli yedekleme eklendi (`--create-backup` + UI).
- `v0.20` tutarlÃƒâ€Ã‚Â±lÃƒâ€Ã‚Â±k denetimi eklendi (`Raporlar > TutarlÃƒâ€Ã‚Â±lÃƒâ€Ã‚Â±k` + `--consistency-check`).
- `v0.21` gÃƒÆ’Ã‚Â¼venli yedek geri yÃƒÆ’Ã‚Â¼kleme eklendi (`--restore-backup` + `--restore-target`).
- `v0.22` backup ekranÃƒâ€Ã‚Â±na UI restore akÃƒâ€Ã‚Â±Ãƒâ€¦Ã…Â¸Ãƒâ€Ã‚Â± eklendi.
- `v0.23` rapor export Ãƒâ€¦Ã…Â¸ablon hizalama ve `YÃƒâ€Ã‚Â±llÃƒâ€Ã‚Â±k Liste` sekmesi tamamlandÃƒâ€Ã‚Â±.
- `v0.24` derleme uyarÃƒâ€Ã‚Â±larÃƒâ€Ã‚Â± temizlendi.
- `v0.25` - `v0.35` arasÃƒâ€Ã‚Â± PDF rapor yerleÃƒâ€¦Ã…Â¸imi ÃƒÆ’Ã‚Â¶rnek matbu dÃƒÆ’Ã‚Â¼zene yaklaÃƒâ€¦Ã…Â¸tÃƒâ€Ã‚Â±rÃƒâ€Ã‚Â±ldÃƒâ€Ã‚Â± ve gÃƒÆ’Ã‚Â¶rsel QA yapÃƒâ€Ã‚Â±ldÃƒâ€Ã‚Â±.
- `v0.36` restore iÃƒÆ’Ã‚Â§in "boÃƒâ€¦Ã…Â¸ olmayan hedef reddedilir" negatif smoke testi self-test kapsamÃƒâ€Ã‚Â±na alÃƒâ€Ã‚Â±ndÃƒâ€Ã‚Â±.
- `v0.37` audit log temeli eklendi; kritik veri iÃƒâ€¦Ã…Â¸lemleri artÃƒâ€Ã‚Â±k `audit_logs` tablosuna kaydediliyor.
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
- Solution dosyasÃƒâ€Ã‚Â±: `FaturaTakip.sln`
- Uygulama projesi: `src/FaturaTakip.App/FaturaTakip.App.csproj`
- GeliÃƒâ€¦Ã…Â¸tirme planÃƒâ€Ã‚Â± proje iÃƒÆ’Ã‚Â§ine alÃƒâ€Ã‚Â±ndÃƒâ€Ã‚Â±: `docs/01-gelistirme-plani.md`
- Roadmap oluÃƒâ€¦Ã…Â¸turuldu: `ROADMAP.md`
- Regresyon kontrol dosyasÃƒâ€Ã‚Â± oluÃƒâ€¦Ã…Â¸turuldu: `REGRESYON.md`
- Yeni Codex chatlerinde devam edebilmek iÃƒÆ’Ã‚Â§in kÃƒâ€Ã‚Â±lavuz ve devam notlarÃƒâ€Ã‚Â± eklendi.

## Yeni Codex Chatinde Ãƒâ€Ã‚Â°lk Okunacak Dosyalar

Yeni bir Codex sohbetinde bu projeye devam ederken ÃƒÆ’Ã‚Â¶nce Ãƒâ€¦Ã…Â¸u dosyalar okunmalÃƒâ€Ã‚Â±:

1. `README.md`
2. `docs/00-codex-devam-kilavuzu.md`
3. `docs/03-devam-notlari.md`
4. `ROADMAP.md`
5. `REGRESYON.md`
6. `docs/01-gelistirme-plani.md`

ArdÃƒâ€Ã‚Â±ndan terminalde durum kontrolÃƒÆ’Ã‚Â¼ yapÃƒâ€Ã‚Â±lmalÃƒâ€Ã‚Â±:

```powershell
git status --short --branch
```

## Yol ve Dosya AdÃƒâ€Ã‚Â± Ãƒâ€Ã‚Â°lkesi

Proje klasÃƒÆ’Ã‚Â¶rÃƒÆ’Ã‚Â¼nÃƒÆ’Ã‚Â¼n adÃƒâ€Ã‚Â± TÃƒÆ’Ã‚Â¼rkÃƒÆ’Ã‚Â§e karakter iÃƒÆ’Ã‚Â§erdiÃƒâ€Ã…Â¸i iÃƒÆ’Ã‚Â§in PowerShell komutlarÃƒâ€Ã‚Â±nda gerekirse `-LiteralPath` kullanÃƒâ€Ã‚Â±lmalÃƒâ€Ã‚Â±. Proje iÃƒÆ’Ã‚Â§indeki yeni dosya ve klasÃƒÆ’Ã‚Â¶r adlarÃƒâ€Ã‚Â± mÃƒÆ’Ã‚Â¼mkÃƒÆ’Ã‚Â¼n olduÃƒâ€Ã…Â¸unca ASCII tutulacak.

ÃƒÆ’Ã¢â‚¬â€œrnek:

```powershell
Get-ChildItem -LiteralPath 'C:\Users\Asus\Documents\FATURA TAKÃƒâ€Ã‚Â°P PROGRAMI'
```

## Son Tamamlanan Faz

Son tamamlanan geliştirme fazı `v0.50 - Islem Gecmisi Filtre Sifirlama`.

Bu fazda audit log ekranina `Filtreleri sifirla` dugmesi eklendi; secili filtreler tek tikla varsayilan duruma donuyor ve kayitli preference dosyasi da buna gore guncelleniyor.

## SÃƒâ€Ã‚Â±radaki Ãƒâ€Ã‚Â°Ãƒâ€¦Ã…Â¸

Sıradaki mantıklı iş: audit log ekranina secili kayda hizli odaklanma veya son disa aktarilan dosyayi acma gibi kullanim rahatligi iyilestirmeleri eklemek.

Bu noktadan sonra yeni chatlerde ÃƒÆ’Ã‚Â¶nce `docs/03-devam-notlari.md` ve `REGRESYON.md` okunmalÃƒâ€Ã‚Â±; en gÃƒÆ’Ã‚Â¼ncel durum artÃƒâ€Ã‚Â±k bu dosyalarda tutuluyor.

## ÃƒÆ’Ã¢â‚¬Â¡alÃƒâ€Ã‚Â±Ãƒâ€¦Ã…Â¸ma KuralÃƒâ€Ã‚Â±

Her faz sonunda Ãƒâ€¦Ã…Â¸u dosyalar gÃƒÆ’Ã‚Â¼ncellenmeli:

- `docs/03-devam-notlari.md`
- `ROADMAP.md`
- `REGRESYON.md`

BÃƒÆ’Ã‚Â¶ylece yeni Codex chatine geÃƒÆ’Ã‚Â§ildiÃƒâ€Ã…Â¸inde nerede kalÃƒâ€Ã‚Â±ndÃƒâ€Ã‚Â±Ãƒâ€Ã…Â¸Ãƒâ€Ã‚Â± dosyalardan anlaÃƒâ€¦Ã…Â¸Ãƒâ€Ã‚Â±lÃƒâ€Ã‚Â±r.
