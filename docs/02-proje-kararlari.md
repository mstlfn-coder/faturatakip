# Proje Kararları

Bu dosya, geliştirme sırasında alınan kalıcı kararları kaydeder. Kararlar değişirse yeni satır eklenmeli; eski karar sessizce silinmemeli.

| Tarih | Karar | Gerekçe |
|---|---|---|
| 2026-05-30 | Uygulama C# / .NET / WPF olarak geliştirilecek | Kurum içi masaüstü kullanım için uygun |
| 2026-05-30 | Veritabanı SQLite olacak | Tek bilgisayar veya ortak klasör senaryosu için hafif ve taşınabilir |
| 2026-05-30 | PDF evraklar veritabanına gömülmeyecek | Veritabanını küçük, yedeklemeyi ve dosya kontrolünü anlaşılır tutar |
| 2026-05-30 | Proje içi yeni dosya adları mümkün olduğunca ASCII olacak | Türkçe karakterli ana klasör yolunda ek risk oluşturmamak için |
| 2026-05-30 | Abonelikler ve faturalar fiziksel silme yerine pasif/iptal mantığıyla yönetilecek | Geçmiş raporlar ve denetlenebilirlik korunur |
| 2026-05-30 | Gecikmiş fatura bilgisi öncelikle son ödeme tarihinden dinamik hesaplanacak | Veritabanındaki durum bilgisini sade tutar |
| 2026-05-30 | Her faz sonunda roadmap, regresyon ve devam notları güncellenecek | Yeni Codex chatinde bağlam kaybını önler |
| 2026-05-30 | İlk uygulama hedefi `net8.0-windows` olacak | Makinede .NET 8 SDK ve Windows Desktop runtime mevcut |
| 2026-05-30 | İlk SQLite erişimi `Microsoft.Data.Sqlite` ile doğrudan yapılacak | v0.1 için hafif ve anlaşılır migration altyapısı yeterli |
| 2026-05-30 | Migration kayıtları `schema_migrations` tablosunda tutulacak | Tekrar çalıştırılabilir ve izlenebilir başlangıç sağlar |
| 2026-05-30 | Fatura türleri fiziksel silinmeyecek, aktif/pasif yapılacak | Geçmiş kayıtlarla ilişki kurulacağı için referans bütünlüğü korunmalı |
| 2026-05-30 | Fatura türü adları büyük/küçük harfe duyarsız benzersiz olacak | Aynı türün farklı yazımlarla tekrarlanmasını önler |
| 2026-05-30 | Veri katmanı için şimdilik doğrudan repository sınıfları kullanılacak | Erken fazlarda ORM eklemeden davranışı sade tutar |
| 2026-05-30 | Abonelikler fiziksel silinmeyecek, aktif/pasif yapılacak | Gelecekte faturalar aboneliklere bağlanacağı için geçmiş ilişki korunmalı |
| 2026-05-30 | Abonelikler zorunlu olarak fatura türüne bağlanacak | Ana veri ilişkisi `Fatura Türü -> Abonelik -> Fatura` şeklinde korunmalı |
| 2026-05-31 | Faturalar aboneliğe ve aboneliğin fatura türüne bağlanacak | Raporlama kolaylaşır, abonelik ana veri ilişkisi korunur |
| 2026-05-31 | Fatura tutarı ve kullanım miktarı uygulama katmanında decimal doğrulanacak | Negatif değerler engellenir ve para hesabı hassasiyeti korunur |
| 2026-05-31 | Aynı abonelikte aynı fatura numarası benzersiz olacak | Çift kayıt riskini azaltır |
| 2026-05-31 | Fatura PDF raporları tasarlanmadan önce kullanıcıdan Excel örneği istenecek | Rapor kolonları, toplamlar, sayfa düzeni ve çıktı beklentisi örneğe göre netleştirilecek |

## Açık Kararlar

Bu başlıklar ilk uygulama fazlarında netleştirilecek:

- Dosya hash algoritması
- UI tema ve bileşen düzeni
