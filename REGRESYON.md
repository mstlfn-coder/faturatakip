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
| Git durumu kontrol edildi | OK | İlk durumda depo boş ve commitsiz |
| Markdown dokümantasyonu UTF-8 olarak okunuyor | OK | Plan dosyası UTF-8 korunarak kopyalandı |
| Yeni dosya adları yol sorunu azaltmak için ASCII tutuluyor | OK | Kök klasör Türkçe, proje içi doküman adları ASCII |
| `README.md` yeni chat başlangıcını açıklıyor | OK | Oluşturuldu |
| Roadmap mevcut | OK | `ROADMAP.md` |
| Regresyon dosyası mevcut | OK | `REGRESYON.md` |

## v0.1 - Proje İskeleti ve Veritabanı

Henüz başlanmadı.

| Kontrol | Durum | Not |
|---|---|---|
| WPF uygulaması derleniyor | TODO | v0.1 sonunda kontrol edilecek |
| Uygulama açılıyor | TODO | v0.1 sonunda kontrol edilecek |
| `database` klasörü oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| `attachments` klasörü oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| `backups` klasörü oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| `logs` klasörü oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| `exports` klasörü oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| `database/fatura_takip.db` oluşturuluyor | TODO | v0.1 sonunda kontrol edilecek |
| Migration başlangıç mantığı tekrar çalıştırıldığında hata vermiyor | TODO | v0.1 sonunda kontrol edilecek |
| Bu fazda veri giriş ekranı eklenmedi | TODO | Kapsam dışı kalmalı |
| Bu fazda rapor ekranı eklenmedi | TODO | Kapsam dışı kalmalı |

## v0.2 - Fatura Türleri Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Fatura türü eklenebiliyor | TODO | v0.2 sonunda |
| Fatura türü düzenlenebiliyor | TODO | v0.2 sonunda |
| Fatura türü aktif/pasif yapılabiliyor | TODO | v0.2 sonunda |
| Kullanım birimi saklanıyor | TODO | v0.2 sonunda |
| Silme yerine pasif yapma davranışı korunuyor | TODO | v0.2 sonunda |

## v0.3 - Abonelik Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik eklenebiliyor | TODO | v0.3 sonunda |
| Abonelik düzenlenebiliyor | TODO | v0.3 sonunda |
| Abonelik aktif/pasif yapılabiliyor | TODO | v0.3 sonunda |
| Abonelik fatura türüne bağlanıyor | TODO | v0.3 sonunda |
| Pasif abonelik geçmiş kayıt mantığını bozmayacak şekilde saklanıyor | TODO | v0.3 sonunda |

## v0.4 - Fatura Kayıt Altyapısı

| Kontrol | Durum | Not |
|---|---|---|
| Fatura aboneliğe bağlı kaydediliyor | TODO | v0.4 sonunda |
| Negatif tutar reddediliyor | TODO | v0.4 sonunda |
| Negatif kullanım reddediliyor | TODO | v0.4 sonunda |
| Aynı abonelikte aynı fatura no tekrarına izin verilmiyor | TODO | v0.4 sonunda |
| Son ödeme tarihi fatura tarihinden önceyse uyarı veriliyor | TODO | v0.4 sonunda |

## v0.5 ve Sonrası İçin Regresyon Başlıkları

Bu başlıklar ilgili fazlar başladığında ayrıntılandırılacak:

- Fatura PDF kopyalama, hash alma ve açma
- Fatura listesi filtreleri
- Ödeme kaydı ve ödeme durumu
- Ödeme PDF kopyalama, hash alma ve açma
- Dashboard toplamları
- Ödenmemiş ve gecikmiş fatura raporları
- Aylık ve yıllık raporlar
- Excel dışa aktarım
- PDF rapor üretimi
- Manuel yedekleme
- Tutarlılık denetimi

## Test Geçmişi

| Tarih | Faz | Sonuç | Not |
|---|---|---|---|
| 2026-05-30 | v0.0 | OK | Plan proje içine alındı, roadmap ve regresyon dosyaları oluşturuldu |
