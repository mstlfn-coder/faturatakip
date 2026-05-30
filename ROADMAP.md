# Roadmap

Bu roadmap, `docs/01-gelistirme-plani.md` içindeki kapsamlı planın uygulanabilir geliştirme sırasına dönüştürülmüş halidir.

## Durum Etiketleri

- `Tamamlandı`: Faz bitti ve doğrulandı.
- `Sıradaki`: Bir sonraki uygulanacak faz.
- `Beklemede`: Henüz başlanmadı.
- `Ertelendi`: Bilerek sonraki aşamaya bırakıldı.

## Fazlar

| Faz | Durum | Kapsam | Çıkış Kriteri |
|---|---|---|---|
| v0.0 | Tamamlandı | Planın projeye alınması, roadmap, regresyon ve devam dokümantasyonu | Bu dosyalar oluşturuldu ve proje içinde yer aldı |
| v0.1 | Tamamlandı | WPF proje iskeleti, SQLite bağlantısı, ana klasör yapısı, başlangıç migration sınıfı, boş dashboard | Uygulama açılır, klasörler hazırlanır, veritabanı dosyası oluşturulur |
| v0.2 | Sıradaki | Fatura türleri yönetimi | Tür ekleme, düzenleme, aktif/pasif yapma çalışır |
| v0.3 | Beklemede | Abonelik yönetimi | Abonelik ekleme, düzenleme, aktif/pasif yapma ve filtreleme çalışır |
| v0.4 | Beklemede | Fatura kayıt altyapısı | PDF olmadan temel fatura kaydı yapılabilir |
| v0.5 | Beklemede | Fatura PDF evrakı ekleme | PDF uygulama klasörüne kopyalanır, açılır, eksikliği raporlanabilir |
| v0.6 | Beklemede | Fatura listesi ve filtreleme | Yıl, ay, tür, abonelik, ödeme durumu, PDF durumu ve fatura no ile filtreleme çalışır |
| v0.7 | Beklemede | Ödeme kayıt altyapısı | Faturaya ödeme kaydı eklenebilir ve ödeme durumu gösterilir |
| v0.8 | Beklemede | Ödeme evrakı PDF ekleme | Ödeme PDF'i kopyalanır, açılır ve eksiklik rapor altyapısına girer |
| v0.9 | Beklemede | Ana gösterge paneli | Aylık toplamlar, ödenmemişler, gecikmişler ve evrak eksikleri görünür |
| v0.10 | Beklemede | Ödenmemiş ve gecikmiş faturalar raporu | Ödenmemiş, gecikmiş ve yaklaşan ödemeler listelenir |
| v0.11 | Beklemede | Aylık fatura listesi | Seçilen ayın tüm faturaları ve özet toplamları alınır |
| v0.12 | Beklemede | Türe özgü aylık fatura listesi | Seçilen tür, yıl ve ay için toplamlar alınır |
| v0.13 | Beklemede | Aboneliğe özgü aylık fatura bilgisi | Tek aboneliğin aylık detayı ve önceki ay karşılaştırması görünür |
| v0.14 | Beklemede | Aboneliğe özgü yıllık fatura listesi | 12 aylık liste, toplamlar, en yüksek ve en düşük ay görünür |
| v0.15 | Beklemede | Türe özgü yıllık fatura listesi | Tür bazlı yıllık toplam ve abonelik dağılımı alınır |
| v0.16 | Beklemede | Evrak eksikliği ve dosya kontrol raporu | Eksik PDF, kayıp dosya ve aynı hash uyarıları görünür |
| v0.17 | Beklemede | Excel dışa aktarım | Ana listeler ve raporlar Excel'e aktarılır |
| v0.18 | Beklemede | Yazdırılabilir PDF raporlar | Temel raporlar PDF olarak üretilebilir |
| v0.19 | Beklemede | Manuel güvenli yedekleme | Veritabanı ve evraklar ZIP yedek olarak alınır |
| v0.20 | Beklemede | Tutarlılık denetimi | Salt okunur veri tutarlılığı raporu çalışır |

## v0.1 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- C# WPF masaüstü uygulama projesi oluşturulacak.
- SQLite kullanılacak.
- `database`, `attachments`, `backups`, `logs`, `exports` klasörleri için altyapı kurulacak.
- Veritabanı dosyası `database/fatura_takip.db` olacak.
- Başlangıç migration mantığı hazırlanacak.
- Ana ekran sade bir dashboard taslağı olarak açılacak.
- Gerçek veri girişi yapılmayacak.

Bu fazda yapılmayacaklar:

- Fatura türü yönetimi
- Abonelik yönetimi
- Fatura kaydı
- Ödeme kaydı
- PDF evrak ekleme
- Rapor ekranları
- Dışa aktarım
- Yedekleme

## v0.1 Sonuç Notu

Tamamlananlar:

- `FaturaTakip.sln` oluşturuldu.
- `src/FaturaTakip.App` WPF projesi oluşturuldu.
- `Microsoft.Data.Sqlite` paketi eklendi.
- `database`, `attachments/invoices`, `attachments/payments`, `backups`, `logs`, `exports` klasörleri için altyapı hazırlandı.
- Runtime veri dosyaları için `.gitignore` kuralları eklendi.
- `database/fatura_takip.db` başlangıçta oluşturuluyor.
- `schema_migrations` ve `app_metadata` tablolarını hazırlayan başlangıç migration mantığı eklendi.
- Boş dashboard ekranı bağlandı.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- Uygulama kısa süreli başlatma testi

## v0.2 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- Fatura türleri için veri modeli ve SQLite tablo migration'ı eklenecek.
- Elektrik, Su, Doğalgaz, Telefon, İnternet, Diğer başlangıç türleri güvenli şekilde seed edilecek.
- Fatura türü listeleme ekranı hazırlanacak.
- Yeni fatura türü ekleme yapılacak.
- Mevcut fatura türü düzenleme yapılacak.
- Aktif/pasif yapma mantığı eklenecek.
- Varsayılan kullanım birimi alanı desteklenecek.

Bu fazda yapılmayacaklar:

- Abonelik yönetimi
- Fatura kaydı
- Ödeme kaydı
- PDF evrak ekleme
- Rapor ekranları
- Excel/PDF dışa aktarım
- Yedekleme

## Her Faz Sonu Kontrolü

Her faz bittiğinde:

1. Uygulama çalıştırılmalı.
2. İlgili regresyon maddeleri `REGRESYON.md` içinde işlenmeli.
3. `docs/03-devam-notlari.md` güncellenmeli.
4. Bu roadmapte ilgili fazın durumu güncellenmeli.
5. `git status --short --branch` çıktısı kontrol edilmeli.
