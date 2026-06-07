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
| v0.22 | Tamamlandi | UI restore (backup) | Backup ekranindan zip secilip bos klasore restore edilebilir |
| v0.23 | Tamamlandi | Rapor export şablon hizalama + Yıllık Liste | Excel/PDF rapor şablonları plana yaklaştırılır, `Yıllık Liste` sekmesi çalışır |
| v0.24 | Tamamlandi | Derleme uyarı temizliği | CS8123 uyarıları kaldırılır, build temiz kalır |
| v0.25 | Tamamlandi | PDF matbu stil başlangıcı | PDF başlık/tablo/toplam yapısı örneğe yaklaşır |
| v0.26 | Tamamlandi | PDF footer sadeleştirme | Varsayılan footer kapatılır |
| v0.27 | Tamamlandi | QuestPDF bağımlılık sabitleme | Build uyarısız hale gelir |
| v0.28 | Tamamlandi | PDF açıklama cümleleri | `Açıklama :` satırı rapor diline uygun üretilir |
| v0.29 | Tamamlandi | PDF başlık sadeliği | Başlık sade kalır, bağlam açıklama satırına taşınır |
| v0.30 | Tamamlandi | PDF tablo başlık sıkılığı | Tablo header satırı kompaktlaşır |
| v0.31 | Tamamlandi | PDF tablo gövde sıkılığı | Veri satırları kompaktlaşır |
| v0.32 | Tamamlandi | PDF tablo yazı boyutu dengesi | Header/gövde/toplam satırı görsel olarak dengelenir |
| v0.33 | Tamamlandi | PDF açıklama-tablo aralığı | Üst bölüm ile tablo arası sıkılaşır |
| v0.34 | Tamamlandi | PDF üst başlık dengesi | Kurum başlığı, rapor başlığı ve tarih hizası iyileşir |
| v0.35 | Tamamlandi | PDF görsel QA | Self-test ve ek örnek PDF'ler gözle kontrol edilir |
| v0.36 | Tamamlandi | Restore negatif smoke | Boş olmayan hedefe restore reddi self-test ile doğrulanır |
| v0.37 | Tamamlandi | Audit log temeli | Kritik abonelik/fatura/ödeme işlemleri `audit_logs` tablosuna kaydedilir ve self-test ile doğrulanır |

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

## Son Guncelleme (2026-06-02)

- v0.38 tamamlandi: Islem Gecmisi raporu/gridi ve audit log export destegi eklendi.
- v0.39 siradaki: Islem Gecmisi icin tarih / islem turu filtreleri.


## Son Guncelleme (2026-06-02 / v0.39)

- v0.39 tamamlandi: Islem Gecmisi icin islem turu + tarih filtreleri eklendi.
- Siradaki mantikli faz: kullanici / varlik bazli ek filtre veya metin arama.


## Son Guncelleme (2026-06-02 / v0.40)

- v0.40 tamamlandi: Islem Gecmisi icin varlik, kullanici ve metin arama filtreleri eklendi.
- Siradaki mantikli faz: audit log kaydi secilince eski/yeni deger detay paneli gostermek.


## Son Guncelleme (2026-06-02 / v0.41)

- v0.41 tamamlandi: Islem Gecmisi icin secili kayit detay paneli eklendi.
- Siradaki mantikli faz: eski/yeni degerler icin daha okunur diff gorunumu.


## Son Guncelleme (2026-06-02 / v0.42)

- v0.42 tamamlandi: Islem Gecmisi detayina alan bazli fark tablosu eklendi.
- Siradaki mantikli faz: diff tablosunda sadece degisen alanlar veya renkli rozetler.


## Son Guncelleme (2026-06-02 / v0.43)

- v0.43 tamamlandi: audit log diff tablosuna sadece degisen alanlar filtresi eklendi.
- Siradaki mantikli faz: diff tablosunda renkli durum rozetleri veya vurgulu satirlar.


## Son Guncelleme (2026-06-04 / v0.44)

- v0.44 tamamlandi: audit log diff durumlari renkli rozetlerle gosterilmeye baslandi.
- Siradaki mantikli faz: audit log detayinda kopyalama/disa aktarma aksiyonlari.


## Son Guncelleme (2026-06-04 / v0.45)

- v0.45 tamamlandi: audit log detay paneline eski ve yeni deger icin kopyalama dugmeleri eklendi.
- Siradaki mantikli faz: tum diff'i kopyala veya detay txt/json disa aktarma aksiyonlari.


## Son Guncelleme (2026-06-04 / v0.46)

- v0.46 tamamlandi: audit log detayinda gorunen diff tablosunu topluca kopyalama aksiyonu eklendi.
- Siradaki mantikli faz: detay txt/json disa aktarma aksiyonlari.


## Son Guncelleme (2026-06-04 / v0.47)

- v0.47 tamamlandi: audit log detayina TXT ve JSON disa aktarma aksiyonlari eklendi.
- Siradaki mantikli faz: audit log ciktilarina ac/kopyala klasor gibi kullanim rahatligi aksiyonlari.


## Son Guncelleme (2026-06-04 / v0.48)

- v0.48 tamamlandi: audit log araclarina exports klasorunu tek tikla acma aksiyonu eklendi.
- Siradaki mantikli faz: audit log filtre tercihlerini hatirlama gibi kullanim rahatligi adimlari.


## Son Guncelleme (2026-06-05 / v0.49)

- v0.49 tamamlandi: audit log filtre tercihleri yeniden acilista hatirlanir hale getirildi.
- Siradaki mantikli faz: filtreleri tek tikla sifirlama veya secili kayda hizli odaklanma aksiyonlari.


## Son Guncelleme (2026-06-05 / v0.50)

- v0.50 tamamlandi: audit log ekranina filtreleri tek tikla sifirlama dugmesi eklendi.
- Siradaki mantikli faz: secili kayda hizli odaklanma veya son disa aktarilan dosyayi acma aksiyonlari.


## Son Guncelleme (2026-06-05 / v0.51)

- v0.51 tamamlandi: audit log araclarina son disa aktarilan dosyayi acma aksiyonu eklendi.
- Siradaki mantikli faz: secili kayda hizli odaklanma veya export geÃ§misi rahatliklari.



## Son Guncelleme (2026-06-05 / v0.52)

- v0.52 tamamlandi: audit log ekranina secili kaydi gorunur alana getiren hizli odak aksiyonu eklendi.
- Siradaki mantikli faz: export gecmisi rahatliklari veya son kullanilan kayda donus yardimcilari.



## Son Guncelleme (2026-06-05 / v0.53)

- v0.53 tamamlandi: audit log araclarina son export gecmisi listesi eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya export gecmisi temizleme rahatliklari.



## Son Guncelleme (2026-06-05 / v0.54)

- v0.54 tamamlandi: audit log export gecmisi icin liste yenileme rahatligi eklendi.
- Siradaki mantikli faz: export gecmisi temizleme veya son kullanilan kayda donus yardimcilari.

## Son Guncelleme (2026-06-05 / v0.55)

- v0.55 tamamlandi: audit log export gecmisi icin liste temizleme rahatligi eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya export gecmisi icin secmeli temizleme yardimcilari.

## Son Guncelleme (2026-06-05 / v0.56)

- v0.56 tamamlandi: audit log export gecmisinde secili dosyayi silme rahatligi eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya son N kaydi koruyup digerlerini temizleme yardimcilari.

## Son Guncelleme (2026-06-05 / v0.57)

- v0.57 tamamlandi: audit log export gecmisinde son 5 kaydi koruyup eskileri temizleme rahatligi eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari.

## Son Guncelleme (2026-06-05 / v0.58)

- v0.58 tamamlandi: audit log export gecmisi listesine dosya tipi ve zaman bilgisini daha okunur gosteren etiketli gorunum eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari.

## Son Guncelleme (2026-06-05 / v0.59)

- v0.59 tamamlandi: audit log export gecmisine Tum / JSON / TXT hizli filtre secimi eklendi.
- Siradaki mantikli faz: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari.

## Son Guncelleme (2026-06-05 / v0.60)

- v0.60 tamamlandi: audit log export gecmisinde son kullanilan dosyayi gorsel olarak isaretleyen yardim eklendi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.

## Son Guncelleme (2026-06-05 / v0.61)

- v0.61 tamamlandi: audit log export gecmisinde son kullanilan dosya gorunuyorsa otomatik secili kalma davranisi eklendi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.

## Son Guncelleme (2026-06-05 / v0.62)

- v0.62 tamamlandi: audit log export gecmisinde secili dosyanin tam yolunu panoya alma rahatligi eklendi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.

## Son Guncelleme (2026-06-05 / v0.63)

- v0.63 tamamlandi: audit log export gecmisinde secili dosyayi Windows Gezgini icinde gosterme rahatligi eklendi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.

## Son Guncelleme (2026-06-05 / v0.64)

- v0.64 tamamlandi: audit log export secim araçlari, seçim yoksa devre disi kalan daha anlatir bir duruma getirildi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.

## Son Guncelleme (2026-06-05 / v0.65)

- v0.65 tamamlandi: audit log export gecmisinde gorunur listedeki son kullanilan ogeyi tek tikla yeniden secme yardimi eklendi.
- Siradaki mantikli faz: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi.



## Son Guncelleme (2026-06-06 / v0.66)

- v0.66 tamamlandi: Faturalar ekranina secili kayittan sonraki ayi taslak olarak hazirlayan Sonrakini Hazirla aksiyonu eklendi.
- Taslak akisinda abonelik, tutar, kullanim, kullanim birimi ve aciklama korunuyor; fatura no ile PDF/odeme taraflari bos baslatiliyor.
- Siradaki mantikli faz: v0.66 cekirdek is akisini odeme girisi rahatliklariyla derinlestirmek, sonra yedekleme UX fazina gecmek.


## Son Guncelleme (2026-06-06 / v0.67)

- v0.67 tamamlandi: Odeme formuna Kalani Doldur ve Son Odemeden Doldur yardimcilari eklendi.
- Bu akis kalan tutari geri yaziyor ve varsa en guncel odeme aciklamasini yeni taslaga tasiyor.
- Siradaki mantikli faz: odeme girisinde daha ileri rahatliklarin ardindan yedekleme UX fazina gecmek.


## Son Guncelleme (2026-06-06 / v0.68)

- v0.68 tamamlandi: Odeme formuna Secili Odemeden Doldur yardimi eklendi.
- Bu akis secili odemenin aciklamasini ve tutarini, kalan tutari asmadan yeni taslaga tasiyor.
- Siradaki mantikli faz: odeme girisi rahatliklarini tamamlayip yedekleme UX fazina gecmek.

## Son Guncelleme (2026-06-06 / v0.69)

- v0.69 tamamlandi: Yedekleme ekranina son 5 zip yedegi gosteren liste eklendi.
- Liste uzerinden secili yedek restore zip alanina tasinabiliyor ve dogrudan acilabiliyor.
- Siradaki mantikli faz: yedekleme UX tarafinda secili yedegi klasorde gosterme veya restore hedefi secimini kolaylastirma.

## Son Guncelleme (2026-06-06 / v0.70)

- v0.70 tamamlandi: Backup listesindeki secili zip dosyasi Windows Gezgini icinde dogrudan gosterilebilir hale geldi.
- Boylece kullanici sadece zipi acmakla kalmadan dosyanin klasor konumuna da tek tikla inebiliyor.
- Siradaki mantikli faz: restore hedef klasoru secimini daha rahat hale getirmek.

## Son Guncelleme (2026-06-06 / v0.71)

- v0.71 tamamlandi: Restore hedef klasoru icin dosya yolu yazmak yerine klasor secici eklendi.
- Mevcut yol varsa ayni klasorde, yoksa uygulama kokunde acilarak kullaniciyi daha az ugrastiriyor.
- Siradaki mantikli faz: restore hedefi icin bos klasor uygunlugu konusunda daha gorunur on kontrol eklemek.

## Son Guncelleme (2026-06-06 / v0.72)

- v0.72 tamamlandi: Restore hedefi icin canli bos/uygunluk dogrulamasi eklendi.
- Secilen ya da yazilan klasor icin "olusturulacak / uygun / kullanilamaz / gecersiz" mesaji aninda gosteriliyor.
- Siradaki mantikli faz: restore hedefi uygun degilse kullaniciyi bos klasor olusturmaya yonlendiren daha aktif bir rahatlik eklemek.

## Son Guncelleme (2026-06-06 / v0.73)

- v0.73 tamamlandi: Uygun olmayan restore hedefinde tek tikla bos klasor olusturma rahatligi eklendi.
- Yeni klasor otomatik uretilip hedef alanina yaziliyor; ad cakismasi varsa benzersiz isim uretiliyor.
- Siradaki mantikli faz: restore akisini ozetleyen son bir onizleme/ozet kutusu eklemek.

## Son Guncelleme (2026-06-06 / v0.74)

- v0.74 tamamlandi: Restore bolumune zip, hedef ve hazirlik durumunu tek yerde gosteren onizleme kutusu eklendi.
- Boylece kullanici geri yukleme oncesi eksik adimi tek bakista gorebiliyor.
- Siradaki mantikli faz: backup UX tarafini yeterince olgun kabul edip yeni bir urun fazina gecmek.

## Son Guncelleme (2026-06-06 / v0.75)

- v0.75 tamamlandi: Gosterge panelindeki odenmemis ve gecikmis kartlarina `Rapora Git` dugmeleri eklendi.
- Bu kisa yol kullaniciyi dogrudan Raporlar ekraninda ilgili sekmeye goturuyor; gunluk takip akisinda daha hizli aksiyon alinabiliyor.
- Siradaki mantikli faz: dashboard tarafinda eksik evrak veya aylik tahsilat icin de benzer hizli gecisler eklemek.

## Son Guncelleme (2026-06-06 / v0.76)

- v0.76 tamamlandi: Gosterge panelindeki bu ay fatura, bu ay odeme, fatura PDF eksik ve odeme PDF eksik kartlarina hizli gecis dugmeleri eklendi.
- Aylik hareket kartlari dogrudan Aylik Liste sekmesine, evrak eksigi kartlari ise Evrak Kontrol sekmesine tasiniyor.
- Siradaki mantikli faz: dashboard tarafinda aktif abonelik veya toplam fatura gibi kartlardan liste ekranlarina hizli gecis eklemek.

## Son Guncelleme (2026-06-07 / v0.77)

- v0.77 tamamlandi: Gosterge panelindeki fatura turu, aktif tur, aktif abonelik ve toplam fatura kartlarina hizli gecis dugmeleri eklendi.
- Bu kisayollar dogrudan Fatura Turleri, Abonelikler ve Faturalar ekranlarina gidiyor; dashboard ana yonlendirme paneli gibi calisiyor.
- Siradaki mantikli faz: dashboard kisayollarini yeterince olgun kabul edip liste ekranlarinda toplu is akisi hizlandirmaya geri donmek.

## Son Guncelleme (2026-06-07 / v0.78)

- v0.78 tamamlandi: Faturalar ekranina Bu Ay, Odenmemis, Gecikmis ve PDF Eksik hazir filtre dugmeleri eklendi.
- Bu kisayollar filtreleri tek tikla kuruyor, sonucu sayisal olarak gosteriyor ve varsa ilk kayda otomatik odaklaniyor.
- Siradaki mantikli faz: fatura hizli filtrelerini disa aktarma veya toplu PDF kontrol akisiyla daha da guclendirmek.
