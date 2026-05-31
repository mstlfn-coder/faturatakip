# Devam Notları

Bu dosya, yeni Codex chatlerinde kaldığımız yeri hızlıca anlamak için tutulur.

## Güncel Kaldığımız Yer

- Tarih: 2026-05-31
- Aktif branch: `codex/v0.5-fatura-pdf-evraki`
- Son tamamlanan faz: `v0.4 - Fatura Kayıt Altyapısı`
- Aktif faz: `v0.5 - Fatura PDF Evrakı Ekleme`
- İlk dokümantasyon commit'i: `e0de4f9 docs: initialize project planning and continuity notes`
- `v0.1` commit'i: `3b3e20a feat: initialize wpf sqlite project skeleton`
- `v0.2` commit'i: `c8ad90c feat: add invoice type management`
- `v0.3` commit'i: `061e1a7 feat: add subscription management`
- `v0.4` commit'i: `252bbbd feat: add invoice entry foundation`
- PDF rapor örneği notu commit'i: `c0643ee docs: note pdf report sample requirement`

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

## Mevcut Codex Görevi

Bu branch'teki görev yalnızca `v0.5` kapsamını uygulamak olmalı.

Başlangıç talimatı:

```text
C# WPF + SQLite tabanlı kurum fatura takip programında v0.5 fazını uygula.

Önce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarını oku.

Bu fazda yalnızca fatura PDF evrakı ekleme yapılacak.

Faturaya PDF ekleme, PDF'i uygulama klasörüne kopyalama, PDF yolunu/orijinal adını/hash bilgisini veritabanına kaydetme, PDF açma ve dosya varlık kontrolü desteklenecek.

Ödeme, ödeme evrakı, rapor, dışa aktarım ve yedekleme yapılmayacak.

Faz sonunda uygulamayı çalıştır veya en azından derleme/test doğrulamasını yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarını güncelle.
```

## Dikkat Edilecekler

- Yol sorunlarına karşı PowerShell'de gerekirse `-LiteralPath` kullanılmalı.
- Uygulama kodu başlamadan önce .NET SDK durumu kontrol edilmeli.
- Yeni oluşturulacak klasör adları ASCII olmalı.
- Faz kapsamı aşılmamalı.
- Önce dokümantasyon okunmalı, sonra uygulama koduna geçilmeli.
- Fatura PDF raporları fazına gelindiğinde kullanıcıdan Excel örneği istenmeden tasarım/uygulama yapılmamalı.
