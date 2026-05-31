# Devam Notları

Bu dosya, yeni Codex chatlerinde kaldığımız yeri hızlıca anlamak için tutulur.

## Güncel Kaldığımız Yer

- Tarih: 2026-05-31
- Aktif branch: `codex/v0.8-odeme-pdf-evraki`
- Son tamamlanan faz: `v0.8 - Ödeme Evrakı PDF Ekleme`
- Sıradaki faz: `v0.9 - Ana Gösterge Paneli`
- İlk dokümantasyon commit'i: `e0de4f9 docs: initialize project planning and continuity notes`
- `v0.1` commit'i: `3b3e20a feat: initialize wpf sqlite project skeleton`
- `v0.2` commit'i: `c8ad90c feat: add invoice type management`
- `v0.3` commit'i: `061e1a7 feat: add subscription management`
- `v0.4` commit'i: `252bbbd feat: add invoice entry foundation`
- PDF rapor örneği notu commit'i: `c0643ee docs: note pdf report sample requirement`
- `v0.5` commit'i: `2192c69 feat: add invoice pdf attachments`
- `v0.6` commit'i: `296defe feat: improve invoice list filters`
- `v0.7` hazırlık commit'i: `63e4c3a docs: prepare v0.7 branch context`
- `v0.7` commit'i: `13375d1 feat: add payment record foundation`

## Bu Oturumda Yapılanlar

1. Proje klasörü kontrol edildi.
2. Proje klasöründe başlangıçta yalnızca `.git` bulundu.
3. Mevcut `.md` dosyası olmadığı görüldü.
4. Masaüstündeki kaynak plan dosyası bulundu:
   `C:\Users\Asus\Desktop\Kurum_Fatura_Takip_Programi_Gelistirme_Plani.md`
5. Plan dosyasının UTF-8 okunması gerektiği doğrulandı.
6. `docs` klasörü oluşturuldu.
7. Kaynak plan proje içine kopyalandı:
   `docs/01-gelistirme-plani.md`
8. Yeni chat devam kılavuzu oluşturuldu:
   `docs/00-codex-devam-kilavuzu.md`
9. Roadmap oluşturuldu:
   `ROADMAP.md`
10. Regresyon kontrol dosyası oluşturuldu:
    `REGRESYON.md`
11. Proje kararları kaydı oluşturuldu:
    `docs/02-proje-kararlari.md`
12. Bu devam notları oluşturuldu.
13. `codex/v0.1-proje-iskeleti` branch'i açıldı.
14. `FaturaTakip.sln` oluşturuldu.
15. `src/FaturaTakip.App` WPF projesi oluşturuldu.
16. `Microsoft.Data.Sqlite` paketi eklendi.
17. `.gitignore` eklendi ve `FaturaTakip.App` klasörü için Windows'taki `*.app` ignore çakışması düzeltildi.
18. Runtime klasör yapısı `.gitkeep` dosyalarıyla takip edilebilir hale getirildi.
19. SQLite başlangıç altyapısı ve idempotent migration runner eklendi.
20. `database/fatura_takip.db` health-check sırasında oluşturuldu.
21. Sade boş dashboard ekranı bağlandı.
22. `dotnet build FaturaTakip.sln` başarılı çalıştı.
23. `dotnet run --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` iki kez başarılı çalıştı.
24. Uygulama kısa süreli başlatma testinden geçti.
25. `v0.1` commit'i master içine fast-forward merge edildi.
26. `codex/v0.2-fatura-turleri` branch'i açıldı.
27. `invoice_types` tablosu için `0002` migration eklendi.
28. Başlangıç fatura türleri seed edildi: Elektrik, Su, Doğalgaz, Telefon, İnternet, Diğer.
29. Fatura türü modeli ve repository katmanı eklendi.
30. Fatura türü listeleme, ekleme, düzenleme ve aktif/pasif yapma UI akışı eklendi.
31. `--self-test` komutu eklendi ve fatura türü repository akışı geçici veritabanıyla doğrulandı.
32. `dotnet build FaturaTakip.sln` başarılı çalıştı.
33. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
34. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
35. Uygulama kısa süreli başlatma testinden geçti.
36. `v0.2` commit'i master içine fast-forward merge edildi.
37. `codex/v0.3-abonelik-yonetimi` branch'i açıldı.
38. `v0.3` başlangıcında build, health-check, self-test ve kısa uygulama başlatma smoke testleri başarılı çalıştı.
39. `subscriptions` tablosu için `0003` migration eklendi.
40. Abonelik modeli ve repository katmanı eklendi.
41. Abonelikler fatura türlerine foreign key ile bağlandı.
42. Abonelik listeleme, ekleme, düzenleme, aktif/pasif yapma ve filtreleme UI akışı eklendi.
43. Dashboard aktif abonelik sayısını göstermeye başladı.
44. `--self-test` abonelik ekleme, düzenleme, fatura türüne bağlama ve pasife alma senaryolarını doğrulayacak şekilde genişletildi.
45. `dotnet build FaturaTakip.sln` başarılı çalıştı.
46. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
47. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
48. Uygulama kısa süreli başlatma testinden geçti.
49. `v0.3` commit'i master içine fast-forward merge edildi.
50. `codex/v0.4-fatura-kayit-altyapisi` branch'i açıldı.
51. `v0.4` başlangıcında build, health-check, self-test ve kısa uygulama başlatma smoke testleri başarılı çalıştı.
52. `invoices` tablosu için `0004` migration eklendi.
53. Fatura modeli ve repository katmanı eklendi.
54. Faturalar aboneliklere ve aboneliğin fatura türüne bağlandı.
55. Fatura listeleme, ekleme ve düzenleme UI akışı eklendi.
56. Aynı abonelikte aynı fatura no, negatif tutar ve negatif kullanım kontrolleri eklendi.
57. Son ödeme tarihi fatura tarihinden önceyse kullanıcı uyarısı eklendi.
58. Dashboard toplam fatura sayısını göstermeye başladı.
59. `--self-test` fatura ekleme, düzenleme, aboneliğe bağlama, tekrar fatura no, negatif tutar/kullanım ve tarih uyarısı senaryolarını doğrulayacak şekilde genişletildi.
60. `dotnet build FaturaTakip.sln` başarılı çalıştı.
61. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
62. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
63. Uygulama kısa süreli başlatma testinden geçti.
64. Kullanıcı, fatura PDF raporları fazına gelindiğinde önce kendisinden Excel örneği istenmesini istedi; bu not proje kararları, roadmap ve regresyon notlarına işlendi.
65. `v0.4` branch'i `master` içine fast-forward merge edildi.
66. Merge sonrası `master` üzerinde build, health-check, self-test ve kısa uygulama başlatma smoke testleri başarılı çalıştı.
67. `codex/v0.5-fatura-pdf-evraki` branch'i açıldı.
68. `0005` migration ile fatura PDF yolu, orijinal dosya adı, SHA-256 hash ve eklenme zamanı alanları eklendi.
69. PDF dosyası doğrulama, güvenli ASCII dosya adıyla `attachments/invoices/yyyy/MM` altına kopyalama ve metadata kaydetme repository akışı eklendi.
70. Fatura ekranına PDF seçme, kaydetme sonrası faturaya bağlama, kayıtlı PDF'i açma, PDF durumu ve PDF eksik sayısı eklendi.
71. `--self-test` PDF kopyalama, hash saklama, dosya varlığı, kayıp dosya algısı ve PDF olmayan dosya reddi senaryolarıyla genişletildi.
72. `dotnet build FaturaTakip.sln` başarılı çalıştı.
73. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
74. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
75. Uygulama kısa süreli başlatma testinden geçti.
76. `v0.5` branch'i `master` içine fast-forward merge edildi.
77. Merge sonrası `master` üzerinde build, health-check, self-test ve kısa uygulama başlatma smoke testleri başarılı çalıştı.
78. `codex/v0.6-fatura-listesi-filtreleme` branch'i açıldı.
79. Fatura filtreleme mantığı `InvoiceFilter` ve `InvoiceFilterCriteria` sınıflarına alındı.
80. Fatura ekranına yıl, ay, fatura türü, abonelik, ödeme durumu, PDF durumu ve filtreleri temizleme kontrolleri eklendi.
81. Serbest metin araması fatura no, tür, abonelik, kurum, açıklama ve PDF orijinal dosya adını kapsayacak şekilde genişletildi.
82. `--self-test` yıl, ay, tür, abonelik, ödeme durumu, gecikmiş, PDF var/eksik ve çok terimli arama filtrelerini doğrulayacak şekilde genişletildi.
83. `dotnet build FaturaTakip.sln` başarılı çalıştı.
84. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
85. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
86. Uygulama kısa süreli başlatma testinden geçti.
87. `v0.6` branch'i `master` içine fast-forward merge edildi.
88. Merge sonrası `master` üzerinde build, health-check, self-test ve kısa uygulama başlatma smoke testleri başarılı çalıştı.
89. `codex/v0.7-odeme-kayit-altyapisi` branch'i açıldı.
90. `v0.7` başlangıç dokümantasyonu commit edildi: `63e4c3a docs: prepare v0.7 branch context`.
91. `payments` tablosu için `0006` migration eklendi.
92. `Payment`, `PaymentInput` ve `PaymentRepository` veri katmanı eklendi.
93. Ödeme kaydı ekleme, ödeme tarihi/tutarı/açıklaması saklama, kalan tutarı aşan ödemeyi engelleme ve fatura durumunu güncelleme akışı eklendi.
94. Fatura modeli ödenen tutar, kalan tutar, ödeme özeti ve kısmi ödeme durumunu gösterecek şekilde genişletildi.
95. Fatura tutarı düzenlendiğinde ödeme durumu ödenen toplam üzerinden yeniden hesaplanacak şekilde güncellendi.
96. Fatura ekranına ödeme özeti, ödeme kayıt formu ve ödeme geçmişi listesi eklendi.
97. Fatura listesine kompakt ödeme özeti sütunu eklendi.
98. `--self-test` kısmi ödeme, tam ödeme, kalan aşımı, tutar değişince durum yenileme, negatif ödeme ve olmayan fatura senaryolarıyla genişletildi.
99. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` başarılı çalıştı.
100. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
101. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
102. `README.md`, `ROADMAP.md`, `REGRESYON.md` ve devam notları v0.7 tamamlandı / v0.8 sıradaki olacak şekilde güncellendi.
103. `v0.7` commit'i oluşturuldu: `13375d1 feat: add payment record foundation`.
104. `v0.7` branch'i `master` içine fast-forward merge edildi.
105. Merge sonrası `master` üzerinde `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` başarılı çalıştı.
106. Merge sonrası `master` üzerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
107. Merge sonrası `master` üzerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
108. `codex/v0.8-odeme-pdf-evraki` branch'i açıldı.
109. `v0.8` başlangıç dokümantasyonu commit edildi: `7699971 docs: prepare v0.8 branch context`.
110. `payments` tablosuna PDF yolu, orijinal dosya adı, SHA-256 hash ve eklenme zamanı alanları için `0007` migration eklendi.
111. Ödeme PDF dosyası doğrulama, güvenli ASCII dosya adıyla `attachments/payments/yyyy/MM` altına kopyalama ve metadata kaydetme repository akışı eklendi.
112. Ödeme modeline PDF durumu ve ödeme PDF eksikliği algılama altyapısı eklendi.
113. Fatura ekranındaki ödeme geçmişine PDF durumu, seçili ödeme için PDF seçme ve kayıtlı ödeme PDF'ini açma akışı eklendi.
114. `--self-test` ödeme PDF kopyalama, hash saklama, dosya varlığı, kayıp dosya algısı ve PDF olmayan ödeme dosyası reddi senaryolarıyla genişletildi.
115. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` başarılı çalıştı.
116. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
117. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
118. `README.md`, `ROADMAP.md`, `REGRESYON.md`, proje kararları ve devam notları v0.8 tamamlandı / v0.9 sıradaki olacak şekilde güncellendi.

## Sıradaki Codex Görevi

`v0.8` kapsamı tamamlandı. Yeni chate geçilirse önce commit/merge durumu kontrol edilmeli.

Sıradaki güvenli operasyon sırası:

1. `git status --short --branch` ile çalışma ağacı kontrol edilsin.
2. `v0.8` commit'i henüz oluşmadıysa `feat: add payment pdf attachments` mesajıyla commit edilsin.
3. Commit oluştuysa `master` branch'ine fast-forward merge edilsin.
4. Merge sonrası build, health-check ve self-test tekrar çalıştırılsın.
5. `codex/v0.9-dashboard` branch'i açılarak roadmapte `Sıradaki` olan v0.9 uygulanmaya başlansın.

v0.9 başlangıç talimatı:

```text
C# WPF + SQLite tabanlı kurum fatura takip programında v0.9 fazını uygula.

Önce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarını oku.

Bu fazda yalnızca ana gösterge paneli geliştirilecek.

Aylık toplamlar, ödenmemişler, gecikmişler, fatura PDF eksikleri ve ödeme PDF eksikleri gösterilecek.

Rapor ekranları, Excel/PDF dışa aktarım ve yedekleme yapılmayacak.

Faz sonunda uygulamayı çalıştır veya en azından derleme/test doğrulamasını yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarını güncelle.
```

## Dikkat Edilecekler

- Yol sorunlarına karşı PowerShell'de gerekirse `-LiteralPath` kullanılmalı.
- Uygulama kodu başlamadan önce .NET SDK durumu kontrol edilmeli.
- Yeni oluşturulacak klasör adları ASCII olmalı.
- Faz kapsamı aşılmamalı.
- Önce dokümantasyon okunmalı, sonra uygulama koduna geçilmeli.
- Fatura PDF raporları fazına gelindiğinde kullanıcıdan Excel örneği istenmeden tasarım/uygulama yapılmamalı.
