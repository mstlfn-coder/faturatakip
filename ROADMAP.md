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
| v0.2 | Tamamlandı | Fatura türleri yönetimi | Tür ekleme, düzenleme, aktif/pasif yapma çalışır |
| v0.3 | Tamamlandı | Abonelik yönetimi | Abonelik ekleme, düzenleme, aktif/pasif yapma ve filtreleme çalışır |
| v0.4 | Tamamlandı | Fatura kayıt altyapısı | PDF olmadan temel fatura kaydı yapılabilir |
| v0.5 | Tamamlandı | Fatura PDF evrakı ekleme | PDF uygulama klasörüne kopyalanır, açılır, eksikliği raporlanabilir |
| v0.6 | Tamamlandı | Fatura listesi ve filtreleme | Yıl, ay, tür, abonelik, ödeme durumu, PDF durumu ve fatura no ile filtreleme çalışır |
| v0.7 | Tamamlandı | Ödeme kayıt altyapısı | Faturaya ödeme kaydı eklenebilir ve ödeme durumu gösterilir |
| v0.8 | Tamamlandı | Ödeme evrakı PDF ekleme | Ödeme PDF'i kopyalanır, açılır ve eksiklik rapor altyapısına girer |
| v0.9 | Tamamlandı | Ana gösterge paneli | Aylık toplamlar, ödenmemişler, gecikmişler ve evrak eksikleri görünür |
| v0.10 | Tamamlandı | Ödenmemiş ve gecikmiş faturalar raporu | Ödenmemiş, gecikmiş ve yaklaşan ödemeler listelenir |
| v0.11 | Tamamlandı | Aylık fatura listesi | Seçilen ayın tüm faturaları ve özet toplamları alınır |
| v0.12 | Tamamlandı | Türe özgü aylık fatura listesi | Seçilen tür, yıl ve ay için toplamlar alınır |
| v0.13 | Tamamlandı | Aboneliğe özgü aylık fatura bilgisi | Tek aboneliğin aylık detayı ve önceki ay karşılaştırması görünür |
| v0.14 | Tamamlandı | Aboneliğe özgü yıllık fatura listesi | 12 aylık liste, toplamlar, en yüksek ve en düşük ay görünür |
| v0.15 | Tamamlandı | Türe özgü yıllık fatura listesi | Tür bazlı yıllık toplam ve abonelik dağılımı alınır |
| v0.16 | Tamamlandı | Evrak eksikliği ve dosya kontrol raporu | Eksik PDF, kayıp dosya ve aynı hash uyarıları görünür |
| v0.17 | Tamamlandı | Excel dışa aktarım | Ana listeler ve raporlar Excel'e aktarılır |
| v0.18 | Tamamlandı | Yazdırılabilir PDF raporlar | Kullanıcıdan alınacak Excel örneğine göre temel raporlar PDF olarak üretilebilir |
| v0.19 | Tamamlandı | Manuel güvenli yedekleme | Veritabanı ve evraklar ZIP yedek olarak alınır (UI + `--create-backup`) |
| v0.20 | Tamamlandı | Tutarlılık denetimi | Salt okunur veri tutarlılığı raporu çalışır (`Raporlar > Tutarlılık` + `--consistency-check`) |
| v0.21 | Tamamlandı | Güvenli yedek geri yükleme | ZIP yedek boş klasöre geri yüklenir (`--restore-backup` + `--restore-target`) |

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

## v0.2 Sonuç Notu

Tamamlananlar:

- `invoice_types` SQLite tablosu için `0002` migration eklendi.
- Elektrik, Su, Doğalgaz, Telefon, İnternet ve Diğer başlangıç türleri güvenli seed edildi.
- Fatura türü modeli ve repository katmanı eklendi.
- Fatura türü listeleme ekranı eklendi.
- Yeni fatura türü ekleme eklendi.
- Mevcut fatura türü düzenleme eklendi.
- Aktif/pasif yapma akışı eklendi.
- Varsayılan kullanım birimi alanı desteklendi.
- Repository davranışları için geçici veritabanıyla çalışan `--self-test` komutu eklendi.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- Uygulama kısa süreli başlatma testi

## v0.3 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- Abonelikler için veri modeli ve SQLite tablo migration'ı eklenecek.
- Abonelik ekleme yapılacak.
- Abonelik düzenleme yapılacak.
- Abonelik aktif/pasif yapma mantığı eklenecek.
- Abonelikler fatura türüne bağlanacak.
- Abonelik listesi filtreleme altyapısı hazırlanacak.

Bu fazda yapılmayacaklar:

- Fatura kaydı
- Ödeme kaydı
- PDF evrak ekleme
- Rapor ekranları
- Excel/PDF dışa aktarım
- Yedekleme

## v0.3 Sonuç Notu

Tamamlananlar:

- `subscriptions` SQLite tablosu için `0003` migration eklendi.
- Abonelik modeli ve repository katmanı eklendi.
- Abonelikler fatura türlerine foreign key ile bağlandı.
- Abonelik listeleme ekranı eklendi.
- Yeni abonelik ekleme eklendi.
- Mevcut abonelik düzenleme eklendi.
- Aktif/pasif yapma akışı eklendi.
- Tür, durum ve metin aramasıyla filtreleme altyapısı eklendi.
- Dashboard aktif abonelik sayısını göstermeye başladı.
- `--self-test` abonelik ekleme, düzenleme, fatura türüne bağlama ve pasife alma senaryolarını doğruluyor.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- Uygulama kısa süreli başlatma testi

## v0.4 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- Faturalar için veri modeli ve SQLite tablo migration'ı eklenecek.
- Fatura ekleme yapılacak.
- Fatura düzenleme yapılacak.
- Fatura aboneliğe bağlanacak.
- Dönem yılı/ayı, fatura tarihi, son ödeme tarihi, fatura no, tutar, kullanım miktarı, kullanım birimi ve açıklama alanları desteklenecek.
- Aynı abonelikte aynı fatura numarası tekrarına izin verilmeyecek.
- Negatif tutar ve negatif kullanım reddedilecek.
- Son ödeme tarihi fatura tarihinden önceyse uyarı verilecek.

Bu fazda yapılmayacaklar:

- Fatura PDF evrak ekleme
- Ödeme kaydı
- Ödeme evrakı
- Rapor ekranları
- Excel/PDF dışa aktarım
- Yedekleme

## v0.4 Sonuç Notu

Tamamlananlar:

- `invoices` SQLite tablosu için `0004` migration eklendi.
- Fatura modeli ve repository katmanı eklendi.
- Faturalar aboneliklere ve aboneliğin fatura türüne bağlandı.
- Fatura listeleme ekranı eklendi.
- Yeni fatura ekleme eklendi.
- Mevcut fatura düzenleme eklendi.
- Dönem yılı/ayı, fatura tarihi, son ödeme tarihi, fatura no, tutar, kullanım miktarı, kullanım birimi ve açıklama alanları desteklendi.
- Aynı abonelikte aynı fatura numarası engellendi.
- Negatif tutar ve negatif kullanım miktarı engellendi.
- Son ödeme tarihi fatura tarihinden önceyse kullanıcıya uyarı gösteriliyor.
- Dashboard toplam fatura sayısını göstermeye başladı.
- `--self-test` fatura ekleme, düzenleme, aboneliğe bağlama, tekrar fatura no, negatif tutar/kullanım ve tarih uyarısı senaryolarını doğruluyor.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- Uygulama kısa süreli başlatma testi

## v0.5 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- Faturaya PDF ekleme alanı ve akışı eklenecek.
- Seçilen PDF `attachments/invoices/yyyy/MM` altında güvenli dosya adıyla kopyalanacak.
- PDF yolu, orijinal dosya adı ve dosya hash bilgisi veritabanında saklanacak.
- Fatura PDF'i açma akışı eklenecek.
- Dosya varlık kontrolü yapılacak.
- Fatura PDF eksikliği raporu için altyapı hazırlanacak.

Bu fazda yapılmayacaklar:

- Ödeme kaydı
- Ödeme evrakı
- Rapor ekranları
- Excel/PDF dışa aktarım
- Yedekleme

## v0.5 Sonuç Notu

Tamamlananlar:

- `invoices` tablosuna PDF yolu, orijinal dosya adı, SHA-256 hash ve eklenme zamanı metadata alanları eklendi.
- Seçilen PDF dosyası geçerli PDF imzasıyla doğrulanıyor.
- PDF dosyası `attachments/invoices/yyyy/MM` altında güvenli ASCII dosya adıyla kopyalanıyor.
- Fatura ekranına PDF seçme ve kayıtlı PDF'i açma akışı eklendi.
- Fatura listesine PDF durumu ve üst özetlere PDF eksik sayısı eklendi.
- `--self-test` PDF kopyalama, hash saklama, dosya varlığı, kayıp dosya algısı ve PDF olmayan dosya reddi senaryolarını doğruluyor.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- Uygulama kısa süreli başlatma testi

## v0.6 Sonuç Notu

Tamamlananlar:

- Fatura listesi için yıl, ay, fatura türü, abonelik, ödeme durumu ve PDF durumu filtreleri eklendi.
- Serbest metin araması fatura no, tür, abonelik, kurum, açıklama ve PDF orijinal dosya adını kapsayacak şekilde genişletildi.
- Filtreleri temizleme düğmesi eklendi.
- Filtreleme mantığı test edilebilir `InvoiceFilter` katmanına alındı.
- `--self-test` yıl, ay, tür, abonelik, ödeme durumu, gecikmiş, PDF var/eksik ve metin arama filtrelerini doğruluyor.

Doğrulama:

- `dotnet build FaturaTakip.sln`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`
- `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- Uygulama kısa süreli başlatma testi

## v0.7 Sonuç Notu

Tamamlananlar:

- `payments` SQLite tablosu için `0006` migration eklendi.
- Ödeme modeli, ödeme girişi ve repository katmanı eklendi.
- Faturaya ödeme tarihi, tutarı ve açıklamasıyla ödeme kaydı ekleme akışı eklendi.
- Kısmi ödeme, tam ödeme, ödenen tutar ve kalan tutar bilgileri fatura listesi ve formunda gösteriliyor.
- Kalan tutarı aşan ödeme, negatif ödeme ve olmayan faturaya ödeme ekleme engellendi.
- Fatura tutarı düzenlenirse ödeme durumu ödenen toplam üzerinden yeniden hesaplanıyor.
- Fatura ekranına ödeme kaydı formu ve ödeme geçmişi listesi eklendi.
- Ödeme PDF evrakı, rapor, dışa aktarım ve yedekleme kapsam dışı bırakıldı.
- `--self-test` kısmi ödeme, tam ödeme, kalan aşımı, durum yeniden hesaplama, negatif ödeme ve olmayan fatura senaryolarını doğruluyor.

Doğrulama:

- `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`

## v0.8 Sonuç Notu

Tamamlananlar:

- `payments` tablosuna ödeme PDF yolu, orijinal dosya adı, SHA-256 hash ve eklenme zamanı metadata alanları eklendi.
- Ödeme PDF dosyası geçerli PDF imzasıyla doğrulanıyor.
- PDF dosyası `attachments/payments/yyyy/MM` altında güvenli ASCII dosya adıyla kopyalanıyor.
- Ödeme kayıt modeline PDF durumu eklendi.
- Fatura ekranındaki ödeme geçmişinde ödeme PDF durumu gösteriliyor.
- Seçili ödeme kaydına PDF seçme ve kayıtlı ödeme PDF'ini açma akışı eklendi.
- Kayıp ödeme PDF dosyası algılama altyapısı eklendi.
- `--self-test` ödeme PDF kopyalama, hash saklama, dosya varlığı, kayıp dosya algısı ve PDF olmayan dosya reddi senaryolarını doğruluyor.

Doğrulama:

- `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`

## v0.9 Sonuç Notu

Tamamlananlar:

- Ana gösterge paneli aylık fatura toplamı ve aylık ödeme toplamını göstermeye başladı.
- Ödenmemiş fatura sayısı ve kalan ödeme toplamı gösteriliyor.
- Gecikmiş fatura sayısı ve gecikmiş kalan ödeme toplamı gösteriliyor.
- Fatura PDF eksik sayısı ve ödeme PDF eksik sayısı gösteriliyor.
- Temel kayıt sayıları dashboard içinde korunuyor: fatura türü, aktif tür, aktif abonelik ve toplam fatura.
- Dashboard hesaplama mantığı test edilebilir `DashboardSummaryCalculator` katmanına alındı.
- `--self-test` aylık toplam, ödeme toplamı, ödenmemiş, gecikmiş, fatura PDF eksik ve ödeme PDF eksik hesaplarını doğruluyor.

Doğrulama:

- `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
- `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check`

## v0.10 Ayrıntılı Kapsam

Yalnızca şu işler yapılacak:

- Ödenmemiş ve gecikmiş faturalar için rapor ekranı veya rapor bölümü hazırlanacak.
- Ödeme durumu `unpaid` olan faturalar aksiyon listesine alınacak.
- Gecikmiş faturalar, son ödeme tarihi bugünden önce olan ödenmemiş faturalar olarak gösterilecek.
- Yaklaşan ödemeler, son ödeme tarihi bugün ile sonraki 7 gün arasında olan ödenmemiş faturalar olarak gösterilecek.
- Liste satırlarında fatura türü, abonelik, kurum, dönem, fatura no, son ödeme tarihi, tutar, ödenen tutar, kalan tutar ve PDF durumu görünecek.
- Rapor üstünde ödenmemiş, gecikmiş ve yaklaşan ödeme sayı/toplam özetleri görünecek.
- Hesaplama veya filtreleme mantığı self-test kapsamına alınacak.

Bu fazda yapılmayacaklar:

- Excel dışa aktarım
- Yazdırılabilir PDF rapor
- Fatura PDF rapor tasarımı
- Yedekleme

## Gelecek Faz Notları

- Fatura PDF raporları fazına geçildiğinde önce kullanıcıdan Excel'de hazırlanmış örnek rapor istenecek. PDF çıktının kolonları, toplamları, başlıkları ve sayfa düzeni bu örnek üzerinden şekillenecek.

## Her Faz Sonu Kontrolü

Her faz bittiğinde:

1. Uygulama çalıştırılmalı.
2. İlgili regresyon maddeleri `REGRESYON.md` içinde işlenmeli.
3. `docs/03-devam-notlari.md` güncellenmeli.
4. Bu roadmapte ilgili fazın durumu güncellenmeli.
5. `git status --short --branch` çıktısı kontrol edilmeli.
