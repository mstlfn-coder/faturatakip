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
| Git durumu kontrol edildi | OK | `codex/v0.6-fatura-listesi-filtreleme` branch'i üzerinde çalışılıyor |
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

## v0.7 ve Sonrası İçin Regresyon Başlıkları

Bu başlıklar ilgili fazlar başladığında ayrıntılandırılacak:

- Ödeme kaydı ve ödeme durumu
- Ödeme PDF kopyalama, hash alma ve açma
- Dashboard toplamları
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
