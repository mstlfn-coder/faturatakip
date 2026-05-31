# Kurum Fatura Takip Programı

Bu depo, kurum aboneliklerine ait elektrik, su, doğalgaz, telefon, internet ve benzeri faturaların takip edilmesi için geliştirilecek masaüstü uygulamanın çalışma alanıdır.

## Güncel Durum

- Proje klasörü: `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI`
- Git deposu hazır.
- `v0.1` WPF + SQLite proje iskeleti oluşturuldu.
- `v0.2` fatura türleri yönetimi eklendi.
- `v0.3` abonelik yönetimi eklendi.
- `v0.4` fatura kayıt altyapısı eklendi.
- `v0.5` fatura PDF evrakı ekleme altyapısı eklendi.
- `v0.6` fatura listesi ve filtreleme geliştirildi.
- `v0.7` ödeme kayıt altyapısı eklendi.
- `v0.8` ödeme PDF evrakı ekleme altyapısı eklendi.
- `v0.9` ana gösterge paneli geliştirildi.
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

Son tamamlanan geliştirme fazı `v0.10 - Ödenmemiş ve Gecikmiş Faturalar Raporu`.

Bu fazda raporlar ekranı eklendi: ödenmemiş, gecikmiş ve yaklaşan (7 gün) ödemeler listelenir. Hesaplama mantığı test edilebilir `ActionableInvoiceReportCalculator` katmanındadır.

## Sıradaki İş

Sıradaki geliştirme fazı `v0.11 - Aylık Fatura Listesi`.

Aktif çalışma branch'i: `codex/v0.11-aylik-fatura-listesi`.

Bu fazda seçilen ayın tüm faturaları ve özet toplamları listelenecek. Excel/PDF dışa aktarım, yazdırılabilir PDF rapor üretimi ve yedekleme yapılmayacak.

## Çalışma Kuralı

Her faz sonunda şu dosyalar güncellenmeli:

- `docs/03-devam-notlari.md`
- `ROADMAP.md`
- `REGRESYON.md`

Böylece yeni Codex chatine geçildiğinde nerede kalındığı dosyalardan anlaşılır.
