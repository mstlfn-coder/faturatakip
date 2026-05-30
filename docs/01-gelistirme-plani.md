# Kurum Fatura Takip Programı — Kapsamlı Geliştirme Planı

## 1. Programın Amacı

Bu program; kurum adına kayıtlı elektrik, su, doğalgaz, telefon, internet ve benzeri aboneliklere ait faturaların düzenli şekilde takip edilmesi için geliştirilecektir.

Programın temel amacı:

- Kuruma ait abonelik bilgilerini kayıt altında tutmak.
- Her aboneliğe ait gelen faturaları sisteme işlemek.
- Fatura PDF evraklarını güvenli şekilde saklamak.
- Fatura ödeme bilgilerini ve ödeme evraklarını kaydetmek.
- Ödenmemiş, gecikmiş veya evrakı eksik faturaları raporlamak.
- Aylık, yıllık, türe göre ve aboneliğe göre ayrıntılı fatura raporları oluşturmak.
- Kurumun geçmiş dönem fatura tutarı ve kullanım miktarı takibini kolaylaştırmak.

Bu program muhasebe programı yerine geçmeyecek; kurum içi takip, belge düzeni ve kontrol amacıyla kullanılacaktır.

---

## 2. Ana Kavramlar

### 2.1. Fatura Türü

Faturaların ait olduğu ana hizmet türüdür.

Örnek türler:

- Elektrik
- Su
- Doğalgaz
- Telefon
- İnternet
- Diğer

Sistem ileride yeni tür eklenmesine izin vermelidir. Örneğin “Asansör Bakım”, “Kamera Sistemi”, “Yakıt”, “Kira” gibi kurumun ihtiyacına göre yeni takip türleri eklenebilir.

### 2.2. Abonelik

Kuruma ait her hizmet bağlantısı ayrı abonelik olarak kaydedilir.

Örnek:

- Elektrik — Ana Bina
- Elektrik — Pansiyon Binası
- Su — Kurum Ana Aboneliği
- Doğalgaz — Isınma Aboneliği
- İnternet — Kurum Fiber Hattı
- Telefon — Müdürlük Sabit Hat

Bir fatura her zaman bir aboneliğe bağlı olmalıdır.

### 2.3. Fatura

Abonelik üzerinden gelen resmi fatura kaydıdır.

Her faturada şu bilgiler tutulmalıdır:

- Fatura türü
- Abonelik
- Fatura tarihi
- Son ödeme tarihi
- Dönem yılı
- Dönem ayı
- Fatura numarası
- Fatura tutarı
- Kullanım miktarı
- Kullanım birimi
- Fatura PDF dosyası
- Açıklama
- Ödeme durumu

### 2.4. Ödeme

Faturanın ödendiğine ilişkin kayıt ve evraktır.

Her ödeme kaydında şu bilgiler tutulmalıdır:

- Bağlı fatura
- Ödeme tarihi
- Ödenen tutar
- Ödeme yöntemi
- Ödeme belge numarası
- Ödeme evrakı PDF dosyası
- Açıklama

İlk sürümde her fatura için tek ödeme kaydı yeterli görülebilir. Ancak ileride kısmi ödeme, fazla ödeme veya düzeltme ihtimali için ödeme tablosu ayrı tutulmalıdır.

---

## 3. Önerilen Teknoloji Yapısı

### 3.1. Program Tipi

Net öneri:

- Masaüstü uygulama
- C# / .NET
- WPF arayüz
- SQLite veritabanı
- PDF evrakları dosya sistemi içinde saklama

Bu yapı kurum içi kullanım için uygundur. İnternet bağlantısı gerektirmez. Tek bilgisayarda veya ortak klasörde çalışacak şekilde ilerletilebilir.

### 3.2. Dosya Yapısı

Önerilen ana klasör yapısı:

```text
FaturaTakip/
├── database/
│   └── fatura_takip.db
├── attachments/
│   ├── invoices/
│   │   └── 2026/
│   │       └── 01/
│   └── payments/
│       └── 2026/
│           └── 01/
├── backups/
├── logs/
└── exports/
```

PDF dosyaları veritabanının içine gömülmemelidir. Veritabanında yalnızca dosya yolu, dosya adı, dosya hash bilgisi ve yüklenme tarihi tutulmalıdır.

---

## 4. Veritabanı Tasarımı

### 4.1. `invoice_types` — Fatura Türleri

Alanlar:

- `id`
- `name`
- `description`
- `default_usage_unit`
- `is_active`
- `created_at`
- `updated_at`

Örnek kayıtlar:

| name | default_usage_unit |
|---|---|
| Elektrik | kWh |
| Su | m³ |
| Doğalgaz | m³ |
| Telefon | Adet / Dakika |
| İnternet | GB / Sabit |

---

### 4.2. `subscriptions` — Abonelikler

Alanlar:

- `id`
- `invoice_type_id`
- `subscription_name`
- `institution_name`
- `subscriber_no`
- `installation_no`
- `meter_no`
- `provider_company`
- `service_address`
- `unit_name`
- `default_usage_unit`
- `is_active`
- `start_date`
- `end_date`
- `description`
- `created_at`
- `updated_at`

Açıklama:

- Birden fazla doğalgaz, elektrik veya internet aboneliği olabilir.
- Abonelik pasife alınabilir ama silinmemelidir.
- Pasif abonelik geçmiş faturaları raporlarda görünmeye devam etmelidir.

---

### 4.3. `invoices` — Faturalar

Alanlar:

- `id`
- `subscription_id`
- `invoice_type_id`
- `invoice_year`
- `invoice_month`
- `invoice_date`
- `due_date`
- `invoice_no`
- `amount`
- `usage_amount`
- `usage_unit`
- `status`
- `invoice_pdf_path`
- `invoice_pdf_original_name`
- `invoice_pdf_hash`
- `description`
- `created_at`
- `updated_at`
- `canceled_at`
- `cancel_reason`

Status değerleri:

- `unpaid`
- `paid`
- `overdue`
- `canceled`

Not:

- `overdue` ayrı kayıt durumu olarak tutulabilir veya son ödeme tarihine göre dinamik hesaplanabilir.
- Daha güvenli yapı: Veritabanında `unpaid` / `paid` / `canceled` tutulsun; gecikmiş bilgisi raporda `due_date` üzerinden hesaplansın.

---

### 4.4. `payments` — Ödemeler

Alanlar:

- `id`
- `invoice_id`
- `payment_date`
- `paid_amount`
- `payment_method`
- `payment_document_no`
- `payment_pdf_path`
- `payment_pdf_original_name`
- `payment_pdf_hash`
- `description`
- `created_at`
- `updated_at`
- `canceled_at`
- `cancel_reason`

Ödeme yöntemi örnekleri:

- Banka
- Kredi Kartı
- Kurum Muhasebe
- Online Ödeme
- Diğer

---

### 4.5. `attachments` — Evrak Dosyaları

İlk aşamada fatura ve ödeme tablolarında PDF yolu tutmak yeterli olabilir. Ancak daha profesyonel yapı için ayrı `attachments` tablosu önerilir.

Alanlar:

- `id`
- `related_table`
- `related_id`
- `attachment_type`
- `original_file_name`
- `stored_file_name`
- `stored_path`
- `file_hash`
- `file_size`
- `uploaded_at`
- `description`
- `is_active`

`attachment_type` örnekleri:

- `invoice_pdf`
- `payment_pdf`
- `other`

Bu yapı ileride aynı faturaya ek evrak eklenmesi gerektiğinde avantaj sağlar.

---

### 4.6. `audit_logs` — İşlem Geçmişi

Alanlar:

- `id`
- `action_type`
- `table_name`
- `record_id`
- `old_value`
- `new_value`
- `description`
- `created_at`
- `user_name`

Kayıt altına alınacak işlemler:

- Abonelik oluşturma
- Abonelik güncelleme
- Fatura ekleme
- Fatura güncelleme
- Fatura iptal
- Fatura PDF ekleme/değiştirme
- Ödeme ekleme
- Ödeme iptal
- Ödeme evrakı ekleme/değiştirme
- Rapor dışa aktarma

---

## 5. Ana Ekranlar

### 5.1. Ana Gösterge Paneli

Bu ekran program açıldığında genel durumu göstermelidir.

Kartlar:

- Toplam aktif abonelik
- Bu ay gelen fatura sayısı
- Bu ay toplam fatura tutarı
- Ödenmemiş fatura sayısı
- Son ödeme tarihi geçmiş fatura sayısı
- Fatura PDF’i eksik kayıt sayısı
- Ödeme evrakı eksik kayıt sayısı
- Yaklaşan son ödeme tarihli faturalar

Liste alanları:

- Son eklenen faturalar
- Yaklaşan ödemeler
- Gecikmiş faturalar
- Evrakı eksik kayıtlar

---

### 5.2. Fatura Türleri Yönetimi

İşlevler:

- Yeni fatura türü ekleme
- Fatura türü düzenleme
- Fatura türünü aktif/pasif yapma
- Varsayılan kullanım birimi belirleme

Silme önerilmez. Pasif yapma kullanılmalıdır.

---

### 5.3. Abonelik Yönetimi

İşlevler:

- Yeni abonelik ekleme
- Abonelik düzenleme
- Abonelik aktif/pasif yapma
- Aboneliğe ait faturaları görüntüleme
- Abonelik bazlı rapor açma

Abonelik alanları:

- Fatura türü
- Abonelik adı
- Kurum adı
- Abone numarası
- Tesisat numarası
- Sayaç numarası
- Hizmet sağlayıcı firma
- Hizmet adresi
- Birim / bina adı
- Varsayılan kullanım birimi
- Açıklama

---

### 5.4. Fatura Kayıt Ekranı

İşlevler:

- Yeni fatura ekleme
- Fatura düzenleme
- Fatura PDF’i ekleme
- Fatura PDF’ini açma
- Fatura iptal etme
- Faturaya ödeme ekleme

Fatura girişinde zorunlu alanlar:

- Abonelik
- Fatura tarihi
- Son ödeme tarihi
- Dönem yılı
- Dönem ayı
- Fatura numarası
- Tutar
- Kullanım miktarı
- Kullanım birimi

Kontroller:

- Aynı abonelikte aynı fatura numarası tekrar girilmemeli.
- Tutar negatif olamaz.
- Kullanım miktarı negatif olamaz.
- Son ödeme tarihi fatura tarihinden önceyse uyarı verilmelidir.
- PDF eklenmediyse kayıt yapılabilir ama “Fatura PDF Eksik” olarak raporlanmalıdır.

---

### 5.5. Fatura Listesi

Filtreler:

- Yıl
- Ay
- Fatura türü
- Abonelik
- Ödeme durumu
- Son ödeme tarihi aralığı
- Fatura tarihi aralığı
- PDF durumu
- Ödeme evrakı durumu
- Firma / sağlayıcı
- Fatura numarası arama

Kolonlar:

- Tür
- Abonelik
- Dönem
- Fatura tarihi
- Son ödeme tarihi
- Fatura no
- Tutar
- Kullanım miktarı
- Birim
- Ödeme durumu
- Ödeme tarihi
- Ödenen tutar
- Fatura PDF
- Ödeme PDF

---

### 5.6. Ödeme Kayıt Ekranı

İşlevler:

- Faturaya ödeme kaydı ekleme
- Ödeme evrakı PDF ekleme
- Ödeme evrakını açma
- Ödeme kaydını iptal etme

Zorunlu alanlar:

- Ödeme tarihi
- Ödenen tutar
- Ödeme yöntemi

Kontroller:

- Ödenen tutar fatura tutarından farklıysa uyarı gösterilmeli.
- Ödeme tarihi son ödeme tarihinden sonraysa “gecikmeli ödeme” bilgisi raporda gösterilmeli.
- Ödeme PDF’i yoksa kayıt yapılabilir ama “Ödeme Evrakı Eksik” olarak raporlanmalıdır.

---

## 6. Raporlar

### 6.1. Ödenmemiş Faturalar Raporu

Amaç:

Henüz ödeme kaydı bulunmayan faturaları listelemek.

Filtreler:

- Tüm yıllar
- Belirli yıl
- Belirli ay
- Fatura türü
- Abonelik
- Son ödeme tarihi yaklaşanlar
- Son ödeme tarihi geçenler

Kolonlar:

- Tür
- Abonelik
- Fatura tarihi
- Son ödeme tarihi
- Gecikme günü
- Fatura no
- Tutar
- Kullanım
- PDF durumu

---

### 6.2. Aylık Fatura Listesi

Örnek:

2026 Ocak ayında gelen tüm faturalar.

İçerik:

- Ay içindeki tüm fatura türleri
- Fatura bilgileri
- Ödeme bilgileri
- Fatura PDF durumu
- Ödeme evrakı durumu

Kolonlar:

- Tür
- Abonelik
- Fatura tarihi
- Son ödeme tarihi
- Fatura no
- Tutar
- Kullanım miktarı
- Ödeme durumu
- Ödeme tarihi
- Ödenen tutar
- Evrak durumu

Özet alanları:

- Toplam fatura sayısı
- Toplam fatura tutarı
- Ödenen toplam tutar
- Ödenmemiş toplam tutar
- Ödenmemiş fatura sayısı
- Evrakı eksik kayıt sayısı

---

### 6.3. Türe Özgü Aylık Fatura Listesi

Örnek:

2026 Ocak ayında gelen tüm doğalgaz faturaları.

Filtreler:

- Yıl
- Ay
- Fatura türü

İçerik:

- Aynı türdeki tüm aboneliklerin ilgili ay faturaları
- Ödeme bilgileri
- Kullanım miktarları
- Tutar toplamı

Özet:

- Abonelik sayısı
- Fatura sayısı
- Toplam tutar
- Toplam kullanım
- Ortalama fatura tutarı
- Ödenmemiş fatura sayısı

---

### 6.4. Aboneliğe Özgü Aylık Fatura Bilgisi

Bu bir liste değil, tek aboneliğe ait ayrıntılı aylık bilgi raporudur.

Örnek:

2026 Ocak — Doğalgaz Ana Bina Aboneliği

Raporda yer alacak bilgiler:

- Abonelik adı
- Fatura türü
- Abone numarası
- Tesisat / sayaç bilgisi
- Hizmet sağlayıcı firma
- Fatura tarihi
- Son ödeme tarihi
- Fatura numarası
- Tutar
- Kullanım miktarı
- Kullanım birimi
- Ödeme durumu
- Ödeme tarihi
- Ödeme yöntemi
- Ödenen tutar
- Fatura PDF durumu
- Ödeme evrakı durumu
- Açıklama

Ek analiz:

- Önceki aya göre tutar farkı
- Önceki aya göre kullanım farkı
- Aynı ayın önceki yılı varsa yıllık karşılaştırma

---

### 6.5. Aboneliğe Özgü Yıllık Fatura Listesi

Örnek:

Doğalgaz Ana Bina Aboneliği — 2026 yılı faturaları.

İçerik:

- Ocak-Aralık dönemleri
- Her ayın fatura bilgisi
- Kullanım miktarı
- Tutar
- Ödeme durumu
- Ödeme tarihi

Özet:

- Yıllık toplam tutar
- Yıllık toplam kullanım
- Ortalama aylık tutar
- En yüksek fatura ayı
- En düşük fatura ayı
- Ödenmemiş fatura sayısı
- Gecikmeli ödenen fatura sayısı

---

### 6.6. Türe Özgü Yıllık Fatura Listesi

Örnek:

2026 yılında gelen tüm doğalgaz faturaları.

İçerik:

- Doğalgaz türüne bağlı tüm abonelikler
- Yıl içindeki tüm faturalar
- Aylık toplamlar
- Abonelik bazlı dağılım

Özet:

- Yıllık toplam tutar
- Yıllık toplam kullanım
- Abonelik bazlı toplamlar
- En yüksek tüketimli abonelik
- En yüksek tutarlı abonelik
- Ödenmemiş fatura sayısı

---

### 6.7. Evrak Eksikliği Raporu

Amaç:

Fatura veya ödeme evrakı eksik olan kayıtları bulmak.

Alt raporlar:

- Fatura PDF’i eksik faturalar
- Ödeme evrakı eksik ödenmiş faturalar
- PDF yolu kayıtlı ama dosyası bulunamayan kayıtlar
- Aynı PDF dosyası birden fazla kayda bağlanmış olabilir uyarısı

---

### 6.8. Gecikmeli Ödeme Raporu

Amaç:

Son ödeme tarihinden sonra ödenmiş faturaları listelemek.

Kolonlar:

- Tür
- Abonelik
- Fatura no
- Son ödeme tarihi
- Ödeme tarihi
- Gecikme günü
- Tutar
- Ödeme yöntemi

---

## 7. Belge Yönetimi

### 7.1. Fatura PDF Kaydı

Fatura PDF’i kullanıcı tarafından seçildiğinde:

- Dosya uygulama klasörüne kopyalanmalıdır.
- Dosya adı güvenli biçimde yeniden adlandırılmalıdır.
- Orijinal dosya adı veritabanında saklanmalıdır.
- Dosya hash değeri alınmalıdır.
- Aynı dosya daha önce yüklenmişse uyarı verilmelidir.

Önerilen dosya adı formatı:

```text
FATURA_2026_01_DOGALGAZ_ABONEADI_FATURANO.pdf
```

### 7.2. Ödeme Evrakı PDF Kaydı

Ödeme evrakı için önerilen dosya adı formatı:

```text
ODEME_2026_01_DOGALGAZ_ABONEADI_FATURANO.pdf
```

### 7.3. Evrak Silme / Değiştirme

İlk sürümlerde fiziksel PDF silme yapılmamalıdır.

Güvenli yaklaşım:

- PDF değiştirilecekse eski dosya korunur.
- Yeni dosya bağlanır.
- Eski dosya pasif/arşiv olarak işaretlenir.
- İşlem geçmişine kayıt düşülür.

---

## 8. Güvenlik ve Veri Bütünlüğü Kuralları

- Aboneliğe bağlı fatura varsa abonelik silinmemelidir.
- Faturaya ödeme bağlıysa fatura doğrudan silinmemelidir.
- Fatura iptal edilecekse iptal sebebi zorunlu olmalıdır.
- Ödeme iptal edilecekse iptal sebebi zorunlu olmalıdır.
- Aynı abonelik + aynı fatura numarası tekrar kaydedilmemelidir.
- Aynı abonelik + aynı yıl + aynı ay için birden fazla fatura girilecekse kullanıcıdan güçlü onay alınmalıdır.
- PDF dosyası uygulama klasörüne kopyalanmadan yalnızca dış dosya yoluna bağlanmamalıdır.
- Veritabanında para alanları decimal olarak tutulmalıdır.
- Tarihler boş geçilmemelidir.
- Tutar ve kullanım miktarı negatif olamaz.

---

## 9. Kullanıcı Arayüzü İlkeleri

- Program sade ve resmi kullanıma uygun olmalıdır.
- Sol menü veya üst menü yapısı net olmalıdır.
- Listelerde filtre alanları üstte yer almalıdır.
- Rapor ekranlarında önce özet kartları, altında detay listesi olmalıdır.
- PDF açma butonları doğrudan ilgili kaydın yanında bulunmalıdır.
- Ödeme durumu renkli rozetlerle gösterilebilir:
  - Ödendi
  - Ödenmedi
  - Gecikmiş
  - İptal
- Evrak durumu ayrı rozetlerle gösterilebilir:
  - Fatura PDF Var
  - Fatura PDF Eksik
  - Ödeme Evrakı Var
  - Ödeme Evrakı Eksik

---

## 10. Arama ve Filtreleme

Programda güçlü filtreleme olmalıdır.

Genel fatura listesinde aranabilecek alanlar:

- Fatura numarası
- Abone numarası
- Abonelik adı
- Firma adı
- Tür
- Yıl
- Ay
- Tutar aralığı
- Son ödeme tarihi aralığı
- Ödeme durumu
- Evrak durumu

---

## 11. Dışa Aktarım

Raporlar ilerleyen sürümlerde dışa aktarılabilir olmalıdır.

Önerilen dışa aktarım türleri:

- Excel
- PDF
- Yazdırılabilir rapor

İlk aşamada Excel dışa aktarımı daha pratik olabilir. PDF raporlar sonraki faza bırakılabilir.

---

## 12. Yedekleme

Programda manuel güvenli yedekleme bulunmalıdır.

Yedek içine alınacaklar:

- SQLite veritabanı
- Fatura PDF evrakları
- Ödeme PDF evrakları
- Yedek bilgi dosyası

Yedek dosya adı:

```text
fatura_takip_guvenli_yedek_yyyy-MM-dd_HH-mm-ss.zip
```

İlk aşamada yalnızca manuel yedekleme yapılmalıdır. Otomatik yedekleme sonraki fazlara bırakılabilir.

---

## 13. Geliştirme Fazları

### v0.1 — Proje İskeleti ve Veritabanı

Kapsam:

- WPF proje yapısı
- SQLite bağlantısı
- Ana klasör yapısı
- Temel tabloların oluşturulması
- Basit ana ekran
- Veritabanı ilk migration mantığı

Bu sürümde fatura kaydı yapılmayacaktır. Sadece temel altyapı hazırlanacaktır.

---

### v0.2 — Fatura Türleri Yönetimi

Kapsam:

- Fatura türü ekleme
- Fatura türü düzenleme
- Aktif/pasif yapma
- Varsayılan kullanım birimi

Bu fazda abonelik ve fatura kayıtlarına dokunulmayacaktır.

---

### v0.3 — Abonelik Yönetimi

Kapsam:

- Abonelik ekleme
- Abonelik düzenleme
- Aktif/pasif yapma
- Fatura türüne bağlama
- Abonelik listesi filtreleme

Bu fazda fatura kaydı yapılmayacaktır.

---

### v0.4 — Fatura Kayıt Altyapısı

Kapsam:

- Fatura ekleme
- Fatura düzenleme
- Abonelik seçimi
- Dönem yılı/ayı
- Fatura tarihi
- Son ödeme tarihi
- Fatura no
- Tutar
- Kullanım miktarı
- Kullanım birimi
- Açıklama

Bu fazda PDF ekleme sonraki faza bırakılabilir.

---

### v0.5 — Fatura PDF Evrakı Ekleme

Kapsam:

- Faturaya PDF ekleme
- PDF’i uygulama klasörüne kopyalama
- PDF yolunu veritabanına kaydetme
- PDF açma
- Dosya varlık kontrolü
- PDF eksik raporu için altyapı

---

### v0.6 — Fatura Listesi ve Filtreleme

Kapsam:

- Yıl/ay filtresi
- Tür filtresi
- Abonelik filtresi
- Ödeme durumu filtresi
- PDF durumu filtresi
- Fatura no arama
- Detay görüntüleme

---

### v0.7 — Ödeme Kayıt Altyapısı

Kapsam:

- Faturaya ödeme ekleme
- Ödeme tarihi
- Ödenen tutar
- Ödeme yöntemi
- Ödeme belge no
- Açıklama
- Fatura durumunu ödendi olarak gösterme

---

### v0.8 — Ödeme Evrakı PDF Ekleme

Kapsam:

- Ödeme PDF’i ekleme
- Ödeme PDF’ini uygulama klasörüne kopyalama
- Ödeme PDF’ini açma
- Ödeme evrakı eksik raporu için altyapı

---

### v0.9 — Ana Gösterge Paneli

Kapsam:

- Bu ay gelen fatura sayısı
- Bu ay toplam fatura tutarı
- Ödenmemiş fatura sayısı
- Gecikmiş fatura sayısı
- Evrakı eksik kayıt sayısı
- Yaklaşan son ödeme tarihleri

---

### v0.10 — Ödenmemiş ve Gecikmiş Faturalar Raporu

Kapsam:

- Ödenmemiş faturalar listesi
- Son ödeme tarihi geçmiş faturalar listesi
- Yaklaşan son ödeme tarihli faturalar
- Özet toplamlar

---

### v0.11 — Aylık Fatura Listesi

Kapsam:

- Seçilen yıl ve ay için tüm faturalar
- Fatura bilgileri
- Ödeme bilgileri
- Evrak bilgileri
- Aylık toplamlar

---

### v0.12 — Türe Özgü Aylık Fatura Listesi

Kapsam:

- Seçilen tür + yıl + ay
- Aynı türdeki tüm abonelik faturaları
- Toplam tutar
- Toplam kullanım
- Ödeme durumu özeti

---

### v0.13 — Aboneliğe Özgü Aylık Fatura Bilgisi

Kapsam:

- Tek aboneliğin seçilen ay detay raporu
- Fatura bilgisi
- Ödeme bilgisi
- Evrak bilgisi
- Önceki ay karşılaştırması

---

### v0.14 — Aboneliğe Özgü Yıllık Fatura Listesi

Kapsam:

- Tek aboneliğin 12 aylık fatura listesi
- Yıllık toplam tutar
- Yıllık toplam kullanım
- En yüksek / en düşük ay
- Eksik ay kontrolü

---

### v0.15 — Türe Özgü Yıllık Fatura Listesi

Kapsam:

- Seçilen türün yıllık tüm faturaları
- Abonelik bazlı toplamlar
- Aylık toplamlar
- Kullanım ve tutar karşılaştırması

---

### v0.16 — Evrak Eksikliği ve Dosya Kontrol Raporu

Kapsam:

- Fatura PDF’i eksik kayıtlar
- Ödeme PDF’i eksik kayıtlar
- Dosya yolu olup fiziksel dosyası bulunmayan kayıtlar
- Aynı dosya hash değerine sahip kayıt uyarısı

---

### v0.17 — Excel Dışa Aktarım

Kapsam:

- Fatura listesi Excel çıktısı
- Aylık rapor Excel çıktısı
- Türe özgü rapor Excel çıktısı
- Abonelik yıllık rapor Excel çıktısı

---

### v0.18 — Yazdırılabilir PDF Raporlar

Kapsam:

- Aylık fatura raporu PDF
- Ödenmemiş faturalar PDF
- Abonelik yıllık raporu PDF
- Türe özgü yıllık rapor PDF

---

### v0.19 — Manuel Güvenli Yedekleme

Kapsam:

- Veritabanı yedeği
- PDF evrakları yedeği
- ZIP yedek oluşturma
- Yedek bilgi dosyası
- Yedek geçmişi listesi

---

### v0.20 — Tutarlılık Denetimi

Kapsam:

- Faturası olup aboneliği pasif olan kayıtlar
- PDF yolu olup dosyası olmayan kayıtlar
- Ödeme var ama fatura durumu ödenmedi görünen kayıtlar
- Ödenmiş fatura ama ödeme evrakı eksik kayıtlar
- Aynı fatura numarası tekrarları
- Negatif veya sıfır tutarlı şüpheli kayıtlar

Bu rapor salt okunur olmalıdır. Otomatik düzeltme yapmamalıdır.

---

## 14. Codex ile Çalışma Stratejisi

Codex’e tek seferde tüm program yaptırılmamalıdır. Her faz küçük, kontrollü ve test edilebilir olmalıdır.

Her faz için Codex’e şu formatta görev verilmelidir:

```text
Mevcut güvenli sürüm: v0.x

Bu sürümde yalnızca şu kapsam uygulanacak:
- ...
- ...
- ...

Şunlara kesinlikle dokunulmayacak:
- ...
- ...
- ...

Beklenen çıktı:
- Çalışan proje
- Değiştirilen dosyaların özeti
- Test notları
- Yeni sürüm notu
```

Her fazdan sonra manuel test yapılmalı, başarılıysa güvenli sürüm paketi oluşturulmalıdır.

---

## 15. İlk Codex Komutu İçin Önerilen Başlangıç Talimatı

```text
C# WPF + SQLite tabanlı bir kurum fatura takip programı geliştireceğiz.

Program elektrik, su, doğalgaz, telefon, internet gibi kurum aboneliklerine ait faturaları takip edecek. Geliştirme küçük ve güvenli fazlarla yapılacak. Bu ilk fazda yalnızca proje iskeleti, klasör yapısı, SQLite veritabanı bağlantısı ve temel boş ana ekran hazırlanacak.

Bu fazda fatura türü, abonelik, fatura, ödeme veya rapor ekranı geliştirilmeyecek. Sadece altyapı hazırlanacak.

Hedef:
- WPF masaüstü uygulama oluştur.
- SQLite kullanılacak.
- database, attachments, backups, logs, exports klasörlerini oluşturacak altyapı kur.
- Veritabanı dosyası database/fatura_takip.db olacak.
- Migration mantığı için başlangıç sınıfı hazırla.
- Ana ekran sade bir dashboard taslağı olarak açılsın.
- Henüz gerçek veri girişi yapılmasın.
- Kod okunabilir, modüler ve sonraki fazlara uygun olsun.

Bu fazda hiçbir rapor, ödeme, PDF evrak ekleme veya abonelik yönetimi yapılmayacak.
```

---

## 16. Net Geliştirme İlkesi

Bu programda en güvenli geliştirme yöntemi şudur:

1. Önce abonelik altyapısı kurulacak.
2. Sonra fatura kaydı yapılacak.
3. Sonra PDF fatura evrakı eklenecek.
4. Sonra ödeme kaydı yapılacak.
5. Sonra ödeme evrakı eklenecek.
6. Sonra listeleme ve filtreleme güçlendirilecek.
7. Sonra raporlar eklenecek.
8. Sonra dışa aktarım ve yedekleme eklenecek.
9. En son tutarlılık denetimi ve gelişmiş kontroller eklenecek.

Fatura takip programında en kritik veri yapısı aboneliktir. Bu nedenle işlemler doğrudan fatura türü üzerinden değil, mutlaka abonelik üzerinden yürütülmelidir.

Doğru ana ilişki şu olmalıdır:

```text
Fatura Türü → Abonelik → Fatura → Ödeme → Evraklar
```

Bu ilişki bozulmadığı sürece program düzenli, genişletilebilir ve güvenli şekilde geliştirilebilir.
