# Kurum Fatura Takip Programı — Excel ve PDF Rapor Tasarım Planı

Bu doküman, **Kurum Fatura Takip Programı** için üretilecek Excel ve PDF raporlarının tasarım kapsamını belirler.

Ana geliştirme planındaki fatura takip sistemi tamamlandıktan veya rapor altyapısı aşamasına gelindikten sonra Codex bu dokümana göre rapor ekranlarını, Excel çıktılarını ve PDF çıktılarını geliştirecektir.

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
   - PDF çıktılar Excel raporları oturduktan sonra geliştirilmelidir.

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

---

### 2.2. Ortak Özet Alanı

Raporların üst kısmında mümkün olduğunca özet kartları veya özet satırları bulunmalıdır.

Ortak özet alanları:

- Toplam fatura sayısı
- Toplam fatura tutarı
- Ödenen toplam tutar
- Ödenmemiş toplam tutar
- Ödenmiş fatura sayısı
- Ödenmemiş fatura sayısı
- Gecikmiş fatura sayısı
- Fatura PDF’i eksik kayıt sayısı
- Ödeme evrakı eksik kayıt sayısı

Her raporda bu alanların tamamı zorunlu değildir. Raporun amacına göre uygun özetler kullanılmalıdır.

---

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

İmza alanı ilk sürümlerde zorunlu olmayabilir. Ancak PDF rapor altyapısı buna uygun tasarlanmalıdır.

---

## 3. Excel Rapor Genel Formatı

### 3.1. Excel Sayfa Yapısı

Her Excel raporu mümkünse şu yapı ile oluşturulmalıdır:

- 1. satır: Rapor başlığı
- 2-5. satırlar: Rapor filtre ve özet bilgileri
- 7. satır: Tablo başlıkları
- 8. satır ve sonrası: Veri listesi
- Son satır: Genel toplamlar

Önerilen genel yapı:

```text
1. Satır   : Rapor Başlığı
2. Satır   : Kurum Bilgisi
3. Satır   : Rapor Dönemi
4. Satır   : Filtre Bilgisi
5. Satır   : Rapor Oluşturma Tarihi
6. Satır   : Boş satır
7. Satır   : Tablo başlıkları
8+ Satırlar: Veri
Son Satır  : Toplamlar
```

---

### 3.2. Excel Biçimlendirme Kuralları

- Başlık satırı kalın olmalıdır.
- Tablo başlıkları koyu zemin + açık yazı şeklinde olmalıdır.
- Para alanları `₺` formatında gösterilmelidir.
- Tarihler `dd.MM.yyyy` formatında olmalıdır.
- Tutar kolonları sağa hizalı olmalıdır.
- Kullanım miktarı kolonları sağa hizalı olmalıdır.
- Metin kolonları sola hizalı olmalıdır.
- Başlık satırında filtre aktif olmalıdır.
- İlk 7 satır dondurulabilir.
- Kolon genişlikleri içeriğe göre ayarlanmalıdır.
- Ödenmemiş/gecikmiş kayıtlar belirgin işaretlenmelidir.
- Toplam satırı kalın olmalıdır.
- Sayısal kolonlarda boş değer yerine mümkünse `0` veya `-` gösterimi tercih edilmelidir.

---

### 3.3. Excel Dosya Adı Standardı

Örnek dosya adları:

```text
Aylik_Fatura_Listesi_2026_01.xlsx
Ture_Ozgu_Aylik_Fatura_Listesi_Dogalgaz_2026_01.xlsx
Abonelik_Aylik_Fatura_Bilgisi_Dogalgaz_Ana_Bina_2026_01.xlsx
Abonelik_Yillik_Fatura_Listesi_Dogalgaz_Ana_Bina_2026.xlsx
Ture_Ozgu_Yillik_Fatura_Listesi_Dogalgaz_2026.xlsx
Odenmemis_Faturalar_Raporu_2026_05_31.xlsx
Evrak_Eksikligi_Raporu_2026_05_31.xlsx
Gecikmeli_Odeme_Raporu_2026_05_31.xlsx
```

Dosya adlarında Türkçe karakter kullanılmaması önerilir. Böylece farklı sistemlerde dosya adı uyumluluğu artar.

---

## 4. PDF Rapor Genel Formatı

### 4.1. PDF Sayfa Yapısı

PDF raporlar iki tür olabilir:

#### A) Liste Raporları

Çok satırlı fatura listeleri için kullanılır.

Önerilen sayfa yönü:

- A4 yatay

Kullanılacak raporlar:

- Aylık Fatura Listesi
- Türe Özgü Aylık Fatura Listesi
- Aboneliğe Özgü Yıllık Fatura Listesi
- Türe Özgü Yıllık Fatura Listesi
- Ödenmemiş Faturalar Raporu
- Gecikmeli Ödeme Raporu
- Evrak Eksikliği Raporu

#### B) Detay Raporları

Tek abonelik veya tek fatura için kullanılır.

Önerilen sayfa yönü:

- A4 dikey

Kullanılacak raporlar:

- Aboneliğe Özgü Aylık Fatura Bilgisi
- Tek Fatura Detay Raporu
- Abonelik Bilgi Kartı

---

### 4.2. PDF Biçimlendirme Kuralları

- Başlık üstte ortalı olmalıdır.
- Kurum adı ve rapor adı belirgin olmalıdır.
- Filtre bilgileri başlığın altında gösterilmelidir.
- Özet kutuları tablo başlamadan önce yer almalıdır.
- Liste raporlarında tablo başlıkları tekrar eden sayfalarda korunmalıdır.
- Sayfa numarası bulunmalıdır.
- Gereksiz renk kullanılmamalıdır.
- Resmi çıktı için sade ve okunabilir görünüm tercih edilmelidir.
- Çok kolonlu listelerde A4 yatay kullanılmalıdır.
- Tek fatura/abonelik detay raporlarında A4 dikey kullanılmalıdır.

---

### 4.3. PDF Dosya Adı Standardı

Örnek dosya adları:

```text
Aylik_Fatura_Listesi_2026_01.pdf
Ture_Ozgu_Aylik_Fatura_Listesi_Dogalgaz_2026_01.pdf
Abonelik_Aylik_Fatura_Bilgisi_Dogalgaz_Ana_Bina_2026_01.pdf
Abonelik_Yillik_Fatura_Listesi_Dogalgaz_Ana_Bina_2026.pdf
Ture_Ozgu_Yillik_Fatura_Listesi_Dogalgaz_2026.pdf
Odenmemis_Faturalar_Raporu_2026_05_31.pdf
Evrak_Eksikligi_Raporu_2026_05_31.pdf
Gecikmeli_Odeme_Raporu_2026_05_31.pdf
Tek_Fatura_Detay_Raporu_FaturaNo.pdf
Abonelik_Bilgi_Karti_AbonelikAdi.pdf
```

---

# 5. Rapor 1 — Aylık Fatura Listesi

## 5.1. Amaç

Seçilen ayda kuruma gelen tüm faturaların, ödeme ve evrak bilgileriyle birlikte listelenmesidir.

Örnek:

```text
2026 Ocak Ayı Fatura Listesi
```

---

## 5.2. Excel Kolonları

| Sıra | Tür | Abonelik | Firma | Dönem | Fatura Tarihi | Son Ödeme Tarihi | Fatura No | Tutar | Kullanım | Birim | Ödeme Durumu | Ödeme Tarihi | Ödenen Tutar | Fatura PDF | Ödeme PDF | Açıklama |
|---:|---|---|---|---|---|---|---|---:|---:|---|---|---|---:|---|---|---|

---

## 5.3. PDF Kolonları

PDF’de daha dar kolon kullanılmalıdır.

| Sıra | Tür | Abonelik | Fatura No | Fatura Tarihi | Son Ödeme | Tutar | Kullanım | Durum | Ödeme Tarihi | Evrak |
|---:|---|---|---|---|---|---:|---:|---|---|---|

---

## 5.4. Özet Alanı

- Toplam fatura sayısı
- Toplam fatura tutarı
- Ödenen toplam tutar
- Ödenmemiş toplam tutar
- Ödenmiş fatura sayısı
- Ödenmemiş fatura sayısı
- Gecikmiş fatura sayısı
- Fatura PDF eksik sayısı
- Ödeme PDF eksik sayısı

---

## 5.5. PDF Sayfa Düzeni

- A4 yatay
- Üstte kurum ve rapor başlığı
- Altında filtre bilgileri
- Özet kutuları
- Detay tablo
- En altta genel toplam ve sayfa numarası

---

# 6. Rapor 2 — Türe Özgü Aylık Fatura Listesi

## 6.1. Amaç

Belirli bir fatura türünün, seçilen ayda gelen tüm faturalarını listelemektir.

Örnek:

```text
2026 Ocak Ayı Doğalgaz Faturaları
```

Bu raporda birden fazla doğalgaz aboneliği olabilir.

---

## 6.2. Excel Kolonları

| Sıra | Abonelik | Abone No | Sayaç/Tesisat No | Firma | Fatura Tarihi | Son Ödeme | Fatura No | Tutar | Kullanım | Birim | Ödeme Durumu | Ödeme Tarihi | Ödenen Tutar | Fatura PDF | Ödeme PDF |
|---:|---|---|---|---|---|---|---|---:|---:|---|---|---|---:|---|---|

---

## 6.3. PDF Kolonları

| Sıra | Abonelik | Fatura No | Son Ödeme | Tutar | Kullanım | Durum | Ödeme |
|---:|---|---|---|---:|---:|---|---|

---

## 6.4. Özet Alanı

- Fatura türü
- Dönem
- Abonelik sayısı
- Fatura sayısı
- Toplam tutar
- Toplam kullanım
- Ortalama fatura tutarı
- Ödenmemiş fatura sayısı
- Gecikmiş fatura sayısı

---

# 7. Rapor 3 — Aboneliğe Özgü Aylık Fatura Bilgisi

## 7.1. Amaç

Tek bir aboneliğin seçilen aya ait faturasını ayrıntılı şekilde göstermektir.

Bu rapor liste değil, detay raporudur.

Örnek:

```text
Doğalgaz Ana Bina Aboneliği — 2026 Ocak Fatura Bilgisi
```

---

## 7.2. PDF Bölümleri

### Bölüm 1 — Abonelik Bilgileri

| Alan | Değer |
|---|---|
| Fatura Türü | Doğalgaz |
| Abonelik Adı | Ana Bina Doğalgaz |
| Abone No | ... |
| Tesisat No | ... |
| Sayaç No | ... |
| Firma | ... |
| Hizmet Adresi | ... |

### Bölüm 2 — Fatura Bilgileri

| Alan | Değer |
|---|---|
| Dönem | Ocak 2026 |
| Fatura Tarihi | ... |
| Son Ödeme Tarihi | ... |
| Fatura No | ... |
| Fatura Tutarı | ... |
| Kullanım Miktarı | ... |
| Kullanım Birimi | m³ |
| Fatura PDF Durumu | Var / Eksik |

### Bölüm 3 — Ödeme Bilgileri

| Alan | Değer |
|---|---|
| Ödeme Durumu | Ödendi / Ödenmedi |
| Ödeme Tarihi | ... |
| Ödenen Tutar | ... |
| Ödeme Yöntemi | ... |
| Ödeme Evrakı | Var / Eksik |
| Gecikme Durumu | Zamanında / Gecikmeli |

### Bölüm 4 — Karşılaştırma

| Karşılaştırma | Tutar Farkı | Kullanım Farkı |
|---|---:|---:|
| Önceki Aya Göre | ... | ... |
| Önceki Yıl Aynı Aya Göre | ... | ... |

---

## 7.3. Excel Formatı

Bu rapor Excel’de iki sayfa olarak üretilebilir:

- Sayfa 1: Detay Bilgi
- Sayfa 2: Karşılaştırma

### Sayfa 1 — Detay Bilgi

| Alan | Değer |
|---|---|
| Fatura Türü | ... |
| Abonelik Adı | ... |
| Abone No | ... |
| Tesisat No | ... |
| Sayaç No | ... |
| Firma | ... |
| Dönem | ... |
| Fatura Tarihi | ... |
| Son Ödeme Tarihi | ... |
| Fatura No | ... |
| Fatura Tutarı | ... |
| Kullanım Miktarı | ... |
| Ödeme Durumu | ... |
| Ödeme Tarihi | ... |
| Ödenen Tutar | ... |
| Ödeme Yöntemi | ... |
| Fatura PDF | ... |
| Ödeme PDF | ... |

### Sayfa 2 — Karşılaştırma

| Karşılaştırma | Önceki Değer | Mevcut Değer | Fark | Fark Yüzdesi |
|---|---:|---:|---:|---:|
| Önceki Aya Göre Tutar | ... | ... | ... | ... |
| Önceki Aya Göre Kullanım | ... | ... | ... | ... |
| Önceki Yıl Aynı Aya Göre Tutar | ... | ... | ... | ... |
| Önceki Yıl Aynı Aya Göre Kullanım | ... | ... | ... | ... |

---

# 8. Rapor 4 — Aboneliğe Özgü Yıllık Fatura Listesi

## 8.1. Amaç

Tek aboneliğin yıl boyunca gelen faturalarını ay ay göstermektir.

Örnek:

```text
Ana Bina Doğalgaz Aboneliği — 2026 Yıllık Fatura Listesi
```

---

## 8.2. Excel Kolonları

| Ay | Fatura Tarihi | Son Ödeme | Fatura No | Tutar | Kullanım | Birim | Ödeme Durumu | Ödeme Tarihi | Ödenen Tutar | Gecikme Günü | Fatura PDF | Ödeme PDF |
|---|---|---|---|---:|---:|---|---|---|---:|---:|---|---|

---

## 8.3. PDF Kolonları

| Ay | Fatura No | Son Ödeme | Tutar | Kullanım | Durum | Ödeme Tarihi | Evrak |
|---|---|---|---:|---:|---|---|---|

---

## 8.4. Özet Alanı

- Yıllık toplam tutar
- Yıllık toplam kullanım
- Ortalama aylık tutar
- Ortalama aylık kullanım
- En yüksek fatura ayı
- En düşük fatura ayı
- En yüksek kullanım ayı
- Eksik fatura girilen aylar
- Ödenmemiş fatura sayısı
- Gecikmeli ödeme sayısı

---

## 8.5. Ek Grafik Önerisi

Excel raporunda isteğe bağlı grafik eklenebilir:

- Aylara göre fatura tutarı grafiği
- Aylara göre kullanım miktarı grafiği

PDF’de grafik ilk aşamada zorunlu değildir. Sonraki sürümlere bırakılabilir.

---

# 9. Rapor 5 — Türe Özgü Yıllık Fatura Listesi

## 9.1. Amaç

Belirli bir fatura türüne ait yıl içindeki tüm faturaları listelemektir.

Örnek:

```text
2026 Yılı Doğalgaz Faturaları
```

---

## 9.2. Excel Kolonları

| Ay | Abonelik | Abone No | Firma | Fatura No | Fatura Tarihi | Son Ödeme | Tutar | Kullanım | Birim | Ödeme Durumu | Ödeme Tarihi | Fatura PDF | Ödeme PDF |
|---|---|---|---|---|---|---|---:|---:|---|---|---|---|---|

---

## 9.3. PDF Kolonları

| Ay | Abonelik | Fatura No | Tutar | Kullanım | Durum | Ödeme |
|---|---|---|---:|---:|---|---|

---

## 9.4. Özet Alanı

- Fatura türü
- Yıl
- Abonelik sayısı
- Toplam fatura sayısı
- Yıllık toplam tutar
- Yıllık toplam kullanım
- Aylık ortalama tutar
- Aylık ortalama kullanım
- En yüksek tutarlı abonelik
- En yüksek kullanımlı abonelik
- Ödenmemiş fatura sayısı

---

## 9.5. Excel Ek Sayfaları

Bu rapor Excel’de çok sayfalı olabilir:

- Sayfa 1: Detay Liste
- Sayfa 2: Aylık Toplamlar
- Sayfa 3: Abonelik Bazlı Toplamlar

### Sayfa 2 — Aylık Toplamlar

| Ay | Fatura Sayısı | Toplam Tutar | Toplam Kullanım | Ödenmiş Tutar | Ödenmemiş Tutar |
|---|---:|---:|---:|---:|---:|

### Sayfa 3 — Abonelik Bazlı Toplamlar

| Abonelik | Fatura Sayısı | Toplam Tutar | Toplam Kullanım | Ödenmemiş Fatura | Gecikmeli Ödeme |
|---|---:|---:|---:|---:|---:|

---

# 10. Rapor 6 — Ödenmemiş Faturalar Raporu

## 10.1. Amaç

Henüz ödeme kaydı bulunmayan faturaların listelenmesidir.

---

## 10.2. Excel Kolonları

| Sıra | Tür | Abonelik | Firma | Fatura Tarihi | Son Ödeme Tarihi | Kalan Gün | Gecikme Günü | Fatura No | Tutar | Kullanım | Fatura PDF | Açıklama |
|---:|---|---|---|---|---|---:|---:|---|---:|---:|---|---|

---

## 10.3. PDF Kolonları

| Sıra | Tür | Abonelik | Fatura No | Son Ödeme | Tutar | Durum | Gecikme |
|---:|---|---|---|---|---:|---|---:|

---

## 10.4. Durum Mantığı

- Son ödeme tarihi geçmemişse: Ödenmemiş
- Son ödeme tarihi bugünse: Bugün son gün
- Son ödeme tarihi geçmişse: Gecikmiş
- Son ödeme tarihine 3 gün veya daha az kaldıysa: Yaklaşıyor

---

## 10.5. Özet Alanı

- Toplam ödenmemiş fatura sayısı
- Toplam ödenmemiş tutar
- Gecikmiş fatura sayısı
- Gecikmiş toplam tutar
- Son ödeme tarihi yaklaşan fatura sayısı

---

# 11. Rapor 7 — Evrak Eksikliği Raporu

## 11.1. Amaç

Fatura PDF’i veya ödeme PDF’i eksik olan kayıtları tespit etmektir.

---

## 11.2. Excel Kolonları

| Sıra | Eksiklik Türü | Tür | Abonelik | Dönem | Fatura No | Tutar | Ödeme Durumu | Fatura PDF | Ödeme PDF | Dosya Kontrol Sonucu | Açıklama |
|---:|---|---|---|---|---|---:|---|---|---|---|---|

---

## 11.3. Eksiklik Türleri

- Fatura PDF eksik
- Ödeme PDF eksik
- PDF yolu var ama dosya bulunamadı
- Aynı PDF birden fazla kayda bağlı
- Ödeme var ama ödeme evrakı yok

---

## 11.4. PDF Kolonları

| Sıra | Eksiklik | Tür | Abonelik | Dönem | Fatura No | Durum |
|---:|---|---|---|---|---|---|

---

## 11.5. Özet Alanı

- Toplam eksik kayıt sayısı
- Fatura PDF eksik sayısı
- Ödeme PDF eksik sayısı
- Dosya yolu hatalı kayıt sayısı
- Aynı dosya uyarısı sayısı

---

# 12. Rapor 8 — Gecikmeli Ödeme Raporu

## 12.1. Amaç

Son ödeme tarihinden sonra ödenmiş faturaları listelemektir.

---

## 12.2. Excel Kolonları

| Sıra | Tür | Abonelik | Firma | Fatura No | Fatura Tarihi | Son Ödeme Tarihi | Ödeme Tarihi | Gecikme Günü | Tutar | Ödenen Tutar | Ödeme Yöntemi | Ödeme PDF |
|---:|---|---|---|---|---|---|---|---:|---:|---:|---|---|

---

## 12.3. PDF Kolonları

| Sıra | Tür | Abonelik | Fatura No | Son Ödeme | Ödeme Tarihi | Gecikme | Tutar |
|---:|---|---|---|---|---|---:|---:|

---

## 12.4. Özet Alanı

- Gecikmeli ödenen fatura sayısı
- Toplam gecikmeli ödeme tutarı
- Ortalama gecikme günü
- En uzun gecikme günü
- En çok gecikme yaşanan abonelik

---

# 13. Rapor 9 — Tek Fatura Detay Raporu

## 13.1. Amaç

Seçilen tek faturanın tüm detaylarını resmi görünümlü şekilde göstermektir.

Bu rapor PDF için özellikle uygundur. Excel çıktısı zorunlu değildir.

---

## 13.2. PDF Bölümleri

### Fatura Bilgileri

| Alan | Değer |
|---|---|
| Fatura Türü | ... |
| Abonelik | ... |
| Firma | ... |
| Dönem | ... |
| Fatura Tarihi | ... |
| Son Ödeme Tarihi | ... |
| Fatura No | ... |
| Tutar | ... |
| Kullanım | ... |

### Ödeme Bilgileri

| Alan | Değer |
|---|---|
| Ödeme Durumu | ... |
| Ödeme Tarihi | ... |
| Ödenen Tutar | ... |
| Ödeme Yöntemi | ... |
| Gecikme Durumu | ... |

### Evrak Bilgileri

| Alan | Değer |
|---|---|
| Fatura PDF | Var / Eksik |
| Ödeme PDF | Var / Eksik |
| Fatura Dosya Adı | ... |
| Ödeme Dosya Adı | ... |

### Açıklama / Notlar

| Alan | Değer |
|---|---|
| Fatura Açıklaması | ... |
| Ödeme Açıklaması | ... |
| Sistem Notu | ... |

---

# 14. Rapor 10 — Abonelik Bilgi Kartı

## 14.1. Amaç

Bir aboneliğin sistemde kayıtlı resmi bilgilerini ve genel fatura durumunu göstermektir.

Bu rapor PDF için uygundur. Gerekirse Excel çıktısı da üretilebilir.

---

## 14.2. PDF Bölümleri

### Abonelik Kimlik Bilgileri

| Alan | Değer |
|---|---|
| Fatura Türü | ... |
| Abonelik Adı | ... |
| Abone No | ... |
| Tesisat No | ... |
| Sayaç No | ... |
| Firma | ... |
| Hizmet Adresi | ... |
| Aktif/Pasif Durumu | ... |

### Genel Fatura Özeti

| Alan | Değer |
|---|---|
| Toplam Fatura Sayısı | ... |
| Toplam Fatura Tutarı | ... |
| Son Fatura Dönemi | ... |
| Son Fatura Tutarı | ... |
| Ödenmemiş Fatura Sayısı | ... |
| Eksik Evrak Sayısı | ... |

### Son 12 Ay Özeti

| Ay | Fatura No | Tutar | Kullanım | Ödeme Durumu |
|---|---|---:|---:|---|

---

# 15. Raporların Program Menü Yapısı

Önerilen rapor menüsü:

```text
Raporlar
├── Genel Raporlar
│   ├── Aylık Fatura Listesi
│   ├── Ödenmemiş Faturalar
│   ├── Gecikmiş / Gecikmeli Ödemeler
│   └── Evrak Eksikliği Raporu
│
├── Türe Göre Raporlar
│   ├── Türe Özgü Aylık Fatura Listesi
│   └── Türe Özgü Yıllık Fatura Listesi
│
├── Aboneliğe Göre Raporlar
│   ├── Aboneliğe Özgü Aylık Fatura Bilgisi
│   ├── Aboneliğe Özgü Yıllık Fatura Listesi
│   └── Abonelik Bilgi Kartı
│
└── Detay Raporları
    └── Tek Fatura Detay Raporu
```

---

# 16. Rapor Ekranı Filtre Standartları

## 16.1. Aylık Rapor Filtreleri

- Yıl
- Ay
- Fatura türü
- Abonelik
- Ödeme durumu
- Evrak durumu

## 16.2. Yıllık Rapor Filtreleri

- Yıl
- Fatura türü
- Abonelik
- Ödeme durumu

## 16.3. Ödenmemiş Fatura Raporu Filtreleri

- Tüm yıllar
- Belirli yıl
- Belirli ay
- Fatura türü
- Abonelik
- Son ödeme tarihi geçmiş olanlar
- Son ödeme tarihi yaklaşanlar

## 16.4. Evrak Eksikliği Raporu Filtreleri

- Fatura PDF eksik
- Ödeme PDF eksik
- Dosya yolu var ama fiziksel dosya yok
- Aynı PDF uyarısı
- Tüm eksikler

---

# 17. Codex İçin Rapor Geliştirme Sırası

Raporlar tek seferde geliştirilmemelidir. Şu sırayla eklenmelidir:

---

## v0.17 — Excel Aylık Fatura Listesi

Kapsam:

- Sadece Excel çıktı
- Sadece aylık tüm fatura listesi
- Yıl ve ay filtresi
- Ödeme bilgileri dahil
- Evrak durumları dahil
- PDF çıktı yok
- Diğer raporlar yok

Dokunulmayacaklar:

- Fatura kayıt mantığı
- Ödeme kayıt mantığı
- PDF evrak saklama mantığı
- Veritabanı migration gerekmiyorsa değiştirilmemeli

---

## v0.18 — Excel Ödenmemiş Faturalar Raporu

Kapsam:

- Ödenmemiş faturalar Excel çıktısı
- Son ödeme tarihi yaklaşan ve geçmiş faturalar
- Gecikme günü hesabı
- Toplam ödenmemiş tutar

Dokunulmayacaklar:

- Fatura ödeme durumu iş mantığı
- Ödeme kayıt ekranı
- Evrak ekleme sistemi

---

## v0.19 — Excel Türe Özgü Aylık Fatura Listesi

Kapsam:

- Fatura türü + yıl + ay filtresi
- Aynı türdeki tüm aboneliklerin faturaları
- Toplam tutar
- Toplam kullanım
- Ortalama tutar

---

## v0.20 — Excel Abonelik Yıllık Fatura Listesi

Kapsam:

- Tek abonelik + yıl filtresi
- 12 aylık fatura listesi
- Yıllık toplam tutar
- Yıllık toplam kullanım
- Eksik ay kontrolü

---

## v0.21 — PDF Aylık Fatura Listesi

Kapsam:

- Mevcut aylık fatura raporunun PDF çıktısı
- A4 yatay
- Özet alanı
- Detay tablo
- Sayfa numarası

---

## v0.22 — PDF Ödenmemiş Faturalar Raporu

Kapsam:

- A4 yatay PDF çıktı
- Ödenmemiş faturalar
- Gecikme günü bilgisi
- Son ödeme yaklaşan kayıtlar
- Özet alanı

---

## v0.23 — PDF Abonelik Aylık Fatura Bilgisi

Kapsam:

- Tek abonelik detay raporu
- A4 dikey
- Abonelik bilgileri
- Fatura bilgileri
- Ödeme bilgileri
- Evrak bilgileri
- Önceki ay karşılaştırması

---

## v0.24 — Evrak Eksikliği Excel/PDF Raporu

Kapsam:

- Fatura PDF eksik
- Ödeme PDF eksik
- Fiziksel dosya bulunamadı
- Aynı PDF uyarısı
- Excel çıktı
- PDF çıktı

---

## v0.25 — Türe Özgü Yıllık Excel/PDF Raporu

Kapsam:

- Seçilen fatura türünün yıllık tüm faturaları
- Abonelik bazlı toplamlar
- Aylık toplamlar
- Excel çok sayfalı çıktı
- PDF özet çıktı

---

## v0.26 — Abonelik Bilgi Kartı ve Tek Fatura Detay PDF Raporları

Kapsam:

- Abonelik Bilgi Kartı PDF
- Tek Fatura Detay PDF
- Resmi çıktı düzeni
- Evrak durum bilgileri

---

# 18. Codex İçin Örnek Rapor Geliştirme Komutu

Aşağıdaki komut, ilk Excel rapor fazı için kullanılabilir:

```text
Mevcut güvenli sürüm: v0.16

Bu sürümde yalnızca v0.17 Excel Aylık Fatura Listesi geliştirilecek.

Kapsam:
- Seçilen yıl ve ay için tüm faturaları listeleyen Excel çıktısı oluştur.
- Raporda fatura türü, abonelik, firma, dönem, fatura tarihi, son ödeme tarihi, fatura no, tutar, kullanım, birim, ödeme durumu, ödeme tarihi, ödenen tutar, fatura PDF durumu, ödeme PDF durumu ve açıklama kolonları bulunsun.
- Üst bölümde kurum adı, rapor adı, dönem, rapor tarihi ve filtre bilgisi yer alsın.
- Özet bölümünde toplam fatura sayısı, toplam fatura tutarı, ödenen toplam tutar, ödenmemiş toplam tutar, ödenmiş fatura sayısı, ödenmemiş fatura sayısı, gecikmiş fatura sayısı, fatura PDF eksik sayısı ve ödeme PDF eksik sayısı yer alsın.
- Excel başlık satırları biçimlendirilsin.
- Tablo başlıklarında filtre aktif olsun.
- Para alanları ₺ formatında gösterilsin.
- Tarihler dd.MM.yyyy formatında gösterilsin.
- Dosya adı Aylik_Fatura_Listesi_YYYY_MM.xlsx formatında oluşturulsun.

Kesinlikle dokunulmayacaklar:
- Fatura kayıt ekranı
- Ödeme kayıt ekranı
- Abonelik yönetimi
- PDF evrak ekleme mantığı
- Veritabanı tabloları, migration gerekmedikçe değiştirilmeyecek
- Mevcut iş mantığı değiştirilmeyecek

Beklenen çıktı:
- Çalışan Excel raporu
- Değiştirilen dosyaların özeti
- Test notları
- Sürüm notu
```

---

# 19. Net Tasarım Kararı

Excel raporları:

- Detaylı
- Kolon sayısı geniş
- Filtrelenebilir
- Analiz edilebilir
- Toplam ve ara toplam üretmeye uygun

PDF raporları:

- Sade
- Resmi
- Yazdırılabilir
- Özet odaklı
- A4 sayfa düzenine uygun
- Gereksiz kolonlardan arındırılmış

Aynı raporun Excel ve PDF tasarımı birebir aynı olmak zorunda değildir.

Excel geniş veri için, PDF resmi özet ve kontrol için tasarlanmalıdır.

---

# 20. Önemli Uygulama Notları

- Raporlar veri değiştirmemelidir.
- Rapor oluşturma işlemi hiçbir faturayı, ödemeyi veya evrak kaydını güncellememelidir.
- Raporlar salt okunur veri üzerinden üretilmelidir.
- Rapor üretim hataları kullanıcıya anlaşılır mesajla gösterilmelidir.
- Rapor dosyası oluşturulamazsa işlem yarıda bırakılmalı ve mevcut veriye dokunulmamalıdır.
- Kullanıcı raporu kaydedeceği klasörü seçebilmelidir.
- Varsayılan kayıt klasörü `exports/` olmalıdır.
- Rapor üretildikten sonra kullanıcıya “Dosyayı Aç” seçeneği sunulabilir.
- Aynı adla dosya varsa üzerine yazmadan önce kullanıcıdan onay alınmalıdır.

---

# 21. Sonuç

Bu rapor tasarım planı, Kurum Fatura Takip Programı'nın Excel ve PDF çıktılarını düzenli, sürdürülebilir ve Codex ile faz faz geliştirilebilir hale getirmek için hazırlanmıştır.

Programda temel veri modeli oturduktan sonra rapor geliştirme süreci şu sırayla ilerlemelidir:

```text
1. Excel Aylık Fatura Listesi
2. Excel Ödenmemiş Faturalar Raporu
3. Excel Türe Özgü Aylık Fatura Listesi
4. Excel Abonelik Yıllık Fatura Listesi
5. PDF Aylık Fatura Listesi
6. PDF Ödenmemiş Faturalar Raporu
7. PDF Abonelik Aylık Fatura Bilgisi
8. Evrak Eksikliği Excel/PDF Raporu
9. Türe Özgü Yıllık Excel/PDF Raporu
10. Abonelik Bilgi Kartı ve Tek Fatura Detay PDF Raporları
```

Raporlar geliştirilirken ana ilke şudur:

```text
Rapor üret → Veri değiştirme → Mevcut iş mantığını bozma → Küçük fazlarla ilerle
```

