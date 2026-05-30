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

## Açık Kararlar

Bu başlıklar ilk uygulama fazlarında netleştirilecek:

- .NET sürümü
- WPF proje adı ve namespace yapısı
- ORM kullanılıp kullanılmayacağı
- SQLite migration yaklaşımı
- Dosya hash algoritması
- UI tema ve bileşen düzeni
