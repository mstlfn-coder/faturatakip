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
| Git durumu kontrol edildi | OK | `codex/v0.19-manuel-guvenli-yedekleme` branch'i üzerinde çalışılıyor |
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
| Evrak kontrol sekmesi açılıyor | OK | Raporlar ekranında `Evrak Kontrol` sekmesi |
| Eksik PDF listesi görünüyor | OK | Fatura/Ödeme için `PDF Yok` ve `PDF Kayıp` uyarıları listeleniyor |
| Aynı hash uyarıları görünüyor | OK | Fatura/Ödeme PDF hash grupları uyarı listesine ekleniyor |
| `--self-test` evrak kontrolünü doğruluyor | OK | Eksik dosya + aynı-hash senaryosu eklendi |

## v0.17 - Excel Dışa Aktarım

| Kontrol | Durum | Not |
|---|---|---|
| Excel dışa aktarım butonu görünüyor | OK | Faturalar ve Raporlar ekranında |
| XLSX dosyası exports/ altına yazılıyor | OK | Dosya adı tarih-saat içerir |
| Kolonlar doğru ve okunabilir | OK | Başlıklar + temel formatlar |
| `--self-test` temel excel exportu doğruluyor | OK | En az bir dosya oluşumu |

## v0.18 - Yazdırılabilir PDF Raporlar

| Kontrol | Durum | Not |
|---|---|---|
| PDF rapor butonu görünüyor | OK | Faturalar ve Raporlar ekranında |
| PDF A4 sayfa düzeni doğru | OK | Başlık + özet + tablo + footer + imza alanı |
| Filtre bilgileri PDF üstünde görünüyor | OK | Dönem/oluşturan/filtre alanları |
| `--self-test` temel PDF üretimini doğruluyor | OK | En az bir dosya oluşumu |

## v0.19 - Manuel Güvenli Yedekleme

| Kontrol | Durum | Not |
|---|---|---|
| Yedekleme butonu görünüyor | OK | `MainWindow.xaml` içinde `BackupNavButton` mevcut ve `BackupNavButton_Click` bağlı |
| ZIP yedek backups/ altına yazılıyor | OK | `--create-backup --backup-no-attachments --backup-no-exports` ile `backups/backup_YYYYMMDD_HHMMSS.zip` oluştu |
| Veritabanı ZIP içinde | OK | ZIP içinde `database/fatura_takip.db` var (SQLite backup ile) |
| Evraklar ZIP içinde | OK | `--create-backup` ile `database/` + `attachments/` + `exports/` + `backup.json` doğrulandı |
| CLI smoke test mevcut | OK | `dotnet run --no-build --project src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --create-backup --backup-no-attachments --backup-no-exports` |

## v0.10 ve Sonrası İçin Regresyon Başlıkları

Bu başlıklar ilgili fazlar başladığında ayrıntılandırılacak:

- Ödenmemiş ve gecikmiş fatura raporları
- Aylık ve yıllık raporlar
- Excel dışa aktarım
- PDF rapor üretimi; başlamadan önce kullanıcıdan Excel örneği istenecek ve çıktı bu örneğe göre doğrulanacak
- Manuel yedekleme
- Tutarlılık denetimi

## v0.20 - Tutarlılık Denetimi

| Kontrol | Durum | Not |
|---|---|---|
| Raporlar ekranında Tutarlılık sekmesi görünüyor | OK | `ReportsView.xaml` içinde `ConsistencyTabButton` mevcut ve `ConsistencyTabButton_Click` bağlı |
| Tutarlılık denetimi liste üretiyor | OK | Boş veri setinde 0 issue; veri varsa WARN/ERROR listelenir |
| CLI tutarlılık denetimi çalışıyor | OK | `dotnet run -c Release --no-build --project src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --consistency-check` |

## v0.21 - Güvenli Yedek Geri Yükleme

| Kontrol | Durum | Not |
|---|---|---|
| CLI geri yükleme çalışıyor | OK | `--restore-backup <zip> --restore-target <emptyFolder>` |
| Boş olmayan hedefe restore engelleniyor | OK | `--self-test` içinde negatif restore senaryosu ile doğrulandı |
| Restore sonrası DB dosyası var | OK | `database/fatura_takip.db` hedefte mevcut |

## v0.22 - UI Backup Restore

| Kontrol | Durum | Not |
|---|---|---|
| Backup ekraninda restore bolumu var | OK | BackupView icinde restore alanlari mevcut |
| Zip secme butonu calisiyor | OK | OpenFileDialog |
| Hedef klasor yolu girilebiliyor | OK | TextBox editable |
| Restore sadece bos klasore izin veriyor | OK | RestoreToEmptyRoot kontrol ediyor |

## v0.23 - Rapor Export Sablon Hizalama (Excel/PDF)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Self-test artifact saklama calisiyor | OK | `FATURATAKIP_SELFTEST_KEEP=1` iken test klasoru silinmez |
| Raporlar Excel export'ta Aciklama satiri var | OK | Excel raporlarinda 6. satirda `Aciklama : ...` |
| Excel export ana sayfa sablon kolonlari + Detay sayfasi | OK | Aylik, Abonelik Aylik/Yillik, Tur Yillik, Yillik Liste: ana sayfa sablon kolonlari + `Detay` |
| Raporlar ekraninda `Yillik Liste` sekmesi var | OK | Filtre degisiminde liste yenileniyor; self-test yillik liste excel export smoke eklendi |
| PDF export halen calisiyor | OK | Build + self-test OK |

## v0.24 - Build Uyari Temizligi (CS8123)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| CS8123 tuple isim uyari temizlendi | OK | `ReportsView.BuildPdfContent()` return tuple eleman isimleri hizalandi |

## v0.25 - PDF Rapor Matbu Stil + Toplam Satiri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF imza blogu yok | OK | PDF raporlar bilgilendirme amacli; imza alanlari kaldirildi |
| PDF tablo matbu stilde | OK | Siyah border, bos satir yok, template raporlarda `GENEL TOPLAM` footer satiri var |

## v0.26 - PDF Footer Kapali

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF footer varsayilan kapali | OK | Ornek PDF'lerde footer yok; `includeFooter=false` default |

## v0.27 - Bagimlilik Uyari Temizligi (QuestPDF)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata, 0 uyari) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| NU1603 yok | OK | QuestPDF paketi 2025.4.0'a sabitlendi |

## v0.28 - PDF Aciklama Cumlesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF Aciklama cumlesi | OK | Template raporlarda `Açıklama :` satiri filtre yerine cumle olarak uretiliyor |

## v0.29 - PDF Baslik Sadeligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF basligi sade | OK | `secondaryTitle` basliga eklenmiyor; detay `Açıklama :` satirinda kalıyor |

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
| 2026-05-31 | v0.16 | OK | Evrak kontrol raporu eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.17 | OK | Excel dışa aktarım eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.18 | OK | PDF rapor export eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.19 | OK | Yedekleme (UI + `--create-backup`) eklendi; build, self-test, health-check ve CLI smoke test doğrulandı |
| 2026-06-01 | v0.20 | OK | Tutarlılık denetimi eklendi; build, self-test, health-check ve `--consistency-check` smoke test doğrulandı |
| 2026-06-01 | v0.21 | OK | Güvenli geri yükleme eklendi; `--restore-backup` smoke test doğrulandı |
| 2026-06-01 | v0.22 | OK | Backup UI restore eklendi; build + self-test OK; restore hata durumda MessageBox yok |
| 2026-06-01 | v0.23 | OK | Rapor export sablon hizalama + yillik liste sekmesi; build + self-test OK |
| 2026-06-01 | v0.24 | OK | CS8123 uyarilari temizlendi; build + self-test OK |
| 2026-06-01 | v0.25 | OK | PDF rapor matbu stil + toplam satiri; build + self-test OK |
| 2026-06-02 | v0.26 | OK | PDF footer varsayilan kapali; build + self-test OK |
| 2026-06-02 | v0.27 | OK | QuestPDF NU1603 kaldirildi; build + self-test OK |
| 2026-06-02 | v0.28 | OK | PDF aciklama satiri cumle olarak; build + self-test OK |
| 2026-06-02 | v0.29 | OK | PDF başlığı sade bırakıldı; build + self-test OK |

## v0.30 - PDF Tablo Baslik Sikiligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF tablo baslik satiri daha kompakt | OK | Baslik hucrelerinin dikey padding degeri dusuruldu; tablo ust satiri daha sik gorunuyor |

| 2026-06-02 | v0.30 | OK | PDF tablo baslik satiri sikilastirildi; build + self-test OK |

## v0.31 - PDF Tablo Govde Sikiligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF govde satirlari daha kompakt | OK | Govde hucrelerinin dikey padding degeri dusuruldu; tablo satirlari daha sik gorunuyor |

| 2026-06-02 | v0.31 | OK | PDF tablo govde satirlari sikilastirildi; build + self-test OK |

## v0.32 - PDF Tablo Yazi Boyutu Dengesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF tablo yazi boyutlari dengeli | OK | Baslik, govde ve footer hucreleri 9 puntoya cekildi; gorunum daha birornek oldu |

| 2026-06-02 | v0.32 | OK | PDF tablo yazi boyutlari dengelendi; build + self-test OK |

## v0.33 - PDF Aciklama Tablo Araligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| `Aciklama :` ile tablo arasi daha sik | OK | Ust icerik kolon spacing'i ve aciklama satiri ust boslugu azaltildi |

| 2026-06-02 | v0.33 | OK | PDF `Aciklama :` satiri ile tablo arasi sikilastirildi; build + self-test OK |

## v0.34 - PDF Ust Baslik Dengesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF ust baslik bosluklari daha dengeli | OK | Baslik blogu spacing'i ve tarih kolonunun ust padding degeri azaltildi |

| 2026-06-02 | v0.34 | OK | PDF ust baslik bosluklari dengelendi; build + self-test OK |

## v0.35 - PDF Gorsel QA

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF gorsel ornekleri kontrol edildi | OK | Self-test PDF + ek aylik/ornek odenmemis PDF goruntulendi; yerlesim temiz bulundu |

| 2026-06-02 | v0.35 | OK | PDF gorsel QA tamamlandi; build + self-test OK |

## v0.36 - Restore Negatif Smoke

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Bos olmayan hedefe restore engelleniyor | OK | Self-test icinde ornek restore zip + dolu hedef klasor senaryosu eklendi |

| 2026-06-02 | v0.36 | OK | Restore negatif smoke self-test kapsamina alindi; build + self-test OK |

## v0.37 - Audit Log Temeli

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Audit log tablosu olusuyor | OK | `0008 - Create audit logs` migration'i eklendi |
| Abonelik islemleri loglaniyor | OK | Olusturma, guncelleme ve aktif/pasif degisimi kayda dusuluyor |
| Fatura islemleri loglaniyor | OK | Olusturma, guncelleme ve PDF ekleme/degistirme kayda dusuluyor |
| Odeme islemleri loglaniyor | OK | Olusturma ve PDF ekleme/degistirme kayda dusuluyor |

| 2026-06-02 | v0.37 | OK | Audit log temeli eklendi; build + self-test OK |

## v0.38 - Islem Gecmisi Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| `Raporlar` ekraninda `Islem Gecmisi` sekmesi var | OK | `ReportsView` icine yeni sekme ve grid eklendi |
| Audit log kayitlari listeleniyor | OK | `AuditLogRepository` verisi tarih, islem, varlik ve aciklama kolonlariyla gosteriliyor |
| Audit log export'u destekleniyor | OK | Excel export generic tabloyu, PDF export `ISLEM GECMISI RAPORU` icerigini uretiyor |

| 2026-06-02 | v0.38 | OK | Islem Gecmisi sekmesi eklendi; build + self-test OK |

## v0.39 - Islem Gecmisi Filtreleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Islem turu filtresi var | OK | `AuditLogActionInput` ile secili aksiyon turune gore daraltma yapiliyor |
| Tarih araligi filtresi var | OK | Baslangic / bitis tarihleri secilebiliyor; ters aralikta bitis tarihi duzeltiliyor |
| Audit log export'u filtreyi yansitiyor | OK | Grid ile ayni filtrelenmis satirlar export akisina gidiyor; not/filter metni guncellendi |

| 2026-06-02 | v0.39 | OK | Islem Gecmisi filtreleri eklendi; build + self-test OK |

## v0.40 - Islem Gecmisi Arama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Varlik filtresi var | OK | `AuditLogEntityInput` ile tablo/varlik bazinda daraltma yapiliyor |
| Kullanici filtresi var | OK | `AuditLogUserInput` ile kaydi yazan kullanici bazinda daraltma yapiliyor |
| Metin aramasi var | OK | Aciklama, islem, varlik, kayit id ve kullanici alanlarinda arama yapiliyor |

| 2026-06-02 | v0.40 | OK | Islem Gecmisi arama filtreleri eklendi; build + self-test OK |

## v0.41 - Islem Gecmisi Detay Paneli

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Audit log satiri secilebiliyor | OK | `AuditLogGrid` secimiyle detay paneli guncelleniyor |
| Eski/yeni deger paneli var | OK | Grid altinda `Eski Deger` ve `Yeni Deger` alanlari gosteriliyor |
| JSON degerleri okunur gorunuyor | OK | Kayitli JSON varsa indent edilerek yazdiriliyor; bossa `Kayit yok` gosteriliyor |

| 2026-06-02 | v0.41 | OK | Islem Gecmisi detay paneli eklendi; build + self-test OK |

## v0.42 - Islem Gecmisi Alan Farki

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Alan bazli fark tablosu var | OK | `AuditLogDiffGrid` alan, eski, yeni ve durum kolonlariyla gosteriliyor |
| Eski/yeni JSON parse edilip karsilastiriliyor | OK | JSON alanlari flatten edilerek birlestirilmis key listesi uzerinden karsilastiriliyor |
| JSON olmayan payload bozulmadan gosteriliyor | OK | Parse edilemeyen icerikler `value` alaninda korunuyor |

| 2026-06-02 | v0.42 | OK | Audit log alan farki eklendi; build + self-test OK |

## v0.43 - Islem Gecmisi Diff Filtresi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Sadece degisen alanlar filtresi var | OK | `AuditLogDiffChangedOnlyCheckBox` ile `Ayni` satirlari gizlenebiliyor |
| Detay secimi korunuyor | OK | Kayit seciliyken filtre degisince ayni secili detay yeniden uygulanuyor |

| 2026-06-02 | v0.43 | OK | Audit log diff filtresi eklendi; build + self-test OK |

## v0.44 - Islem Gecmisi Diff Rozetleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Diff durumlari renkli rozet olarak gorunuyor | OK | `Degisti`, `Eklendi`, `Silindi`, `Ayni` icin ayri renk stili tanimlandi |
| Diff okunabilirligi artti | OK | `Durum` kolonu template column ile daha belirgin hale geldi |

| 2026-06-04 | v0.44 | OK | Audit log diff rozetleri eklendi; build + self-test OK |

## v0.45 - Islem Gecmisi Kopyalama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Eski deger kopyalanabiliyor | OK | `CopyOldAuditLogButton` secili detaydaki metni panoya yazar |
| Yeni deger kopyalanabiliyor | OK | `CopyNewAuditLogButton` secili detaydaki metni panoya yazar |
| Kopyalama sonucu ipucu veriliyor | OK | Basari veya hata mesaji `AuditLogHintText` uzerinden gosteriliyor |

| 2026-06-04 | v0.45 | OK | Audit log detay paneline kopyalama aksiyonlari eklendi; build + self-test OK |

## v0.46 - Islem Gecmisi Diff Kopyalama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Tum diff kopyalanabiliyor | OK | `CopyAuditLogDiffButton` gorunen diff satirlarini topluca panoya yazar |
| Filtrelenmis gorunum korunuyor | OK | `Sadece degisen alanlari goster` secimi acikken kopyalama ayni listeyi kullanir |
| Diff meta bilgisi ekleniyor | OK | Islem, varlik, kayit id, kullanici ve tarih satirlari ustte yazdiriliyor |

| 2026-06-04 | v0.46 | OK | Audit log diff toplu kopyalama aksiyonu eklendi; build + self-test OK |

## v0.47 - Islem Gecmisi Disa Aktarma

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| TXT disa aktarma var | OK | `ExportAuditLogTxtButton` secili diff ozetini `exports` altina yazar |
| JSON disa aktarma var | OK | `ExportAuditLogJsonButton` secili kaydi ve diff satirlarini yapisal olarak kaydeder |
| Durum mesaji gosteriliyor | OK | Basari veya hata sonucu `AuditLogHintText` uzerinden iletiliyor |

| 2026-06-04 | v0.47 | OK | Audit log txt/json disa aktarma aksiyonlari eklendi; build + self-test OK |

## v0.48 - Islem Gecmisi Exports Kolayligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Exports klasoru acilabiliyor | OK | `OpenAuditLogExportsButton` klasoru olusturup Windows Gezgini ile acar |
| Klasor yoksa olusturuluyor | OK | `Directory.CreateDirectory(exportsDir)` ile guvence altina aliniyor |
| Durum mesaji gosteriliyor | OK | Basari veya hata sonucu `AuditLogHintText` uzerinden iletiliyor |

| 2026-06-04 | v0.48 | OK | Audit log exports klasoru acma aksiyonu eklendi; build + self-test OK |

## v0.49 - Islem Gecmisi Filtre Tercihleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Filtre tercihleri saklaniyor | OK | `AuditLogFilterPreferences` action, varlik, kullanici, arama ve tarih araligini `config` altina yazar |
| Tercihler geri yukleniyor | OK | `ReportsView.Initialize` acilisinda kayitli filtreler tekrar UI'ya uygulanir |
| Degisen alan filtresi de korunuyor | OK | `ChangedOnly` secimi de ayni preference kaydinda tutuluyor |

| 2026-06-05 | v0.49 | OK | Audit log filtre tercihleri kalici hale getirildi; build + self-test OK |

## v0.50 - Islem Gecmisi Filtre Sifirlama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Filtreleri sifirla dugmesi var | OK | `ResetAuditLogFiltersButton` audit log filtre panelinde gosteriliyor |
| Tum filtre alanlari temizleniyor | OK | Islem, varlik, kullanici, tarih, arama ve `ChangedOnly` secimi varsayilana donuyor |
| Preference kaydi da guncelleniyor | OK | Sifirlama sonrasi config dosyasi `Default` filtrelerle yeniden yaziliyor |

| 2026-06-05 | v0.50 | OK | Audit log filtre sifirlama aksiyonu eklendi; build + self-test OK |

## v0.51 - Islem Gecmisi Son Dosya Acma

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Son dosyayi ac dugmesi var | OK | `OpenLastAuditLogExportButton` audit log araclari icinde gosteriliyor |
| Son export yolu hatirlaniyor | OK | `ExportAuditLogDetail` son yazilan dosya yolunu `_lastAuditLogExportPath` icinde tutuyor |
| Geriye donuk bulma var | OK | Oturumda yol yoksa `exports` altindaki en yeni `audit-log-*` dosyasi aciliyor |

| 2026-06-05 | v0.51 | OK | Audit log son disa aktarilan dosyayi acma aksiyonu eklendi; build + self-test OK |

## v0.52 - Islem Gecmisi Hizli Odak

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Secili kayda git dugmesi var | OK | FocusSelectedAuditLogButton audit log filtre panelinde gosteriliyor |
| Secili kayit gorunur alana getiriliyor | OK | ScrollIntoView ile secili row tekrar ekranda odaklaniyor |
| Filtre degisince secim korununca da odak veriliyor | OK | ApplyTab(ReportTab.AuditLog) icinde eslesen secili satir bulunursa gorunur alana aliniyor |

| 2026-06-05 | v0.52 | OK | Audit log secili kayda hizli odak aksiyonu eklendi; build + self-test OK |


## v0.53 - Islem Gecmisi Export Gecmisi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son export listesi var | OK | AuditLogRecentExportsInput son 5 audit export dosyasini gosteriyor |
| Listeden ac aksiyonu var | OK | OpenSelectedAuditLogExportButton secilen export dosyasini aciyor |
| Son dosya fallback korunuyor | OK | Son dosya yoksa en yeni export yine listeden/fallback ile bulunuyor |

| 2026-06-05 | v0.53 | OK | Audit log export gecmisi rahatligi eklendi; build + self-test OK |

