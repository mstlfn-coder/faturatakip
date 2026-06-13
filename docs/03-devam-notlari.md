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



95. v0.52 olarak audit log filtre paneline Secili kayda git dugmesi eklendi.
96. Dugme secili audit log kaydini grid icinde tekrar gorunur alana getiriyor; filtre yeniden uygulandiginda korunan secim de otomatik odaklaniyor.
97. Smoke test: dotnet build -c Release OK.
98. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
99. Siradaki mantikli is: export gecmisi rahatliklari veya son kullanilan kayda donus yardimcilari eklemek.



100. v0.53 olarak audit log araclarina son export gecmisi listesi eklendi.
101. Son 5 audit export dosyasi listeleniyor; Listeden ac dugmesi secilen ciktiyi, Son dosyayi ac ise en guncel dosyayi aciyor.
102. Smoke test: dotnet build -c Release OK.
103. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
104. Siradaki mantikli is: son kullanilan kayda donus veya export gecmisi temizleme rahatliklari eklemek.



105. v0.54 olarak audit log export gecmisine Listeyi yenile dugmesi eklendi.
106. Audit log sekmesi acildiginda liste otomatik tazeleniyor; kullanici isterse butonla manuel de yenileyebiliyor.
107. Smoke test: dotnet build -c Release OK.
108. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
109. Siradaki mantikli is: export gecmisi temizleme veya son kullanilan kayda donus yardimcilari eklemek.

110. v0.55 olarak audit log export gecmisine Listeyi temizle dugmesi eklendi.
111. Temizleme akisi yalnizca audit-log-* dosyalarini hedefliyor; karisik export klasorunde baska raporlara dokunmuyor.
112. Temizleme sonrasi export listesi yeniden yukleniyor ve ipucu metni silinen dosya sayisini gosteriyor.
113. Smoke test: dotnet build -c Release OK.
114. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
115. Siradaki mantikli is: son kullanilan kayda donus veya export gecmisi icin secmeli temizleme yardimcilari eklemek.

116. v0.56 olarak audit log export gecmisine Secileni sil dugmesi eklendi.
117. Kullanici listeden tek bir audit export dosyasi secip silebiliyor; secimsiz durumda acik yonlendirme mesaji veriliyor.
118. Silinen dosya son export referansiyla ayniysa `_lastAuditLogExportPath` sifirlaniyor ve liste yeniden yukleniyor.
119. Smoke test: dotnet build -c Release OK.
120. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
121. Siradaki mantikli is: son kullanilan kayda donus veya son N kaydi koruyup digerlerini temizleme yardimcilari eklemek.

122. v0.57 olarak audit log export gecmisine Eskileri temizle dugmesi eklendi.
123. Bu akis en yeni 5 audit export dosyasini koruyor; sadece daha eski `audit-log-*` dosyalari siliniyor.
124. Temizlik sonrasi export listesi yeniden yukleniyor ve ipucu metni kac dosyanin silindigini belirtiyor.
125. Smoke test: dotnet build -c Release OK.
126. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
127. Siradaki mantikli is: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari eklemek.

128. v0.58 olarak audit log export gecmisi listesine dosya tipi etiketi ve zaman bilgisi daha okunur bicimde eklendi.
129. ComboBox item template icinde JSON/TXT tipi, zaman damgasi ve dosya adi ayri ayri gosteriliyor; tam yol tooltip olarak korunuyor.
130. Smoke test: dotnet build -c Release OK.
131. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
132. Siradaki mantikli is: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari eklemek.

133. v0.59 olarak audit log export gecmisine Tum / JSON / TXT hizli filtre secimi eklendi.
134. Tip filtresi degisince export listesi yeniden yukleniyor; secili tipe uygun dosyalar son 5 kayit mantigiyla gosteriliyor.
135. Smoke test: dotnet build -c Release OK.
136. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
137. Siradaki mantikli is: son kullanilan kayda donus veya export gecmisi icin daha zengin secim/etiket yardimcilari eklemek.

138. v0.60 olarak audit log export gecmisinde son kullanilan dosyayi gorsel olarak isaretleyen SON yardimi eklendi.
139. Kullanici bir exportu actiginda ya da son dosyayi actiginda `_lastAuditLogExportPath` yenileniyor ve listede ilgili oge isaretleniyor.
140. Smoke test: dotnet build -c Release OK.
141. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
142. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.

143. v0.61 olarak audit log export gecmisinde son kullanilan dosya gorunuyorsa otomatik secili kalma davranisi eklendi.
144. Liste yenilendiginde once SON isaretli oge seciliyor; o yoksa onceki secim korunmaya calisiliyor.
145. Smoke test: dotnet build -c Release OK.
146. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
147. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.

148. v0.62 olarak audit log export gecmisine secili dosyanin tam yolunu panoya alan Yolu kopyala dugmesi eklendi.
149. Secimsiz durumda acik yonlendirme mesaji veriliyor; secili oge varsa FilePath dogrudan panoya yaziliyor.
150. Smoke test: dotnet build -c Release OK.
151. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
152. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.

153. v0.63 olarak audit log export gecmisine secili dosyayi Windows Gezgini icinde gosteren Klasorde goster dugmesi eklendi.
154. Bu akis `explorer.exe /select` kullaniyor; kullanici secili exportu klasor icinde dogrudan odakta goruyor.
155. Smoke test: dotnet build -c Release OK.
156. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
157. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.

158. v0.64 olarak audit log export secim araçlari, secim yoksa devre disi kalan daha anlatir bir duruma getirildi.
159. Listeden ac, Yolu kopyala, Klasorde goster ve Secileni sil butonlari secim durumuna gore aktif/pasif oluyor.
160. Smoke test: dotnet build -c Release OK.
161. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
162. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.

163. v0.65 olarak audit log export gecmisinde gorunur listedeki son kullanilan ogeyi tek tikla yeniden sececek yardim eklendi.
164. Son kullanilani sec dugmesi yalnizca listede IsLastUsed isaretli bir oge gorunuyorsa aktif oluyor.
165. Smoke test: dotnet build -c Release OK.
166. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
167. Siradaki mantikli is: export gecmisi icin daha zengin secim/etiket yardimcilari veya son kullanilan kayda dogrudan donus akisi eklemek.


168. v0.66 baslangici olarak Faturalar ekranina Sonrakini Hazirla dugmesi eklendi.
169. Bu akis secili faturadan yeni kayit taslagi uretiyor; abonelik, tutar, kullanim, birim ve aciklama korunurken fatura no bos birakiliyor.
170. Taslakta fatura tarihi ve son odeme tarihi bir ay ileri tasiniyor; yil donusu senaryosu InvoiceDraftTemplateBuilder self-test'i ile dogrulandi.
171. Smoke test: dotnet build -c Release OK.
172. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
173. Siradaki mantikli is: v0.66 cekirdek is akisini odeme girisi rahatliklariyla derinlestirmek.

174. v0.67 olarak odeme formuna Kalani Doldur ve Son Odemeden Doldur dugmeleri eklendi.
175. PaymentEntrySuggestionBuilder, yeni odeme taslaginda kalan tutari kullanip varsa en guncel bos olmayan aciklamayi oneriyor.
176. Bos veya anlamsiz son aciklamalar yeni taslaga tasinmiyor; bu davranis self-test ile dogrulandi.
177. Smoke test: dotnet build -c Release OK.
178. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
179. Siradaki mantikli is: odeme girisi rahatliklarini biraz daha derinlestirip sonra yedekleme UX fazina gecmek.

180. v0.68 olarak odeme formuna Secili Odemeden Doldur dugmesi eklendi.
181. Bu akis secili odemenin aciklamasini ve tutarini yeni taslaga tasiyor; tutar mevcut kalan odemeyi asmayacak sekilde kirpiliyor.
182. Secili odeme yoksa ya da kalan tutar yoksa kullaniciya acik yonlendirme mesaji veriliyor.
183. Smoke test: dotnet build -c Release OK.
184. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
185. Siradaki mantikli is: odeme girisi rahatliklarini tamamlayip yedekleme UX fazina gecmek.
186. v0.69 olarak Yedekleme ekranina son 5 zip yedegi gosteren liste eklendi.
187. BackupFileCatalog yardimcisi ile en guncel yedekler siralanip UI listesine baglandi.
188. Secileni Kullan dugmesi secili yedegi restore zip alanina tasiyor; Zipi Ac dugmesi ise secili zipi dogrudan aciyor.
189. Self-test icine backup katalog siralama ve son N limiti dogrulamasi eklendi.
190. Smoke test: dotnet build -c Release OK.
191. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
192. Siradaki mantikli is: yedekleme UX tarafinda secili yedegi klasorde gosterme veya restore hedefi secmeyi kolaylastirma.
193. v0.70 olarak backup listesindeki secili zip icin Klasorde Goster dugmesi eklendi.
194. Bu akis `explorer.exe /select` ile dogrudan secili zip dosyasini Windows Gezgini icinde odakli aciyor.
195. Dugme, secim yokken devre disi kaliyor; boylece backup listesindeki aksiyonlar daha kendini anlatir hale geldi.
196. Smoke test: dotnet build -c Release OK.
197. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
198. Siradaki mantikli is: restore hedef klasoru secimini daha rahat hale getirmek.
199. v0.71 olarak restore hedef alani icin Hedef Sec dugmesi eklendi.
200. OpenFolderDialog ile bos klasor secme akisi, mevcut yol varsa orayi ya da parent klasoru baslangic noktasi olarak kullaniyor.
201. Boylece geri yukleme hedefini elle yazma yukunu azalttik; hedef yolu seciciyle doldurmak daha hizli hale geldi.
202. Smoke test: dotnet build -c Release OK.
203. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
204. Siradaki mantikli is: secilen restore hedefinin bos/uygun oldugunu daha gorunur sekilde onceden gostermek.
205. v0.72 olarak restore hedefi icin canli uygunluk metni eklendi.
206. BackupRestoreService icine ortak hedef degerlendirme yordamı tasindi; UI ve restore akisi ayni kontrol mantigini kullaniyor.
207. Hedef klasor henuz yoksa "olusturulacak", varsa ve bossa "uygun", doluysa "kullanilamaz" mesaji gosteriliyor.
208. Geri Yukle dugmesi de bu uygunluk sonucuyla senkron pasif/aktif hale getirildi.
209. Smoke test: dotnet build -c Release OK.
210. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
211. Siradaki mantikli is: uygun olmayan restore hedefinde kullaniciyi bos klasor olusturmaya yonlendiren daha aktif bir rahatlik eklemek.
212. v0.73 olarak restore alanina Bos Klasor Olustur dugmesi eklendi.
213. Bu akis mevcut hedefe ya da parent klasore gore uygun bir temel klasor secip benzersiz bir `restore_target_yyyyMMdd_HHmmss` klasoru uretiyor.
214. Olusturulan bos klasor hedef alanina otomatik yaziliyor; kullanici elle klasor olusturmak zorunda kalmiyor.
215. Self-test icine benzersiz klasor adi uretilmesi ve klasorun fiziksel olarak olusmasi dogrulamasi eklendi.
216. Smoke test: dotnet build -c Release OK.
217. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
218. Siradaki mantikli is: restore akisini zip + hedef + uygunluk ozetleyen son bir onizleme kutusuyla tamamlamak.
219. v0.74 olarak restore bolumune onizleme kutusu eklendi.
220. Bu kutu secili zip, hedef klasorun uygunluk mesaji ve geri yukleme hazirlik sonucunu tek bakista gosteriyor.
221. BackupRestoreService icine BuildPreviewSummary yardimi eklendi; UI ve self-test ayni ozet mantigini kullaniyor.
222. Smoke test: dotnet build -c Release OK.
223. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
224. Siradaki mantikli is: backup UX tarafini yeterince olgun kabul edip yeni urun fazina gecmek.
225. v0.75 olarak gosterge panelindeki Odenmemis ve Gecikmis kartlarina Rapora Git dugmeleri eklendi.
226. `MainWindow` tarafinda bu dugmeler Raporlar ekranini acip ilgili actionable rapor sekmesini secili hale getiriyor.
227. `ReportsView` icine `ShowUnpaidReport()` ve `ShowOverdueReport()` yardimlari eklendi; panel kendi sekmesini dogrudan acabiliyor.
228. Smoke test: dotnet build -c Release OK.
229. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
230. Siradaki mantikli is: dashboard tarafinda eksik evrak veya aylik tahsilat icin de benzer hizli aksiyonlar eklemek.
231. v0.76 olarak dashboard uzerindeki Bu Ay Fatura ve Bu Ay Odeme kartlarina `Listeye Git` dugmeleri eklendi.
232. Ayni turda Fatura PDF Eksik ve Odeme PDF Eksik kartlarina `Kontrole Git` dugmeleri eklendi.
233. `MainWindow` bu kisayollari Raporlar ekranindaki Aylik Liste ve Evrak Kontrol sekmelerine bagliyor.
234. `ReportsView` icine `ShowMonthlyReport()` ve `ShowDocumentHealthReport()` yardimlari eklendi.
235. Smoke test: dotnet build -c Release OK.
236. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
237. Siradaki mantikli is: dashboard tarafinda aktif abonelik veya toplam fatura gibi kartlardan liste ekranlarina hizli gecis eklemek.
238. v0.77 olarak dashboard uzerindeki Fatura Turu ve Aktif Tur kartlarina `Turlere Git` dugmeleri eklendi.
239. Ayni turda Aktif Abonelik kartina `Aboneliklere Git`, Toplam Fatura kartina `Faturalara Git` dugmesi eklendi.
240. Bu kisayollar `MainWindow` icinden ilgili ekran panellerini dogrudan aciyor; ek rapor sekmesi gerektirmiyor.
241. Smoke test: dotnet build -c Release OK.
242. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
243. Siradaki mantikli is: dashboard kisayollarini yeterince olgun kabul edip liste ekranlarinda toplu is akisi hizlandirmaya geri donmek.
244. v0.78 olarak Faturalar ekranina Bu Ay, Odenmemis, Gecikmis ve PDF Eksik hizli filtre dugmeleri eklendi.
245. Bu kisayollar mevcut filtreleri tek tikla kuruyor, sonuc listesini yeniliyor ve varsa ilk kayda otomatik odaklaniyor.
246. Filtre sonucundaki kayit sayisi icin ayri bir ipucu metni eklendi; kullanici kac kayitla calistigini hemen gorebiliyor.
247. Smoke test: dotnet build -c Release OK.
248. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
249. Siradaki mantikli is: hazir filtre sonucunu tek tikla disa aktarma ya da toplu PDF kontrol akisiyla daha ileri hizlandirmak.
250. v0.79 olarak Faturalar ekranindaki PDF alanina `Klasorde Goster` dugmesi eklendi.
251. PDF varsa dosya Windows Gezgini icinde secili aciliyor; yoksa faturanin yil/ay klasoru bulunup gerekirse olusturularak aciliyor.
252. `InvoiceRepository` icine `GetPdfDirectoryAbsolutePath()` eklendi; UI beklenen ek klasorunu repository mantigiyla hesapliyor.
253. Smoke test: dotnet build -c Release OK.
254. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
255. Siradaki mantikli is: `PDF Eksik` ya da `Gecikmis` hizli filtresini tek tikla disa aktarma veya toplu kontrol turuna baglamak.
256. v0.80 olarak Faturalar ekranindaki export dugmeleri filtreli liste baglamini dosya adina tasiyacak sekilde guncellendi.
257. InvoiceExportContextBuilder eklendi; pdf-eksik, gecikmis, u-ay gibi sluglar tek noktadan uretiliyor.
258. Export dugmeleri artik gorunur listeyi acikca hedefliyor ve durum mesajinda aktif filtre baglamini da yaziyor.
259. Self-test icine filtreli export slug dogrulamalari eklendi.
260. Siradaki mantikli is: filtreli listeyi toplu PDF kontrol ya da siradaki kayda hizli gecis akisi ile birlestirmek.
261. v0.81 olarak Faturalar ekranindaki PDF alanina Onceki Kayit ve Sonraki Kayit dugmeleri eklendi.
262. InvoiceReviewNavigator yardimcisi eklendi; filtreli liste icinde ileri/geri gezinme sinirlari tek noktadan kontrol ediliyor.
263. Secili kaydin listedeki sirasi Kontrol sirasi: x/y metniyle gosteriliyor.
264. Self-test icine kontrol turu ileri/geri ve sinir davranisi dogrulamalari eklendi.
265. Siradaki mantikli is: bu gezinmeyi sadece PDF Eksik ya da Gecikmis akisina odaklayan daha yonlendirilmis bir inceleme modu ile tamamlamak.
266. v0.82 olarak Faturalar ekranina PDF Eksik Turu ve Gecikmis Turu kisayollari eklendi.
267. Bu kisayollar filtreyi tek tikla kuruyor, ilk kayda odaklaniyor ve kontrol turunu aktif alt kume uzerinde baslatiyor.
268. InvoiceReviewNavigator artik kontrol ipucu metnini de uretiyor; mod adi ve sira ayni yerde gorunuyor.
269. Self-test icine inceleme modu ipucu dogrulamasi eklendi.
270. Siradaki mantikli is: inceleme modunda operator ritmini daha da hizlandiracak PDF Ac / Klasorde Goster / Sonraki Kayit tipi akici kisayollar eklemek.
271. v0.83 olarak Faturalar ekranindaki inceleme turuna PDF Ac + Sonraki ve Klasor + Sonraki dugmeleri eklendi.
272. Bu birlesik aksiyonlar mevcut PDF acma ve klasor gosterme davranisini bozmayip hemen sonraki kayda gecisle birlestiriyor.
273. Son kayitta birlesik aksiyon yine calisiyor ama ileri gitmek yerine tur sonu mesaji veriyor.
274. Boylece operator akisi ayri ayri PDF Ac / Klasorde Goster / Sonraki Kayit tiklari yerine daha seri bir ritme kavustu.
275. Siradaki mantikli is: bu akisa klavye odakli kisayol ya da minik isaretleme rahatligi eklemek.
276. v0.84 olarak Faturalar ekranindaki inceleme turuna klavye kisayollari eklendi.
277. Ctrl+Shift+Sol/Sag onceki/sonraki kayit icin, Ctrl+Shift+O ve Ctrl+Shift+K ise birlesik inceleme aksiyonlari icin calisiyor.
278. Kontrol ipucu artik bu kisayollari da yaziyor; kullanici akisi ekrandan ogrenebiliyor.
279. Self-test icine kisayol ipucu metni dogrulamasi eklendi.
280. Siradaki mantikli is: inceleme akisina minik isaretleme/not rahatligi eklemek.
281. v0.85 olarak Faturalar ekranindaki PDF/Inceleme bolumune kalici inceleme notu alani eklendi.
282. Inceleme notu ve son inceleme zamani veritabaninda ayri alanlarda tutuluyor: `review_note` ve `reviewed_at`.
283. Kullanici secili fatura icin not kaydedebiliyor, isterse tek tikla isareti temizleyebiliyor.
284. InvoiceRepository icine `UpdateReviewStatus()` ve `ClearReviewStatus()` eklendi; audit log tarafinda `invoice_review_updated` kaydi olusuyor.
285. Self-test icine inceleme notu kaydetme/temizleme ve audit log dogrulamasi eklendi.
286. Smoke test: dotnet build -c Release OK.
287. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
288. Siradaki mantikli is: bu not akisini operator ritmine daha da yaklastiracak "bakildi + sonraki kayit" gibi minik yardimlar dusunmek.
289. v0.86 olarak Faturalar ekranina `Bakıldı + Sonraki` yardimi eklendi.
290. Bu akis secili faturanin inceleme notunu ve `reviewed_at` bilgisini kaydedip filtreli listedeki bir sonraki kayda otomatik geciyor.
291. Tur sonunda ise kayit yine isaretleniyor ama kullaniciya turun bittigi acikca soyleniyor.
292. Ayni turda programdaki gorunur Turkce karakter sorunlari duzeltildi.
293. Ozellikle Faturalar ve Raporlar ekranlarindaki bozuk metinler ile `PDF Kayıp` gibi durum etiketleri dogru Turkce karakterlerle gosterilir hale getirildi.
294. Smoke test: dotnet build -c Release OK.
295. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
296. Siradaki mantikli is: inceleme turu icin gerekirse “bakildi” durumuna gore filtreleme ya da daha hizli klavye yardimi eklemek.
297. v0.87 olarak Faturalar ekranina `Tum Inceleme / Incelendi / Incelenmedi` review filtresi eklendi.
298. Hazir filtre dugmelerine de `Incelenmedi` kisayolu eklendi; operator inceleme notu birakilmamis kayitlari tek tikla ayirabiliyor.
299. `InvoiceFilter` artik reviewed/unreviewed ayrimini da yapiyor; self-test icine bu senaryolar eklendi.
300. Bu turda Faturalar ekranindaki kalan gorunur Turkce etiket sorunlari da toparlandi.
301. Smoke test: dotnet build -c Release OK.
302. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
303. Siradaki mantikli is: inceleme durumunu dashboard veya rapor akislarina tasiyip daha ust seviye takip gorunumu sunmak.
304. v0.88 olarak dashboard uzerine `Inceleme Bekleyen` karti eklendi.
305. Bu kart `DashboardSummary` icindeki incelenmedi fatura sayisini gosteriyor ve `Faturalara Git` dugmesiyle Faturalar ekranini dogrudan `Incelenmedi` filtresiyle aciyor.
306. `InvoicesView` icine `ShowUnreviewedInvoices()` yardimi eklendi; dashboard yonlendirmesi filtreyi kurup listeyi ilk kayda odakliyor.
307. Self-test icine dashboard ozetinde incelenmedi sayisi dogrulamasi eklendi.
308. Smoke test: dotnet build -c Release OK.
309. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
310. Siradaki mantikli is: inceleme durumunu rapor/export akislarina da tasiyip ust seviye takip gorunumu olusturmak.
311. v0.89 olarak Raporlar ekranindaki actionable sekmelere `Incelenmedi` sekmesi eklendi.
312. `ActionableInvoiceReport` icine incelenmedi liste ve kalan toplam alani eklendi; `ReportsView` bu sekmeyi grid, tile ve export basligina bagliyor.
313. `ShowUnreviewedReport()` yardimi da eklendi; boylece ileride dashboard veya baska yonlendirmeler rapor ekranini dogrudan bu sekmeyle acabilecek.
314. Self-test icine actionable rapor tarafinda incelenmedi sayisi ve kalan toplam dogrulamasi eklendi.
315. Smoke test: dotnet build -c Release OK.
316. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
317. Siradaki mantikli is: rapor ekranindaki bu incelenmedi gorunumunu Faturalar ekranindaki filtreli inceleme akisi ile cift yonlu baglamak.
318. v0.90 olarak `Incelenmedi` rapor sekmesine `Faturalarda Incele` gecis dugmesi eklendi.
319. `ReportsView` bu gecis icin bir event yayinliyor; `MainWindow` bunu yakalayip `InvoicesView.StartUnreviewedReviewMode()` akisini calistiriyor.
320. Faturalar ekrani bu akista `Incelenmedi Inceleme` moduyla aciliyor; filtre kuruluyor, hizli filtreler sifirlaniyor ve ilk kayda odaklaniliyor.
321. Smoke test: dotnet build -c Release OK.
322. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
323. Siradaki mantikli is: benzer rapordan inceleme akisina gecisleri gecikmis veya PDF eksik odakli operator turlarina da yaymak.
324. v0.91 olarak rapordaki hizli inceleme paneli `Gecikmis` ve `Evrak Kontrol` sekmelerinde de aktif hale getirildi.
325. `ReportsView` aktif sekmeye gore artik `OverdueInvoiceReviewRequested` ve `MissingPdfInvoiceReviewRequested` event'lerini de yayinlayabiliyor.
326. `MainWindow` bu event'leri `InvoicesView.StartOverdueReviewMode()` ve `InvoicesView.StartMissingPdfReviewMode()` akislarina bagladi.
327. Boylece rapor ekranindan gecikmis veya PDF eksik inceleme turlarina tek tikla gecis saglandi.
328. Smoke test: dotnet build -c Release OK.
329. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
330. Siradaki mantikli is: rapordan inceleme akisina geciste secili satir veya issue baglamini da koruyacak daha hedefli yonlendirmeler dusunmek.
331. v0.92 olarak rapordan inceleme akisina geciste secili kaydin `invoice id` baglami da tasinmaya baslandi.
332. `ReportRow` ve `DocumentHealthRow` kaynak fatura kimligini tasiyor; review gecis dugmesi aktif sekmeye gore bu kimligi event args icine koyuyor.
333. `MainWindow` tercih edilen kaydi `InvoicesView` review moduna iletiyor; filtreli listede varsa ayni kayit seciliyor, yoksa ilk uygun kayitla akış devam ediyor.
334. Smoke test: dotnet build -c Release OK.
335. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
336. Siradaki mantikli is: secili issue tipi veya rapor alt filtresi gibi daha zengin operator baglamlarini da gecis ipucuna eklemek.
337. v0.93 olarak rapordan gelen gecis istegine `ContextLabel` bilgisi eklendi.
338. `ReportsView` bu etiketi aktif sekmeye gore uretiyor; evrak kontrolde secili satirin issue tipi varsa `Rapor: Evrak Kontrol > ...` seklinde detaylandiriyor.
339. `InvoiceReviewNavigator` artik kontrol ipucuna `Baglam: ...` alanini da ekleyebiliyor; Faturalar ekraninda operator neden o akisa geldigini kaybetmiyor.
340. Self-test icine yeni baglamli kontrol ipucu dogrulamasi eklendi.
341. Smoke test: dotnet build -c Release OK.
342. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
343. Siradaki mantikli is: aktif alt filtre veya secili export baglamini da bu ipucu zincirine katmak.
344. v0.94 olarak baglam etiketi secili kayit ozetiyle genisletildi.
345. Actionable raporlarda secili satirdan `Tur / Fatura No`, evrak kontrolde ise `IssueType / EntityType / PeriodOrDate` ozeti baglama eklendi.
346. Boylece operator inceleme ekraninda neden o akisa geldigini daha somut okuyabiliyor.
347. Smoke test: dotnet build -c Release OK.
348. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
349. Siradaki mantikli is: bu baglami kopyalama ya da gorunurluk tercihi gibi minik operator rahatliklariyla tamamlamak.
350. v0.95 olarak inceleme ipucuna `Bağlamı Göster` kutusu ve `Bağlamı Kopyala` dugmesi eklendi.
351. Baglam gorunurlugu artik operator tarafinda anlik sadeleştirilebiliyor; kopyalama aksiyonu mevcut context label'i panoya tasiyor.
352. Smoke test: dotnet build -c Release OK.
353. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
354. Siradaki mantikli is: bu yardimlari kalici tercih veya klavye rahatligi ile tamamlamak.
355. v0.96 olarak `Bağlamı Göster` secimi kalici kullanici tercihine baglandi.
356. `InvoiceReviewPreferences` eklendi; tercih `config/invoice-review-preferences.json` icine yazilip acilista geri okunuyor.
357. `InvoicesView` kutuyu son kaydedilen secime gore kuruyor; kaydetme hatasi olursa ekran akisi bozulmuyor.
358. Self-test icine inceleme baglami gorunurluk tercihinin load/save dogrulamasi eklendi.
359. Smoke test: dotnet build -c Release OK.
360. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
361. Siradaki mantikli is: bu inceleme yardimlarini klavye kisayolu veya daha hizli operator aksiyonlariyla tamamlamak.
362. v0.97 olarak inceleme turuna `Ctrl+Shift+B` ile baglam gorunurlugunu ac/kapat kisayolu eklendi.
363. `InvoicesView` icinde bu kisayol `Bağlamı Göster` kutusunu tersleyip kullaniciya durum mesaji veriyor.
364. `InvoiceReviewNavigator` kisayol ipucu metni de yeni kombinasyonu gosterecek sekilde guncellendi.
365. Self-test icindeki kontrol modu kisayol ipucu dogrulamasi yeni metne gore guncellendi.
366. Smoke test: dotnet build -c Release OK (calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu, build basarili tamamladi).
367. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
368. Siradaki mantikli is: baglam kopyalama aksiyonunu da klavye rahatligiyla tamamlamak.
369. v0.98 olarak inceleme turuna `Ctrl+Shift+C` ile baglam kopyalama kisayolu eklendi.
370. `CopyInvoiceReviewContextButton_Click()` akisi tek yardimci metoda tasindi; buton ve klavye ayni davranisi kullaniyor.
371. `InvoiceReviewNavigator` kisayol ipucu metni `Ctrl+Shift+C` bilgisini de gosterecek sekilde guncellendi.
372. Self-test icindeki kontrol modu kisayol ipucu dogrulamasi yeni metne gore guncellendi.
373. Smoke test: dotnet build -c Release OK (calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu, build basarili tamamladi).
374. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
375. Siradaki mantikli is: inceleme baglami yardimlarini rapordan gelen operator gecislerinde daha gorunur hale getirmek.
376. v0.99 olarak rapordan gelen inceleme baglami Faturalar ekraninda ayri bir bilgi paneline tasindi.
377. `Bağlamı Göster` acikken bu panel gorunuyor; kontrol ipucu satiri ise tekrar sadece gezinme ve kisayol bilgisini veriyor.
378. `UpdateInvoiceReviewContextPresentation()` eklendi; panel gorunurlugu, panel metni ve `Bağlamı Kopyala` dugmesinin aktif/pasif durumu tek noktadan yonetiliyor.
379. Baglam yoksa kopyalama dugmesi pasif kaliyor; baglam varsa okunur bir blokta ozet sunuluyor.
380. Smoke test: dotnet build -c Release OK (calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu, build basarili tamamladi).
381. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
382. Siradaki mantikli is: bu baglam panelini secili issue tipi veya rapor sekmesi icin daha kisa rozetli ozetlerle zenginlestirmek.
383. v1.00 olarak inceleme baglam paneline rozetli ozetler eklendi.
384. `InvoiceReviewContextFormatter` yeni yardimcisi baglam etiketini once `>` ile, sonra gerekirse `/` ile parcalayip chip listesi uretiyor.
385. `InvoicesView` baglam panelinde bu rozetleri `ItemsControl + WrapPanel` ile gosteriyor; tam metin de altta korunuyor.
386. Self-test icine `Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01` ornegiyle rozet parcasi dogrulamasi eklendi.
387. Smoke test: dotnet build -c Release OK (calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu, build basarili tamamladi).
388. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
389. Siradaki mantikli is: rozetlerde rapor tipi veya issue turu icin renk farkliligi ya da ikonlu ayrim dusunmek.
390. v1.01 olarak baglam rozetlerine tur bazli renk ayrimi eklendi.
391. `InvoiceReviewContextFormatter` her rozet icin `report`, `issue`, `entity`, `period` veya `detail` tipi uretiyor.
392. `InvoicesView.xaml` icindeki chip stilleri `DataTrigger` ile bu tipe gore arka plan ve yazi rengini degistiriyor.
393. Self-test rozet parcalarinin yalnizca metnini degil, tip atamalarini da dogrulayacak sekilde genisletildi.
394. Smoke test: dotnet build -c Release OK.
395. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
396. Siradaki mantikli is: rozetlere hafif ikon veya kisaltma ekleyip taramayi daha da hizlandirmak.
397. v1.02 olarak baglam rozetlerine tip bazli kisa on ekler eklendi.
398. `InvoiceReviewContextFormatter` her rozet icin `Prefix` uretiyor: `RPR`, `ISS`, `VAR`, `DNM`, `DET`.
399. `InvoicesView.xaml` rozet icinde bu on eki kucuk ama belirgin bir yardimci metin olarak gosteriyor.
400. Self-test metin ve tip atamasina ek olarak Prefix atamalarini da dogruluyor.
401. Smoke test: dotnet build -c Release OK.
402. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
403. Siradaki mantikli is: rozetlerin sirasini daha baglam-duyarli hale getirmek veya bu panelden ek odak aksiyonlari uretmek.
404. v1.03 olarak baglam rozetleri sabit tip onceligine gore siralanir hale getirildi.
405. `InvoiceReviewContextFormatter` tekrar eden kind+text rozetlerini tekillestirip `report -> issue -> entity -> detail -> period` sirasiyla donduruyor.
406. Self-test icine daginik ve tekrarli bir baglam etiketinden uretilen rozetlerin sayi ve sira dogrulamasi eklendi.
407. Smoke test: dotnet build -c Release OK.
408. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
409. Siradaki mantikli is: bu panelden dogrudan baglama dayali filtre veya odak aksiyonu cikarmak.
410. v1.04 olarak baglam rozet tekillestirme mantigi sadeleştirildi.
411. `InvoiceReviewContextFormatter` icindeki `DistinctBy` tabanli tuple karsilastirmasi yerine `GroupBy(...).First()` akisi kullanildi.
412. Bu duzeltme, v1.03 sonrasinda self-testte yakalanan tekrarli baglam parcasi regresyonunu kapatti.
413. Smoke test: dotnet build -c Release OK.
414. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
415. Siradaki mantikli is: bu panelden dogrudan baglama dayali filtre veya odak aksiyonu cikarmak.
416. v1.05 olarak inceleme baglam paneline `Bağlam Filtresi` aksiyonu eklendi.
417. `InvoiceReviewContextFormatter.TryResolveSuggestedFilter()` baglam metninden `Unreviewed`, `Overdue` veya `MissingPdf` onerisi cikariyor.
418. `InvoicesView` icindeki yeni dugme bu oneriyi uygun filtreye cevirip tek tikla uyguluyor; onerisi olmayan baglamlarda pasif kaliyor.
419. Self-test baglamdan filtre cikarimi icin incelenmedi, gecikmis ve evrak kontrol senaryolarini dogruluyor.
420. Smoke test: dotnet build -c Release OK.
421. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
422. Siradaki mantikli is: baglama dayali daha odakli alt filtre veya secili kayda donus aksiyonlari eklemek.
423. v1.06 olarak inceleme baglam paneline `Bağlam Kaydına Git` aksiyonu eklendi.
424. `InvoicesView` rapor gecisinden gelen `preferred invoice id` bilgisini ayri alanda tutuyor.
425. Panel aksiyonu uygun review baglami varsa ilgili modu yeniden kurup ayni kayda odaklanmaya calisiyor.
426. Tercihli kayit yoksa ya da mevcut filtre icinde bulunamiyorsa kullaniciya acik durum mesaji veriliyor.
427. Smoke test: dotnet build -c Release OK.
428. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
429. Siradaki mantikli is: baglam panelinden alt filtre, donem veya issue bazli daha ince odak aksiyonlari eklemek.
430. v1.07 olarak inceleme baglam paneline `Bağlam Dönemi` aksiyonu eklendi.
431. `InvoiceReviewContextFormatter.TryResolvePeriod()` baglam rozetlerinden `yyyy-MM` donem bilgisini ayristiriyor.
432. `InvoicesView` icindeki yeni dugme yil ve ay filtresini bu doneme gore kurup gorunur listeyi yeniliyor.
433. Self-test baglamdan `2026-01` donemi cikarimini dogruluyor.
434. Smoke test: dotnet build -c Release OK.
435. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
436. Siradaki mantikli is: issue tipi veya varlik tipine gore daha ince alt filtre aksiyonlari eklemek.
437. v1.08 olarak inceleme baglam paneline `Bağlam Türü` aksiyonu eklendi.
438. `InvoiceReviewContextFormatter.TryResolveInvoiceTypeName()` actionable rapor baglamindan fatura turunu ayristiriyor.
439. `InvoicesView` icindeki yeni dugme mevcut fatura turu filtresini bu baglama gore secip gorunur listeyi yeniliyor.
440. Evrak Kontrol baglamlari yanlis pozitif olmasin diye kapsam disi tutuldu.
441. Self-test hem tur cikarimini hem de negatif senaryoyu dogruluyor.
442. Smoke test: dotnet build -c Release OK.
443. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
444. Siradaki mantikli is: baglamdan daha ince kayit detayi veya ek hedefli aksiyonlar cikarmak.
445. v1.09 olarak inceleme baglam paneline `Bağlam No` aksiyonu eklendi.
446. `InvoiceReviewContextFormatter.TryResolveInvoiceNumber()` actionable rapor baglamindan fatura numarasini ayristiriyor.
447. `InvoicesView` icindeki yeni dugme arama kutusunu bu numarayla doldurup gorunur listeyi yeniliyor.
448. Evrak Kontrol baglamlari bu aksiyon icin kapsam disi tutuldu.
449. Self-test hem fatura no cikarimini hem de negatif senaryoyu dogruluyor.
450. Smoke test: dotnet build -c Release OK.
451. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
452. Siradaki mantikli is: baglam paneli metinlerini ve Turkce karakter temizliklerini sistematik bicimde toparlamak.

453. v1.10 olarak `InvoicesView` baglam panelindeki gorunur Turkce metinler temizlendi.
454. Baglam aksiyon dugmeleri ve ilgili durum mesajlari dogru Turkce karakterlerle gosterilir hale getirildi.
455. `LooksLikePeriod()` mantigi yalnizca gercek `yyyy-MM` formatini donem sayacak sekilde daraltildi.
456. Boylece `INV-001` gibi fatura numaralari `Bağlam Türü` ve `Bağlam No` parser'inda yanlislikla donem sayilmiyor.
457. Smoke test: dotnet build -c Release OK.
458. Smoke test: dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
459. Siradaki mantikli is: benzer gorunur Turkce metin bozulmalarini `ReportsView` ve ortak uygulama metinlerinde toparlamak.

460. v1.11 olarak `ReportsView` icindeki gorunur Turkce rapor metinleri temizlendi.
461. Sekme/alt baslik metinleri ile PDF rapor basliklarindaki `Ödenmemiş`, `Gecikmiş`, `Yaklaşan`, `İncelenmedi`, `İşlem Geçmişi` ifadeleri duzeltildi.
462. `App.xaml.cs` icindeki acilis hata basligi `Uygulama başlatılamadı` olarak guncellendi.
463. Smoke test: dotnet build -c Release OK.
464. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
465. Siradaki mantikli is: dokuman dosyalarindaki encoding kalintilarini ayri bir turda toparlamak.

466. v1.12 olarak `docs/06-guncel-durum-ozeti.md` olusturuldu.
467. `docs/00-codex-devam-kilavuzu.md` temiz UTF-8 icerikle yeniden yazildi.
468. Yeni chatlerde once temiz ozet dosyasinin okunmasi esas alindi.
469. Smoke test: dotnet build -c Release OK.
470. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
471. Siradaki mantikli is: tarihsel dokumanlardaki encoding kalintilarini parca parca temizlemek veya uygulama akisinda yeni kucuk iyilestirmeye gecmek.

472. v1.13 olarak inceleme baglam paneline `Bağlamı Daralt` aksiyonu eklendi.
473. Bu aksiyon baglamdan cozulebilen filtre, donem, fatura turu ve fatura no ipuclarini tek seferde uyguluyor.
474. `InvoiceReviewContextFormatter` icindeki Turkce baglam eslesmeleri de temizlenerek parser daha tutarli hale getirildi.
475. Smoke test: dotnet build -c Release OK.
476. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
477. Siradaki mantikli is: daraltma aksiyonunu secili kayda otomatik atlama veya review modunu yeniden kurma rahatligiyla zenginlestirmek.

478. v1.14 olarak `Bağlamı Daralt` sonrasi secim davranisi netlestirildi.
479. Baglamdan gelen tercihli kayit filtre sonucunda listede varsa dogrudan o kayda odaklaniliyor.
480. Tercihli kayit bulunamazsa akis ilk uygun kayda dusuyor ve durum mesaji fallback bilgisini acikca veriyor.
481. Smoke test: dotnet build -c Release OK.
482. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
483. Siradaki mantikli is: tek tikla review modunu yeniden kuran daha guclu bir baglam aksiyonu eklemek.

484. v1.15 olarak baglam panelindeki aksiyon `Bağlamdan İncele` olacak sekilde guclendirildi.
485. Uygun baglamlarda ilgili review modu otomatik kuruluyor.
486. Donem, tur ve fatura no gibi ikincil ipuclari ayni akis icinde uygulanip en uygun kayda odaklaniliyor.
487. Smoke test: dotnet build -c Release OK.
488. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
489. Siradaki mantikli is: bu akis icin klavye kisayolu veya daha belirgin operator ipucu eklemek.

490. v1.16 olarak `Bağlamdan İncele` akisina `Ctrl+Shift+I` kisayolu eklendi.
491. Inceleme ipucu satirindaki kisayol listesi guncellendi.
492. Self-test beklentisi yeni kisayolu kapsayacak sekilde guncellendi.
493. Smoke test: dotnet build -c Release OK.
494. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
495. Siradaki mantikli is: baglam aksiyonlari icin daha gorunur operator ipucu veya tooltip katmani eklemek.

496. v1.17 olarak baglam panelindeki aksiyonlara aciklayici tooltip'ler eklendi.
497. `Bağlamdan İncele` tooltip'i davranis ozetini ve `Ctrl+Shift+I` kisayolunu gosterir hale getirildi.
498. Bu tur islev degistirmeden kesfedilebilirligi artirdi.
499. Smoke test: dotnet build -c Release OK.
500. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
501. Siradaki mantikli is: baglam aksiyonlarini daha derli toplu gosterecek kompakt toolbar duzeni dusunmek.

502. v1.18 olarak baglam aksiyon satiri `WrapPanel` tabanli daha dayanikli bir duzene alindi.
503. Checkbox ve baglam dugmeleri dar alanda alt satira akabilecek hale getirildi.
504. Bu tur davranis degistirmeden duzen kararliligini artirdi.
505. Smoke test: dotnet build -c Release OK.
506. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
507. Siradaki mantikli is: baglam aksiyonlari icin daha belirgin bir birincil/ikincil grup hiyerarsisi kurmak.

508. v1.19 olarak baglam aksiyonlari `Ana Aksiyonlar` ve `Detay Araçlari` olarak ayrildi.
509. `Bağlamdan İncele` birincil dugme yapilarak ana akis daha belirgin hale getirildi.
510. Donem, tur ve no aksiyonlari ikincil satira alinarak panel daha okunur hale getirildi.
511. Smoke test: dotnet build -c Release OK.
512. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
513. Siradaki mantikli is: baglam panelinde secili aksiyonlara daha belirgin gorsel vurgu eklemek.

514. v1.19.1 hotfix olarak uygulama acilisindaki `TargetInvocationException` / `NullReferenceException` hatasi kapatildi.
515. Sorun, `Bağlamı Göster` eventi XAML yuklenirken navigasyon kontrolleri daha hazir olmadan tetiklenmesiydi.
516. `UpdateInvoiceReviewNavigationControls()` icine erken cikis korumasi eklendi.
517. Smoke test: dotnet build -c Release OK.
518. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
519. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --health-check OK.

520. v1.20 olarak inceleme baglam panelindeki uygulanabilir aksiyonlara renkli durum vurgulari eklendi.
521. Hizli filtre yesil, baglam akisi mavi, donem sari, detay araclari mor tonla one cikacak sekilde stiller ayrildi.
522. Uygulanamayan aksiyonlar daha soluk gosterilerek gorsel ayrim artirildi.
523. Smoke test: dotnet build -c Release OK.
524. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

525. v1.21 olarak baglam paneline hazir aksiyon ozeti eklendi.
526. Ozet satiri hazir aksiyon sayisini ve aktif aksiyon adlarini metin olarak gosteriyor.
527. Renkli durum vurgulari artik metinsel bir ozetle de destekleniyor.
528. Smoke test: dotnet build -c Release OK.
529. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

530. v1.22 olarak hazir aksiyon ozeti mini rozetlere donusturuldu.
531. Hazir aksiyonlar artik baglam tipiyle uyumlu renkli kucuk etiketler halinde gosteriliyor.
532. Bu tur panelin metin yogunlugunu azaltip taranabilirligini artirdi.
533. Smoke test: dotnet build -c Release OK.
534. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

535. v1.23 olarak hazir aksiyon rozetleri tiklanabilir hale getirildi.
536. Rozetler artik ilgili baglam aksiyonunu dogrudan tetikliyor.
537. Ozet katmani yalnizca bilgi veren degil, ayni zamanda etkileşimli bir hizli yol haline geldi.
538. Smoke test: dotnet build -c Release OK.
539. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

540. v1.24 olarak tiklanan hazir aksiyon rozetine secim geri bildirimi eklendi.
541. Son calistirilan rozet daha belirgin kenarlik ve tonla secili gibi gosteriliyor.
542. Bu tur hizli ardışık kullanimi daha okunur hale getirdi.
543. Smoke test: dotnet build -c Release OK.
544. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

545. v1.25 olarak secili rozet vurgusunun baglam degistiginde otomatik temizlenmesi saglandi.
546. Rozet secimi baglam metni ve tercihli kayit kimliginden turetilen imzaya baglandi.
547. Boylece eski baglam vurgusu yeni baglama tasinmiyor.
548. Smoke test: dotnet build -c Release OK.
549. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

550. v1.26 olarak baglam paneline `Bağlamı Temizle` aksiyonu eklendi.
551. Bu dugme review modu, baglam metni ve rozet secim vurgularini temizliyor.
552. Filtreler varsayilan akisina dondurulerek normal moda tek tikla gecis saglandi.
553. Smoke test: dotnet build -c Release OK.
554. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

555. v1.27 olarak `Bağlamı Temizle` aksiyonuna `Ctrl+Shift+X` kisayolu eklendi.
556. Inceleme ipucu satirindaki kisayol listesi guncellendi.
557. Self-test beklentisi yeni kisayolu kapsayacak sekilde duzeltildi.
558. Smoke test: dotnet build -c Release OK.
559. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

560. v1.28 olarak inceleme baglami aksiyonlarinin durum mesajlari kisaltilip ortak formata toplandi.
561. `Filtre`, `Daraltma`, `Inceleme`, `Donem`, `Tur`, `Fatura no` ve `Temizle` akislarinda `Bağlam: ...` bicimi kullanilmaya baslandi.
562. Boylece alt durum cubugundaki operator geri bildirimi daha hizli okunur hale geldi.
563. Smoke test: dotnet build -c Release OK.
564. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

565. v1.29 olarak baglam aksiyonuyla odaklanan secili satira tablo icinde kucuk bir `ODAK` rozeti eklendi.
566. Rozet uzerinde aksiyonun kisa tipi gosteriliyor; tooltip ise tam baglam odak bilgisini tasiyor.
567. Kullanici farkli bir satira gectiginde veya baglam temizlendiginde odak izi otomatik temizleniyor.
568. Smoke test: dotnet build -c Release OK.
569. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

570. v1.30 olarak secili fatura form basligina baglam odagini anlatan kisa yardimci satir eklendi.
571. Bu satir sadece baglam aksiyonuyla odaklanan secili kayitta gorunuyor ve hangi akisla gelindigini sakin bir dille anlatiyor.
572. Normal secim veya baglam temizleme sonrasi yardimci satir otomatik gizleniyor.
573. Smoke test: dotnet build -c Release OK.
574. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

575. v1.31 olarak ustteki filtre ozet satirina baglam aksiyonu sonrasi kisa sureli vurgu eklendi.
576. Ozet satiri gecici olarak `Bağlam: ...` etiketiyle belirginlesiyor ve birkac saniye sonra normale donuyor.
577. Boylece baglam aksiyonunun listeye etkisi ikinci bir yerde daha hizli fark ediliyor.
578. Smoke test: dotnet build -c Release OK.
579. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

580. v1.32 olarak review baglam paneline son calistirilan aksiyonu gosteren mikro satir eklendi.
581. `Son aksiyon: ...` bilgisi rozet, ana dugme ve kisayol akislarinda ayni kaynakla guncelleniyor.
582. Boylece paneldeki son hareket daha sakin ama daha acik bir sekilde okunabiliyor.
583. Smoke test: dotnet build -c Release OK.
584. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

585. v1.33 olarak son kullanilan review baglam ana aksiyon dugmesine hafif ama kalici bir vurgu eklendi.
586. Dugme kenarligi ve yazi agirligi, `Son aksiyon` metniyle ayni aksiyon anahtarindan besleniyor.
587. Boylece son hareket panelde hem metin hem dugme yuzeyi uzerinden ayni anda okunabiliyor.
588. Smoke test: dotnet build -c Release OK.
589. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

590. v1.34 olarak pasif review baglam aksiyon dugmelerine neden kullanilamadigini anlatan tooltip metinleri eklendi.
591. Aktif durumda kisa gorev aciklamasi, pasif durumda ise `Kullanılamıyor: ...` nedeni gosteriliyor.
592. Boylece kullanici baglamda eksik olan parcayi daha hizli anlayabiliyor.
593. Smoke test: dotnet build -c Release OK.
594. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

595. v1.35 olarak review baglam metni icin ozet ve detay gorunumu arasinda gecis eklendi.
596. `Detay Metin` secimi acikken tam baglam metni, kapaliyken daha kisa ozet metin gosteriliyor.
597. Bu tercih review baglam ayarlariyla birlikte kalici olarak saklaniyor.
598. Smoke test: dotnet build -c Release OK.
599. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

600. v1.36 olarak review baglam cipleri tiklanabilir hizli kopyalama aracina donusturuldu.
601. Cipe tiklandiginda ilgili baglam parcasi panoya aliniyor ve kisa durum mesaji gosteriliyor.
602. Boylece baglam bilgisinin parcali yeniden kullanimi daha rahat hale geldi.
603. Smoke test: dotnet build -c Release OK.
604. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.

605. v1.37 olarak uygun review baglam cipleri dogrudan mikro aksiyon tetikler hale getirildi.
606. Rapor/donem/tur/fatura no cipleri uygun oldugunda ilgili filtre akislarini calistiriyor; diger ciplere tiklamak ise kopyalama davranisini koruyor.
607. Boylece baglam paneli daha aktif bir hizli yol yuzeyi haline geldi.
608. Smoke test: dotnet build -c Release OK.
609. Smoke test: dotnet run -c Release --no-build --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test OK.
