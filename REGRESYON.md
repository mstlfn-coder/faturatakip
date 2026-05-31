# Regresyon Kontrol Listesi

Bu dosya, her geliştirme fazından sonra mevcut çalışan davranışların bozulmadığını kontrol etmek için tutulur. Yeni Codex chatlerinde önce bu dosya okunmalı, sonra ilgili fazın testleri çalıştırılmalıdır.

## Kullanım

Her faz sonunda:

1. Genel kontroller yapılır.
2. Tamamlanmış tüm önceki fazların kritik kontrolleri tekrar edilir.
3. Yeni fazın kontrolleri eklenir ve sonuç kaydı düşülür.
4. Başarısız madde varsa bir sonraki faza geçilmez.

Sonuç etiketleri:

- `OK`: Sorun yok.
- `FAIL`: Sorun var.
- `N/A`: Bu faz için uygulanabilir değil.
- `TODO`: Henüz test edilmedi.

## Genel Kontroller

| Kontrol | Durum | Not |
|---|---|---|
| Proje klasörü doğru yerde açılıyor | OK | `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI` |
| Git durumu kontrol edildi | OK | `codex/v0.15-ture-ozgu-yillik-fatura-listesi` branch'i üzerinde çalışılıyor |
| Markdown dokümantasyonu UTF-8 olarak okunuyor | OK | Plan dosyası UTF-8 korunarak kopyalandı |
| Yeni dosya adları yol sorunu azaltmak için ASCII tutuluyor | OK | Kök klasör Türkçe, proje içi doküman adları ASCII |
| `README.md` yeni chat başlangıcını açıklıyor | OK | Oluşturuldu |
| Roadmap mevcut | OK | `ROADMAP.md` |
| Regresyon dosyası mevcut | OK | `REGRESYON.md` |

## v0.1 - Proje İskeleti ve Veritabanı

| Kontrol | Durum | Not |
|---|---|---|
| WPF uygulaması derleniyor | OK | `dotnet build FaturaTakip.sln` başarılı, 0 hata |
| Uygulama açılıyor | OK | Kısa başlatma testi başarılı, uygulama erken kapanmadı |
| `database` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor, runtime DB ignore ediliyor |
| `attachments` klasörü oluşturuluyor | OK | `attachments/invoices` ve `attachments/payments` hazır |
| `backups` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `logs` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `exports` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `database/fatura_takip.db` oluşturuluyor | OK | Health-check sonrası oluştu ve git tarafından ignore ediliyor |
| Migration başlangıç mantığı tekrar çalıştırıldığında hata vermiyor | OK | Health-check iki kez çalıştırıldı |
| Bu fazda veri giriş ekranı eklenmedi | OK | Sadece boş dashboard ve sistem durumu var |
| Bu fazda rapor ekranı eklenmedi | OK | Rapor ekranı eklenmedi |

## v0.2 - Fatura Türleri Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Fatura türü eklenebiliyor | OK | `--self-test` repository ekleme akışını doğruladı |
| Fatura türü düzenlenebiliyor | OK | `--self-test` repository güncelleme akışını doğruladı |
| Fatura türü aktif/pasif yapılabiliyor | OK | `--self-test` pasife alma akışını doğruladı |
| Kullanım birimi saklanıyor | OK | `--self-test` güncel kullanım birimini doğruladı |
| Silme yerine pasif yapma davranışı korunuyor | OK | UI ve repository fiziksel silme sunmuyor |
| Başlangıç türleri seed ediliyor | OK | Self-test en az 6 başlangıç türünü doğruladı |
| Aynı isimli fatura türü engelleniyor | OK | Repository isim benzersizliği kontrolü içeriyor |

## v0.3 - Abonelik Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik eklenebiliyor | OK | `--self-test` repository ekleme akışını doğruladı |
| Abonelik düzenlenebiliyor | OK | `--self-test` repository güncelleme akışını doğruladı |
| Abonelik aktif/pasif yapılabiliyor | OK | `--self-test` pasife alma akışını doğruladı |
| Abonelik fatura türüne bağlanıyor | OK | `--self-test` `invoice_type_id` bağını doğruladı |
| Pasif abonelik geçmiş kayıt mantığını bozmayacak şekilde saklanıyor | OK | UI ve repository fiziksel silme sunmuyor |
| Abonelik listesi filtrelenebiliyor | OK | Tür, durum ve metin araması UI içinde destekleniyor |

## v0.4 - Fatura Kayıt Altyapısı

| Kontrol | Durum | Not |
|---|---|---|
| Fatura aboneliğe bağlı kaydediliyor | OK | `--self-test` fatura-abonelik bağını doğruladı |
| Negatif tutar reddediliyor | OK | `--self-test` negatif tutar senaryosunu doğruladı |
| Negatif kullanım reddediliyor | OK | `--self-test` negatif kullanım senaryosunu doğruladı |
| Aynı abonelikte aynı fatura no tekrarına izin verilmiyor | OK | `--self-test` aynı abonelikte tekrar fatura no senaryosunu doğruladı |
| Son ödeme tarihi fatura tarihinden önceyse uyarı veriliyor | OK | `--self-test` tarih uyarısı üretimini doğruladı |
| PDF olmadan temel fatura kaydı yapılabiliyor | OK | Fatura formunda PDF alanı yok, temel kayıt self-test ile doğrulandı |

## v0.5 - Fatura PDF Evrakı Ekleme

| Kontrol | Durum | Not |
|---|---|---|
| Faturaya PDF eklenebiliyor | OK | `--self-test` PDF ekleme akışını doğruladı |
| PDF uygulama klasörüne kopyalanıyor | OK | Hedef klasör `attachments/invoices/yyyy/MM` altında oluşturuluyor |
| PDF orijinal adı saklanıyor | OK | `--self-test` orijinal dosya adını doğruladı |
| PDF SHA-256 hash bilgisi saklanıyor | OK | `--self-test` hash alanının dolduğunu doğruladı |
| PDF olmayan dosya reddediliyor | OK | `--self-test` PDF olmayan dosya ekleme girişimini doğruladı |
| Kayıp PDF dosyası algılanıyor | OK | `--self-test` kopyalanan dosya silinince eksik durumunu doğruladı |
| Kayıtlı PDF açma akışı mevcut | OK | UI kayıtlı PDF'i varsayılan sistem uygulamasıyla açıyor |
| PDF eksikliği görülebiliyor | OK | Fatura ekranında PDF eksik sayısı ve satır PDF durumu gösteriliyor |

## v0.6 - Fatura Listesi ve Filtreleme

| Kontrol | Durum | Not |
|---|---|---|
| Fatura listesi yıla göre filtrelenebiliyor | OK | `--self-test` yıl filtresini doğruladı |
| Fatura listesi aya göre filtrelenebiliyor | OK | `--self-test` ay filtresini doğruladı |
| Fatura listesi fatura türüne göre filtrelenebiliyor | OK | `--self-test` tür filtresini doğruladı |
| Fatura listesi aboneliğe göre filtrelenebiliyor | OK | `--self-test` abonelik filtresini doğruladı |
| Fatura listesi ödeme durumuna göre filtrelenebiliyor | OK | `--self-test` ödenmiş ve gecikmiş filtrelerini doğruladı |
| Fatura listesi PDF durumuna göre filtrelenebiliyor | OK | `--self-test` PDF var ve PDF eksik filtrelerini doğruladı |
| Fatura listesi metin/fatura no ile aranabiliyor | OK | `--self-test` çok terimli aramayı doğruladı |
| Filtreler temizlenebiliyor | OK | UI üzerinde filtreleri temizleme düğmesi eklendi |

## v0.7 - Ödeme Kayıt Altyapısı

| Kontrol | Durum | Not |
|---|---|---|
| Faturaya ödeme kaydı eklenebiliyor | OK | `--self-test` ödeme ekleme akışını doğruladı |
| Ödeme tarihi, tutarı ve açıklaması saklanıyor | OK | `--self-test` ödeme açıklamasını ve ödeme listesini doğruladı |
| Kısmi ödeme faturayı erken ödendi yapmıyor | OK | `--self-test` kısmi ödeme sonrası `unpaid` ve `Kısmi` durumunu doğruladı |
| Tam ödeme faturayı ödendi yapıyor | OK | `--self-test` tam ödeme sonrası `paid` durumunu doğruladı |
| Ödenen ve kalan tutar gösteriliyor | OK | Fatura listesi ve ödeme formu ödenen/kalan tutarı gösteriyor |
| Kalan tutarı aşan ödeme engelleniyor | OK | `--self-test` kalan aşımı senaryosunu doğruladı |
| Negatif ödeme tutarı reddediliyor | OK | `--self-test` negatif ödeme senaryosunu doğruladı |
| Olmayan faturaya ödeme eklenemiyor | OK | `--self-test` geçersiz fatura senaryosunu doğruladı |
| Fatura tutarı değişince ödeme durumu yeniden hesaplanıyor | OK | `--self-test` tutar artırma sonrası kalan ödeme durumunu doğruladı |
| Bu fazda ödeme PDF evrakı eklenmedi | OK | Kapsam v0.8'e bırakıldı |

## v0.8 - Ödeme Evrakı PDF Ekleme

| Kontrol | Durum | Not |
|---|---|---|
| Ödeme kaydına PDF eklenebiliyor | OK | `--self-test` ödeme PDF ekleme akışını doğruladı |
| Ödeme PDF'i uygulama klasörüne kopyalanıyor | OK | Hedef klasör `attachments/payments/yyyy/MM` altında oluşturuluyor |
| Ödeme PDF orijinal adı saklanıyor | OK | `--self-test` orijinal dosya adını doğruladı |
| Ödeme PDF SHA-256 hash bilgisi saklanıyor | OK | `--self-test` hash alanının dolduğunu doğruladı |
| Ödeme PDF olmayan dosya reddediliyor | OK | `--self-test` PDF olmayan ödeme dosyası ekleme girişimini doğruladı |
| Kayıp ödeme PDF dosyası algılanıyor | OK | `--self-test` kopyalanan ödeme PDF'i silinince eksik durumunu doğruladı |
| Kayıtlı ödeme PDF açma akışı mevcut | OK | UI seçili ödeme PDF'ini varsayılan sistem uygulamasıyla açıyor |
| Ödeme PDF eksikliği görülebiliyor | OK | Ödeme geçmişi satırında PDF durumu ve seçili ödeme PDF bilgi alanı gösteriliyor |
| Bu fazda rapor, dışa aktarım ve yedekleme eklenmedi | OK | Kapsam v0.9+ fazlara bırakıldı |

## v0.9 - Ana Gösterge Paneli

| Kontrol | Durum | Not |
|---|---|---|
| Bu ay fatura toplamı gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Bu ay ödeme toplamı gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Ödenmemiş fatura sayısı ve kalan toplam gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Gecikmiş fatura sayısı ve kalan toplam gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Fatura PDF eksikleri gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Ödeme PDF eksikleri gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Temel kayıt sayıları korunuyor | OK | Fatura türü, aktif tür, aktif abonelik ve toplam fatura dashboard içinde gösteriliyor |
| Bu fazda rapor ekranı eklenmedi | OK | Rapor ekranları v0.10+ fazlara bırakıldı |

## v0.10 - Ödenmemiş ve Gecikmiş Faturalar Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Rapor ekranı açılıyor | OK | Sol menüde `Raporlar` sekmesi |
| Ödenmemiş listesi gösteriliyor | OK | `ActionableInvoiceReportCalculator` ile hesaplanır |
| Gecikmiş listesi gösteriliyor | OK | `unpaid` ve son ödeme tarihi bugünden önce |
| Yaklaşan listesi gösteriliyor | OK | `unpaid` ve son ödeme tarihi bugün–7 gün |
| Üst özetler (sayı/kalan) gösteriliyor | OK | Ödenmemiş, gecikmiş, yaklaşan için ayrı tile |
| Liste kolonları beklenen alanları içeriyor | OK | Tür, abonelik, kurum, dönem, no, son ödeme, tutar, ödenen, kalan, PDF |
| `--self-test` rapor hesaplarını doğruluyor | OK | `ActionableInvoiceReportCalculator` senaryosu eklendi |

## v0.11 - Aylık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Aylık liste sekmesi açılıyor | OK | Raporlar ekranında `Aylık Liste` sekmesi |
| Yıl/ay seçimi ile liste güncelleniyor | OK | Dönem seçimi UI üzerinden değişebilir |
| Üst özetler (toplam/ödenen/kalan) gösteriliyor | OK | Aylık rapor özetleri tile olarak gösterilir |
| Ödenmemiş ve gecikmiş adetleri hesaplanıyor | OK | Aylık raporda ödenmemiş/gecikmiş adetleri detayda gösterilir |
| PDF eksik sayısı hesaplanıyor | OK | Aylık raporda PDF eksik sayısı gösterilir |
| `--self-test` aylık rapor hesaplarını doğruluyor | OK | `MonthlyInvoiceReportCalculator` senaryosu eklendi |

## v0.12 - Türe Özgü Aylık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Aylık liste için tür filtresi var | OK | Dönem filtresine `Fatura Türü` seçimi eklendi |
| Tür seçimiyle liste daralıyor | OK | Seçilen tür + yıl + ay için faturalar listelenir |
| `--self-test` tür filtresini doğruluyor | OK | `invoiceTypeId` filtresi için senaryo eklendi |

## v0.13 - Aboneliğe Özgü Aylık Fatura Bilgisi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik sekmesi açılıyor | OK | Raporlar ekranında `Abonelik` sekmesi |
| Abonelik + dönem seçimi var | OK | Abonelik, yıl, ay seçimi ile liste güncellenir |
| Önceki ay karşılaştırması görünüyor | OK | Tile detaylarında delta gösterilir |
| `--self-test` abonelik karşılaştırmasını doğruluyor | OK | `SubscriptionMonthlyComparisonCalculator` senaryosu eklendi |

## v0.14 - Aboneliğe Özgü Yıllık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik yıllık sekmesi açılıyor | OK | Raporlar ekranında `Abonelik Yıllık` sekmesi |
| 12 ay özet tablosu görünüyor | OK | Aylara göre fatura/özet kolonları |
| En yüksek/en düşük ay gösteriliyor | OK | Tile’larda ay adı ve toplam |
| `--self-test` yıllık raporu doğruluyor | OK | `SubscriptionYearlyReportCalculator` senaryosu eklendi |

## v0.15 - Türe Özgü Yıllık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Tür yıllık sekmesi açılıyor | OK | Raporlar ekranında `Tür Yıllık` sekmesi |
| Tür + yıl seçimi var | OK | Fatura türü ve yıl seçimiyle rapor güncellenir |
| 12 ay toplamları görünüyor | OK | Ay bazlı toplamlar hesaplanır |
| Abonelik dağılımı listesi görünüyor | OK | Seçilen tür+ yıl için abonelik bazlı toplamlar sıralanır |
| `--self-test` tür yıllık raporunu doğruluyor | OK | `InvoiceTypeYearlyReportCalculator` senaryosu eklendi |

## v0.16 - Evrak Eksikliği ve Dosya Kontrol Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Evrak kontrol sekmesi açılıyor | TODO | Raporlar ekranında yeni sekme |
| Eksik PDF listesi görünüyor | TODO | Kayıt var ama dosya yok/erişilemiyor |
| Aynı hash uyarıları görünüyor | TODO | Muhtemel mükerrer evraklar gruplu gösterilir |
| `--self-test` evrak kontrolünü doğruluyor | TODO | En az bir eksik dosya ve bir aynı-hash senaryosu |

## v0.10 ve Sonrası İçin Regresyon Başlıkları

Bu başlıklar ilgili fazlar başladığında ayrıntılandırılacak:

- Ödenmemiş ve gecikmiş fatura raporları
- Aylık ve yıllık raporlar
- Excel dışa aktarım
- PDF rapor üretimi; başlamadan önce kullanıcıdan Excel örneği istenecek ve çıktı bu örneğe göre doğrulanacak
- Manuel yedekleme
- Tutarlılık denetimi

## Test Geçmişi

| Tarih | Faz | Sonuç | Not |
|---|---|---|---|
| 2026-05-30 | v0.0 | OK | Plan proje içine alındı, roadmap ve regresyon dosyaları oluşturuldu |
| 2026-05-30 | v0.1 | OK | WPF iskeleti, SQLite başlangıcı, klasör altyapısı ve boş dashboard doğrulandı |
| 2026-05-30 | v0.2 | OK | Fatura türleri migration, seed, listeleme, ekleme, düzenleme ve aktif/pasif akışı doğrulandı |
| 2026-05-30 | v0.3 | OK | Abonelik migration, listeleme, filtreleme, ekleme, düzenleme ve aktif/pasif akışı doğrulandı |
| 2026-05-31 | v0.4 | OK | Fatura migration, listeleme, ekleme, düzenleme ve temel doğrulamalar tamamlandı |
| 2026-05-31 | v0.5 başlangıç | OK | v0.4 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.5 | OK | Fatura PDF metadata, kopyalama, hash, açma akışı ve eksik dosya kontrolü tamamlandı |
| 2026-05-31 | v0.6 başlangıç | OK | v0.5 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.6 | OK | Fatura listesi yıl, ay, tür, abonelik, ödeme durumu, PDF durumu ve metin aramasıyla filtrelenebilir hale geldi |
| 2026-05-31 | v0.7 başlangıç | OK | v0.6 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.7 | OK | Ödeme kayıt altyapısı, kısmi/tam ödeme durumu, kalan tutar kontrolü ve self-test kapsamı tamamlandı |
| 2026-05-31 | v0.8 başlangıç | OK | v0.7 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.8 | OK | Ödeme PDF metadata, kopyalama, hash, açma akışı ve eksik dosya kontrolü tamamlandı |
| 2026-05-31 | v0.9 başlangıç | OK | v0.8 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.9 | OK | Dashboard aylık toplamlar, ödenmemiş/gecikmiş özetler ve evrak eksikleriyle geliştirildi |
| 2026-05-31 | v0.10 başlangıç | OK | v0.9 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.11 başlangıç | OK | v0.10 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.15 | OK | Tür yıllık raporu eklendi; build, self-test ve health-check doğrulandı |
