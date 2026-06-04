# Kurum Fatura Takip Programı

Bu depo, kurum aboneliklerine ait elektrik, su, doğalgaz, telefon, internet ve benzeri faturaların takip edilmesi için geliştirilecek masaüstü uygulamanın çalışma alanıdır.

## Güncel Durum

- Proje klasörü: `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI`
- Git deposu hazır.
- `v0.1` - `v0.18` arası temel uygulama, raporlar ve Excel/PDF export altyapısı tamamlandı.
- `v0.19` manuel güvenli yedekleme eklendi (`--create-backup` + UI).
- `v0.20` tutarlılık denetimi eklendi (`Raporlar > Tutarlılık` + `--consistency-check`).
- `v0.21` güvenli yedek geri yükleme eklendi (`--restore-backup` + `--restore-target`).
- `v0.22` backup ekranına UI restore akışı eklendi.
- `v0.23` rapor export şablon hizalama ve `Yıllık Liste` sekmesi tamamlandı.
- `v0.24` derleme uyarıları temizlendi.
- `v0.25` - `v0.35` arası PDF rapor yerleşimi örnek matbu düzene yaklaştırıldı ve görsel QA yapıldı.
- `v0.36` restore için "boş olmayan hedef reddedilir" negatif smoke testi self-test kapsamına alındı.
- `v0.37` audit log temeli eklendi; kritik veri işlemleri artık `audit_logs` tablosuna kaydediliyor.
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
- Solution dosyası: `FaturaTakip.sln`
- Uygulama projesi: `src/FaturaTakip.App/FaturaTakip.App.csproj`
- Geliştirme planı proje içine alındı: `docs/01-gelistirme-plani.md`
- Roadmap oluşturuldu: `ROADMAP.md`
- Regresyon kontrol dosyası oluşturuldu: `REGRESYON.md`
- Yeni Codex chatlerinde devam edebilmek için kılavuz ve devam notları eklendi.

## Yeni Codex Chatinde İlk Okunacak Dosyalar

Yeni bir Codex sohbetinde bu projeye devam ederken önce şu dosyalar okunmalı:

1. `README.md`
2. `docs/00-codex-devam-kilavuzu.md`
3. `docs/03-devam-notlari.md`
4. `ROADMAP.md`
5. `REGRESYON.md`
6. `docs/01-gelistirme-plani.md`

Ardından terminalde durum kontrolü yapılmalı:

```powershell
git status --short --branch
```

## Yol ve Dosya Adı İlkesi

Proje klasörünün adı Türkçe karakter içerdiği için PowerShell komutlarında gerekirse `-LiteralPath` kullanılmalı. Proje içindeki yeni dosya ve klasör adları mümkün olduğunca ASCII tutulacak.

Örnek:

```powershell
Get-ChildItem -LiteralPath 'C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI'
```

## Son Tamamlanan Faz

Son tamamlanan geliştirme fazı `v0.47 - Islem Gecmisi Disa Aktarma`.

Bu fazda audit log detayina `TXT disa aktar` ve `JSON disa aktar` aksiyonlari eklendi; secili kaydin detaylari `exports` klasorune okunur ya da yapisal cikti olarak alinabiliyor.

## Sıradaki İş

Sıradaki mantıklı iş: audit log ciktilarina dogrudan ac/kopyala klasor aksiyonu ya da filtre ayarlarini kaydetme gibi kullanim rahatligi iyilestirmeleri eklemek.

Bu noktadan sonra yeni chatlerde önce `docs/03-devam-notlari.md` ve `REGRESYON.md` okunmalı; en güncel durum artık bu dosyalarda tutuluyor.

## Çalışma Kuralı

Her faz sonunda şu dosyalar güncellenmeli:

- `docs/03-devam-notlari.md`
- `ROADMAP.md`
- `REGRESYON.md`

Böylece yeni Codex chatine geçildiğinde nerede kalındığı dosyalardan anlaşılır.
