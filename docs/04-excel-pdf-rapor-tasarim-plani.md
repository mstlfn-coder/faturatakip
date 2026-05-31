# Kurum Fatura Takip Programı — Excel ve PDF Rapor Tasarım Planı

Kaynak dosya (repo iÃ§ine kopyalandÄ±): `docs/05-excel-pdf-rapor-tasarim-plani-kaynak.md`

Bu doküman, **Kurum Fatura Takip Programı** için üretilecek Excel ve PDF raporlarının tasarım kapsamını belirler.

Notlar:

- Bu repo içinde Excel/PDF rapor geliştirmesi yapılacağı zaman, önce kullanıcıdan **Excel rapor örneği** istenmeden PDF çıktısı tasarlanmayacak/uygulanmayacak.
- Bu doküman özellikle `v0.17` (Excel dışa aktarım) ve `v0.18` (yazdırılabilir PDF raporlar) fazlarında ana referans olarak kullanılacak.

---

## 1. Genel Rapor Yaklaşımı

Programdan alınacak raporlar iki ana çıktı türünde tasarlanacaktır:

1. **Excel Raporları**
   - Filtreleme, sıralama, hesaplama ve detay inceleme için kullanılacaktır.
   - Çok satırlı veri listeleri için uygundur.
   - Kullanıcı Excel üzerinde ek analiz yapabilecektir.
   - Kolon sayısı PDF raporlarına göre daha geniş olabilir.
   - Veri doğrulama ve test için ilk öncelik Excel raporlarında olmalıdır.

2. **PDF Raporları**
   - Resmi çıktı, arşiv, imza, kontrol ve dosyalama için kullanılacaktır.
   - Daha düzenli, sade ve yazdırılabilir formatta olacaktır.
   - PDF raporlar mümkün olduğunca A4 sayfa düzenine uygun hazırlanacaktır.
   - PDF çıktılar Excel raporlar oturduktan sonra geliştirilmelidir.

Net öncelik sırası:

```text
Önce Excel raporlar → Sonra PDF raporlar
```

---

## 2. Ortak Rapor Tasarım Standardı

### 2.1. Ortak Başlık Alanı

Tüm PDF ve Excel raporlarında üst bölümde şu bilgiler yer almalıdır:

- Kurum adı
- Rapor adı
- Rapor dönemi
- Rapor tarihi
- Raporu oluşturan kullanıcı
- Filtre bilgileri

Örnek başlık:

```text
KURUM FATURA TAKİP PROGRAMI
2026 OCAK AYI FATURA LİSTESİ

Kurum       : ........................................
Dönem       : Ocak 2026
Rapor Tarihi: 31.05.2026
Filtre      : Tüm Fatura Türleri / Tüm Abonelikler
```

### 2.2. Ortak Özet Alanı

Raporların üst kısmında mümkün olduğunca özet kartlar veya özet satırlar bulunmalıdır.

Ortak özet alanları:

- Toplam fatura sayısı
- Toplam fatura tutarı
- Ödenen toplam tutar
- Ödenmemiş toplam tutar
- Ödenmiş fatura sayısı
- Ödenmemiş fatura sayısı
- Gecikmiş fatura sayısı
- Fatura PDF'i eksik kayıt sayısı
- Ödeme evrakı eksik kayıt sayısı

### 2.3. Ortak Alt Bilgi Alanı

PDF raporların alt kısmında şu bilgiler yer almalıdır:

```text
Bu rapor Kurum Fatura Takip Programı tarafından oluşturulmuştur.
Sayfa: 1 / 3
```

Opsiyonel imza alanı:

```text
Hazırlayan                         Kontrol Eden
Ad Soyad / İmza                    Ad Soyad / İmza
```

---

## 3. Excel Rapor Genel Formatı

### 3.1. Excel Sayfa Yapısı

Her Excel raporu mümkünse şu yapı ile oluşturulmalıdır:

- 1. satır: Rapor başlığı
- 2-5. satırlar: Rapor filtre ve özet bilgileri
- 7. satır: Tablo başlıkları
- 8. satır ve sonrası: Veri listesi
- Son satır: Genel toplamlar

Bu dosya kaynak planın tamamının bir kopyası değildir; ana içerik, kullanıcı tarafından sağlanan kaynak dokümanın özetlenmiş halidir.

