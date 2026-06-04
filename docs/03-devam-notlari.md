﻿﻿﻿﻿﻿﻿# Devam NotlarÄ±

Bu dosya, yeni Codex chatlerinde kaldÄ±ÄŸÄ±mÄ±z yeri hÄ±zlÄ±ca anlamak iÃ§in tutulur.

## GÃ¼ncel KaldÄ±ÄŸÄ±mÄ±z Yer

- Tarih: 2026-06-01
- Aktif branch: `master`
- Son commit: `1417a8d squash: v0.19 backup + v0.20 consistency`
- Son tamamlanan faz: `v0.19 - Manuel GÃ¼venli Yedekleme` (branch Ã¼zerinde)
- SÄ±radaki faz: `v0.20 - TutarlÄ±lÄ±k Denetimi`
- Durum: build + health-check + self-test OK. `--create-backup --backup-no-attachments --backup-no-exports` smoke testi OK (zip oluÅŸuyor).
- Ä°lk dokÃ¼mantasyon commit'i: `e0de4f9 docs: initialize project planning and continuity notes`
- `v0.1` commit'i: `3b3e20a feat: initialize wpf sqlite project skeleton`
- `v0.2` commit'i: `c8ad90c feat: add invoice type management`
- `v0.3` commit'i: `061e1a7 feat: add subscription management`
- `v0.4` commit'i: `252bbbd feat: add invoice entry foundation`
- PDF rapor Ã¶rneÄŸi notu commit'i: `c0643ee docs: note pdf report sample requirement`
- `v0.5` commit'i: `2192c69 feat: add invoice pdf attachments`
- `v0.6` commit'i: `296defe feat: improve invoice list filters`
- `v0.7` hazÄ±rlÄ±k commit'i: `63e4c3a docs: prepare v0.7 branch context`
- `v0.7` commit'i: `13375d1 feat: add payment record foundation`
- `v0.8` hazÄ±rlÄ±k commit'i: `7699971 docs: prepare v0.8 branch context`
- `v0.8` commit'i: `fa49fd9 feat: add payment pdf attachments`
- `v0.9` hazÄ±rlÄ±k commit'i: `5a1981d docs: prepare v0.9 branch context`
- `v0.9` commit'i: `f88fc53 feat: improve dashboard summaries`
- `v0.10` hazÄ±rlÄ±k commit'i: `b13d8d5 docs: prepare v0.10 branch context`
- `v0.10` commit'i: `9771c31 feat: add unpaid/overdue invoice report`

## Bu Oturumda YapÄ±lanlar

(2026-06-01 / v0.22)

1. BackupView uzerine UI restore bolumu eklendi (Zip sec + hedef klasor yolu + Geri Yukle).
2. CLI restore hata durumunda MessageBox gosterimi kapatildi (headless/run kilitlenmesin).
3. Smoke test: dotnet build -c Release OK.
4. Smoke test: dotnet run -c Release --no-build --project src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test OK.
5. Smoke test: --restore-backup negatif senaryo (non-empty hedef) exit=1 OK.

6. UX: Hedefi Ac butonu eklendi + UI tarafinda hedef klasor bos mu on kontrolu eklendi.
(2026-06-01 / v0.21)

1. CLI eklendi: `--restore-backup <zip> --restore-target <emptyFolder>`.
2. Güvenlik: yalnızca boş klasöre restore yapılır.
3. Smoke test: `--create-backup` (DB-only) -> `--restore-backup` OK.
(2026-06-01 / v0.20)

1. Raporlar ekranına `Tutarlılık` sekmesi eklendi.
2. Salt okunur tutarlılık denetimi hesaplayıcısı eklendi: `ConsistencyReportCalculator`.
3. CLI eklendi: `--consistency-check`.
4. Smoke test: `dotnet run -c Release --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --consistency-check` OK.
5. Smoke test: `dotnet run -c Debug --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` OK.
(2026-06-01 / v0.19)

1. `Views/BackupView` eklendi: DB + attachments + exports iÃ§eren zip yedek Ã¼retiyor.
2. CLI eklendi: `--create-backup` (opsiyonel: `--backup-no-attachments`, `--backup-no-exports`).
3. Smoke test: `dotnet build -c Release` OK.
4. Smoke test: `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` OK.
5. Smoke test: `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` OK.
6. Smoke test: `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --create-backup --backup-no-attachments --backup-no-exports` OK (ZIP oluÅŸtu).

1. Proje klasÃ¶rÃ¼ kontrol edildi.
2. Proje klasÃ¶rÃ¼nde baÅŸlangÄ±Ã§ta yalnÄ±zca `.git` bulundu.
3. Mevcut `.md` dosyasÄ± olmadÄ±ÄŸÄ± gÃ¶rÃ¼ldÃ¼.
4. MasaÃ¼stÃ¼ndeki kaynak plan dosyasÄ± bulundu:
   `C:\Users\Asus\Desktop\Kurum_Fatura_Takip_Programi_Gelistirme_Plani.md`
5. Plan dosyasÄ±nÄ±n UTF-8 okunmasÄ± gerektiÄŸi doÄŸrulandÄ±.
6. `docs` klasÃ¶rÃ¼ oluÅŸturuldu.
7. Kaynak plan proje iÃ§ine kopyalandÄ±:
   `docs/01-gelistirme-plani.md`
8. Yeni chat devam kÄ±lavuzu oluÅŸturuldu:
   `docs/00-codex-devam-kilavuzu.md`
9. Roadmap oluÅŸturuldu:
   `ROADMAP.md`
10. Regresyon kontrol dosyasÄ± oluÅŸturuldu:
    `REGRESYON.md`
11. Proje kararlarÄ± kaydÄ± oluÅŸturuldu:
    `docs/02-proje-kararlari.md`
12. Bu devam notlarÄ± oluÅŸturuldu.
13. `codex/v0.1-proje-iskeleti` branch'i aÃ§Ä±ldÄ±.
14. `FaturaTakip.sln` oluÅŸturuldu.
15. `src/FaturaTakip.App` WPF projesi oluÅŸturuldu.
16. `Microsoft.Data.Sqlite` paketi eklendi.
17. `.gitignore` eklendi ve `FaturaTakip.App` klasÃ¶rÃ¼ iÃ§in Windows'taki `*.app` ignore Ã§akÄ±ÅŸmasÄ± dÃ¼zeltildi.
18. Runtime klasÃ¶r yapÄ±sÄ± `.gitkeep` dosyalarÄ±yla takip edilebilir hale getirildi.
19. SQLite baÅŸlangÄ±Ã§ altyapÄ±sÄ± ve idempotent migration runner eklendi.
20. `database/fatura_takip.db` health-check sÄ±rasÄ±nda oluÅŸturuldu.
21. Sade boÅŸ dashboard ekranÄ± baÄŸlandÄ±.
22. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
23. `dotnet run --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` iki kez baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
24. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
25. `v0.1` commit'i master iÃ§ine fast-forward merge edildi.
26. `codex/v0.2-fatura-turleri` branch'i aÃ§Ä±ldÄ±.
27. `invoice_types` tablosu iÃ§in `0002` migration eklendi.
28. BaÅŸlangÄ±Ã§ fatura tÃ¼rleri seed edildi: Elektrik, Su, DoÄŸalgaz, Telefon, Ä°nternet, DiÄŸer.
29. Fatura tÃ¼rÃ¼ modeli ve repository katmanÄ± eklendi.
30. Fatura tÃ¼rÃ¼ listeleme, ekleme, dÃ¼zenleme ve aktif/pasif yapma UI akÄ±ÅŸÄ± eklendi.
31. `--self-test` komutu eklendi ve fatura tÃ¼rÃ¼ repository akÄ±ÅŸÄ± geÃ§ici veritabanÄ±yla doÄŸrulandÄ±.
32. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
33. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
34. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
35. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
36. `v0.2` commit'i master iÃ§ine fast-forward merge edildi.
37. `codex/v0.3-abonelik-yonetimi` branch'i aÃ§Ä±ldÄ±.
38. `v0.3` baÅŸlangÄ±cÄ±nda build, health-check, self-test ve kÄ±sa uygulama baÅŸlatma smoke testleri baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
39. `subscriptions` tablosu iÃ§in `0003` migration eklendi.
40. Abonelik modeli ve repository katmanÄ± eklendi.
41. Abonelikler fatura tÃ¼rlerine foreign key ile baÄŸlandÄ±.
42. Abonelik listeleme, ekleme, dÃ¼zenleme, aktif/pasif yapma ve filtreleme UI akÄ±ÅŸÄ± eklendi.
43. Dashboard aktif abonelik sayÄ±sÄ±nÄ± gÃ¶stermeye baÅŸladÄ±.
44. `--self-test` abonelik ekleme, dÃ¼zenleme, fatura tÃ¼rÃ¼ne baÄŸlama ve pasife alma senaryolarÄ±nÄ± doÄŸrulayacak ÅŸekilde geniÅŸletildi.
45. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
46. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
47. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
48. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
49. `v0.3` commit'i master iÃ§ine fast-forward merge edildi.
50. `codex/v0.4-fatura-kayit-altyapisi` branch'i aÃ§Ä±ldÄ±.
51. `v0.4` baÅŸlangÄ±cÄ±nda build, health-check, self-test ve kÄ±sa uygulama baÅŸlatma smoke testleri baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
52. `invoices` tablosu iÃ§in `0004` migration eklendi.
53. Fatura modeli ve repository katmanÄ± eklendi.
54. Faturalar aboneliklere ve aboneliÄŸin fatura tÃ¼rÃ¼ne baÄŸlandÄ±.
55. Fatura listeleme, ekleme ve dÃ¼zenleme UI akÄ±ÅŸÄ± eklendi.
56. AynÄ± abonelikte aynÄ± fatura no, negatif tutar ve negatif kullanÄ±m kontrolleri eklendi.
57. Son Ã¶deme tarihi fatura tarihinden Ã¶nceyse kullanÄ±cÄ± uyarÄ±sÄ± eklendi.
58. Dashboard toplam fatura sayÄ±sÄ±nÄ± gÃ¶stermeye baÅŸladÄ±.
59. `--self-test` fatura ekleme, dÃ¼zenleme, aboneliÄŸe baÄŸlama, tekrar fatura no, negatif tutar/kullanÄ±m ve tarih uyarÄ±sÄ± senaryolarÄ±nÄ± doÄŸrulayacak ÅŸekilde geniÅŸletildi.
60. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
61. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
62. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
63. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
64. KullanÄ±cÄ±, fatura PDF raporlarÄ± fazÄ±na gelindiÄŸinde Ã¶nce kendisinden Excel Ã¶rneÄŸi istenmesini istedi; bu not proje kararlarÄ±, roadmap ve regresyon notlarÄ±na iÅŸlendi.
65. `v0.4` branch'i `master` iÃ§ine fast-forward merge edildi.
66. Merge sonrasÄ± `master` Ã¼zerinde build, health-check, self-test ve kÄ±sa uygulama baÅŸlatma smoke testleri baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
67. `codex/v0.5-fatura-pdf-evraki` branch'i aÃ§Ä±ldÄ±.
68. `0005` migration ile fatura PDF yolu, orijinal dosya adÄ±, SHA-256 hash ve eklenme zamanÄ± alanlarÄ± eklendi.
69. PDF dosyasÄ± doÄŸrulama, gÃ¼venli ASCII dosya adÄ±yla `attachments/invoices/yyyy/MM` altÄ±na kopyalama ve metadata kaydetme repository akÄ±ÅŸÄ± eklendi.
70. Fatura ekranÄ±na PDF seÃ§me, kaydetme sonrasÄ± faturaya baÄŸlama, kayÄ±tlÄ± PDF'i aÃ§ma, PDF durumu ve PDF eksik sayÄ±sÄ± eklendi.
71. `--self-test` PDF kopyalama, hash saklama, dosya varlÄ±ÄŸÄ±, kayÄ±p dosya algÄ±sÄ± ve PDF olmayan dosya reddi senaryolarÄ±yla geniÅŸletildi.
72. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
73. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
74. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
75. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
76. `v0.5` branch'i `master` iÃ§ine fast-forward merge edildi.
77. Merge sonrasÄ± `master` Ã¼zerinde build, health-check, self-test ve kÄ±sa uygulama baÅŸlatma smoke testleri baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
78. `codex/v0.6-fatura-listesi-filtreleme` branch'i aÃ§Ä±ldÄ±.
79. Fatura filtreleme mantÄ±ÄŸÄ± `InvoiceFilter` ve `InvoiceFilterCriteria` sÄ±nÄ±flarÄ±na alÄ±ndÄ±.
80. Fatura ekranÄ±na yÄ±l, ay, fatura tÃ¼rÃ¼, abonelik, Ã¶deme durumu, PDF durumu ve filtreleri temizleme kontrolleri eklendi.
81. Serbest metin aramasÄ± fatura no, tÃ¼r, abonelik, kurum, aÃ§Ä±klama ve PDF orijinal dosya adÄ±nÄ± kapsayacak ÅŸekilde geniÅŸletildi.
82. `--self-test` yÄ±l, ay, tÃ¼r, abonelik, Ã¶deme durumu, gecikmiÅŸ, PDF var/eksik ve Ã§ok terimli arama filtrelerini doÄŸrulayacak ÅŸekilde geniÅŸletildi.
83. `dotnet build FaturaTakip.sln` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
84. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
85. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
86. Uygulama kÄ±sa sÃ¼reli baÅŸlatma testinden geÃ§ti.
87. `v0.6` branch'i `master` iÃ§ine fast-forward merge edildi.
88. Merge sonrasÄ± `master` Ã¼zerinde build, health-check, self-test ve kÄ±sa uygulama baÅŸlatma smoke testleri baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
89. `codex/v0.7-odeme-kayit-altyapisi` branch'i aÃ§Ä±ldÄ±.
90. `v0.7` baÅŸlangÄ±Ã§ dokÃ¼mantasyonu commit edildi: `63e4c3a docs: prepare v0.7 branch context`.
91. `payments` tablosu iÃ§in `0006` migration eklendi.
92. `Payment`, `PaymentInput` ve `PaymentRepository` veri katmanÄ± eklendi.
93. Ã–deme kaydÄ± ekleme, Ã¶deme tarihi/tutarÄ±/aÃ§Ä±klamasÄ± saklama, kalan tutarÄ± aÅŸan Ã¶demeyi engelleme ve fatura durumunu gÃ¼ncelleme akÄ±ÅŸÄ± eklendi.
94. Fatura modeli Ã¶denen tutar, kalan tutar, Ã¶deme Ã¶zeti ve kÄ±smi Ã¶deme durumunu gÃ¶sterecek ÅŸekilde geniÅŸletildi.
95. Fatura tutarÄ± dÃ¼zenlendiÄŸinde Ã¶deme durumu Ã¶denen toplam Ã¼zerinden yeniden hesaplanacak ÅŸekilde gÃ¼ncellendi.
96. Fatura ekranÄ±na Ã¶deme Ã¶zeti, Ã¶deme kayÄ±t formu ve Ã¶deme geÃ§miÅŸi listesi eklendi.
97. Fatura listesine kompakt Ã¶deme Ã¶zeti sÃ¼tunu eklendi.
98. `--self-test` kÄ±smi Ã¶deme, tam Ã¶deme, kalan aÅŸÄ±mÄ±, tutar deÄŸiÅŸince durum yenileme, negatif Ã¶deme ve olmayan fatura senaryolarÄ±yla geniÅŸletildi.
99. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
100. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
101. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
102. `README.md`, `ROADMAP.md`, `REGRESYON.md` ve devam notlarÄ± v0.7 tamamlandÄ± / v0.8 sÄ±radaki olacak ÅŸekilde gÃ¼ncellendi.
103. `v0.7` commit'i oluÅŸturuldu: `13375d1 feat: add payment record foundation`.
104. `v0.7` branch'i `master` iÃ§ine fast-forward merge edildi.
105. Merge sonrasÄ± `master` Ã¼zerinde `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
106. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
107. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
108. `codex/v0.8-odeme-pdf-evraki` branch'i aÃ§Ä±ldÄ±.
109. `v0.8` baÅŸlangÄ±Ã§ dokÃ¼mantasyonu commit edildi: `7699971 docs: prepare v0.8 branch context`.
110. `payments` tablosuna PDF yolu, orijinal dosya adÄ±, SHA-256 hash ve eklenme zamanÄ± alanlarÄ± iÃ§in `0007` migration eklendi.
111. Ã–deme PDF dosyasÄ± doÄŸrulama, gÃ¼venli ASCII dosya adÄ±yla `attachments/payments/yyyy/MM` altÄ±na kopyalama ve metadata kaydetme repository akÄ±ÅŸÄ± eklendi.
112. Ã–deme modeline PDF durumu ve Ã¶deme PDF eksikliÄŸi algÄ±lama altyapÄ±sÄ± eklendi.
113. Fatura ekranÄ±ndaki Ã¶deme geÃ§miÅŸine PDF durumu, seÃ§ili Ã¶deme iÃ§in PDF seÃ§me ve kayÄ±tlÄ± Ã¶deme PDF'ini aÃ§ma akÄ±ÅŸÄ± eklendi.
114. `--self-test` Ã¶deme PDF kopyalama, hash saklama, dosya varlÄ±ÄŸÄ±, kayÄ±p dosya algÄ±sÄ± ve PDF olmayan Ã¶deme dosyasÄ± reddi senaryolarÄ±yla geniÅŸletildi.
115. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
116. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
117. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
118. `README.md`, `ROADMAP.md`, `REGRESYON.md`, proje kararlarÄ± ve devam notlarÄ± v0.8 tamamlandÄ± / v0.9 sÄ±radaki olacak ÅŸekilde gÃ¼ncellendi.
119. `v0.8` commit'i oluÅŸturuldu: `fa49fd9 feat: add payment pdf attachments`.
120. `v0.8` branch'i `master` iÃ§ine fast-forward merge edildi.
121. Merge sonrasÄ± `master` Ã¼zerinde `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
122. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
123. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
124. `codex/v0.9-dashboard` branch'i aÃ§Ä±ldÄ±.
125. `v0.9` baÅŸlangÄ±Ã§ dokÃ¼mantasyonu commit edildi: `5a1981d docs: prepare v0.9 branch context`.
126. Dashboard Ã¶zet hesaplama mantÄ±ÄŸÄ± `DashboardSummaryCalculator` katmanÄ±na alÄ±ndÄ±.
127. Ana gÃ¶sterge paneline bu ay fatura toplamÄ± ve bu ay Ã¶deme toplamÄ± eklendi.
128. Ã–denmemiÅŸ fatura sayÄ±sÄ±, kalan tutar, gecikmiÅŸ fatura sayÄ±sÄ± ve gecikmiÅŸ kalan tutar dashboard Ã¼zerinde gÃ¶sterildi.
129. Fatura PDF eksik sayÄ±sÄ± ve Ã¶deme PDF eksik sayÄ±sÄ± dashboard Ã¼zerinde gÃ¶sterildi.
130. Fatura tÃ¼rÃ¼, aktif tÃ¼r, aktif abonelik ve toplam fatura sayÄ±larÄ± dashboard iÃ§inde korunarak yeni Ã¶zetlerle birlikte dÃ¼zenlendi.
131. `--self-test` dashboard hesaplarÄ±nÄ± doÄŸrulayacak ÅŸekilde geniÅŸletildi.
132. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
133. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
134. `dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
135. `README.md`, `ROADMAP.md`, `REGRESYON.md`, proje kararlarÄ± ve devam notlarÄ± v0.9 tamamlandÄ± / v0.10 sÄ±radaki olacak ÅŸekilde gÃ¼ncellendi.
136. `v0.9` commit'i oluÅŸturuldu: `f88fc53 feat: improve dashboard summaries`.
137. `v0.9` branch'i `master` iÃ§ine fast-forward merge edildi.
138. Merge sonrasÄ± `master` Ã¼zerinde `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
139. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
140. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
141. `codex/v0.10-odenmemis-gecikmis-raporu` branch'i aÃ§Ä±ldÄ±.
142. `v0.10` baÅŸlangÄ±Ã§ dokÃ¼mantasyonu hazÄ±rlandÄ±.
143. `ReportsView` eklendi: Ã¶denmemiÅŸ, gecikmiÅŸ ve yaklaÅŸan (7 gÃ¼n) Ã¶demeler iÃ§in rapor ekranÄ± oluÅŸturuldu.
144. Rapor ekranÄ± iÃ§in `ActionableInvoiceReportCalculator` hesaplama katmanÄ± eklendi.
145. Ana menÃ¼ye `Raporlar` navigasyonu eklendi.
146. `--self-test` rapor hesaplarÄ±nÄ± doÄŸrulayacak ÅŸekilde geniÅŸletildi.
147. `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
148. `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
149. `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
150. `v0.10` commit'i oluÅŸturuldu: `9771c31 feat: add unpaid/overdue invoice report`.
151. `v0.10` branch'i `master` iÃ§ine fast-forward merge edildi.
152. Merge sonrasÄ± `master` Ã¼zerinde `dotnet build .\src\FaturaTakip.App\FaturaTakip.App.csproj` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
153. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
154. Merge sonrasÄ± `master` Ã¼zerinde `dotnet run --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.
155. `codex/v0.11-aylik-fatura-listesi` branch'i aÃ§Ä±ldÄ±.
156. Excel/PDF rapor tasarÄ±m planÄ± dokÃ¼mana alÄ±ndÄ±: `docs/04-excel-pdf-rapor-tasarim-plani.md`.
157. v0.11 iÃ§in `AylÄ±k Liste` rapor sekmesi eklendi; dÃ¶nem seÃ§imiyle seÃ§ilen ayÄ±n faturalarÄ± listeleniyor ve Ã¶zet toplamlar gÃ¶steriliyor.
158. `--self-test` aylÄ±k rapor hesaplarÄ±nÄ± doÄŸrulayacak ÅŸekilde geniÅŸletildi.

## Mevcut Codex GÃ¶revi

Bu branch'teki gÃ¶rev yalnÄ±zca `v0.15` kapsamÄ±nÄ± uygulamak olmalÄ±.

v0.17 baÅŸlangÄ±Ã§ talimatÄ±:

```text
C# WPF + SQLite tabanlÄ± kurum fatura takip programÄ±nda v0.17 fazÄ±nÄ± uygula.

Ã–nce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarÄ±nÄ± oku.

Bu fazda yalnÄ±zca Excel dÄ±ÅŸa aktarÄ±m geliÅŸtirilecek.

En azÄ±ndan fatura listesi ve raporlar ekranÄ±ndaki listeler xlsx olarak dÄ±ÅŸa aktarÄ±labilir olmalÄ±.
Ã‡Ä±ktÄ± exports/ altÄ±na tarih-saatli dosya adÄ±yla yazÄ±lmalÄ±.

Bu fazda yazdÄ±rÄ±labilir PDF rapor Ã¼retimi ve yedekleme yapÄ±lmayacak.

Faz sonunda derleme/test doÄŸrulamasÄ±nÄ± yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarÄ±nÄ± gÃ¼ncelle.
```

v0.17 ilerleme notu (2026-06-01):

- Branch aÃ§Ä±ldÄ±: `codex/v0.17-excel-disa-aktarim`.
- `ClosedXML` eklendi ve `ExcelExportWriter` ile XLSX dÄ±ÅŸa aktarÄ±m altyapÄ±sÄ± kuruldu.
- `Faturalar` ekranÄ±na `Excel'e Aktar` butonu eklendi (mevcut filtrelenmiÅŸ liste `exports/` altÄ±na yazÄ±lÄ±r).
- `Raporlar` ekranÄ±na `Excel'e Aktar` butonu eklendi (aktif sekme tablosu `exports/` altÄ±na yazÄ±lÄ±r).
- `--self-test` iÃ§ine temel excel dosya oluÅŸumu kontrolÃ¼ eklendi.
- `dotnet build`, `--self-test` ve `--health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.

v0.17 sonuÃ§ notu:

- `Faturalar` ekranÄ±na `Excel'e Aktar` eklendi; filtrelenmiÅŸ liste `exports/` altÄ±na `.xlsx` olarak yazÄ±lÄ±r.
- `Raporlar` ekranÄ±na `Excel'e Aktar` eklendi; aktif sekme tablosu `exports/` altÄ±na `.xlsx` olarak yazÄ±lÄ±r.
- `--self-test` temel excel export dosya oluÅŸumunu doÄŸruluyor.

v0.18 baÅŸlangÄ±Ã§ talimatÄ±:

```text
C# WPF + SQLite tabanlÄ± kurum fatura takip programÄ±nda v0.18 fazÄ±nÄ± uygula.

Ã–nce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md, REGRESYON.md ve docs/04-excel-pdf-rapor-tasarim-plani.md dosyalarÄ±nÄ± oku.

Bu fazda yalnÄ±zca yazdÄ±rÄ±labilir PDF raporlar geliÅŸtirilecek.

PDF tasarÄ±mÄ±na baÅŸlamadan Ã¶nce kullanÄ±cÄ±dan en az 1 adet Excel rapor Ã¶rneÄŸi istenecek (kolonlar, toplamlar ve sayfa dÃ¼zeni birebir bu Ã¶rneÄŸe gÃ¶re yapÄ±lacak).

Bu fazda Excel dÄ±ÅŸa aktarÄ±m, yedekleme ve farklÄ± kapsam iÅŸleri yapÄ±lmayacak.

Faz sonunda derleme/test doÄŸrulamasÄ±nÄ± yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarÄ±nÄ± gÃ¼ncelle.
```

v0.18 sonuÃ§ notu (2026-06-01):

- PDF rapor motoru eklendi (QuestPDF).
- `Raporlar` ekranÄ±na `PDF'e Aktar` eklendi; aktif sekme A4 PDF olarak `exports/` altÄ±na yazÄ±lÄ±r.
- `Faturalar` ekranÄ±na `PDF'e Aktar` eklendi; filtrelenmiÅŸ liste A4 PDF olarak `exports/` altÄ±na yazÄ±lÄ±r.
- PDF ÅŸablonu standart: baÅŸlÄ±k + filtre bloÄŸu + Ã¶zet + tablo + footer (sayfa no) + imza alanÄ±.
- `--self-test` temel PDF dosyasÄ± oluÅŸumunu doÄŸruluyor.
- `dotnet build`, `--self-test` ve `--health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.

v0.19 baÅŸlangÄ±Ã§ talimatÄ±:

```text
C# WPF + SQLite tabanlÄ± kurum fatura takip programÄ±nda v0.19 fazÄ±nÄ± uygula.

Ã–nce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarÄ±nÄ± oku.

Bu fazda yalnÄ±zca manuel gÃ¼venli yedekleme geliÅŸtirilecek.

Kapsam:
- VeritabanÄ± dosyasÄ± ve attachments altÄ±ndaki evraklar ZIP iÃ§ine alÄ±nacak.
- Ã‡Ä±ktÄ± backups/ altÄ±nda tarih-saatli dosya adÄ±yla oluÅŸturulacak.
- Yedekleme iÅŸleminden sonra kullanÄ±cÄ±ya dosya yolu bildirilecek.

Bu fazda otomatik zamanlanmÄ±ÅŸ yedekleme veya bulut senaryosu yapÄ±lmayacak.

Faz sonunda derleme/test doÄŸrulamasÄ±nÄ± yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarÄ±nÄ± gÃ¼ncelle.
```

v0.16 baÅŸlangÄ±Ã§ talimatÄ±:

```text
C# WPF + SQLite tabanlÄ± kurum fatura takip programÄ±nda v0.16 fazÄ±nÄ± uygula.

Ã–nce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarÄ±nÄ± oku.

Bu fazda yalnÄ±zca evrak eksikliÄŸi ve dosya kontrol raporu geliÅŸtirilecek.

Rapor, eksik PDF (kayÄ±t var ama dosya yok), kayÄ±p dosya/eriÅŸilemeyen yol ve aynÄ± hash (muhtemel mÃ¼kerrer evrak) uyarÄ±larÄ±nÄ± gÃ¶rÃ¼nÃ¼r kÄ±lacak.

Excel/PDF dÄ±ÅŸa aktarÄ±m, yazdÄ±rÄ±labilir PDF rapor ve yedekleme yapÄ±lmayacak.

Faz sonunda derleme/test doÄŸrulamasÄ±nÄ± yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarÄ±nÄ± gÃ¼ncelle.
```

v0.16 ilerleme notu (2026-05-31):

- Branch aÃ§Ä±ldÄ±: `codex/v0.16-evrak-eksikligi-dosya-kontrol-raporu`.
- Raporlar ekranÄ±na `Evrak Kontrol` sekmesi eklendi.
- Fatura ve Ã¶deme kayÄ±tlarÄ± iÃ§in `PDF Yok`, `PDF KayÄ±p` ve `AynÄ± Hash` uyarÄ±larÄ± listeleniyor.
- Hesaplama mantÄ±ÄŸÄ± test edilebilir `DocumentHealthReportCalculator` katmanÄ±na alÄ±ndÄ±.
- `--self-test` kapsamÄ±na evrak kontrol senaryosu eklendi.
- `dotnet build`, `--self-test` ve `--health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.

v0.16 sonuÃ§ notu:

- Raporlar ekranÄ±nda `Evrak Kontrol` sekmesi hazÄ±rlandÄ± ve doÄŸrulandÄ±.
- Fatura ve Ã¶deme evraklarÄ± iÃ§in `PDF Yok`, `PDF KayÄ±p` ve `AynÄ± Hash` uyarÄ±larÄ± gÃ¶rÃ¼nÃ¼r.
- `DocumentHealthReportCalculator` eklendi ve `--self-test` kapsadÄ±.

v0.15 baÅŸlangÄ±Ã§ talimatÄ±:

```text
C# WPF + SQLite tabanlÄ± kurum fatura takip programÄ±nda v0.15 fazÄ±nÄ± uygula.

Ã–nce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarÄ±nÄ± oku.

Bu fazda yalnÄ±zca tÃ¼re Ã¶zgÃ¼ yÄ±llÄ±k fatura listesi geliÅŸtirilecek.

TÃ¼r bazlÄ± yÄ±llÄ±k toplam ve abonelik daÄŸÄ±lÄ±mÄ± gÃ¶sterilecek.

Excel/PDF dÄ±ÅŸa aktarÄ±m, yazdÄ±rÄ±labilir PDF rapor ve yedekleme yapÄ±lmayacak.

Faz sonunda uygulamayÄ± Ã§alÄ±ÅŸtÄ±r veya en azÄ±ndan derleme/test doÄŸrulamasÄ±nÄ± yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarÄ±nÄ± gÃ¼ncelle.
```

v0.15 sonuÃ§ notu:

- Raporlar ekranÄ±na `TÃ¼r YÄ±llÄ±k` sekmesi eklendi.
- Fatura tÃ¼rÃ¼ + yÄ±l seÃ§imiyle 12 aylÄ±k toplamlar gÃ¶steriliyor.
- AynÄ± ekranda abonelik daÄŸÄ±lÄ±mÄ± tablosu gÃ¶steriliyor (abonelik bazlÄ± toplamlar, bÃ¼yÃ¼kten kÃ¼Ã§Ã¼ÄŸe).
- `InvoiceTypeYearlyReportCalculator` eklendi ve `--self-test` kapsamÄ±na alÄ±ndÄ±.
- `dotnet build`, `--self-test` ve `--health-check` baÅŸarÄ±lÄ± Ã§alÄ±ÅŸtÄ±.

v0.14 sonuÃ§ notu:

- Raporlar ekranÄ±na `Abonelik YÄ±llÄ±k` sekmesi eklendi.
- 12 aylÄ±k Ã¶zet tablo ve yÄ±l toplamlarÄ± gÃ¶steriliyor.
- En yÃ¼ksek/en dÃ¼ÅŸÃ¼k ay tile olarak gÃ¶steriliyor.
- `SubscriptionYearlyReportCalculator` eklendi ve `--self-test` kapsadÄ±.

v0.13 sonuÃ§ notu:

- Raporlar ekranÄ±na `Abonelik` sekmesi eklendi.
- Abonelik + yÄ±l + ay seÃ§imiyle seÃ§ilen ay faturalarÄ± listeleniyor.
- Ã–nceki ay karÅŸÄ±laÅŸtÄ±rmasÄ± tile detaylarÄ±nda delta olarak gÃ¶steriliyor.
- `SubscriptionMonthlyComparisonCalculator` eklendi ve `--self-test` kapsadÄ±.

v0.12 sonuÃ§ notu:

- AylÄ±k liste raporuna `Fatura TÃ¼rÃ¼` filtresi eklendi.
- `MonthlyInvoiceReportCalculator` artÄ±k `invoiceTypeId` filtresi alÄ±yor.
- `--self-test` tÃ¼r filtresi senaryosuyla geniÅŸletildi.

## Dikkat Edilecekler

- Yol sorunlarÄ±na karÅŸÄ± PowerShell'de gerekirse `-LiteralPath` kullanÄ±lmalÄ±.
- Uygulama kodu baÅŸlamadan Ã¶nce .NET SDK durumu kontrol edilmeli.
- Yeni oluÅŸturulacak klasÃ¶r adlarÄ± ASCII olmalÄ±.
- Faz kapsamÄ± aÅŸÄ±lmamalÄ±.
- Ã–nce dokÃ¼mantasyon okunmalÄ±, sonra uygulama koduna geÃ§ilmeli.
- Fatura PDF raporlarÄ± fazÄ±na gelindiÄŸinde kullanÄ±cÄ±dan Excel Ã¶rneÄŸi istenmeden tasarÄ±m/uygulama yapÄ±lmamalÄ±.


(2026-06-01 / v0.23 baslangic)

1. Rapor tasarim referanslari repo icine alindi: docs/references/02-excel-pdf-rapor-tasarim-plani.md + docs/references/fatura-excel-ornekler.xlsx.
2. Excel rapor cikti format iyilestirmeleri: baslik ortalama + freeze + toplam formulleri + formatlar (ExcelExportWriter).
3. PDF rapor ozet alani 2 satira kadar genisletildi ve imza blogu opsiyonel hale getirildi (PdfReportWriter).
4. Rapor meta bilgileri config/report-meta.json ile merkezi hale getirildi (kurum satirlari cok satirli destek).
5. Raporlar ekraninda aksiyon raporlari (Odenmemis/Gecikmis/Yaklasan) export kolonlari genisletildi (odeme bilgisi kolonlari dahil) ve self-test icindeki bozuk ornek metinler duzeltildi.
6. Excel export'lara ornek sablondaki gibi "Aciklama" (notes) satiri eklendi; Raporlar Excel export'unda otomatik kisa aciklama yaziliyor.
7. `--self-test` icin `FATURATAKIP_SELFTEST_KEEP=1` env eklendi: test artifact klasoru silinmez ve path console'a yazilir; self-test Excel raporu artik tam baslik/meta + kolon ornegi uretiyor.
8. Excel export ana sayfasi ornek sablon kolonlarina yaklastirildi ve bilgi kaybi olmamasi icin mevcut genis tablo `Detay` sayfasinda korundu (Aylik, Abonelik Aylik/Yillik, Tur Yillik).
9. Raporlar ekranina `Yillik Liste` sekmesi eklendi; Excel export ana sayfasi `yillikfaturaraporu` sablon kolonlariyla hizalandi + `Detay` sayfasi korundu.
10. PDF raporlar ornek matbu gorunume yaklastirildi: imza blogu kaldirildi, baslik duzeni (ortali kurum + `Tarih :`), `Açıklama :` satiri, tablo siyah border ve template raporlarinda `GENEL TOPLAM` footer satiri eklendi.
11. PDF footer (sayfa numarasi ve otomatik metin) orneklerle uyum icin varsayilan kapali hale getirildi.
12. PDF rapor basligi sade tutuldu; tur/abonelik/yil gibi ek baglamlar basliga eklenmeyip `Açıklama :` satirinda birakildi.
13. PDF tablo baslik satiri daha kompakt hale getirildi; ust satirdaki dikey bosluk azaltildi ve matbu gorunum biraz daha sikilastirildi.
14. PDF tablo govde satirlari da biraz sikilastirildi; veri satirlarindaki dikey bosluk azaltildi ve baslik/govde dengesi iyilestirildi.
15. PDF tablo yazi boyutlari dengelendi; baslik, govde ve toplam satiri 9 puntoya cekilerek daha birornek matbu gorunum elde edildi.
16. PDF ust bolumde `Aciklama :` satiri ile tablo arasindaki bosluk daraltildi; rapor akisi daha butun gorunuyor.
17. PDF ust baslik blogu dengelendi; kurum satirlari ile rapor basligi arasindaki bosluk ve tarih kolonunun ust offset'i hafifce azaltildi.
18. Gorsel QA icin self-test PDF ve iki ek ornek PDF uretildi; aylik sablon ve odenmemis raporu yerlesim olarak temiz bulundu, belirgin yeni duzeltme ihtiyaci gorulmedi.
19. v0.21 altindaki eski restore TODO'su kapatildi: self-test icine "bos olmayan hedefe restore reddedilir" negatif smoke senaryosu eklendi.
20. v0.37 baslangici olarak audit log temeli eklendi: `audit_logs` migration'i, `AuditLogRepository` ve abonelik/fatura/odeme islemlerinde temel kayit dusme akisi self-test kapsaminda dogrulandi.

21. v0.38 olarak Raporlar ekranina Islem Gecmisi sekmesi eklendi; audit log kayitlari grid, Excel export ve PDF export uzerinden gorunur hale getirildi.
22. Smoke test: dotnet build -c Release OK.
23. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
24. Siradaki mantikli is: Islem Gecmisi icin tarih / islem turu filtreleri eklemek.


25. v0.39 olarak Islem Gecmisi sekmesine islem turu combobox'i ile baslangic/bitis tarih filtreleri eklendi.
26. Grid ve export akislarinda filtrelenmis audit log listesi kullaniliyor; export notu secili filtreyi yansitiyor.
27. Smoke test: dotnet build -c Release OK.
28. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
29. Siradaki mantikli is: kullanici / varlik bazli ek filtre veya audit log arama kutusu eklemek.


30. v0.40 olarak Islem Gecmisi ekranina varlik ve kullanici combobox filtreleri ile serbest metin arama kutusu eklendi.
31. Filtre zinciri action + entity + user + tarih + arama olarak tek yerden calisiyor; export da ayni filtrelenmis listeyi kullaniyor.
32. Smoke test: dotnet build -c Release OK.
33. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
34. Siradaki mantikli is: secili audit log satiri icin eski/yeni deger detay paneli eklemek.


35. v0.41 olarak AuditLogGrid altina detay paneli eklendi; secili kaydin eski ve yeni degerleri iki ayri kutuda gosteriliyor.
36. JSON payload varsa indent edilerek yazdiriliyor, bos degerlerde Kayit yok gosteriliyor.
37. Smoke test: dotnet build -c Release OK.
38. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
39. Siradaki mantikli is: audit log detayinda alan bazli veya renkli diff gostermek.


40. v0.42 olarak audit log detay paneline alan bazli fark tablosu eklendi.
41. JSON payload varsa flatten edilerek alan, eski deger, yeni deger ve durum kolonlariyla listeleniyor; parse edilemeyen icerikler ham olarak korunuyor.
42. Smoke test: dotnet build -c Release OK.
43. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
44. Siradaki mantikli is: diff tablosunda sadece degisen alanlari gosteren filtre veya renkli durum rozetleri eklemek.


45. v0.43 olarak audit log diff tablosuna Sadece degisen alanlari goster filtresi eklendi.
46. Ayni durumundaki satirlar gizlenebiliyor; secili audit log detay akisi korunuyor.
47. Smoke test: dotnet build -c Release OK.
48. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
49. Siradaki mantikli is: diff tablosunda renkli durum rozetleri veya vurgulu satirlar eklemek.


50. v0.44 olarak audit log diff tablosundaki durum alani renkli rozetlerle gosterilmeye baslandi.
51. Degisti, Eklendi, Silindi, Ayni durumlari ayri arka plan / yazi rengiyle daha hizli taranabiliyor.
52. Smoke test: dotnet build -c Release OK.
53. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
54. Siradaki mantikli is: audit log detayina JSON kopyalama veya disa aktarma mini aksiyonlari eklemek.


55. v0.45 olarak audit log detay paneline eski ve yeni deger icin `Kopyala` dugmeleri eklendi.
56. Kopyalama basarisi veya hata durumu `AuditLogHintText` uzerinden kullaniciya gosteriliyor.
57. Smoke test: dotnet build -c Release OK.
58. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
59. Siradaki mantikli is: tum diff'i kopyala veya detay txt/json disa aktarma aksiyonlari eklemek.


60. v0.46 olarak audit log detayindaki diff tablosuna `Tum diff'i kopyala` dugmesi eklendi.
61. Kopyalanan metin; islem, varlik, kayit id, kullanici ve tarih ustbilgisiyle birlikte gorunen diff satirlarini tab ayrimli olarak iceriyor.
62. `Sadece degisen alanlari goster` filtresi acikken kopyalama da ayni gorunur satirlari baz aliyor.
63. Smoke test: dotnet build -c Release OK.
64. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
65. Siradaki mantikli is: audit log detayini txt/json olarak disa aktarma aksiyonlari eklemek.


66. v0.47 olarak audit log detay paneline `TXT disa aktar` ve `JSON disa aktar` dugmeleri eklendi.
67. TXT cikti gorunen diff ozetini, JSON cikti ise secili kaydin ham detay + diff satirlarini `exports` klasorune yaziyor.
68. Disa aktarma sonucu veya hata durumu `AuditLogHintText` uzerinden kullaniciya gosteriliyor.
69. Smoke test: dotnet build -c Release OK.
70. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
71. Siradaki mantikli is: audit log ciktilarina ac/kopyala klasor veya filtre tercihlerini hatirlama gibi kullanim rahatligi adimlari eklemek.


72. v0.48 olarak audit log araclarina `Exports klasorunu ac` dugmesi eklendi.
73. Dugme `exports` klasorunu gerekirse olusturup Windows Gezgini ile dogrudan aciyor.
74. Klasor acma basarisi veya hata durumu `AuditLogHintText` uzerinden kullaniciya gosteriliyor.
75. Smoke test: dotnet build -c Release OK.
76. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
77. Siradaki mantikli is: audit log filtre tercihlerini hatirlama veya kayit odakli kisayollar eklemek.


78. v0.49 olarak audit log filtre tercihleri `config/audit-log-filter-preferences.json` dosyasinda saklanmaya baslandi.
79. Islem, varlik, kullanici, arama, baslangic/bitis tarihi ve `Sadece degisen alanlari goster` secimi yeni acilista geri yukleniyor.
80. Self-test icine preference save/load dogrulamasi eklendi.
81. Smoke test: dotnet build -c Release OK.
82. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
83. Siradaki mantikli is: filtreleri tek tikla sifirlama veya secili kayda hizli odaklanma aksiyonlari eklemek.


84. v0.50 olarak audit log filtre paneline `Filtreleri sifirla` dugmesi eklendi.
85. Dugme islem, varlik, kullanici, arama, tarih araligi ve `Sadece degisen alanlari goster` secimini varsayilana donduruyor.
86. Sifirlama sonrasi preference dosyasi da varsayilan degerlerle yeniden kaydediliyor.
87. Smoke test: dotnet build -c Release OK.
88. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
89. Siradaki mantikli is: secili kayda hizli odaklanma veya son disa aktarilan dosyayi acma aksiyonlari eklemek.


90. v0.51 olarak audit log araclarina `Son dosyayi ac` dugmesi eklendi.
91. Disa aktarma sonrasi son yazilan dosya yolu hatirlaniyor; oturumda bilgi yoksa `exports` altindaki en yeni `audit-log-*` dosyasi bulunup aciliyor.
92. Smoke test: dotnet build -c Release OK.
93. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
94. Siradaki mantikli is: secili kayda hizli odaklanma veya export gecmisi rahatliklari eklemek.

